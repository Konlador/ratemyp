import { Action, Reducer } from 'redux';
import { AppThunkAction } from '.';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface BadgesState {
    isLoading: boolean;
    badges: Badge[];
}

export interface Badge {
    id: string,
    image: string;
    description: string;
    size: number,
    type: string,
    data: ByteBuffer,
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestBadgesAction {
    type: 'REQUEST_BADGES';
}

interface ReceiveBadgesAction {
    type: 'RECEIVE_BADGES';
    badges: Badge[];
}

interface RequestBadgeAction {
    type: 'REQUEST_BADGE';
}

interface ReceiveBadgeAction {
    type: 'RECEIVE_BADGE';
    badges: Badge;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestBadgesAction | ReceiveBadgesAction | RequestBadgeAction | ReceiveBadgeAction

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestBadges: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.badges &&
            appState.badges.isLoading === false) {
            fetch(`api/badges`)
                .then(response => response.json() as Promise<Badge[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_BADGES', badges: data });
                });

            dispatch({ type: 'REQUEST_BADGES' });
        }
    },
    requestBadge: (badgeId: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.badges &&
            appState.badges.isLoading === false) {
            fetch(`api/badges/badge=${badgeId}`)
                .then(response => response.json() as Promise<Badge>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_BADGE', badges: data });
                });

            dispatch({ type: 'REQUEST_BADGE'});
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: BadgesState = { badges: [], isLoading: false };

export const reducer: Reducer<BadgesState> = (state: BadgesState | undefined, incomingAction: Action): BadgesState => {
    if (state === undefined)
        return unloadedState;

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_BADGES':
            return {
                badges: state.badges,
                isLoading: true,
            };
        case 'RECEIVE_BADGES':
            return {
                badges: [...state.badges, ...action.badges],
                isLoading: false,
            };
        case 'REQUEST_BADGE':
            return {
                badges: state.badges,
                isLoading: true,
            };
        case 'RECEIVE_BADGE':
            return {
                badges: state.badges,
                isLoading: false,
            };
    }

    return state;
};
