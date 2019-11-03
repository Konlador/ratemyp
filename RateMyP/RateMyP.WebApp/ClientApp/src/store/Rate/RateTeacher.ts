import { Action, Reducer } from 'redux';
import { AppThunkAction } from '..';
import { Tag } from '../Tags';
import { Rating } from '../Ratings';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface RateTeacherState {
    allTags: Tag[];
    submitButtonClicked: boolean;
    rating: Rating
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe sometheState that is goeState to happen.
interface SetOverallMarkAction { 
    type: 'SET_OVERALL_MARK'; 
    value: number;
}

interface SetLevelOfDifficultyAction {
    type: 'SET_LEVEL_OF_DIFFICULTY'
    value: number;
}

interface SetWouldTakeTeacherAgainTrueAction {
    type: 'SET_WOULD_TAKE_TEACHER_AGAIN_TRUE'
}

interface SetWouldTakeTeacherAgainFalseAction {
    type: 'SET_WOULD_TAKE_TEACHER_AGAIN_FALSE'
}

interface ChangeCommentAction {
    type: 'CHANGE_COMMENT'
    value: string;
}

interface RequestTagsAction {
    type: 'REQUEST_TAGS'
}

interface ReceiveTagsAction {
    type: 'RECEIVE_TAGS'
    tags: Tag[];
}

interface ChangeTagsAction {
    type: 'CHANGE_TAGS'
    tags: Tag[];
}

interface SubmitReviewAction {
    type: 'SUBMIT_REVIEW'
}

interface SendRatingAction {
    type: 'SEND_RATING'
}

interface SetTeacherIdAction {
    type: 'SET_TEACHER_ID'
    value: string
}

interface SetCourseIdAction {
    type: 'SET_COURSE_ID'
    value: string
}
// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type streStates (and not any other arbitrary streState).
type KnownAction = SetOverallMarkAction | SetLevelOfDifficultyAction | SetWouldTakeTeacherAgainTrueAction | SetWouldTakeTeacherAgainFalseAction | ChangeCommentAction | ReceiveTagsAction | RequestTagsAction | ChangeTagsAction | SubmitReviewAction | SendRatingAction | SetTeacherIdAction | SetCourseIdAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loadeState data).

export const actionCreators = {
    setOverallMark: (value: number): AppThunkAction<KnownAction> => (dispatch) => { 
        dispatch({ type: 'SET_OVERALL_MARK', value: value });
    },
    setLevelOfDifficulty: (value: number): AppThunkAction<KnownAction> => (dispatch) => { 
        dispatch({ type: 'SET_LEVEL_OF_DIFFICULTY', value: value });
    },
    changeComment: (value: string): AppThunkAction<KnownAction> => (dispatch) => { 
        dispatch({ type: 'CHANGE_COMMENT', value: value });
    },
    setWouldTakeTeacherAgainTrue: () => ({ type: 'SET_WOULD_TAKE_TEACHER_AGAIN_TRUE' } as SetWouldTakeTeacherAgainTrueAction),
    setWouldTakeTeacherAgainFalse: () => ({ type: 'SET_WOULD_TAKE_TEACHER_AGAIN_FALSE' } as SetWouldTakeTeacherAgainFalseAction),    
    requestTags: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.rate &&
            appState.rate.allTags.length === 0) {
            fetch(`api/tags`)
                .then(response => response.json() as Promise<Tag[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_TAGS', tags: data });
                });

            dispatch({ type: 'REQUEST_TAGS' });
        }
    }, // nekurk naujos funkcijos kai jau yra tokia kuri atlieka ta pati darba
    changeTags: (value: Array<Tag>): AppThunkAction<KnownAction> => (dispatch) => { 
        dispatch({ type: 'CHANGE_TAGS', tags: value });
    },
    submitReview: () => ({ type: 'SUBMIT_REVIEW' } as SubmitReviewAction),
    sendRating: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const state = getState().rate;
        if(state !== undefined){
            fetch('api/ratings', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(state.rating)
            }).then(res => res.json()).catch(error => console.error('Error:', error)).then(response => console.log('Success:', response));
        }
        dispatch({type: 'SEND_RATING'});
    },
    setTeacherId: (value: string): AppThunkAction<KnownAction> => (dispatch) => { 
        dispatch({ type: 'SET_TEACHER_ID', value: value });
    },
    setCourseId: (value: string): AppThunkAction<KnownAction> => (dispatch) => { 
        dispatch({ type: 'SET_COURSE_ID', value: value });
    },
};
// this.setState((state: { rating: { overallMark: any; levelOfDifficulty: any; wouldTakeTeacherAgain: any; comment: any; tags: any; dateCreated: undefined; teacherId: any; courseId: any; Id: string; }; allTags: any; submitButtonClicked: any; })
// => (state.rating.overallMark = state.rating.overallMark,
//    state.rating.levelOfDifficulty = state.rating.levelOfDifficulty, 
//    state.rating.wouldTakeTeacherAgain = state.rating.wouldTakeTeacherAgain, 
//    state.rating.comment = state.rating.comment, 
//    state.rating.tags = state.rating.tags, 
//    state.allTags = state.allTags,
//    state.submitButtonClicked = state.submitButtonClicked,
//    state.rating.dateCreated = undefined,
//    state.rating.teacherId = state.rating.teacherId,
//    state.rating.courseId = state.rating.courseId,
//    state.rating.Id = '' ))
// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.
const unloadedRating: Rating = {id: '', overallMark: 0, levelOfDifficulty: 0, wouldTakeTeacherAgain: false, comment: '', tags: [], dateCreated: new Date(), teacherId: '', courseId: ''}
const unloadedState: RateTeacherState = {allTags: [], submitButtonClicked: false, rating: unloadedRating};

export const reducer: Reducer<RateTeacherState> = (state: RateTeacherState | undefined, incomeStateAction: Action): RateTeacherState => {
    if (state === undefined)
        return unloadedState;

    const action = incomeStateAction as KnownAction;
    switch (action.type) {
        case 'SET_OVERALL_MARK':
            return Object.assign({}, state, {
                rating: Object.assign({}, state.rating, {
                    overallMark: action.value,
                }),
                });
        case 'SET_LEVEL_OF_DIFFICULTY':
                return Object.assign({}, state, {
                    rating: Object.assign({}, state.rating, {
                        levelOfDifficulty: action.value, 
                    }),
                    });
        case 'SET_WOULD_TAKE_TEACHER_AGAIN_TRUE':
                return Object.assign({}, state, {
                    rating: Object.assign({}, state.rating, {
                        wouldTakeTeacherAgain: true, 
                    }),
                    });
        case 'SET_WOULD_TAKE_TEACHER_AGAIN_FALSE':
                return Object.assign({}, state, {
                    rating: Object.assign({}, state.rating, {
                        wouldTakeTeacherAgain: false, 
                    }),
                    });
        case 'CHANGE_COMMENT':
                return Object.assign({}, state, {
                    rating: Object.assign({}, state.rating, {
                        comment: action.value, 
                    }),
                    });
        case 'REQUEST_TAGS':
                return Object.assign({}, state, {
                    });
        case 'RECEIVE_TAGS':
                return Object.assign({}, state, {
                    allTags: action.tags,
                    });
        case 'CHANGE_TAGS':
                return Object.assign({}, state, {
                    rating: Object.assign({}, state.rating, {
                        tags: action.tags, 
                    }),
                    });
        case 'SUBMIT_REVIEW':
                return Object.assign({}, state, {
                    submitButtonClicked: true,
                    });
        case 'SET_TEACHER_ID':
                return Object.assign({}, state, {
                    rating: Object.assign({}, state.rating, {
                        teacherId: action.value,
                    }),
                    });
        case 'SEND_RATING':
                return Object.assign({}, state, {
                    });
        case 'SET_COURSE_ID':
                return Object.assign({}, state, {
                    rating: Object.assign({}, state.rating, {
                        courseId: action.value,
                    }),
                    });
        default:
                return state;
    }
};
