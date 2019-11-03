import { Action, Reducer } from 'redux';
import { AppThunkAction } from '.';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface TeachersState {
    teachers: Teacher[];
    selectedTeacher: Teacher | undefined;
    isLoading: boolean;
    currentIndex: number;
    canLoadMore: boolean;
}

export interface Teacher {
    id: string,
    firstName: string;
    lastName: string;
    rank: string;
    faculty: string;
    description: string;
    activities: TeacherActivity[];
}

export interface TeacherActivity {
    id: string,
    teacherId: string;
    courseId: string;
    dateStarted: string;
    lectureType: string;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestTeachersAction {
    type: 'REQUEST_TEACHERS';
}

interface ReceiveTeachersAction {
    type: 'RECEIVE_TEACHERS';
    teachers: Teacher[];
}

interface RequestTeacherAction {
    type: 'REQUEST_TEACHER';
}

interface ReceiveTeacherAction {
    type: 'RECEIVE_TEACHER';
    selectedTeacher: Teacher;
}

interface CheckTeacherAvailabilityAction {
    type: 'CHECK_TEACHER_AVAILABILITY';
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestTeachersAction | ReceiveTeachersAction | RequestTeacherAction | ReceiveTeacherAction | CheckTeacherAvailabilityAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestTeachers: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.teachers && appState.teachers.isLoading === false) {
            fetch(`api/teachers/startIndex=${appState.teachers.currentIndex}`)
                .then(response => response.json() as Promise<Teacher[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_TEACHERS', teachers: data });
                    if (data.length < 20) dispatch({ type: 'CHECK_TEACHER_AVAILABILITY'})
                });
            dispatch({ type: 'REQUEST_TEACHERS' });
        }
    },
    requestTeacher: (teacherId: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.teachers &&
            appState.teachers.isLoading === false &&
            (appState.teachers.selectedTeacher === undefined ||
            appState.teachers.selectedTeacher.id !== teacherId)) {
            fetch(`api/teachers/${teacherId}`)
                .then(response => response.json() as Promise<Teacher>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_TEACHER', selectedTeacher: data });
                });

            dispatch({ type: 'REQUEST_TEACHER' });
        }
    },
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: TeachersState = { teachers: [], selectedTeacher: undefined, isLoading: false, currentIndex: 0, canLoadMore: true };

export const reducer: Reducer<TeachersState> = (state: TeachersState | undefined, incomingAction: Action): TeachersState => {
    if (state === undefined)
        return unloadedState;

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_TEACHERS':
            return {
                teachers: state.teachers,
                selectedTeacher: state.selectedTeacher,
                isLoading: true,
                currentIndex: state.currentIndex,
                canLoadMore: state.canLoadMore
            };
        case 'RECEIVE_TEACHERS':
            return {
                teachers: [...state.teachers, ...action.teachers],
                selectedTeacher: state.selectedTeacher,
                isLoading: false,
                currentIndex: state.currentIndex + 20,
                canLoadMore: state.canLoadMore
            };
        case 'REQUEST_TEACHER':
            return {
                teachers: state.teachers,
                selectedTeacher: state.selectedTeacher,
                isLoading: true,
                currentIndex: state.currentIndex,
                canLoadMore: state.canLoadMore
            };
        case 'RECEIVE_TEACHER':
            return {
                teachers: state.teachers,
                selectedTeacher: action.selectedTeacher,
                isLoading: false,
                currentIndex: state.currentIndex,
                canLoadMore: state.canLoadMore
            };
        case 'CHECK_TEACHER_AVAILABILITY':
            return {
                teachers: state.teachers,
                selectedTeacher: state.selectedTeacher,
                isLoading: false,
                currentIndex: state.currentIndex,
                canLoadMore: false
            }
    }

    return state;
};
