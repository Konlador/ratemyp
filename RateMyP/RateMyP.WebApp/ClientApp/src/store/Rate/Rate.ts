import { Action, Reducer } from 'redux';
import { AppThunkAction } from '..';
import { TeacherActivity } from '../TeacherActivities';
import { statement } from '@babel/template';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface RateState {
    OverallMark: number;
    LevelOfDifficulty: number;
    WouldTakeTeacherAgain: boolean;
    Comment: string;
    Tags: Tag[];
    AllTags: Tag[];
    SubmitButtonClicked: boolean;
    TeacherActivity: TeacherActivity | undefined;
    TeacherId: string;
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

interface SetTeacherActivityAction {
    type: 'SET_TEACHER_ACTIVITY'
    value: TeacherActivity | undefined
}

interface SetTeacherIdAction {
    type: 'SET_TEACHER_ID'
    value: string
}
// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type streStates (and not any other arbitrary streState).
type KnownAction = SetOverallMarkAction | SetLevelOfDifficultyAction | SetWouldTakeTeacherAgainTrueAction | SetWouldTakeTeacherAgainFalseAction | ChangeCommentAction | ReveiveTagsAction | RequestTagsAction | ChangeTagsAction | SubmitReviewAction | SendRatingAction | SetTeacherActivityAction | SetTeacherIdAction;

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
            appState.rate.AllTags.length === 0) {
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
    setTeacherActivity: (value: TeacherActivity | undefined): AppThunkAction<KnownAction> => (dispatch) => { 
        dispatch({ type: 'SET_TEACHER_ACTIVITY', value: value });
    },
    sendRating: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const state = getState().rate;
        if(state != undefined && state.TeacherActivity != undefined){
            fetch('api/ratings', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    Comment: state.Comment,
                    CourseId: state.TeacherActivity.courseId,
                    LevelOfDifficulty: state.LevelOfDifficulty,
                    OverallMark: state.OverallMark,
                    Tags: state.Tags,
                    TeacherId: state.TeacherId,
                    WouldTakeTeacherAgain: state.WouldTakeTeacherAgain
                })
            }).then(res => res.json()).catch(error => console.error('Error:', error)).then(response => console.log('Success:', response));
            // console.log(JSON.stringify({
            //     headers: {
            //         'Accept': 'application/json',
            //         'Content-Type': 'application/json',
            //     },}))
        }
        dispatch({type: 'SEND_RATING'});
    },
    setTeacherId: (value: string): AppThunkAction<KnownAction> => (dispatch) => { 
        dispatch({ type: 'SET_TEACHER_ID', value: value });
    },
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.
const unloadedState: RateState = { OverallMark: 0, LevelOfDifficulty: 0, WouldTakeTeacherAgain: true, Comment: '', Tags: [], AllTags: [], SubmitButtonClicked: false, TeacherActivity: undefined, TeacherId: ''};

export const reducer: Reducer<RateState> = (state: RateState | undefined, incomeStateAction: Action): RateState => {
    if (state === undefined)
        return unloadedState;

    const action = incomeStateAction as KnownAction;
    switch (action.type) {
        case 'SET_OVERALL_MARK':
            return { OverallMark: action.value,
                     LevelOfDifficulty: state.LevelOfDifficulty,
                     WouldTakeTeacherAgain: state.WouldTakeTeacherAgain,
                     Comment: state.Comment,
                     Tags: state.Tags,
                     AllTags: state.Tags, 
                     SubmitButtonClicked: state.SubmitButtonClicked,
                     TeacherActivity: state.TeacherActivity,
                     TeacherId: state.TeacherId
                    };
        case 'SET_LEVEL_OF_DIFFICULTY':
            return { OverallMark: state.OverallMark,
                     LevelOfDifficulty: action.value,
                     WouldTakeTeacherAgain: state.WouldTakeTeacherAgain,
                     Comment: state.Comment,
                     Tags: state.Tags,
                     AllTags: state.Tags, 
                     SubmitButtonClicked: state.SubmitButtonClicked,
                     TeacherActivity: state.TeacherActivity,
                     TeacherId: state.TeacherId
                    };
        case 'SET_WOULD_TAKE_TEACHER_AGAIN_TRUE':
            return { OverallMark: state.OverallMark,
                     LevelOfDifficulty: state.LevelOfDifficulty,
                     WouldTakeTeacherAgain: true,
                     Comment: state.Comment,
                     Tags: state.Tags,
                     AllTags: state.Tags, 
                     SubmitButtonClicked: state.SubmitButtonClicked,
                     TeacherActivity: state.TeacherActivity,
                     TeacherId: state.TeacherId
                    };
        case 'SET_WOULD_TAKE_TEACHER_AGAIN_FALSE':
            return { OverallMark: state.OverallMark,
                     LevelOfDifficulty: state.LevelOfDifficulty,
                     WouldTakeTeacherAgain: false,
                     Comment: state.Comment,
                     Tags: state.Tags,
                     AllTags: state.Tags, 
                     SubmitButtonClicked: state.SubmitButtonClicked,
                     TeacherActivity: state.TeacherActivity,
                     TeacherId: state.TeacherId
                    };
        case 'CHANGE_COMMENT':
            return { OverallMark: state.OverallMark,
                     LevelOfDifficulty: state.LevelOfDifficulty,
                     WouldTakeTeacherAgain: state.WouldTakeTeacherAgain,
                     Comment: action.value,
                     Tags: state.Tags,
                     AllTags: state.Tags, 
                     SubmitButtonClicked: state.SubmitButtonClicked,
                     TeacherActivity: state.TeacherActivity,
                     TeacherId: state.TeacherId
                    };
        case 'REQUEST_TAGS':
            return { OverallMark: state.OverallMark,
                     LevelOfDifficulty: state.LevelOfDifficulty,
                     WouldTakeTeacherAgain: state.WouldTakeTeacherAgain,
                     Comment: state.Comment,
                     Tags: state.Tags,
                     AllTags: state.Tags, 
                     SubmitButtonClicked: state.SubmitButtonClicked,
                     TeacherActivity: state.TeacherActivity,
                     TeacherId: state.TeacherId
            };
        case 'RECEIVE_TAGS':
            return { OverallMark: state.OverallMark,
                     LevelOfDifficulty: state.LevelOfDifficulty,
                     WouldTakeTeacherAgain: state.WouldTakeTeacherAgain,
                     Comment: state.Comment,
                     Tags: state.Tags,
                     AllTags: action.tags, 
                     SubmitButtonClicked: state.SubmitButtonClicked,
                     TeacherActivity: state.TeacherActivity,
                     TeacherId: state.TeacherId
            };
        case 'CHANGE_TAGS':
            return { OverallMark: state.OverallMark,
                     LevelOfDifficulty: state.LevelOfDifficulty,
                     WouldTakeTeacherAgain: state.WouldTakeTeacherAgain,
                     Comment: state.Comment,
                     Tags: action.tags,
                     AllTags: state.AllTags, 
                     SubmitButtonClicked: state.SubmitButtonClicked,
                     TeacherActivity: state.TeacherActivity,
                     TeacherId: state.TeacherId
            };
        case 'SUBMIT_REVIEW':
            return { OverallMark: state.OverallMark,
                     LevelOfDifficulty: state.LevelOfDifficulty,
                     WouldTakeTeacherAgain: state.WouldTakeTeacherAgain,
                     Comment: state.Comment,
                     Tags: state.Tags,
                     AllTags: state.AllTags, 
                     SubmitButtonClicked: true,
                     TeacherActivity: state.TeacherActivity,
                     TeacherId: state.TeacherId
            };
        case 'SET_TEACHER_ACTIVITY':
            return { OverallMark: state.OverallMark,
                     LevelOfDifficulty: state.LevelOfDifficulty,
                     WouldTakeTeacherAgain: state.WouldTakeTeacherAgain,
                     Comment: state.Comment,
                     Tags: state.Tags,
                     AllTags: state.AllTags, 
                     SubmitButtonClicked: state.SubmitButtonClicked,
                     TeacherActivity: action.value,
                     TeacherId: state.TeacherId
            };   
            case 'SET_TEACHER_ID':
                return { OverallMark: state.OverallMark,
                         LevelOfDifficulty: state.LevelOfDifficulty,
                         WouldTakeTeacherAgain: state.WouldTakeTeacherAgain,
                         Comment: state.Comment,
                         Tags: state.Tags,
                         AllTags: state.AllTags, 
                         SubmitButtonClicked: state.SubmitButtonClicked,
                         TeacherActivity: state.TeacherActivity,
                         TeacherId: action.value
                };  
            case 'SEND_RATING':
                return { OverallMark: state.OverallMark,
                         LevelOfDifficulty: state.LevelOfDifficulty,
                         WouldTakeTeacherAgain: state.WouldTakeTeacherAgain,
                         Comment: state.Comment,
                         Tags: state.Tags,
                         AllTags: state.AllTags, 
                         SubmitButtonClicked: state.SubmitButtonClicked,
                         TeacherActivity: state.TeacherActivity,
                         TeacherId: state.TeacherId
                }    
        default:
                return state;
    }
};
