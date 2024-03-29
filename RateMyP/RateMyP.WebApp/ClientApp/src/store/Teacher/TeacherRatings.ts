import { Action, Reducer } from 'redux';
import { AppThunkAction } from '..';
import { Rating, RatingThumb } from "../Ratings";

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface TeacherRatingsState {
    isLoading: boolean;
    ratings: Rating[];
    teacherId: string | undefined;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestTeacherRatingsAction {
    type: 'REQUEST_TEACHER_RATINGS';
    teacherId: string;
}

interface ReceiveTeacherRatingsAction {
    type: 'RECEIVE_TEACHER_RATINGS';
    ratings: Rating[];
}

interface RequestTeacherRatingAction {
    type: 'REQUEST_TEACHER_RATING';
    ratingId: string;
}

interface ReceiveTeacherRatingAction {
    type: 'RECEIVE_TEACHER_RATING';
    rating: Rating;
}

interface SendRatingThumb {
    type: 'SEND_RATING_THUMB';
    ratingId: string;
    thumbUp: boolean;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestTeacherRatingsAction | ReceiveTeacherRatingsAction | SendRatingThumb | RequestTeacherRatingAction | ReceiveTeacherRatingAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestTeacherRatings: (teacherId: string): AppThunkAction<KnownAction> => async (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.teacherRatings &&
            appState.teacherRatings.isLoading === false &&
            appState.teacherRatings.teacherId !== teacherId) {
                fetch(`api/ratings/teacher=${teacherId}`)
                    .then(response => response.json() as Promise<Rating[]>)
                    .then(data => {
                        dispatch({ type: 'RECEIVE_TEACHER_RATINGS', ratings: data });
                    });
            dispatch({ type: 'REQUEST_TEACHER_RATINGS', teacherId: teacherId });
        }
    },
    sendRatingThumb: (ratingId: string, thumbUp: boolean): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.student &&
            appState.student.user) {
            appState.student.user.getIdToken().then(userToken => {
                fetch('api/ratings/thumb', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${userToken}`,
                    },
                    body: JSON.stringify({ ratingId, thumbUp } as RatingThumb)
                }).then(res => res.json())
                .catch(error => console.error('Error:', error));
            });
        }
        dispatch({type: 'SEND_RATING_THUMB', ratingId, thumbUp });
    },
    updateRating: (ratingId: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.teacherRatings &&
            appState.teacherRatings.isLoading === false) {
            fetch(`api/ratings/${ratingId}`)
                .then(response => response.json() as Promise<Rating>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_TEACHER_RATING', rating: data });
                });
        }
        dispatch({type: 'REQUEST_TEACHER_RATING', ratingId });
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: TeacherRatingsState = { ratings: [], isLoading: false, teacherId: undefined };

export const reducer: Reducer<TeacherRatingsState> = (state: TeacherRatingsState | undefined, incomingAction: Action): TeacherRatingsState => {
    if (state === undefined)
        return unloadedState;

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_TEACHER_RATINGS':
            return {
                ratings: state.ratings,
                isLoading: true,
                teacherId: action.teacherId
            };
        case 'RECEIVE_TEACHER_RATINGS':
            return {
                ratings: action.ratings,
                isLoading: false,
                teacherId: state.teacherId
            };
        case 'REQUEST_TEACHER_RATING':
            return {
                ratings: state.ratings,
                isLoading: true,
                teacherId: state.teacherId
            };
        case 'RECEIVE_TEACHER_RATING':
            let ratings = [...state.ratings];
            let ratingIndex = ratings.findIndex(r => r.id === action.rating.id);
            if (ratingIndex !== -1)
                ratings[ratingIndex] = action.rating;
            return {
                ratings: ratings,
                isLoading: false,
                teacherId: state.teacherId
            };
        case 'SEND_RATING_THUMB':
            ratings = [...state.ratings];
            ratingIndex = ratings.findIndex(r => r.id === action.ratingId);
            if (ratingIndex !== -1)
                action.thumbUp ? ratings[ratingIndex].thumbUps!++ : ratings[ratingIndex].thumbDowns!++;
            return {
                ratings: ratings,
                isLoading: state.isLoading,
                teacherId: state.teacherId
            };
    }

    return state;
};
