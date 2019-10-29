import * as WeatherForecasts from './WeatherForecasts';
import * as Counter from './Counter';
import * as Teachers from './Teachers';
import * as Ratings from './Ratings';
import * as Tags from './Tags';
import * as Courses from './Courses';
import * as TeacherActivites from './TeacherActivities';

// The top-level state object
export interface ApplicationState {
    counter: Counter.CounterState | undefined;
    weatherForecasts: WeatherForecasts.WeatherForecastsState | undefined;
    teachers: Teachers.TeachersState | undefined;
    ratings: Ratings.RatingsState | undefined;
    tags: Tags.TagsState | undefined;
    courses: Courses.CoursesState | undefined;
    teacherActivites: TeacherActivites.TeacherActivitesState | undefined;
}

// Whenever an action is dispatched, Redux will update each top-level application state property using
// the reducer with the matching name. It's important that the names match exactly, and that the reducer
// acts on the corresponding ApplicationState property type.
export const reducers = {
    counter: Counter.reducer,
    weatherForecasts: WeatherForecasts.reducer,
    teachers: Teachers.reducer,
    ratings: Ratings.reducer,
    tags: Tags.reducer,
    courses: Courses.reducer,
    teacherActivites: TeacherActivites.reducer
};

// This type can be used as a hint on action creators so that its 'dispatch' and 'getState' params are
// correctly typed to match your store.
export interface AppThunkAction<TAction> {
    (dispatch: (action: TAction) => void, getState: () => ApplicationState): void;
}