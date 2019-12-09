import { Action, Reducer } from 'redux';
import { AppThunkAction } from '..';
import { Rating } from "../Ratings";
import { RatingReport } from "./RatingReports";

export interface RatingReportCreationState {
    submitButtonClicked: boolean;
    ratingId : string;
    rating?: Rating;
    report: RatingReport;
}

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

type KnownAction = RequestTeacherAction | ReceiveTeacherAction | ChangeReasonAction | SubmitReportAction | SendReportAction | SetRatingIdAction |  ClearStoreAction | SetStudentIdAction;

export const actionCreators = {
    requestRating: (ratingId: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.ratingReportCreation &&
            (appState.ratingReportCreation.rating === undefined ||
            appState.ratingReportCreation.rating.id !== ratingId)) {
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
        const appState = getState();
        if (appState &&
            appState.student && appState.student.user &&
            appState.ratingReportCreation) {
            appState.student.user.getIdToken().then(userToken => {
                fetch('api/reports/rating', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${userToken}`,
                    },
                    body: JSON.stringify(appState.ratingReportCreation!.report)
                }).then(res => res.json()).catch(error => console.error('Error:', error));
            });
            dispatch({type: 'SEND_REPORT'});
        }
    },
    setRatingId: (value: string): AppThunkAction<KnownAction> => (dispatch) => { 
        dispatch({ type: 'SET_RATING_ID', value: value });
    },
    setStudentId: (value: string): AppThunkAction<KnownAction> => (dispatch) => { 
        dispatch({ type: 'SET_STUDENT_ID', value: value });
    },
    clearStore: () => ({ type: 'CLEAR_STORE'} as ClearStoreAction),
};

const unloadedRatingReport: RatingReport = { id: '', ratingId: '', reason: '', studentId: '', dateCreated: undefined }
const unloadedState: RatingReportCreationState = { ratingId: '', rating: undefined, submitButtonClicked: false, report: unloadedRatingReport };

export const reducer: Reducer<RatingReportCreationState> = (state: RatingReportCreationState | undefined, incomeStateAction: Action): RatingReportCreationState => {
    if (state === undefined)
        return unloadedState;

    const action = incomeStateAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_RATING':
            return {
                ...state,
                rating: undefined,
                ratingId: action.ratingId,
            };
        case 'RECEIVE_RATING':
            return {
                ...state,
                rating: action.rating,
            };
        case 'CHANGE_REASON':
            return {
                ...state,
                report: {
                    ...state.report,
                    reason: action.value,
                },
            };
        case 'SUBMIT_REPORT':
            return {
                ...state,
                submitButtonClicked: true,
            };
        case 'SET_RATING_ID':
            return {
                ...state,
                report: {
                    ...state.report,
                    ratingId: action.value,
                },
            };
        case 'SET_STUDENT_ID':
            return {
                ...state,
                report: {
                    ...state.report,
                    studentId: action.value,
                },
            };
        case 'CLEAR_STORE':
            return unloadedState;
        default:
            return state;
    }
};
