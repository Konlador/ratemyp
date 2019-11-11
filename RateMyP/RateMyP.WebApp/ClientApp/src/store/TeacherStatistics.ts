import { Action, Reducer } from 'redux';
import { AppThunkAction } from '.';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface TeacherStatisticsState {
    teacherId: string | undefined;
    teacherStatistics: TeacherStatistics;
    isLoading: boolean;
}

export interface TeacherStatistics {
    id: string,
    teacherId: string,
    averageMark: number,
    averageMarks: DateMark[],
    averageLevelOfDifficulty: number,
    wouldTakeAgainRatio: number;
}

export interface DateMark {
    date: Date,
    mark: number
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestTeacherStatisticsAction {
    type: 'REQUEST_TEACHERSTATISTICS';
    teacherId: string;
}

interface ReceiveTeacherStatisticsAction {
    type: 'RECEIVE_TEACHERSTATISTICS';
    teacherStatistics: TeacherStatistics;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestTeacherStatisticsAction | ReceiveTeacherStatisticsAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).
export const actionCreators = {
    requestTeacherStatistics: (teacherId: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState &&
            appState.teacherStatistics &&
            appState.teacherStatistics.isLoading === false &&
            appState.teacherStatistics.teacherId !== teacherId) {
            fetch(`api/statistics/teacher=${teacherId}/parts=10`)
                .then(response => response.json() as Promise<TeacherStatistics>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_TEACHERSTATISTICS', teacherStatistics: data });
                });

            dispatch({ type: 'REQUEST_TEACHERSTATISTICS', teacherId });
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.
const undefinedTeacherStatistic: TeacherStatistics = {
    id: "undefined",
    teacherId: "undefined",
    averageMark: 0,
    averageMarks: [],
    averageLevelOfDifficulty: 0,
    wouldTakeAgainRatio: 0
};
const unloadedState: TeacherStatisticsState = { teacherId: undefined, teacherStatistics: undefinedTeacherStatistic , isLoading: false };

export const reducer: Reducer<TeacherStatisticsState> = (state: TeacherStatisticsState | undefined, incomingAction: Action): TeacherStatisticsState => {
    if (state === undefined)
        return unloadedState;

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_TEACHERSTATISTICS':
            return {
                teacherId: action.teacherId,
                teacherStatistics: state.teacherStatistics,
                isLoading: true
            };
        case 'RECEIVE_TEACHERSTATISTICS':
            return {
                teacherId: state.teacherId,
                teacherStatistics: action.teacherStatistics,
                isLoading: false
            };
    }

    return state;
};
