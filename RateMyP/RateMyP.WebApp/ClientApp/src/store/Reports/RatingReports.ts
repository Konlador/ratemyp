import { Action, Reducer } from 'redux';
import { AppThunkAction } from '..';
import { Rating } from "../Ratings";

export interface RatingReportsState {
    reports: RatingReport[];
    isLoading: boolean;
}

export interface RatingReport {
    id: string;
    ratingId: string;
    rating?: Rating;
    studentId: string;
    reason: string;
    dateCreated?: Date;
}

interface RequestRatingReportsAction {
    type: 'REQUEST_RATING_REPORTS';
}

interface ReceiveRatingReportsAction {
    type: 'RECEIVE_RATING_REPORTS';
    reports: RatingReport[];
}

interface DeleteRatingReportAction {
    type: 'DELETE_RATING_REPORT';
    ratingReportId: string;
}

interface RemoveRatingReportAction {
    type: 'REMOVE_RATING_REPORT';
    ratingReportId: string;
}

type KnownAction = RequestRatingReportsAction | ReceiveRatingReportsAction | DeleteRatingReportAction | RemoveRatingReportAction;

export const actionCreators = {
    requestRatingReports: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.student && appState.student.user &&
            appState.ratingReports &&
            appState.ratingReports.isLoading === false) {
            appState.student.user.getIdToken().then(userToken => {
                fetch(`api/reports/rating`, {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${userToken}`,
                    }
                })
                .then(response => response.json() as Promise<RatingReport[]>)
                .then(reports => {
                    dispatch({ type: 'RECEIVE_RATING_REPORTS', reports });
                });
            });
            dispatch({ type: 'REQUEST_RATING_REPORTS' });
        }
    },
    deleteRatingReport: (ratingReportId: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.student && appState.student.user) {
            appState.student.user.getIdToken().then(userToken => {
                fetch(`api/reports/rating/${ratingReportId}`, {
                    method: 'DELETE',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${userToken}`,
                    }
                })
                .then(res => res.json())
                .catch(error => console.error('Error:', error))
                .then(() => dispatch({ type: 'REMOVE_RATING_REPORT', ratingReportId }));
            });
            dispatch({type: 'DELETE_RATING_REPORT', ratingReportId });
        }
    },
};

const unloadedState: RatingReportsState = { reports: [], isLoading: false };

export const reducer: Reducer<RatingReportsState> = (state: RatingReportsState | undefined, incomeStateAction: Action): RatingReportsState => {
    if (state === undefined)
        return unloadedState;

    const action = incomeStateAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_RATING_REPORTS':
            return {
                ...state,
                isLoading: true,
            };
        case 'RECEIVE_RATING_REPORTS':
            return {
                ...state,
                reports: action.reports,
                isLoading: false,
            };
        case 'REMOVE_RATING_REPORT':
            return {
                ...state,
                reports: state.reports.filter(r => r.id !== action.ratingReportId),
            };
        default:
            return state;
    }
};
