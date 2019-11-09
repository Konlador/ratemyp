import { Action, Reducer } from 'redux';
import { AppThunkAction } from '.';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface TagsState {
    isLoading: boolean;
    tags: Tag[];
}

export interface Tag {
    id: string,
    text: string;
    tagType: number;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestTagsAction {
    type: 'REQUEST_TAGS';
}

interface ReceiveTagsAction {
    type: 'RECEIVE_TAGS';
    tags: Tag[];
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestTagsAction | ReceiveTagsAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestTags: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState &&
            appState.tags &&
            appState.tags.isLoading === false &&
            appState.tags.tags.length === 0) {
            fetch(`api/tags`)
                .then(response => response.json() as Promise<Tag[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_TAGS', tags: data });
                });

            dispatch({ type: 'REQUEST_TAGS' });
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: TagsState = { tags: [], isLoading: false };

export const reducer: Reducer<TagsState> = (state: TagsState | undefined, incomingAction: Action): TagsState => {
    if (state === undefined)
        return unloadedState;

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_TAGS':
            return {
                tags: state.tags,
                isLoading: true
            };
        case 'RECEIVE_TAGS':
            return {
                tags: action.tags,
                isLoading: false
            };
    }

    return state;
};
