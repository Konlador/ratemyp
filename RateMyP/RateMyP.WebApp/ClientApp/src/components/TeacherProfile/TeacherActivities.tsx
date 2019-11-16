import * as React from 'react';
import { connect } from 'react-redux';
import { Table, Spinner } from 'reactstrap';
import { RouteComponentProps, withRouter } from 'react-router';
import { ApplicationState } from '../../store';
import { LectureType } from '../../store/Teacher/TeacherActivities';
import * as TeacherActivitiesStore from '../../store/Teacher/TeacherActivities';
import * as TeacherCoursesStore from '../../store/Teacher/TeacherCourses';
import * as CoursesStore from '../../store/Courses';

interface OwnProps {
    teacherId: string
};

type Props =
    {
    activities: TeacherActivitiesStore.TeacherActivitesState,
    courses: TeacherCoursesStore.TeacherCoursesState
    } &
    typeof TeacherActivitiesStore.actionCreators &
    typeof TeacherCoursesStore.actionCreators &
    RouteComponentProps<{}>;

class TeacherActivities extends React.PureComponent<Props & OwnProps> {
    public componentDidMount() {
        this.ensureDataFetched();
    }

    public componentDidUpdate() {
        this.ensureDataFetched();
    }

    public render() {
        return (
            <React.Fragment>
                {this.renderTeacherActivites()}
            </React.Fragment>
        );
    }

    private ensureDataFetched() {
        this.props.requestTeacherCourses(this.props.teacherId);
        this.props.requestTeacherActivities(this.props.teacherId);
    }

    private renderTeacherActivites() {
        return (
            <div>
                <h2>Courses</h2>
                {this.props.activities.isLoading && <Spinner type="grow" color="success" />}
                <Table className='table table-striped' aria-labelledby="tabelLabel" size="sm" hover>
                    <thead>
                        <tr>
                            <th>Course</th>
                            <th>Course type</th>
                            <th>Teacher does</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.props.activities.teacherActivites.map((activity: TeacherActivitiesStore.TeacherActivity) =>
                            <tr onClick={() => this.props.history.push(`/course-profile/${activity.courseId}`)}>
                                <td>{this.getCourseName(activity.courseId)}</td>
                                <td>{CoursesStore.CourseType[this.getCourseType(activity.courseId)]}</td>
                                <td>{LectureType[activity.lectureType]}</td>
                            </tr>
                        )}
                    </tbody>
                </Table>
            </div>
        );
    }

    private getCourseName(courseId: string): string {
        const course = this.props.courses.courses.find(x => x.id === courseId);
        return course ? course.name : "Unknown";
    }

    private getCourseType(courseId: string): CoursesStore.CourseType {
        const course = this.props.courses.courses.find(x => x.id === courseId);
        return course ? course.courseType : 0;
    }
}

function mapStateToProps(state: ApplicationState, ownProps: OwnProps) {
    return {
        activities: state.teacherActivites,
        courses: state.teacherCourses,
        teacherId: ownProps.teacherId
    }
};

const actions = {
    ...TeacherActivitiesStore.actionCreators,
    ...TeacherCoursesStore.actionCreators
}

export default withRouter(
    connect(
        mapStateToProps,
        actions
    )(TeacherActivities as any) as React.ComponentType<any>
);