import * as React from 'react';
import { connect } from 'react-redux';
import { Table } from 'reactstrap';
import { ApplicationState } from '../../store';
import * as TeacherActivitiesStore from '../../store/TeacherActivities';
import * as CoursesStore from '../../store/Courses';


interface TeacherActivitiesOwnProps {
    teacherId: string
};

type TeacherActivitiesProps =
    {
    activities: TeacherActivitiesStore.TeacherActivitesState,
    courses: CoursesStore.CoursesState
    } &
    typeof TeacherActivitiesStore.actionCreators &
    typeof CoursesStore.actionCreators;

class TeacherActivities extends React.PureComponent<TeacherActivitiesProps & TeacherActivitiesOwnProps> {
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
                <h1>Activities</h1>
                {this.props.activities.isLoading && <span>Loading...</span>}
                <Table className='table table-striped' aria-labelledby="tabelLabel" size="sm">
                    <thead>
                        <tr>
                            <th>Course</th>
                            <th>Date started</th>
                            <th>Type</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.props.activities.teacherActivites.map((activity: TeacherActivitiesStore.TeacherActivity) =>
                            <tr>
                                <td>{this.getCourseName(activity.courseId)}</td>
                                <td>{new Date(activity.dateStarted).toISOString().split('T')[0]}</td>
                                <td>{activity.lectureType}</td>
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
}

function mapStateToProps(state: ApplicationState, ownProps: TeacherActivitiesOwnProps) {
    return {
        activities: state.teacherActivites,
        courses: state.courses,
        teacherId: ownProps.teacherId
    }
};

const actions = {
    ...TeacherActivitiesStore.actionCreators,
    ...CoursesStore.actionCreators
}

export default connect(
    mapStateToProps,
    actions
)(TeacherActivities as any);

