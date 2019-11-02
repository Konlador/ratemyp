import { Action, Reducer } from 'redux';
import { AppThunkAction } from '..';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface RateTeacherActivitesState {
    teacherId: string | undefined;
    teacherActivites: TeacherActivity[];
    isLoading: boolean;
    selectedTeacherActivity: TeacherActivity | undefined;
}

export interface TeacherActivity {
    id: string,
    teacherId: string;
    courseId: string;
    dateStarted: Date;
    lectureType: number;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestTeacherActivitiesAction {
    type: 'REQUEST_TEACHERACTIVITIES';
    teacherId: string;
}

interface ReceiveTeacherActivitiesAction {
    type: 'RECEIVE_TEACHERACTIVITIES';
    teacherActivites: TeacherActivity[];
}

interface SetSelectedTeacherActivityAction {
    type: 'SET_SELECTED_TEACHER_ACTIVITY';
    teacherActivity: TeacherActivity;
}
// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestTeacherActivitiesAction | ReceiveTeacherActivitiesAction | SetSelectedTeacherActivityAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestTeacherActivities: (teacherId: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState &&
            appState.teacherActivites &&
            appState.teacherActivites.isLoading === false &&
            appState.teacherActivites.teacherId !== teacherId) {
            fetch(`api/teacheractivities/teacher=${teacherId}`)
                .then(response => response.json() as Promise<TeacherActivity[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_TEACHERACTIVITIES', teacherActivites: data });
                });

            dispatch({ type: 'REQUEST_TEACHERACTIVITIES', teacherId });
        }
    },
    setSelectedTeacherActivity: (value: TeacherActivity): AppThunkAction<KnownAction> => (dispatch) => { 
        dispatch({ type: 'SET_SELECTED_TEACHER_ACTIVITY', teacherActivity: value });
    },
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: RateTeacherActivitesState = { teacherId: undefined, teacherActivites: [], isLoading: false, selectedTeacherActivity: undefined };

export const reducer: Reducer<RateTeacherActivitesState> = (state: RateTeacherActivitesState | undefined, incomingAction: Action): RateTeacherActivitesState => {
    if (state === undefined)
        return unloadedState;

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_TEACHERACTIVITIES':
            return {
                teacherId: action.teacherId,
                teacherActivites: state.teacherActivites,
                isLoading: true,
                selectedTeacherActivity: state.selectedTeacherActivity
            };
        case 'RECEIVE_TEACHERACTIVITIES':
            return {
                teacherId: state.teacherId,
                teacherActivites: action.teacherActivites,
                isLoading: false,
                selectedTeacherActivity: state.selectedTeacherActivity
            };
        case 'SET_SELECTED_TEACHER_ACTIVITY':
            return {
                teacherId: state.teacherId,
                teacherActivites: state.teacherActivites,
                isLoading: state.isLoading,
                selectedTeacherActivity: action.teacherActivity
            };
    }

    return state;
};
