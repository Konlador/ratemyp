import { Action, Reducer } from 'redux';
import { AppThunkAction } from '.';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface TeacherLeaderboardState {
    entries: TeacherLeaderboardEntry[];
    selectedEntry: TeacherLeaderboardEntry | undefined;
    isLoading: boolean;
}

export interface TeacherLeaderboardEntry {
    id: string,
    allTimePosition: number,
    allTimeRatingCount: number,
    allTimeAverage: number,
    thisYearPosition: number,
    thisYearRatingCount: number,
    thisYearAverage: number,
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestTeacherLeaderboardAction {
    type: 'REQUEST_TEACHER_LEADERBOARD';
}

interface ReceiveTeacherLeaderboardAction {
    type: 'RECEIVE_TEACHER_LEADERBOARD';
    entries: TeacherLeaderboardEntry[];
}

interface RequestTeacherEntryAction {
    type: 'REQUEST_TEACHER_ENTRY';
}

interface ReceiveTeacherEntryAction {
    type: 'RECEIVE_TEACHER_ENTRY';
    selectedEntry: TeacherLeaderboardEntry;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestTeacherLeaderboardAction | ReceiveTeacherLeaderboardAction | RequestTeacherEntryAction | ReceiveTeacherEntryAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestTeacherLeaderboard: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState && appState.teacherLeaderboardEntries && appState.teacherLeaderboardEntries.isLoading === false) {
            fetch(`api/leaderboard/teachers`)
                .then(response => response.json() as Promise<TeacherLeaderboardEntry[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_TEACHER_LEADERBOARD', entries: data });
                });
            dispatch({ type: 'REQUEST_TEACHER_LEADERBOARD' });
        }
    },
    requestTeacherEntry: (teacherId: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState && appState.teacherLeaderboardEntries &&
            appState.teacherLeaderboardEntries.isLoading === false &&
            (appState.teacherLeaderboardEntries.selectedEntry === undefined ||
            appState.teacherLeaderboardEntries.selectedEntry.id !== teacherId)) {
            fetch(`api/leaderboard/teacher=${teacherId}`)
                .then(response => response.json() as Promise<TeacherLeaderboardEntry>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_TEACHER_ENTRY', selectedEntry: data });
                });
            dispatch({ type: 'REQUEST_TEACHER_ENTRY' });
        }
    },

};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: TeacherLeaderboardState= { entries: [], selectedEntry: undefined, isLoading: false };

export const reducer: Reducer<TeacherLeaderboardState> = (state: TeacherLeaderboardState | undefined, incomingAction: Action): TeacherLeaderboardState => {
    if (state === undefined)
        return unloadedState;

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_TEACHER_LEADERBOARD':
            return {
                entries: state.entries,
                selectedEntry: state.selectedEntry,
                isLoading: true,
            };
        case 'RECEIVE_TEACHER_LEADERBOARD':
            return {
                entries: [...state.entries, ...action.entries],
                selectedEntry: state.selectedEntry,
                isLoading: false,
            };
        case 'REQUEST_TEACHER_ENTRY':
            return {
                entries: state.entries,
                selectedEntry: state.selectedEntry,
                isLoading: true,
            };
        case 'RECEIVE_TEACHER_ENTRY':
            return {
                entries: state.entries,
                selectedEntry: action.selectedEntry,
                isLoading: false
            };
    }

    return state;
};
