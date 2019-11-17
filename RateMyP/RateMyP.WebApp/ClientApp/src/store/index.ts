import * as Teachers from './Teachers';
import * as TeacherCourses from './Teacher/TeacherCourses';
import * as TeacherActivites from './Teacher/TeacherActivities';
import * as TeacherRatings from './Teacher/TeacherRatings';
import * as TeacherStatistics from './TeacherStatistics';
import * as Courses from './Courses';
import * as CourseTeachers from './Course/CourseTeachers';
import * as CourseTeacherActivities from './Course/CourseTeacherActivities';
import * as Tags from './Tags';
import * as Student from './Student';
import * as Rate from './Rate/Rate';
import * as RatingReport from './Rate/RatingReport';
import * as TeacherLeaderboard from './Leaderboard/TeacherLeaderboard';
import * as CourseLeaderboard from './Leaderboard/CourseLeaderboard';

// The top-level state object
export interface ApplicationState {
    teachers: Teachers.TeachersState | undefined;
    teacherCourses: TeacherCourses.TeacherCoursesState | undefined;
    teacherActivites: TeacherActivites.TeacherActivitesState | undefined;
    teacherRatings: TeacherRatings.TeacherRatingsState | undefined;
    teacherStatistics: TeacherStatistics.TeacherStatisticsState | undefined;
    teacherLeaderboard: TeacherLeaderboard.TeacherLeaderboardState | undefined;

    courses: Courses.CoursesState | undefined;
    courseTeachers: CourseTeachers.CourseTeachersState | undefined;
    courseTeacherActivities: CourseTeacherActivities.CourseTeacherActivitiesState | undefined;
    courseLeaderboard: CourseLeaderboard.CourseLeaderboardState | undefined;

    tags: Tags.TagsState | undefined;
    student: Student.StudentState | undefined;
    rate: Rate.RateState | undefined; 
    ratingReport: RatingReport.RatingReportState | undefined;
}

// Whenever an action is dispatched, Redux will update each top-level application state property using
// the reducer with the matching name. It's important that the names match exactly, and that the reducer
// acts on the corresponding ApplicationState property type.
export const reducers = {
    teachers: Teachers.reducer,
    teacherCourses: TeacherCourses.reducer,
    teacherActivites: TeacherActivites.reducer,
    teacherRatings: TeacherRatings.reducer,
    teacherStatistics: TeacherStatistics.reducer,
    teacherLeaderboard: TeacherLeaderboard.reducer,

    courses: Courses.reducer,
    courseTeachers: CourseTeachers.reducer,
    courseTeacherActivities: CourseTeacherActivities.reducer,
    courseLeaderboard: CourseLeaderboard.reducer,

    tags: Tags.reducer,
    student: Student.reducer,
    rate: Rate.reducer,
    ratingReport: RatingReport.reducer,
};

// This type can be used as a hint on action creators so that its 'dispatch' and 'getState' params are
// correctly typed to match your store.
export interface AppThunkAction<TAction> {
    (dispatch: (action: TAction) => void, getState: () => ApplicationState): void;
}
