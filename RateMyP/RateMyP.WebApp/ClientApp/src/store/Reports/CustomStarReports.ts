import { Action, Reducer } from 'redux';
import { AppThunkAction } from '..';
import { CustomStar } from "../CustomStar/CustomStar";

export interface CustomStarReportsState {
    reports: CustomStarReport[];
    isLoading: boolean;
}

export interface CustomStarReport {
    id: string;
    customStarId: string;
    customStar?: CustomStar;
    studentId: string;
    reason: string;
    dateCreated?: Date;
}

interface RequestCustomStarReportsAction {
    type: 'REQUEST_CUSTOM_STAR_REPORTS';
}

interface ReceiveCustomStarReportsAction {
    type: 'RECEIVE_CUSTOM_STAR_REPORTS';
    reports: CustomStarReport[];
}

interface DeleteCustomStarReportAction {
    type: 'DELETE_CUSTOM_STAR_REPORT';
    customStarReportId: string;
}

interface RemoveCustomStarReportAction {
    type: 'REMOVE_CUSTOM_STAR_REPORT';
    customStarReportId: string;
}

type KnownAction = RequestCustomStarReportsAction | ReceiveCustomStarReportsAction | DeleteCustomStarReportAction | RemoveCustomStarReportAction;

export const actionCreators = {
    requestCustomStarReports: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.student && appState.student.user &&
            appState.customStarReports &&
            appState.customStarReports.isLoading === false) {
            appState.student.user.getIdToken().then(userToken => {
                fetch(`api/reports/custom-star`, {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${userToken}`,
                    }
                })
                .then(response => response.json() as Promise<CustomStarReport[]>)
                .then(reports => {
                    dispatch({ type: 'RECEIVE_CUSTOM_STAR_REPORTS', reports });
                });
            });
            dispatch({ type: 'REQUEST_CUSTOM_STAR_REPORTS' });
        }
    },
    deleteCustomStarReport: (customStarReportId: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.student && appState.student.user) {
            appState.student.user.getIdToken().then(userToken => {
                fetch(`api/reports/custom-star/${customStarReportId}`, {
                    method: 'DELETE',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${userToken}`,
                    }
                })
                .then(res => res.json())
                .catch(error => console.error('Error:', error))
                .then(() => dispatch({ type: 'REMOVE_CUSTOM_STAR_REPORT', customStarReportId }));
            });
            dispatch({type: 'DELETE_CUSTOM_STAR_REPORT', customStarReportId });
        }
    },
};

const unloadedState: CustomStarReportsState = { reports: [], isLoading: false };

export const reducer: Reducer<CustomStarReportsState> = (state: CustomStarReportsState | undefined, incomeStateAction: Action): CustomStarReportsState => {
    if (state === undefined)
        return unloadedState;

    const action = incomeStateAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_CUSTOM_STAR_REPORTS':
            return {
                ...state,
                isLoading: true,
            };
        case 'RECEIVE_CUSTOM_STAR_REPORTS':
            return {
                ...state,
                reports: action.reports,
                isLoading: false,
            };
        case 'REMOVE_CUSTOM_STAR_REPORT':
            return {
                ...state,
                reports: state.reports.filter(r => r.id !== action.customStarReportId),
            };
        default:
            return state;
    }
};
