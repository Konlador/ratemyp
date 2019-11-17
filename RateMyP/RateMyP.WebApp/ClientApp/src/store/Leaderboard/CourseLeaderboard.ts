import { Action, Reducer } from 'redux';
import { AppThunkAction } from '..';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface CourseLeaderboardState {
    allTimeEntries: CourseLeaderboardEntry[];
    thisYearEntries: CourseLeaderboardEntry[];
    selectedEntry: CourseLeaderboardEntry | undefined;
    isLoading: boolean;
}

export interface CourseLeaderboardEntry {
    id: string,
    entryType: number,
    name: string,
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

interface RequestAllTimeCourseLeaderboardAction {
    type: 'REQUEST_ALLTIME_COURSE_LEADERBOARD';
}

interface ReceiveAllTimeCourseLeaderboardAction {
    type: 'RECEIVE_ALLTIME_COURSE_LEADERBOARD';
    allTimeEntries: CourseLeaderboardEntry[];
}

interface RequestThisYearCourseLeaderboardAction {
    type: 'REQUEST_THIS_YEAR_COURSE_LEADERBOARD'
}

interface ReceiveThisYearCourseLeaderboardAciton {
    type: 'RECEIVE_THIS_YEAR_COURSE_LEADERBOARD';
    thisYearEntries: CourseLeaderboardEntry[];
}

interface RequestCourseEntryAction {
    type: 'REQUEST_COURSE_ENTRY';
}

interface ReceiveCourseEntryAction {
    type: 'RECEIVE_COURSE_ENTRY';
    selectedEntry: CourseLeaderboardEntry;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestAllTimeCourseLeaderboardAction | ReceiveAllTimeCourseLeaderboardAction | 
                   RequestThisYearCourseLeaderboardAction | ReceiveThisYearCourseLeaderboardAciton | 
                   RequestCourseEntryAction | ReceiveCourseEntryAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestAllTimeCourseLeaderboard: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.courseLeaderboard &&
            appState.courseLeaderboard.isLoading === false) {
            fetch(`api/leaderboard/courses/all`)
                .then(response => response.json() as Promise<CourseLeaderboardEntry[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_ALLTIME_COURSE_LEADERBOARD', allTimeEntries: data });
                });
            dispatch({ type: 'REQUEST_ALLTIME_COURSE_LEADERBOARD' });
        }
    },
    requestThisYearCourseLeaderboard: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.courseLeaderboard &&
            appState.courseLeaderboard.isLoading === false) {
            fetch(`api/leaderboard/courses/year`)
                .then(response => response.json() as Promise<CourseLeaderboardEntry[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_THIS_YEAR_COURSE_LEADERBOARD', thisYearEntries: data });
                });
            dispatch({ type: 'REQUEST_THIS_YEAR_COURSE_LEADERBOARD' });
        }
    },
    requestCourseEntry: (courseId: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.courseLeaderboard &&
            appState.courseLeaderboard.isLoading === false &&
            (appState.courseLeaderboard.selectedEntry === undefined ||
            appState.courseLeaderboard.selectedEntry.id !== courseId)) {
            fetch(`api/leaderboard/course=${courseId}`)
                .then(response => response.json() as Promise<CourseLeaderboardEntry>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_COURSE_ENTRY', selectedEntry: data });
                });
            dispatch({ type: 'REQUEST_COURSE_ENTRY' });
        }
    },

};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: CourseLeaderboardState= { allTimeEntries: [], thisYearEntries: [], selectedEntry: undefined, isLoading: false };

export const reducer: Reducer<CourseLeaderboardState> = (state: CourseLeaderboardState | undefined, incomingAction: Action): CourseLeaderboardState => {
    if (state === undefined)
        return unloadedState;

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_ALLTIME_COURSE_LEADERBOARD':
            return {
                allTimeEntries: state.allTimeEntries,
                thisYearEntries: state.thisYearEntries,
                selectedEntry: state.selectedEntry,
                isLoading: true,
            };
        case 'RECEIVE_ALLTIME_COURSE_LEADERBOARD':
            return {
                allTimeEntries: [...state.allTimeEntries, ...action.allTimeEntries],
                thisYearEntries: state.thisYearEntries,
                selectedEntry: state.selectedEntry,
                isLoading: false,
            };
        case 'REQUEST_THIS_YEAR_COURSE_LEADERBOARD':
            return {
                allTimeEntries: state.allTimeEntries,
                thisYearEntries: state.thisYearEntries,
                selectedEntry: state.selectedEntry,
                isLoading: true,
            };
        case 'RECEIVE_THIS_YEAR_COURSE_LEADERBOARD':
            return {
                allTimeEntries: state.allTimeEntries,
                thisYearEntries: [...state.thisYearEntries, ...action.thisYearEntries],
                selectedEntry: state.selectedEntry,
                isLoading: false,
            };
        case 'REQUEST_COURSE_ENTRY':
            return {
                allTimeEntries: state.allTimeEntries,
                thisYearEntries: state.thisYearEntries,
                selectedEntry: state.selectedEntry,
                isLoading: true,
            };
        case 'RECEIVE_COURSE_ENTRY':
            return {
                allTimeEntries: state.allTimeEntries,
                thisYearEntries: state.thisYearEntries,
                selectedEntry: action.selectedEntry,
                isLoading: false
            };
    }

    return state;
};
