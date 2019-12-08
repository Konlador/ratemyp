import { Action, Reducer } from 'redux';
import { AppThunkAction } from '..';
import { CustomStar } from '../CustomStar/CustomStar';
import { CustomStarReport } from "./CustomStarReports";

export interface CustomStarReportCreationState {
    submitButtonClicked: boolean;
    customStarId : string;
    customStar: CustomStar | undefined;
    report: CustomStarReport;
}

interface RequestCustomStarAction {
    type: 'REQUEST_CUSTOM_STAR';
    customStarId: string;
}

interface ReceiveCustomStarAction {
    type: 'RECEIVE_CUSTOM_STAR';
    customStar: CustomStar;
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

interface SetCustomStarIdAction {
    type: 'SET_CUSTOM_STAR_ID'
    value: string
}

interface SetStudentIdAction {
    type: 'SET_STUDENT_ID'
    value: string
}

interface ClearStoreAction {
    type: 'CLEAR_STORE'
}

type KnownAction = RequestCustomStarAction | ReceiveCustomStarAction | ChangeReasonAction | SubmitReportAction | SendReportAction | SetCustomStarIdAction |  ClearStoreAction | SetStudentIdAction;

export const actionCreators = {
    requestCustomStar: (customStarId: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.customStarReportCreation &&
            (appState.customStarReportCreation.customStar === undefined ||
            appState.customStarReportCreation.customStar.id !== customStarId)) {
            fetch(`api/images/${customStarId}`)
                .then(response => response.json() as Promise<CustomStar>)
                .then(customStar => {
                    dispatch({ type: 'RECEIVE_CUSTOM_STAR', customStar });
                });
            dispatch({ type: 'REQUEST_CUSTOM_STAR', customStarId });
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
            appState.customStarReportCreation) {
            appState.student.user.getIdToken().then(userToken => {
                fetch('api/reports/custom-star', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${userToken}`,
                    },
                    body: JSON.stringify(appState.customStarReportCreation!.report)
                }).then(res => res.json()).catch(error => console.error('Error:', error));
            });
            dispatch({type: 'SEND_REPORT'});
        }
    },
    setCustomStarId: (value: string): AppThunkAction<KnownAction> => (dispatch) => { 
        dispatch({ type: 'SET_CUSTOM_STAR_ID', value: value });
    },
    setStudentId: (value: string): AppThunkAction<KnownAction> => (dispatch) => { 
        dispatch({ type: 'SET_STUDENT_ID', value: value });
    },
    clearStore: () => ({ type: 'CLEAR_STORE'} as ClearStoreAction),
};

const unloadedCustomStarReport: CustomStarReport = { id: '', customStarId: '', reason: '', studentId: '', dateCreated: undefined };
const unloadedState: CustomStarReportCreationState = { customStarId: '', customStar: undefined, submitButtonClicked: false, report: unloadedCustomStarReport };

export const reducer: Reducer<CustomStarReportCreationState> = (state: CustomStarReportCreationState | undefined, incomeStateAction: Action): CustomStarReportCreationState => {
    if (state === undefined)
        return unloadedState;

    const action = incomeStateAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_CUSTOM_STAR':
            return {
                customStar: state.customStar,
                customStarId: action.customStarId,
                report: state.report,
                submitButtonClicked: state.submitButtonClicked
            };
        case 'RECEIVE_CUSTOM_STAR':
            return {
                customStar: action.customStar,
                customStarId: state.customStarId,
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
        case 'SET_CUSTOM_STAR_ID':
            return Object.assign({}, state, {
                    report: Object.assign({}, state.report, {
                        customStarId: action.value,
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
