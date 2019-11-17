import { Action, Reducer } from 'redux';
import { AppThunkAction } from '.';
import CryptoJS from 'crypto-js';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface CustomStarState {
    image: string | ArrayBuffer | null,
    teacherId: string,
    submitButtonClicked: boolean,
    height: number,
    width: number,
}


// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface SetImageAction {
    type: 'SET_IMAGE';
    value: string | ArrayBuffer | null;
}

interface SetImageSizeAction {
    type: 'SET_IMAGE_SIZE';
    width: number;
    height: number
}

interface SetTeacherIdAction {
    type: 'SET_TEACHER_ID'
    value: string
}

interface SubmitButtonClickAction {
    type: 'SUBMIT_BUTTON_CLICK'
}

interface ClearStoreAction {
    type: 'CLEAR_STORE'
}
// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = SetImageAction | SetTeacherIdAction | SubmitButtonClickAction | ClearStoreAction | SetImageSizeAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    setImage: (value: string | ArrayBuffer | null): AppThunkAction<KnownAction> => (dispatch) => { 
        dispatch({ type: 'SET_IMAGE', value: value });
    },
    setImageSize: (width: number, height: number): AppThunkAction<KnownAction> => (dispatch) => { 
        dispatch({ type: 'SET_IMAGE_SIZE', width: width, height: height });
    },
    setTeacherId: (value: string): AppThunkAction<KnownAction> => (dispatch) => { 
        dispatch({ type: 'SET_TEACHER_ID', value: value });
    },
    submitButtonClick: () => ({ type: 'SUBMIT_BUTTON_CLICK' } as SubmitButtonClickAction),
    uploadImage: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if(appState !== undefined &&
           appState.customStarUpload !== undefined &&
           appState.customStarUpload.image !== undefined){
            var timeStamp = String(Date.now())
            var publicId = appState.customStarUpload.teacherId
            var stringToSign = 'public_id=' + publicId + '&timestamp=' + timeStamp +'7Ghux4duxnZD8JWDXZBPzond6X0';
            var signatureHash = CryptoJS.SHA1(stringToSign);
            var signature = CryptoJS.enc.Hex.stringify(signatureHash);
            fetch('https://api.cloudinary.com/v1_1/drodzj9pr/image/upload', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({file: appState.customStarUpload.image,
                                      api_key: '566771129442981',
                                      timestamp: timeStamp,
                                      public_id: publicId,
                                      signature: signature})
            }).then(res => res.json()).catch(error => console.error('Error:', error)).then(response => console.log('Success:', response));
        }
    },
    clearStore: () => ({ type: 'CLEAR_STORE'} as ClearStoreAction),
};


// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: CustomStarState = { image: null, teacherId: '', submitButtonClicked: false, height: 0, width: 0, };

export const reducer: Reducer<CustomStarState> = (state: CustomStarState | undefined, incomingAction: Action): CustomStarState => {
    if (state === undefined)
        return unloadedState;

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'SET_IMAGE':
            return {
                image: action.value,
                teacherId: state.teacherId,
                submitButtonClicked: false,
                height: state.height,
                width: state.width
            };
        case 'SET_IMAGE_SIZE':
            return {
                image: state.image,
                teacherId: state.teacherId,
                submitButtonClicked: state.submitButtonClicked,
                height: action.height,
                width: action.width
            };            
        case 'SET_TEACHER_ID':
            return {
                image: state.image,
                teacherId: action.value,
                submitButtonClicked: state.submitButtonClicked,
                height: state.height,
                width: state.width
            };       
        case 'SUBMIT_BUTTON_CLICK':
            return {
                image: state.image,
                teacherId: state.teacherId,
                submitButtonClicked: true,
                height: state.height,
                width: state.width
            };   
        case 'CLEAR_STORE':
            return unloadedState;                          
    }

    return state;
};
