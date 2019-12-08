import { Action, Reducer } from 'redux';
import { AppThunkAction } from '..';
import { Teacher } from '../Teachers';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface TeacherLeaderboardState {
    allTimeEntries: TeacherLeaderboardEntry[];
    thisYearEntries: TeacherLeaderboardEntry[];
    selectedEntry: TeacherLeaderboardEntry | undefined;
    isLoading: boolean;
}

export interface TeacherLeaderboardEntry {
    teacherId: string,
    teacher: Teacher,
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

interface RequestAllTimeTeacherLeaderboardAction {
    type: 'REQUEST_ALLTIME_TEACHER_LEADERBOARD';
}

interface ReceiveAllTimeTeacherLeaderboardAction {
    type: 'RECEIVE_ALLTIME_TEACHER_LEADERBOARD';
    allTimeEntries: TeacherLeaderboardEntry[];
}

interface RequestThisYearTeacherLeaderboardAction {
    type: 'REQUEST_THIS_YEAR_TEACHER_LEADERBOARD'
}

interface ReceiveThisYearTeacherLeaderboardAciton {
    type: 'RECEIVE_THIS_YEAR_TEACHER_LEADERBOARD';
    thisYearEntries: TeacherLeaderboardEntry[];
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
type KnownAction = RequestAllTimeTeacherLeaderboardAction | ReceiveAllTimeTeacherLeaderboardAction | 
                   RequestThisYearTeacherLeaderboardAction | ReceiveThisYearTeacherLeaderboardAciton | 
                   RequestTeacherEntryAction | ReceiveTeacherEntryAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestAllTimeTeacherLeaderboard: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.teacherLeaderboard &&
            appState.teacherLeaderboard.isLoading === false &&
            appState.teacherLeaderboard.allTimeEntries.length === 0) {
            fetch(`api/leaderboard/teachers/global`)
                .then(response => response.json() as Promise<TeacherLeaderboardEntry[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_ALLTIME_TEACHER_LEADERBOARD', allTimeEntries: data });
                });
            dispatch({ type: 'REQUEST_ALLTIME_TEACHER_LEADERBOARD' });
        }
    },
    requestThisYearTeacherLeaderboard: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.teacherLeaderboard &&
            appState.teacherLeaderboard.isLoading === false &&
            appState.teacherLeaderboard.thisYearEntries.length === 0) {
            fetch(`api/leaderboard/teachers/year`)
                .then(response => response.json() as Promise<TeacherLeaderboardEntry[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_THIS_YEAR_TEACHER_LEADERBOARD', thisYearEntries: data });
                });
            dispatch({ type: 'REQUEST_THIS_YEAR_TEACHER_LEADERBOARD' });
        }
    },
    requestTeacherEntry: (teacherId: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.teacherLeaderboard &&
            appState.teacherLeaderboard.isLoading === false &&
            (appState.teacherLeaderboard.selectedEntry === undefined ||
            appState.teacherLeaderboard.selectedEntry.teacherId !== teacherId)) {
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

const unloadedState: TeacherLeaderboardState = { allTimeEntries: [], thisYearEntries: [], selectedEntry: undefined, isLoading: false };

export const reducer: Reducer<TeacherLeaderboardState> = (state: TeacherLeaderboardState | undefined, incomingAction: Action): TeacherLeaderboardState => {
    if (state === undefined)
        return unloadedState;

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_ALLTIME_TEACHER_LEADERBOARD':
            return {
                allTimeEntries: state.allTimeEntries,
                thisYearEntries: state.thisYearEntries,
                selectedEntry: state.selectedEntry,
                isLoading: true,
            };
        case 'RECEIVE_ALLTIME_TEACHER_LEADERBOARD':
            return {
                allTimeEntries: [...state.allTimeEntries, ...action.allTimeEntries],
                thisYearEntries: state.thisYearEntries,
                selectedEntry: state.selectedEntry,
                isLoading: false,
            };
        case 'REQUEST_THIS_YEAR_TEACHER_LEADERBOARD':
            return {
                allTimeEntries: state.allTimeEntries,
                thisYearEntries: state.thisYearEntries,
                selectedEntry: state.selectedEntry,
                isLoading: true,
            };
        case 'RECEIVE_THIS_YEAR_TEACHER_LEADERBOARD':
            return {
                allTimeEntries: state.allTimeEntries,
                thisYearEntries: [...state.thisYearEntries, ...action.thisYearEntries],
                selectedEntry: state.selectedEntry,
                isLoading: false,
            };
        case 'REQUEST_TEACHER_ENTRY':
            return {
                allTimeEntries: state.allTimeEntries,
                thisYearEntries: state.thisYearEntries,
                selectedEntry: state.selectedEntry,
                isLoading: true,
            };
        case 'RECEIVE_TEACHER_ENTRY':
            return {
                allTimeEntries: state.allTimeEntries,
                thisYearEntries: state.thisYearEntries,
                selectedEntry: action.selectedEntry,
                isLoading: false
            };
    }
    return state;
};
