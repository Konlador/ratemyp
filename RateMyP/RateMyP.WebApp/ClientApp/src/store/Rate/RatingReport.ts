import { Action, Reducer } from 'redux';
import { AppThunkAction } from '..';
import { Rating } from "../Ratings";

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface RatingReportState {
    submitButtonClicked: boolean;
    ratingId : string;
    rating: Rating | undefined;
    report: RatingReport;
}

export interface RatingReport {
    ratingId: string;
    studentId: string;
    reason: string;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe sometheState that is goeState to happen.

interface RequestTeacherAction {
    type: 'REQUEST_RATING';
    ratingId: string;
}

interface ReceiveTeacherAction {
    type: 'RECEIVE_RATING';
    rating: Rating;
}

interface ChangeReasonAction {
    type: 'CHANGE_REASON'
    value: string;
}

interface SubmitReportAction {
    type: 'SUBMIT_REPORT'
}

interface SendReportAction {
    type: 'SEND_REPORT'
}

interface SetRatingIdAction {
    type: 'SET_RATING_ID'
    value: string
}

interface SetStudentIdAction {
    type: 'SET_STUDENT_ID'
    value: string
}

interface ClearStoreAction {
    type: 'CLEAR_STORE'
}


// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type streStates (and not any other arbitrary streState).
type KnownAction = RequestTeacherAction | ReceiveTeacherAction | ChangeReasonAction | SubmitReportAction | SendReportAction | SetRatingIdAction |  ClearStoreAction | SetStudentIdAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loadeState data).

export const actionCreators = {
    requestRating: (ratingId: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.ratingReport &&
            (appState.ratingReport.rating === undefined ||
            appState.ratingReport.rating.id !== ratingId)) {
            fetch(`api/ratings/${ratingId}`)
                .then(response => response.json() as Promise<Rating>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_RATING', rating: data });
                });

            dispatch({ type: 'REQUEST_RATING', ratingId });
        }
    },
    changeReason: (value: string): AppThunkAction<KnownAction> => (dispatch) => { 
        dispatch({ type: 'CHANGE_REASON', value: value });
    },
    submitReport: () => ({ type: 'SUBMIT_REPORT' } as SubmitReportAction),
    sendReport: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const state = getState().ratingReport;
        if(state !== undefined){
            fetch('api/reports', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(state.report)
            }).then(res => res.json()).catch(error => console.error('Error:', error));
        }
        dispatch({type: 'SEND_REPORT'});
    },
    setRatingId: (value: string): AppThunkAction<KnownAction> => (dispatch) => { 
        dispatch({ type: 'SET_RATING_ID', value: value });
    },
    setStudentId: (value: string): AppThunkAction<KnownAction> => (dispatch) => { 
        dispatch({ type: 'SET_STUDENT_ID', value: value });
    },
    clearStore: () => ({ type: 'CLEAR_STORE'} as ClearStoreAction),
};
// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.
const unloadedRatingReport: RatingReport = { ratingId: '', reason: '', studentId: '' }
const unloadedState: RatingReportState = { ratingId: '', rating: undefined, submitButtonClicked: false, report: unloadedRatingReport };

export const reducer: Reducer<RatingReportState> = (state: RatingReportState | undefined, incomeStateAction: Action): RatingReportState => {
    if (state === undefined)
        return unloadedState;

    const action = incomeStateAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_RATING':
            return {
                rating: undefined,
                ratingId: action.ratingId,
                report: state.report,
                submitButtonClicked: state.submitButtonClicked
            };
        case 'RECEIVE_RATING':
            return {
                rating: action.rating,
                ratingId: state.ratingId,
                report: state.report,
                submitButtonClicked: state.submitButtonClicked
            };
        case 'CHANGE_REASON':
            return Object.assign({}, state, {
                report: Object.assign({}, state.report, {
                    reason: action.value, 
                }),
                });
        case 'SUBMIT_REPORT':
            return Object.assign({}, state, {
                submitButtonClicked: true,
                });
        case 'SET_RATING_ID':
            return Object.assign({}, state, {
                    report: Object.assign({}, state.report, {
                        ratingId: action.value,
                    }),
                });
        case 'SET_STUDENT_ID':
            return Object.assign({}, state, {
                    report: Object.assign({}, state.report, {
                        studentId: action.value,
                    }),
                });
        case 'SEND_REPORT':
            return state;
        case 'CLEAR_STORE':
            return unloadedState;
        default:
            return state;
    }
};
