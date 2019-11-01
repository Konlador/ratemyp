import { Action, Reducer } from 'redux';
import { AppThunkAction } from '..';
import { Course } from "../Courses";

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface TeacherCoursesState {
    isLoading: boolean;
    courses: Course[];
    teacherId: string | undefined;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestTeacherCoursesAction {
    type: 'REQUEST_TEACHER_COURSES';
    teacherId: string;
}

interface ReceiveTeacherCoursesAction {
    type: 'RECEIVE_TEACHER_COURSES';
    courses: Course[];
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestTeacherCoursesAction | ReceiveTeacherCoursesAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestTeacherCourses: (teacherId: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.teacherCourses &&
            appState.teacherCourses.isLoading === false &&
            appState.teacherCourses.teacherId !== teacherId) {
            fetch(`api/courses/teacher=${teacherId}`)
                .then(response => response.json() as Promise<Course[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_TEACHER_COURSES', courses: data });
                });

            dispatch({ type: 'REQUEST_TEACHER_COURSES', teacherId });
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: TeacherCoursesState = { courses: [], isLoading: false, teacherId: undefined };

export const reducer: Reducer<TeacherCoursesState> = (state: TeacherCoursesState | undefined, incomingAction: Action): TeacherCoursesState => {
    if (state === undefined)
        return unloadedState;

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_TEACHER_COURSES':
            return {
                courses: state.courses,
                isLoading: true,
                teacherId: action.teacherId
            };
        case 'RECEIVE_TEACHER_COURSES':
            return {
                courses: action.courses,
                isLoading: false,
                teacherId: state.teacherId
            };
    }

    return state;
};
