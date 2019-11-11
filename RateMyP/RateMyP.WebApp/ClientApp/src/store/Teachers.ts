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

interface RequestAllTeachersAction {
    type: 'REQUEST_ALL_TEACHERS';
}

interface ReceiveAllTeachersAction {
    type: 'RECEIVE_ALL_TEACHERS';
    teachers: Teacher[]
}

interface RequestSearchedTeachersAction {
    type: 'REQUEST_SEARCHED_TEACHERS'
}

interface ReceiveSearchedTeachersAction {
    type: 'RECEIVE_SEARCHED_TEACHERS';
    teachers: Teacher[];
}

interface CheckTeacherAvailabilityAction {
    type: 'CHECK_TEACHER_AVAILABILITY';
}

interface ClearSelectedTeacher {
    type: 'CLEAR_SELECTED_TEACHER'
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestTeachersAction | ReceiveTeachersAction | RequestTeacherAction | ReceiveTeacherAction | CheckTeacherAvailabilityAction | ClearSelectedTeacher |
                   RequestAllTeachersAction | ReceiveAllTeachersAction | RequestSearchedTeachersAction | ReceiveSearchedTeachersAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestTeachers: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState && appState.teachers && appState.teachers.isLoading === false) {
            fetch(`api/teachers/startIndex=${appState.teachers.currentIndex}`)
                .then(response => response.json() as Promise<Teacher[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_TEACHERS', teachers: data });
                    if (data.length === 0) dispatch({ type: 'CHECK_TEACHER_AVAILABILITY'})
                });
            dispatch({ type: 'REQUEST_TEACHERS' });
        }
    },
    requestTeacher: (teacherId: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
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
    requestAllTeachers: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState && appState.teachers && appState.teachers.isLoading === false) {
            fetch(`api/teachers/`)
                .then(response => response.json() as Promise<Teacher[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_ALL_TEACHERS', teachers: data});
                });
            dispatch({ type: 'REQUEST_ALL_TEACHERS' });
        }
    },
    searchTeacher: (searchString: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        if (appState && appState.teachers && appState.teachers.isLoading === false) {
            fetch(`api/teachers/search=${searchString}`)
                .then(response => response.json() as Promise<Teacher[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_SEARCHED_TEACHERS', teachers: data});
                });
            dispatch({ type: 'REQUEST_SEARCHED_TEACHERS'});
        }
    },
    clearSelectedTeacher: () => ({ type: 'CLEAR_SELECTED_TEACHER' } as ClearSelectedTeacher)
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
                currentIndex: state.currentIndex + action.teachers.length,
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
            };
        case 'CLEAR_SELECTED_TEACHER':
            return {
                teachers: state.teachers,
                selectedTeacher: undefined,
                isLoading: state.isLoading,
                currentIndex: state.currentIndex,
                canLoadMore: state.canLoadMore
            };
        case 'REQUEST_ALL_TEACHERS':
            return {
                teachers: state.teachers,
                selectedTeacher: state.selectedTeacher,
                isLoading: true,
                currentIndex: state.currentIndex,
                canLoadMore: state.canLoadMore
            };
        case 'RECEIVE_ALL_TEACHERS':
            return {
                teachers: [...state.teachers, ...action.teachers],
                selectedTeacher: state.selectedTeacher,
                isLoading: false,
                currentIndex: state.currentIndex + action.teachers.length,
                canLoadMore: state.canLoadMore
            };
        case 'REQUEST_SEARCHED_TEACHERS':
                return {
                    teachers: state.teachers,
                    selectedTeacher: state.selectedTeacher,
                    isLoading: true,
                    currentIndex: state.currentIndex,
                    canLoadMore: state.canLoadMore
                };
        case 'RECEIVE_SEARCHED_TEACHERS':
                return {
                    teachers: [...state.teachers, ...action.teachers],
                    selectedTeacher: state.selectedTeacher,
                    isLoading: false,
                    currentIndex: state.currentIndex,
                    canLoadMore: state.canLoadMore
                };
    }

    return state;
};
