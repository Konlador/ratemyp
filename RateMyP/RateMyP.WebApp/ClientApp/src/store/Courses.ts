import { Action, Reducer } from 'redux';
import { AppThunkAction } from '.';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface CoursesState {
    isLoading: boolean;
    courses: Course[];
    selectedCourse: Course | undefined;
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

interface ClearSelectedCourse {
    type: 'CLEAR_SELECTED_COURSE'
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestCoursesAction | ReceiveCoursesAction | RequestCourseAction | ReceiveCourseAction | ClearSelectedCourse;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestCourses: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.courses &&
            appState.courses.isLoading === false &&
            appState.courses.courses.length === 0) {
            fetch(`api/courses`)
                .then(response => response.json() as Promise<Course[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_COURSES', courses: data });
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
    clearSelectedCourse: () => ({ type: 'CLEAR_SELECTED_COURSE' } as ClearSelectedCourse)
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: CoursesState = { courses: [], selectedCourse: undefined, isLoading: false };

export const reducer: Reducer<CoursesState> = (state: CoursesState | undefined, incomingAction: Action): CoursesState => {
    if (state === undefined)
        return unloadedState;

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_COURSES':
            return {
                courses: state.courses,
                selectedCourse: state.selectedCourse,
                isLoading: true
            };
        case 'RECEIVE_COURSES':
            return {
                courses: action.courses,
                selectedCourse: state.selectedCourse,
                isLoading: false
            };
        case 'REQUEST_COURSE':
            return {
                courses: state.courses,
                selectedCourse: state.selectedCourse,
                isLoading: true
            };
        case 'RECEIVE_COURSE':
            return {
                courses: state.courses,
                selectedCourse: action.course,
                isLoading: false
            };
        case 'CLEAR_SELECTED_COURSE':
            return {
                courses: state.courses,
                selectedCourse: undefined,
                isLoading: state.isLoading
            };
    }

    return state;
};
