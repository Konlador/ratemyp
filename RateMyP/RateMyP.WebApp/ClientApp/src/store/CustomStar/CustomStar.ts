import { Action, Reducer } from 'redux';
import { AppThunkAction } from '..';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface CustomStarState {
    image: string | ArrayBuffer | null,
    submitButtonClicked: boolean,
    height: number,
    width: number,
    zoomLevel: number,
    imageUploaded: boolean
}

export interface CustomStar {
    id: string, 
    teacherId: string,
    studentId: string,
    thumbUps: number,
    thumbDowns: number,
    dateCreated: Date,
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

interface SubmitButtonClickAction {
    type: 'SUBMIT_BUTTON_CLICK'
}

interface UploadCustomStarAction {
    type: 'UPLOAD_CUSTOM_STAR'
}

interface SetZoomLevelAction {
    type: 'SET_ZOOM_LEVEL';
    value: number
}

interface ClearStoreAction {
    type: 'CLEAR_STORE'
}
// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = SetImageAction | SubmitButtonClickAction | ClearStoreAction | SetImageSizeAction | SetZoomLevelAction | UploadCustomStarAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    setImage: (value: string | ArrayBuffer | null): AppThunkAction<KnownAction> => (dispatch) => { 
        dispatch({ type: 'SET_IMAGE', value });
    },
    setImageSize: (width: number, height: number): AppThunkAction<KnownAction> => (dispatch) => { 
        dispatch({ type: 'SET_IMAGE_SIZE', width, height });
    },
    setZoomLevel: (value: number): AppThunkAction<KnownAction> => (dispatch) => { 
        dispatch({ type: 'SET_ZOOM_LEVEL', value,  });
    },
    submitButtonClick: () => ({ type: 'SUBMIT_BUTTON_CLICK' } as SubmitButtonClickAction),
    uploadCustomStar : (teacherId: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.student && appState.student.user &&
            appState.customStarUpload && appState.customStarUpload.image){
            appState.student.user.getIdToken().then(userToken => {
                fetch(`api/customstar/teacher=${teacherId}`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${userToken}`,
                    },
                    body: JSON.stringify({image: appState.customStarUpload!.image})
                }).then(res => res.json()).catch(error => console.error('Error:', error));
            });
            dispatch({ type: 'UPLOAD_CUSTOM_STAR' });
        }
    },
    clearStore: () => ({ type: 'CLEAR_STORE'} as ClearStoreAction),
};


// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: CustomStarState = { image: null, submitButtonClicked: false, height: 0, width: 0, zoomLevel: 1, imageUploaded: false};

export const reducer: Reducer<CustomStarState> = (state: CustomStarState | undefined, incomingAction: Action): CustomStarState => {
    if (state === undefined)
        return unloadedState;

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'SET_IMAGE':
            return {
                image: action.value,
                submitButtonClicked: false,
                height: state.height,
                width: state.width,
                zoomLevel: state.zoomLevel,
                imageUploaded: state.imageUploaded
            };
        case 'SET_ZOOM_LEVEL':
            return {
                image: state.image,
                submitButtonClicked: false,
                height: state.height,
                width: state.width,
                zoomLevel: action.value,
                imageUploaded: state.imageUploaded
            };            
        case 'SET_IMAGE_SIZE':
            return {
                image: state.image,
                submitButtonClicked: state.submitButtonClicked,
                height: action.height,
                width: action.width,
                zoomLevel: state.zoomLevel,
                imageUploaded: state.imageUploaded
            };            
        case 'SUBMIT_BUTTON_CLICK':
            return {
                image: state.image,
                submitButtonClicked: true,
                height: state.height,
                width: state.width,
                zoomLevel: state.zoomLevel,
                imageUploaded: state.imageUploaded
            };   
        case 'UPLOAD_CUSTOM_STAR':
            return {
                image: null,
                submitButtonClicked: true,
                height: 0,
                width: 0,
                zoomLevel: 1,
                imageUploaded: true
            };               
        case 'CLEAR_STORE':
            return unloadedState;                          
    }

    return state;
};
