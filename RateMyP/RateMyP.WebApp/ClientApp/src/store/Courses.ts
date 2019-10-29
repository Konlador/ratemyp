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

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestCoursesAction | ReceiveCoursesAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestCourses: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.courses && appState.courses.isLoading === false && appState.courses.courses.length === 0) {
            fetch(`api/courses`)
                .then(response => response.json() as Promise<Course[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_COURSES', courses: data });
                });

            dispatch({ type: 'REQUEST_COURSES' });
        }
    },
    requestTeacherCourses: (teacherId: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.courses && appState.courses.isLoading === false && appState.courses.courses.length === 0) {
            fetch(`api/courses`)
                .then(response => response.json() as Promise<Course[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_COURSES', courses: data });
                });

            dispatch({ type: 'REQUEST_COURSES' });
        }
    }
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
    }

    return state;
};