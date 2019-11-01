import { Action, Reducer } from 'redux';
import { AppThunkAction } from '..';
import { Teacher } from "../Teachers";

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface CourseTeachersState {
    isLoading: boolean;
    teachers: Teacher[];
    courseId: string | undefined;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestCourseTeachersAction {
    type: 'REQUEST_COURSE_TEACHERS';
    courseId: string;
}

interface ReceiveCourseTeachersAction {
    type: 'RECEIVE_COURSE_TEACHERS';
    teachers: Teacher[];
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestCourseTeachersAction | ReceiveCourseTeachersAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestCourseTeachers: (courseId: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.courseTeachers &&
            appState.courseTeachers.isLoading === false &&
            appState.courseTeachers.courseId !== courseId) {
            fetch(`api/teachers/course=${courseId}`)
                .then(response => response.json() as Promise<Teacher[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_COURSE_TEACHERS', teachers: data });
                });

            dispatch({ type: 'REQUEST_COURSE_TEACHERS', courseId });
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: CourseTeachersState = { teachers: [], isLoading: false, courseId: undefined };

export const reducer: Reducer<CourseTeachersState> = (state: CourseTeachersState | undefined, incomingAction: Action): CourseTeachersState => {
    if (state === undefined)
        return unloadedState;

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_COURSE_TEACHERS':
            return {
                teachers: state.teachers,
                isLoading: true,
                courseId: action.courseId
            };
        case 'RECEIVE_COURSE_TEACHERS':
            return {
                teachers: action.teachers,
                isLoading: false,
                courseId: state.courseId
            };
    }

    return state;
};
