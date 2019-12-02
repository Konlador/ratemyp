import { Action, Reducer } from 'redux';
import { AppThunkAction } from '.';
import firebase from "firebase";

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface StudentState {
    isLoggedIn: boolean;
    student: Student | undefined;
    user: firebase.User | undefined;
}

export interface Student {
    id: string;
    studies: string | undefined;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestStudentAction {
    type: 'REQUEST_STUDENT';
    studentId: string;
    user: firebase.User;
}

interface ReceiveStudentAction {
    type: 'RECEIVE_STUDENT';
    student: Student;
}

interface ClearStudentAction {
    type: 'CLEAR_STUDENT';
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestStudentAction | ReceiveStudentAction | ClearStudentAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    login: (user: firebase.User): AppThunkAction<KnownAction> => (dispatch, getState) => {
        console.log(user)
        const studentId = user.uid;
        fetch(`api/students/${studentId}`)
            .then(response => response.json() as Promise<Student>)
            .then(data => {
                dispatch({ type: 'RECEIVE_STUDENT', student: data });
            });

        dispatch({ type: 'REQUEST_STUDENT', studentId, user });
    },
    logout: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'CLEAR_STUDENT' });
    },
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: StudentState = { student: undefined, isLoggedIn: false, user: undefined };

export const reducer: Reducer<StudentState> = (state: StudentState | undefined, incomingAction: Action): StudentState => {
    if (state === undefined)
        return unloadedState;

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_STUDENT':
            return {
                student: state.student,
                isLoggedIn: false,
                user: action.user
            };
        case 'RECEIVE_STUDENT':
            return {
                student: action.student,
                isLoggedIn: true,
                user: state.user
            };
        case 'CLEAR_STUDENT':
            return unloadedState;
    }

    return state;
};
