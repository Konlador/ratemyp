import { Action, Reducer } from 'redux';
import { AppThunkAction } from '..';
import { TeacherActivity } from '../Teacher/TeacherActivities';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface CourseTeacherActivitiesState {
    isLoading: boolean;
    teacherActivites: TeacherActivity[];
    courseId: string | undefined;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestCourseTeacherActivitiesAction {
    type: 'REQUEST_COURSE_TEACHER_ACTIVITIES';
    courseId: string;
}

interface ReceiveCourseTeacherActivitiesAction {
    type: 'RECEIVE_COURSE_TEACHER_ACTIVITIES';
    teacherActivites: TeacherActivity[];
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestCourseTeacherActivitiesAction | ReceiveCourseTeacherActivitiesAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestCourseTeacherActivities: (courseId: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState &&
            appState.courseTeacherActivities &&
            appState.courseTeacherActivities.isLoading === false &&
            appState.courseTeacherActivities.courseId !== courseId) {
            fetch(`api/teacheractivities/course=${courseId}`)
                .then(response => response.json() as Promise<TeacherActivity[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_COURSE_TEACHER_ACTIVITIES', teacherActivites: data });
                });

            dispatch({ type: 'REQUEST_COURSE_TEACHER_ACTIVITIES', courseId });
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: CourseTeacherActivitiesState = { courseId: undefined, teacherActivites: [], isLoading: false };

export const reducer: Reducer<CourseTeacherActivitiesState> = (state: CourseTeacherActivitiesState | undefined, incomingAction: Action): CourseTeacherActivitiesState => {
    if (state === undefined)
        return unloadedState;

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_COURSE_TEACHER_ACTIVITIES':
            return {
                courseId: action.courseId,
                teacherActivites: state.teacherActivites,
                isLoading: true
            };
        case 'RECEIVE_COURSE_TEACHER_ACTIVITIES':
            return {
                courseId: state.courseId,
                teacherActivites: action.teacherActivites,
                isLoading: false
            };
    }

    return state;
};
