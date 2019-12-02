import { Action, Reducer } from 'redux';
import { AppThunkAction } from '.';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface CourseStatisticsState {
    courseId: string | undefined;
    courseStatistics: CourseStatistics;
    isLoading: boolean;
}

export interface CourseStatistics {
    id: string,
    courseId: string,
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

interface RequestCourseStatisticsAction {
    type: 'REQUEST_COURSE_STATISTICS';
    courseId: string;
}

interface ReceiveCourseStatisticsAction {
    type: 'RECEIVE_COURSE_STATISTICS';
    courseStatistics: CourseStatistics;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestCourseStatisticsAction | ReceiveCourseStatisticsAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).
export const actionCreators = {
    requestCourseStatistics: (courseId: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState &&
            appState.courseStatistics &&
            appState.courseStatistics.isLoading === false &&
            appState.courseStatistics.courseId !== courseId) {
            fetch(`api/statistics/course=${courseId}/timeStamps=10`)
                .then(response => response.json() as Promise<CourseStatistics>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_COURSE_STATISTICS', courseStatistics: data });
                });

            dispatch({ type: 'REQUEST_COURSE_STATISTICS', courseId });
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.
const undefinedTeacherStatistic: CourseStatistics = {
    id: "undefined",
    courseId: "undefined",
    averageMark: 0,
    averageMarks: [],
    averageLevelOfDifficulty: 0,
    wouldTakeAgainRatio: 0
};
const unloadedState: CourseStatisticsState = { courseId: undefined, courseStatistics: undefinedTeacherStatistic , isLoading: false };

export const reducer: Reducer<CourseStatisticsState> = (state: CourseStatisticsState | undefined, incomingAction: Action): CourseStatisticsState => {
    if (state === undefined)
        return unloadedState;

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_COURSE_STATISTICS':
            return {
                courseId: action.courseId,
                courseStatistics: state.courseStatistics,
                isLoading: true
            };
        case 'RECEIVE_COURSE_STATISTICS':
            return {
                courseId: state.courseId,
                courseStatistics: action.courseStatistics,
                isLoading: false
            };
    }

    return state;
};
