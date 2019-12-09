import { Action, Reducer } from 'redux';
import { AppThunkAction } from '..';
import { Badge } from "../Badges";

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface TeacherBadgesState {
    isLoading: boolean;
    badges: Badge[];
    teacherId: string | undefined;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestTeacherBadgesAction {
    type: 'REQUEST_TEACHER_BADGES';
    teacherId: string;
}

interface ReceiveTeacherBadgesAction {
    type: 'RECEIVE_TEACHER_BADGES';
    badges: Badge[];
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestTeacherBadgesAction | ReceiveTeacherBadgesAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestTeacherBadges: (teacherId: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.teacherBadges &&
            appState.teacherBadges.isLoading === false &&
            appState.teacherBadges.teacherId !== teacherId) {
            fetch(`api/badges/teacher=${teacherId}`)
                .then(response => response.json() as Promise<Badge[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_TEACHER_BADGES', badges: data });
                });

            dispatch({ type: 'REQUEST_TEACHER_BADGES', teacherId });
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: TeacherBadgesState = { badges: [], isLoading: false, teacherId: undefined };

export const reducer: Reducer<TeacherBadgesState> = (state: TeacherBadgesState | undefined, incomingAction: Action): TeacherBadgesState => {
    if (state === undefined)
        return unloadedState;

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_TEACHER_BADGES':
            return {
                badges: state.badges,
                isLoading: true,
                teacherId: action.teacherId
            };
        case 'RECEIVE_TEACHER_BADGES':
            return {
                badges: action.badges,
                isLoading: false,
                teacherId: state.teacherId
            };
    }

    return state;
};
