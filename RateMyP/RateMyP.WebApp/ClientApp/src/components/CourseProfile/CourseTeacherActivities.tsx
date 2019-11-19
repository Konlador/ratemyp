import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps, withRouter } from 'react-router';
import { Table, Spinner } from 'reactstrap';
import { ApplicationState } from '../../store';
import { LectureType } from '../../store/Teacher/TeacherActivities';
import * as TeacherActivitiesStore from '../../store/Teacher/TeacherActivities';
import * as CourseTeacherActivitiesStore from '../../store/Course/CourseTeacherActivities';
import * as CourseTeachersStore from '../../store/Course/CourseTeachers';

interface OwnProps {
    courseId: string
};

type Props =
    {
    activities: CourseTeacherActivitiesStore.CourseTeacherActivitiesState,
    teachers: CourseTeachersStore.CourseTeachersState
    } &
    typeof CourseTeacherActivitiesStore.actionCreators &
    typeof CourseTeachersStore.actionCreators &
    RouteComponentProps<any>;

class CourseTeacherActivities extends React.PureComponent<Props & OwnProps> {
    public componentDidMount() {
        this.ensureDataFetched();
    }

    public componentDidUpdate() {
        this.ensureDataFetched();
    }

    public render() {
        return (
            <React.Fragment>
                {this.renderCourseTeacherActivites()}
            </React.Fragment>
        );
    }

    private ensureDataFetched() {
        this.props.requestCourseTeachers(this.props.courseId);
        this.props.requestCourseTeacherActivities(this.props.courseId);
    }

    private renderCourseTeacherActivites() {
        return (
            <div>
                <h2>Teachers</h2>
                {this.props.activities.isLoading && <Spinner type="grow" color="success" />}
                <Table className='table table-striped' aria-labelledby="tabelLabel" size="sm" hover>
                    <thead>
                        <tr>
                            <th>Teacher</th>
                            <th>Type</th>
                            <th>Date started</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.props.activities.teacherActivites.map((activity: TeacherActivitiesStore.TeacherActivity) =>
                            <tr key={activity.id} onClick={() => this.props.history.push(`/teacher-profile/${activity.teacherId}`)}>
                                <td>{this.getTeacherName(activity.teacherId)}</td>
                                <td>{LectureType[activity.lectureType]}</td>
                                <td>{new Date(activity.dateStarted).toISOString().split('T')[0]}</td>
                            </tr>
                        )}
                    </tbody>
                </Table>
            </div>
        );
    }

    private getTeacherName(teacherId: string): string {
        const teacher = this.props.teachers.teachers.find(x => x.id === teacherId);
        return teacher ? `${teacher.firstName} ${teacher.lastName}` : "Unknown";
    }
}

function mapStateToProps(state: ApplicationState, ownProps: OwnProps) {
    return {
        activities: state.courseTeacherActivities,
        teachers: state.courseTeachers,
        courseId: ownProps.courseId
    }
};

const actions = {
    ...CourseTeacherActivitiesStore.actionCreators,
    ...CourseTeachersStore.actionCreators
}

export default withRouter(
    connect(
        mapStateToProps,
        actions
    )(CourseTeacherActivities as any) as React.ComponentType<any>
);
