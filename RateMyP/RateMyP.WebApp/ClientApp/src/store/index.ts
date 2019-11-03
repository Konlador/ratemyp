import * as Teachers from './Teachers';
import * as TeacherCourses from './Teacher/TeacherCourses';
import * as TeacherActivites from './Teacher/TeacherActivities';
import * as TeacherRatings from './Teacher/TeacherRatings';
import * as Courses from './Courses';
import * as CourseTeachers from './Course/CourseTeachers';
import * as CourseTeacherActivities from './Course/CourseTeacherActivities';
import * as Ratings from './Ratings';
import * as Tags from './Tags';
import * as Rate from './Rate/Rate';
import * as RateTeacherActivites from './Rate/RateTeacherActivities'
import * as RateCourses from './Rate/RateCourses'

// The top-level state object
export interface ApplicationState {
    teachers: Teachers.TeachersState | undefined;
    teacherCourses: TeacherCourses.TeacherCoursesState | undefined;
    teacherActivites: TeacherActivites.TeacherActivitesState | undefined;
    teacherRatings: TeacherRatings.TeacherRatingsState | undefined;

    courses: Courses.CoursesState | undefined;
    courseTeachers: CourseTeachers.CourseTeachersState | undefined;
    courseTeacherActivities: CourseTeacherActivities.CourseTeacherActivitiesState | undefined;

    ratings: Ratings.RatingsState | undefined;
    tags: Tags.TagsState | undefined;
    rate: Rate.RateState | undefined;
    rateTeacherActivites: RateTeacherActivites.RateTeacherActivitesState | undefined;
    rateCourses: RateCourses.RateCoursesState | undefined;
}

// Whenever an action is dispatched, Redux will update each top-level application state property using
// the reducer with the matching name. It's important that the names match exactly, and that the reducer
// acts on the corresponding ApplicationState property type.
export const reducers = {
    teachers: Teachers.reducer,
    teacherCourses: TeacherCourses.reducer,
    teacherActivites: TeacherActivites.reducer,
    teacherRatings: TeacherRatings.reducer,

    courses: Courses.reducer,
    courseTeachers: CourseTeachers.reducer,
    courseTeacherActivities: CourseTeacherActivities.reducer,

    ratings: Ratings.reducer,
    tags: Tags.reducer,
    rateTeacherActivites: RateTeacherActivites.reducer,
    rate: Rate.reducer,
    rateCourses: RateCourses.reducer
};

// This type can be used as a hint on action creators so that its 'dispatch' and 'getState' params are
// correctly typed to match your store.
export interface AppThunkAction<TAction> {
    (dispatch: (action: TAction) => void, getState: () => ApplicationState): void;
}
