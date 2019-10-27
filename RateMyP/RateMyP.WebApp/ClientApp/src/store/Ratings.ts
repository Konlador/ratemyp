import { Action, Reducer } from 'redux';
import { AppThunkAction } from '.';
//import { Tag } from './Tags';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface RatingsState {
    isLoading: boolean;
    ratings: Rating[];
}

export interface Rating {
    id: string,
    teacherId: string;
    courseId: string;
    overallMark: number;
    levelOfDifficulty: number;
    wouldTakeTeacherAgain: boolean;
    dateCreated: Date;
    comment: string;
    tags: RatingTag[];
}

interface RatingTag {
    ratingId: string,
    tagId: string;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestRatingsAction {
    type: 'REQUEST_RATINGS';
}

interface ReceiveRatingsAction {
    type: 'RECEIVE_RATINGS';
    ratings: Rating[];
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestRatingsAction | ReceiveRatingsAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestRatings: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.ratings && appState.ratings.isLoading === false && appState.ratings.ratings.length === 0) {
            fetch(`api/ratings`)
                .then(response => response.json() as Promise<Rating[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_RATINGS', ratings: data });
                });

            dispatch({ type: 'REQUEST_RATINGS' });
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: RatingsState = { ratings: [], isLoading: false };

export const reducer: Reducer<RatingsState> = (state: RatingsState | undefined, incomingAction: Action): RatingsState => {
    if (state === undefined)
        return unloadedState;

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_RATINGS':
            return {
                ratings: state.ratings,
                isLoading: true
            };
        case 'RECEIVE_RATINGS':
            return {
                ratings: action.ratings,
                isLoading: false
            };
    }

    return state;
};
