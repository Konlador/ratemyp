import { Action, Reducer } from 'redux';
import { AppThunkAction } from '.';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface TeachersState {
    isLoading: boolean;
    teachers: Teacher[];
}

export interface Teacher {
    id: string,
    firstName: string;
    lastName: string;
    rank: string;
    faculty: string;
    description: string;
    activities: TeacherActivity[];
}

export interface TeacherActivity {
    id: string,
    teacherId: string;
    courseId: string;
    dateStarted: string;
    lectureType: string;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestTeachersAction {
    type: 'REQUEST_TEACHERS';
}

interface ReceiveTeachersAction {
    type: 'RECEIVE_TEACHERS';
    teachers: Teacher[];
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestTeachersAction | ReceiveTeachersAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestTeachers: (): AppThunkAction<KnownAction> => (dispatch) => {
        fetch(`api/teachers`)
            .then(response => response.json() as Promise<Teacher[]>)
            .then(data => {
                dispatch({ type: 'RECEIVE_TEACHERS', teachers: data });
            });

        dispatch({ type: 'REQUEST_TEACHERS' });
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: TeachersState = { teachers: [], isLoading: false };

export const reducer: Reducer<TeachersState> = (state: TeachersState | undefined, incomingAction: Action): TeachersState => {
    if (state === undefined)
        return unloadedState;

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_TEACHERS':
            return {
                teachers: state.teachers,
                isLoading: true
            };
        case 'RECEIVE_TEACHERS':
            return {
                teachers: action.teachers,
                isLoading: false
            };
    }

    return state;
};
