import { Action, Reducer } from 'redux';
import { AppThunkAction } from '..';
import { Image } from './CustomStarLeaderboard';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface CustomStarReportState {
    submitButtonClicked: boolean;
    customStarId : string;
    image: Image | undefined;
    report: CustomStarReport;
}

export interface CustomStarReport {
    customStarId: string;
    studentId: string;
    reason: string;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe sometheState that is goeState to happen.

interface RequestCustomStarAction {
    type: 'REQUEST_CUSTOM_STAR';
    customStarId: string;
}

interface ReceiveCustomStarAction {
    type: 'RECEIVE_CUSTOM_STAR';
    image: Image;
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


// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type streStates (and not any other arbitrary streState).
type KnownAction = RequestCustomStarAction | ReceiveCustomStarAction | ChangeReasonAction | SubmitReportAction | SendReportAction | SetCustomStarIdAction |  ClearStoreAction | SetStudentIdAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loadeState data).

export const actionCreators = {
    requestCustomStar: (customStarId: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.customStarReport &&
            (appState.customStarReport.image === undefined ||
            appState.customStarReport.image.id !== customStarId)) {
            fetch(`api/images/${customStarId}`)
                .then(response => response.json() as Promise<Image>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_CUSTOM_STAR', image: data });
                });
            dispatch({ type: 'REQUEST_CUSTOM_STAR', customStarId });
        }
    },
    changeReason: (value: string): AppThunkAction<KnownAction> => (dispatch) => { 
        dispatch({ type: 'CHANGE_REASON', value: value });
    },
    submitReport: () => ({ type: 'SUBMIT_REPORT' } as SubmitReportAction),
    sendReport: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const state = getState();
        if(state.customStarReport !== undefined){
            fetch('api/reports/custom-star', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(state.customStarReport.report)
            }).then(res => res.json()).catch(error => console.error('Error:', error));
        }
        dispatch({type: 'SEND_REPORT'});
    },
    setCustomStarId: (value: string): AppThunkAction<KnownAction> => (dispatch) => { 
        dispatch({ type: 'SET_CUSTOM_STAR_ID', value: value });
    },
    setStudentId: (value: string): AppThunkAction<KnownAction> => (dispatch) => { 
        dispatch({ type: 'SET_STUDENT_ID', value: value });
    },
    clearStore: () => ({ type: 'CLEAR_STORE'} as ClearStoreAction),
};
// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.
const unloadedCustomStarReport: CustomStarReport = { customStarId: '', reason: '', studentId: '' }
const unloadedState: CustomStarReportState = { customStarId: '', image: undefined, submitButtonClicked: false, report: unloadedCustomStarReport };

export const reducer: Reducer<CustomStarReportState> = (state: CustomStarReportState | undefined, incomeStateAction: Action): CustomStarReportState => {
    if (state === undefined)
        return unloadedState;

    const action = incomeStateAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_CUSTOM_STAR':
            return {
                image: state.image,
                customStarId: action.customStarId,
                report: state.report,
                submitButtonClicked: state.submitButtonClicked
            };
        case 'RECEIVE_CUSTOM_STAR':
            return {
                image: action.image,
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
