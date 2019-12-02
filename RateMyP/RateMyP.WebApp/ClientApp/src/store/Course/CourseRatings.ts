import { Action, Reducer } from 'redux';
import { AppThunkAction } from '..';
import { Rating, RatingThumb } from "../Ratings";

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface CourseRatingsState {
    isLoading: boolean;
    ratings: Rating[];
    courseId: string | undefined;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestCourseRatingsAction {
    type: 'REQUEST_COURSE_RATINGS';
    courseId: string;
}

interface ReceiveCourseRatingsAction {
    type: 'RECEIVE_COURSE_RATINGS';
    ratings: Rating[];
}

interface RequestCourseRatingAction {
    type: 'REQUEST_COURSE_RATING';
    ratingId: string;
}

interface ReceiveCourseRatingAction {
    type: 'RECEIVE_COURSE_RATING';
    rating: Rating;
}

interface SendRatingThumb {
    type: 'SEND_RATING_THUMB';
    ratingId: string;
    thumbUp: boolean;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestCourseRatingsAction | ReceiveCourseRatingsAction | SendRatingThumb | RequestCourseRatingAction | ReceiveCourseRatingAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestCourseRatings: (courseId: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.courseRatings &&
            appState.courseRatings.isLoading === false &&
            appState.courseRatings.courseId !== courseId) {
            fetch(`api/ratings/course=${courseId}`)
                .then(response => response.json() as Promise<Rating[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_COURSE_RATINGS', ratings: data });
                });

            dispatch({ type: 'REQUEST_COURSE_RATINGS', courseId: courseId });
        }
    },
    sendRatingThumb: (ratingId: string, thumbUp: boolean): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState) {
            fetch('api/ratings/thumb', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ratingId, studentId: "studento idas", thumbUp } as RatingThumb)
            }).then(res => res.json())
            .catch(error => console.error('Error:', error));
        }
        dispatch({type: 'SEND_RATING_THUMB', ratingId, thumbUp });
    },
    updateRating: (ratingId: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.courseRatings &&
            appState.courseRatings.isLoading === false) {
            fetch(`api/ratings/${ratingId}`)
                .then(response => response.json() as Promise<Rating>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_COURSE_RATING', rating: data });
                });
        }
        dispatch({type: 'REQUEST_COURSE_RATING', ratingId });
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: CourseRatingsState = { ratings: [], isLoading: false,  courseId: undefined };

export const reducer: Reducer<CourseRatingsState> = (state: CourseRatingsState | undefined, incomingAction: Action): CourseRatingsState => {
    if (state === undefined)
        return unloadedState;

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_COURSE_RATINGS':
            return {
                ratings: state.ratings,
                isLoading: true,
                courseId: action.courseId
            };
        case 'RECEIVE_COURSE_RATINGS':
            return {
                ratings: action.ratings,
                isLoading: false,
                courseId: state.courseId
            };
        case 'REQUEST_COURSE_RATING':
            return {
                ratings: state.ratings,
                isLoading: true,
                courseId: state.courseId
            };
        case 'RECEIVE_COURSE_RATING':
            let ratings = [...state.ratings];
            let ratingIndex = ratings.findIndex(r => r.id === action.rating.id);
            if (ratingIndex !== -1)
                ratings[ratingIndex] = action.rating;
            return {
                ratings: ratings,
                isLoading: false,
                courseId: state.courseId
            };
        case 'SEND_RATING_THUMB':
            ratings = [...state.ratings];
            ratingIndex = ratings.findIndex(r => r.id === action.ratingId);
            if (ratingIndex !== -1)
                action.thumbUp ? ratings[ratingIndex].thumbUps!++ : ratings[ratingIndex].thumbDowns!++;
            return {
                ratings: ratings,
                isLoading: state.isLoading,
                courseId: state.courseId
            };
    }

    return state;
};
