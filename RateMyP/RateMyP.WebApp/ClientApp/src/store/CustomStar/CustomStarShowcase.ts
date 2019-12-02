import { Action, Reducer } from 'redux';
import { AppThunkAction } from '..';
import { CustomStar } from './CustomStar';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface CustomStarShowcaseState {
    isLoading: boolean,
    customStars: CustomStar[]
}

export interface CustomStarThumb {
    customStarId: string;
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
    customStars: CustomStar[];
}

interface SendCustomStarThumb {
    type: 'SEND_CUSTOM_STAR_THUMB';
    customStarId: string;
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
            appState.customStarShowcase &&
            appState.customStarShowcase.isLoading === false) {
            fetch(`api/customstar/teacher=${teacherId}`)
                .then(response => response.json() as Promise<CustomStar[]>)
                .then(customStars => {
                    dispatch({ type: 'RECEIVE_CUSTOM_STARS', customStars });
                });

            dispatch({ type: 'REQUEST_CUSTOM_STARS' });
        }
    },
    sendCustomStarThumb: (customStarId: string, thumbUp: boolean): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.student &&
            appState.student.user) {
            appState.student.user.getIdToken().then(userToken => {
                fetch('api/customstar/thumb', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${userToken}`,
                    },
                    body: JSON.stringify({ customStarId, thumbUp } as CustomStarThumb)
                }).then(res => res.json())
                .catch(error => console.error('Error:', error));
            });
            dispatch({type: 'SEND_CUSTOM_STAR_THUMB', customStarId, thumbUp });
        }
    },
};


// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: CustomStarShowcaseState = { customStars: [], isLoading: false };

export const reducer: Reducer<CustomStarShowcaseState> = (state: CustomStarShowcaseState | undefined, incomingAction: Action): CustomStarShowcaseState => {
    if (state === undefined)
        return unloadedState;

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_CUSTOM_STARS':
            return {
                customStars: state.customStars,
                isLoading: true,
            };
        case 'RECEIVE_CUSTOM_STARS':
            return {
                customStars: action.customStars,
                isLoading: false
            };          
        case 'SEND_CUSTOM_STAR_THUMB':
            let customStars = [...state.customStars];
            let imageIndex = customStars.findIndex(r => r.id === action.customStarId);
            if (imageIndex !== -1)
                action.thumbUp ? customStars[imageIndex].thumbUps!++ : customStars[imageIndex].thumbDowns!++;
            return {
                customStars,
                isLoading: state.isLoading,
            };                 
    }

    return state;
};
