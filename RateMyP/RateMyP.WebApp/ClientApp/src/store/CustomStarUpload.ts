import { Action, Reducer } from 'redux';
import { AppThunkAction } from '.';
import CryptoJS from 'crypto-js';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface CustomStar {
    image: string | ArrayBuffer | null,
    teacherId: string,
    submitButtonClicked: boolean
}


// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface SetImageAction {
    type: 'SET_IMAGE';
    value: string | ArrayBuffer | null;
}


interface SetTeacherIdAction {
    type: 'SET_TEACHER_ID'
    value: string
}

interface SubmitButtonClickedAction {
    type: 'SUBMIT_BUTTON_CLICKED'
}

interface ClearStoreAction {
    type: 'CLEAR_STORE'
}
// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = SetImageAction | SetTeacherIdAction | SubmitButtonClickedAction | ClearStoreAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    setImage: (value: string | ArrayBuffer | null): AppThunkAction<KnownAction> => (dispatch) => { 
        dispatch({ type: 'SET_IMAGE', value: value });
    },
    setTeacherId: (value: string): AppThunkAction<KnownAction> => (dispatch) => { 
        dispatch({ type: 'SET_TEACHER_ID', value: value });
    },
    submitButtonClicked: () => ({ type: 'SUBMIT_BUTTON_CLICKED' } as SubmitButtonClickedAction),
    uploadImage: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const state = getState().customStarUpload;
        if(state !== undefined && state.image !== undefined){
            var timeStamp = String(Date.now())
            var publicId = state.teacherId
            var stringToSign = 'public_id=' + publicId + '&timestamp=' + timeStamp +'7Ghux4duxnZD8JWDXZBPzond6X0';
            var signatureHash = CryptoJS.SHA1(stringToSign);
            var signature = CryptoJS.enc.Hex.stringify(signatureHash);
            fetch('https://api.cloudinary.com/v1_1/drodzj9pr/image/upload', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({file: state.image,
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

const unloadedState: CustomStar = { image: null, teacherId: '', submitButtonClicked: false };

export const reducer: Reducer<CustomStar> = (state: CustomStar | undefined, incomingAction: Action): CustomStar => {
    if (state === undefined)
        return unloadedState;

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'SET_IMAGE':
            return {
                image: action.value,
                teacherId: state.teacherId,
                submitButtonClicked: state.submitButtonClicked,
            };
        case 'SET_TEACHER_ID':
            return {
                image: state.image,
                teacherId: action.value,
                submitButtonClicked: state.submitButtonClicked,
            };       
        case 'SUBMIT_BUTTON_CLICKED':
            return {
                image: state.image,
                teacherId: state.teacherId,
                submitButtonClicked: true,
            };   
        case 'CLEAR_STORE':
            return unloadedState;                          
    }

    return state;
};
