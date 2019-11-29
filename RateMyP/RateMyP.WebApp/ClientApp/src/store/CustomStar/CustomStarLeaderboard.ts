import { Action, Reducer } from 'redux';
import { AppThunkAction } from '..';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface CustomStarLeaderboardState {
    isLoading: boolean,
    images: Image[]
}

export interface Image {
    id: string, 
    studentId: string,
    thumbUps: number,
    thumbDowns: number,
    dateCreated: Date,
}

export interface CustomStarThumb {
    customStarId: string;
    studentId: string;
    thumbUp: boolean;
}


// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.
interface RequestCustomStarsAction {
    type: 'REQUEST_CUSTOM_STARS';
}

interface ReceiveCustomStarsAction {
    type: 'RECEIVE_CUSTOM_STARS';
    images: Image[];
}

interface SendCustomStarThumb {
    type: 'SEND_CUSTOM_STAR_THUMB';
    imageId: string;
    thumbUp: boolean;
}


// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestCustomStarsAction | ReceiveCustomStarsAction | SendCustomStarThumb;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestCustomStars: (teacherId: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.customStarLeaderboard &&
            appState.customStarLeaderboard.isLoading === false) {
            fetch(`api/images/data/teacher=${teacherId}`)
                .then(response => response.json() as Promise<Image[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_CUSTOM_STARS', images: data });
                });

            dispatch({ type: 'REQUEST_CUSTOM_STARS' });
        }
    },
    sendCustomStarThumb: (imageId: string, thumbUp: boolean): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState) {
            fetch('api/images/thumb', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({'customStarId': imageId, studentId: "spagetio", thumbUp } as CustomStarThumb)
            }).then(res => res.json())
            .catch(error => console.error('Error:', error));
        }
        dispatch({type: 'SEND_CUSTOM_STAR_THUMB', imageId, thumbUp });
    },
};


// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: CustomStarLeaderboardState = { images: [], isLoading: false };

export const reducer: Reducer<CustomStarLeaderboardState> = (state: CustomStarLeaderboardState | undefined, incomingAction: Action): CustomStarLeaderboardState => {
    if (state === undefined)
        return unloadedState;

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_CUSTOM_STARS':
            return {
                images: state.images,
                isLoading: true,
            };
        case 'RECEIVE_CUSTOM_STARS':
            return {
                images: action.images,
                isLoading: false
            };          
        case 'SEND_CUSTOM_STAR_THUMB':
            let images = [...state.images];
            let imageIndex = images.findIndex(r => r.id === action.imageId);
            if (imageIndex !== -1)
                action.thumbUp ? images[imageIndex].thumbUps!++ : images[imageIndex].thumbDowns!++;
            return {
                images: images,
                isLoading: state.isLoading,
            };                 
    }

    return state;
};
