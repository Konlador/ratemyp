import { Action, Reducer } from 'redux';
import { AppThunkAction } from '.';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface CoursesState {
    isLoading: boolean;
    courses: Course[];
    selectedCourse: Course | undefined;
    canLoadMore: boolean;
    currentIndex: number;
}

export interface Course {
    id: string,
    name: string;
    courseType: number;
    credits: number;
    faculty: string;
}

export enum CourseType {
    None = 0,
    Optional = 1,
    Compulsory = 2,
    Complimentary = 3,
    BUS = 4
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestCoursesAction {
    type: 'REQUEST_COURSES';
}

interface ReceiveCoursesAction {
    type: 'RECEIVE_COURSES';
    courses: Course[];
}

interface RequestCourseAction {
    type: 'REQUEST_COURSE';
    courseId: string;
}

interface ReceiveCourseAction {
    type: 'RECEIVE_COURSE';
    course: Course;
}

interface RequestAllCoursesAction {
    type: 'REQUEST_ALL_COURSES';
}

interface ReceiveAllCoursesAction {
    type: 'RECEIVE_ALL_COURSES';
    courses: Course[]
}

interface RequestSearchedCoursesAction {
    type: 'REQUEST_SEARCHED_COURSES'
}

interface ReceiveSearchedCoursesAction {
    type: 'RECEIVE_SEARCHED_COURSES';
    courses: Course[];
}

interface ClearSelectedCourse {
    type: 'CLEAR_SELECTED_COURSE'
}

interface ClearAllCourses {
    type: 'CLEAR_ALL_COURSES'
}

interface CheckCourseAvailabilityAction {
    type: 'CHECK_COURSE_AVAILABILITY'
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestCoursesAction | ReceiveCoursesAction | RequestCourseAction | ReceiveCourseAction | ClearSelectedCourse | ClearAllCourses |
                   CheckCourseAvailabilityAction | RequestAllCoursesAction | ReceiveAllCoursesAction | RequestSearchedCoursesAction | ReceiveSearchedCoursesAction ;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestCourses: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.courses &&
            appState.courses.isLoading === false) {
            fetch(`api/courses/startIndex=${appState.courses.currentIndex}`)
                .then(response => response.json() as Promise<Course[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_COURSES', courses: data });
                    if (data.length === 0) dispatch({ type: 'CHECK_COURSE_AVAILABILITY' });
                });

            dispatch({ type: 'REQUEST_COURSES' });
        }
    },
    requestCourse: (courseId: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.courses &&
            appState.courses.isLoading === false &&
            (appState.courses.selectedCourse === undefined ||
                appState.courses.selectedCourse.id !== courseId)) {
            fetch(`api/courses/${courseId}`)
                .then(response => response.json() as Promise<Course>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_COURSE', course: data });
                });

            dispatch({ type: 'REQUEST_COURSE', courseId });
        }
    },
    requestAllCourses: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.courses &&
            appState.courses.isLoading === false &&
            appState.courses.courses.length === 0) {
            fetch(`api/courses`)
                .then(response => response.json() as Promise<Course[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_ALL_COURSES', courses: data});
                });
            dispatch({ type: 'REQUEST_ALL_COURSES' });
        }
    },
    searchCourse: (searchString: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState && appState.courses && appState.courses.isLoading === false) {
            fetch(`api/courses/search=${searchString}`)
                .then(response => response.json() as Promise<Course[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_SEARCHED_COURSES', courses: data});
                });
            dispatch({ type: 'REQUEST_SEARCHED_COURSES'});
        }
    },
    clearSelectedCourse: () => ({ type: 'CLEAR_SELECTED_COURSE' } as ClearSelectedCourse),
    clearAllCourses: () => ({ type: 'CLEAR_ALL_COURSES' } as ClearAllCourses)
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: CoursesState = { courses: [], selectedCourse: undefined, isLoading: false, currentIndex: 0, canLoadMore: true };

export const reducer: Reducer<CoursesState> = (state: CoursesState | undefined, incomingAction: Action): CoursesState => {
    if (state === undefined)
        return unloadedState;

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_COURSES':
            return {
                courses: state.courses,
                selectedCourse: state.selectedCourse,
                isLoading: true,
                canLoadMore: state.canLoadMore,
                currentIndex: state.currentIndex
            };
        case 'RECEIVE_COURSES':
            return {
                courses: [...state.courses, ...action.courses],
                selectedCourse: state.selectedCourse,
                isLoading: false,
                canLoadMore: state.canLoadMore,
                currentIndex: state.currentIndex + action.courses.length
            };
        case 'REQUEST_COURSE':
            return {
                courses: state.courses,
                selectedCourse: state.selectedCourse,
                isLoading: true,
                canLoadMore: state.canLoadMore,
                currentIndex: state.currentIndex
            };
        case 'RECEIVE_COURSE':
            return {
                courses: state.courses,
                selectedCourse: action.course,
                isLoading: false,
                canLoadMore: state.canLoadMore,
                currentIndex: state.currentIndex
            };
        case 'CLEAR_SELECTED_COURSE':
            return {
                courses: state.courses,
                selectedCourse: undefined,
                isLoading: state.isLoading,
                canLoadMore: state.canLoadMore,
                currentIndex: state.currentIndex
            };
        case 'CHECK_COURSE_AVAILABILITY':
            return {
                courses: state.courses,
                selectedCourse: state.selectedCourse,
                isLoading: false,
                currentIndex: state.currentIndex,
                canLoadMore: false
            }
        case 'REQUEST_ALL_COURSES':
            return {
                courses: state.courses,
                selectedCourse: state.selectedCourse,
                isLoading: true,
                currentIndex: state.currentIndex,
                canLoadMore: state.canLoadMore
            };
        case 'RECEIVE_ALL_COURSES':
            return {
                courses: [...state.courses, ...action.courses],
                selectedCourse: state.selectedCourse,
                isLoading: false,
                currentIndex: state.currentIndex + action.courses.length,
                canLoadMore: state.canLoadMore
            };    
        case 'REQUEST_SEARCHED_COURSES':
            return {
                courses: state.courses,
                selectedCourse: state.selectedCourse,
                isLoading: true,
                currentIndex: state.currentIndex,
                canLoadMore: state.canLoadMore
            };
        case 'RECEIVE_SEARCHED_COURSES':
            return {
                courses: [...state.courses, ...action.courses],
                selectedCourse: state.selectedCourse,
                isLoading: false,
                currentIndex: state.currentIndex,
                canLoadMore: state.canLoadMore
            };
        case 'CLEAR_ALL_COURSES':
            return {
                courses: [],
                selectedCourse: state.selectedCourse,
                isLoading: state.isLoading,
                currentIndex: state.currentIndex,
                canLoadMore: state.canLoadMore
            };
    }

    return state;
};
