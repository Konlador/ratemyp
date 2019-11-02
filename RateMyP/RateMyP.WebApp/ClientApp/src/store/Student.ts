import { Action, Reducer } from 'redux';
import { AppThunkAction } from '.';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface StudentState {
    isLoggedIn: boolean;
    student: Student | undefined;
}

export interface Student {
    id: string;
    firstName: string;
    lastName: string;
    studies: string | undefined;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestStudentAction {
    type: 'REQUEST_STUDENT';
    studentId: string;
}

interface ReceiveStudentAction {
    type: 'RECEIVE_STUDENT';
    student: Student;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestStudentAction | ReceiveStudentAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestStudent: (studentId: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.student &&
            appState.student.student &&
            appState.student.student.id !== studentId) {
            fetch(`api/students/${studentId}`)
                .then(response => response.json() as Promise<Student>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_STUDENT', student: data });
                });

            dispatch({ type: 'REQUEST_STUDENT', studentId });
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: StudentState = { student: undefined, isLoggedIn: false };

export const reducer: Reducer<StudentState> = (state: StudentState | undefined, incomingAction: Action): StudentState => {
    if (state === undefined)
        return unloadedState;

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_STUDENT':
            return {
                student: state.student,
                isLoggedIn: state.isLoggedIn
            };
        case 'RECEIVE_STUDENT':
            return {
                student: action.student,
                isLoggedIn: true
            };
    }

    return state;
};
