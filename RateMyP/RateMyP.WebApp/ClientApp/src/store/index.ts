import * as Teachers from './Teachers';
import * as TeacherBadges from './Teacher/TeacherBadges'
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
import * as Badges from './Badges';
import * as Student from './Student';
import * as Rate from './Rate/Rate';
import * as CustomStarUpload from './CustomStar/CustomStar';
import * as RatingReport from './Rate/RatingReport';
import * as CustomStarReport from './CustomStar/CustomStarReport'
import * as TeacherLeaderboard from './Leaderboard/TeacherLeaderboard';
import * as CourseLeaderboard from './Leaderboard/CourseLeaderboard';
import * as CustomStarShowcase from './CustomStar/CustomStarShowcase';

// The top-level state object
export interface ApplicationState {
    teachers: Teachers.TeachersState | undefined;
    teacherBadges: TeacherBadges.TeacherBadgesState | undefined;
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

    badges: Badges.BadgesState | undefined;
    tags: Tags.TagsState | undefined;
    student: Student.StudentState | undefined;
    rate: Rate.RateState | undefined; 
    ratingReport: RatingReport.RatingReportState | undefined;

    customStarUpload: CustomStarUpload.CustomStarState | undefined;
    customStarShowcase: CustomStarShowcase.CustomStarShowcaseState | undefined;
    customStarReport: CustomStarReport.CustomStarReportState | undefined;
}

// Whenever an action is dispatched, Redux will update each top-level application state property using
// the reducer with the matching name. It's important that the names match exactly, and that the reducer
// acts on the corresponding ApplicationState property type.
export const reducers = {
    teachers: Teachers.reducer,
    teacherBadges: TeacherBadges.reducer,
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

    badges: Badges.reducer,
    tags: Tags.reducer,
    student: Student.reducer,
    rate: Rate.reducer,
    ratingReport: RatingReport.reducer,

    customStarUpload: CustomStarUpload.reducer,
    customStarShowcase: CustomStarShowcase.reducer,
    customStarReport: CustomStarReport.reducer,
};

// This type can be used as a hint on action creators so that its 'dispatch' and 'getState' params are
// correctly typed to match your store.
export interface AppThunkAction<TAction> {
    (dispatch: (action: TAction) => void, getState: () => ApplicationState): void;
}
