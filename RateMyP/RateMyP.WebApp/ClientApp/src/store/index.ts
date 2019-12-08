import * as Teachers from './Teachers';
import * as TeacherCourses from './Teacher/TeacherCourses';
import * as TeacherActivites from './Teacher/TeacherActivities';
import * as TeacherRatings from './Teacher/TeacherRatings';
import * as TeacherStatistics from './TeacherStatistics';
import * as Courses from './Courses';
import * as CourseTeachers from './Course/CourseTeachers';
import * as CourseTeacherActivities from './Course/CourseTeacherActivities';
import * as CourseStatistics from './CourseStatistics';
import * as CourseRatings from './Course/CourseRatings';
import * as Tags from './Tags';
import * as Student from './Student';
import * as Rate from './Rate/Rate';
import * as CustomStarUpload from './CustomStar/CustomStar';
import * as RatingReportCreation from './Reports/RatingReportCreation';
import * as RatingReports from './Reports/RatingReports';
import * as CustomStarReportCreation from './Reports/CustomStarReportCreation';
import * as CustomStarReports from './Reports/CustomStarReports';
import * as TeacherLeaderboard from './Leaderboard/TeacherLeaderboard';
import * as CourseLeaderboard from './Leaderboard/CourseLeaderboard';
import * as CustomStarShowcase from './CustomStar/CustomStarShowcase';
import * as Shop from './Shop/Shop';

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
    courseStatistics: CourseStatistics.CourseStatisticsState | undefined;
    courseLeaderboard: CourseLeaderboard.CourseLeaderboardState | undefined;
    courseRatings: CourseRatings.CourseRatingsState | undefined;

    tags: Tags.TagsState | undefined;
    student: Student.StudentState | undefined;
    rate: Rate.RateState | undefined; 
    ratingReportCreation: RatingReportCreation.RatingReportCreationState | undefined;
    ratingReports: RatingReports.RatingReportsState | undefined;

    customStarUpload: CustomStarUpload.CustomStarState | undefined;
    customStarShowcase: CustomStarShowcase.CustomStarShowcaseState | undefined;
    customStarReportCreation: CustomStarReportCreation.CustomStarReportCreationState | undefined;
    customStarReports: CustomStarReports.CustomStarReportsState | undefined;

    shop: Shop.ShopState | undefined;
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
    courseStatistics: CourseStatistics.reducer,
    courseLeaderboard: CourseLeaderboard.reducer,
    courseRatings: CourseRatings.reducer,

    tags: Tags.reducer,
    student: Student.reducer,
    rate: Rate.reducer,
    ratingReportCreation: RatingReportCreation.reducer,
    ratingReports: RatingReports.reducer,

    customStarUpload: CustomStarUpload.reducer,
    customStarShowcase: CustomStarShowcase.reducer,
    customStarReportCreation: CustomStarReportCreation.reducer,
    customStarReports: CustomStarReports.reducer,

    shop: Shop.reducer,
};

// This type can be used as a hint on action creators so that its 'dispatch' and 'getState' params are
// correctly typed to match your store.
export interface AppThunkAction<TAction> {
    (dispatch: (action: TAction) => void, getState: () => ApplicationState): void;
}
