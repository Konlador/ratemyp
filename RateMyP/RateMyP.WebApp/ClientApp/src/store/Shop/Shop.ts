import { Action, Reducer } from 'redux';
import { AppThunkAction } from '..';

export interface ShopState {
    merch: Merchandise[];
    isLoading: boolean;
}

export interface Merchandise {
    id: string;
    name: string;
    description: string;
    price: number;
}

interface RequestMerchandiseAction {
    type: 'REQUEST_MERCHANSISE';
}

interface ReceiveMerchandiseAction {
    type: 'RECEIVE_MERCHANSISE';
    merch: Merchandise[];
}

type KnownAction = RequestMerchandiseAction | ReceiveMerchandiseAction;

export const actionCreators = {
    requestMerchandise: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.shop &&
            appState.shop.isLoading === false) {
            fetch(`api/merch`)
                .then(response => response.json() as Promise<Merchandise[]>)
                .then(merch => {
                    dispatch({ type: 'RECEIVE_MERCHANSISE', merch });
                });

            dispatch({ type: 'REQUEST_MERCHANSISE' });
        }
    },
};

const unloadedState: ShopState = { merch: [], isLoading: false };

export const reducer: Reducer<ShopState> = (state: ShopState | undefined, incomeStateAction: Action): ShopState => {
    if (state === undefined)
        return unloadedState;

    const action = incomeStateAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_MERCHANSISE':
            return {
                ...state,
                isLoading: true,
            };
        case 'RECEIVE_MERCHANSISE':
            return {
                ...state,
                merch: action.merch,
                isLoading: false,
            };
        default:
            return state;
    }
};
