import { Action, Reducer } from 'redux';
import { AppThunkAction } from '..';
import { TeacherActivity } from './RateTeacherActivities';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface RateState {
    overallMark: number;
    levelOfDifficulty: number;
    wouldTakeTeacherAgain: boolean;
    comment: string;
    tags: Tag[];
    allTags: Tag[];
    submitButtonClicked: boolean;
    teacherId: string;
    courseId: string;
}

export interface Tag {
    id: string,
    text: string
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

interface ReveiveTagsAction {
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
type KnownAction = SetOverallMarkAction | SetLevelOfDifficultyAction | SetWouldTakeTeacherAgainTrueAction | SetWouldTakeTeacherAgainFalseAction | ChangeCommentAction | ReveiveTagsAction | RequestTagsAction | ChangeTagsAction | SubmitReviewAction | SendRatingAction | SetTeacherIdAction | SetCourseIdAction;

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
        // Only load data if it's something we don't already have (and are not already loading)
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
    },
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
                body: JSON.stringify({
                    Comment: state.comment,
                    CourseId: state.courseId,
                    LevelOfDifficulty: state.levelOfDifficulty,
                    OverallMark: state.overallMark,
                    Tags: state.tags,
                    TeacherId: state.teacherId,
                    WouldTakeTeacherAgain: state.wouldTakeTeacherAgain
                })
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

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.
const unloadedState: RateState = { overallMark: 0, levelOfDifficulty: 0, wouldTakeTeacherAgain: true, comment: '', tags: [], allTags: [], submitButtonClicked: false, teacherId: '', courseId: ''};

export const reducer: Reducer<RateState> = (state: RateState | undefined, incomeStateAction: Action): RateState => {
    if (state === undefined)
        return unloadedState;

    const action = incomeStateAction as KnownAction;
    switch (action.type) {
        case 'SET_OVERALL_MARK':
            return { overallMark: action.value,
                     levelOfDifficulty: state.levelOfDifficulty,
                     wouldTakeTeacherAgain: state.wouldTakeTeacherAgain,
                     comment: state.comment,
                     tags: state.tags,
                     allTags: state.tags, 
                     submitButtonClicked: state.submitButtonClicked,
                     teacherId: state.teacherId,
                     courseId: state.courseId
                    };
        case 'SET_LEVEL_OF_DIFFICULTY':
            return { overallMark: state.overallMark,
                     levelOfDifficulty: action.value,
                     wouldTakeTeacherAgain: state.wouldTakeTeacherAgain,
                     comment: state.comment,
                     tags: state.tags,
                     allTags: state.tags, 
                     submitButtonClicked: state.submitButtonClicked,
                     teacherId: state.teacherId,
                     courseId: state.courseId
                    };
        case 'SET_WOULD_TAKE_TEACHER_AGAIN_TRUE':
            return { overallMark: state.overallMark,
                     levelOfDifficulty: state.levelOfDifficulty,
                     wouldTakeTeacherAgain: true,
                     comment: state.comment,
                     tags: state.tags,
                     allTags: state.tags, 
                     submitButtonClicked: state.submitButtonClicked,
                     teacherId: state.teacherId,
                     courseId: state.courseId
                    };
        case 'SET_WOULD_TAKE_TEACHER_AGAIN_FALSE':
            return { overallMark: state.overallMark,
                     levelOfDifficulty: state.levelOfDifficulty,
                     wouldTakeTeacherAgain: false,
                     comment: state.comment,
                     tags: state.tags,
                     allTags: state.tags, 
                     submitButtonClicked: state.submitButtonClicked,
                     teacherId: state.teacherId,
                     courseId: state.courseId
                    };
        case 'CHANGE_COMMENT':
            return { overallMark: state.overallMark,
                     levelOfDifficulty: state.levelOfDifficulty,
                     wouldTakeTeacherAgain: state.wouldTakeTeacherAgain,
                     comment: action.value,
                     tags: state.tags,
                     allTags: state.tags, 
                     submitButtonClicked: state.submitButtonClicked,
                     teacherId: state.teacherId,
                     courseId: state.courseId
                    };
        case 'REQUEST_TAGS':
            return { overallMark: state.overallMark,
                     levelOfDifficulty: state.levelOfDifficulty,
                     wouldTakeTeacherAgain: state.wouldTakeTeacherAgain,
                     comment: state.comment,
                     tags: state.tags,
                     allTags: state.tags, 
                     submitButtonClicked: state.submitButtonClicked,
                     teacherId: state.teacherId,
                     courseId: state.courseId
            };
        case 'RECEIVE_TAGS':
            return { overallMark: state.overallMark,
                     levelOfDifficulty: state.levelOfDifficulty,
                     wouldTakeTeacherAgain: state.wouldTakeTeacherAgain,
                     comment: state.comment,
                     tags: state.tags,
                     allTags: action.tags, 
                     submitButtonClicked: state.submitButtonClicked,
                     teacherId: state.teacherId,
                     courseId: state.courseId
            };
        case 'CHANGE_TAGS':
            return { overallMark: state.overallMark,
                     levelOfDifficulty: state.levelOfDifficulty,
                     wouldTakeTeacherAgain: state.wouldTakeTeacherAgain,
                     comment: state.comment,
                     tags: action.tags,
                     allTags: state.allTags, 
                     submitButtonClicked: state.submitButtonClicked,
                     teacherId: state.teacherId,
                     courseId: state.courseId
            };
        case 'SUBMIT_REVIEW':
            return { overallMark: state.overallMark,
                     levelOfDifficulty: state.levelOfDifficulty,
                     wouldTakeTeacherAgain: state.wouldTakeTeacherAgain,
                     comment: state.comment,
                     tags: state.tags,
                     allTags: state.allTags, 
                     submitButtonClicked: true,
                     teacherId: state.teacherId,
                     courseId: state.courseId
            };
            case 'SET_TEACHER_ID':
                return { overallMark: state.overallMark,
                         levelOfDifficulty: state.levelOfDifficulty,
                         wouldTakeTeacherAgain: state.wouldTakeTeacherAgain,
                         comment: state.comment,
                         tags: state.tags,
                         allTags: state.allTags, 
                         submitButtonClicked: state.submitButtonClicked,
                         teacherId: action.value,
                         courseId: state.courseId
                };  
            case 'SEND_RATING':
                return { overallMark: state.overallMark,
                         levelOfDifficulty: state.levelOfDifficulty,
                         wouldTakeTeacherAgain: state.wouldTakeTeacherAgain,
                         comment: state.comment,
                         tags: state.tags,
                         allTags: state.allTags, 
                         submitButtonClicked: state.submitButtonClicked,
                         teacherId: state.teacherId,
                         courseId: state.courseId
                };
            case 'SET_COURSE_ID':
                return { overallMark: state.overallMark,
                         levelOfDifficulty: state.levelOfDifficulty,
                         wouldTakeTeacherAgain: state.wouldTakeTeacherAgain,
                         comment: state.comment,
                         tags: state.tags,
                         allTags: state.allTags, 
                         submitButtonClicked: state.submitButtonClicked,
                         teacherId: state.teacherId,
                         courseId: action.value
                };  
        default:
                return state;
    }
};
