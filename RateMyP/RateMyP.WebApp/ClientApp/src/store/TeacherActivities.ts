import { Action, Reducer } from 'redux';
import { AppThunkAction } from '.';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface TeacherActivitesState {
    teacherId: string | undefined;
    teacherActivites: TeacherActivity[];
    isLoading: boolean;
}

export interface TeacherActivity {
    id: string,
    teacherId: string;
    courseId: number;
    dateStarted: Date;
    lectureType: number;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestTeacherActivitesAction {
    type: 'REQUEST_TEACHERACTIVITIES';
    teacherId: string;
}

interface ReceiveTeacherActivitesAction {
    type: 'RECEIVE_TEACHERACTIVITIES';
    teacherActivites: TeacherActivity[];
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestTeacherActivitesAction | ReceiveTeacherActivitesAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestTeacherActivites: (teacherId: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState &&
            appState.teacherActivites &&
            appState.teacherActivites.isLoading === false &&
            appState.teacherActivites.teacherActivites.length === 0) {
            fetch(`api/teacheractivites/${teacherId}`)
                .then(response => response.json() as Promise<TeacherActivity[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_TEACHERACTIVITIES', teacherActivites: data });
                });

            dispatch({ type: 'REQUEST_TEACHERACTIVITIES', teacherId });
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: TeacherActivitesState = { teacherId: undefined, teacherActivites: [], isLoading: false };

export const reducer: Reducer<TeacherActivitesState> = (state: TeacherActivitesState | undefined, incomingAction: Action): TeacherActivitesState => {
    if (state === undefined)
        return unloadedState;

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_TEACHERACTIVITIES':
            return {
                teacherId: action.teacherId,
                teacherActivites: state.teacherActivites,
                isLoading: true
            };
        case 'RECEIVE_TEACHERACTIVITIES':
            return {
                teacherId: state.teacherId,
                teacherActivites: action.teacherActivites,
                isLoading: false
            };
    }

    return state;
};
