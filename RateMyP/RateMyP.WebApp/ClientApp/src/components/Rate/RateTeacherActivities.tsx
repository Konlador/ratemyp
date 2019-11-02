import * as React from 'react';
import { connect } from 'react-redux';
import { Table } from 'reactstrap';
import { ApplicationState } from '../../store';
import * as TeacherActivitiesStore from '../../store/Rate/RateTeacherActivities';
import * as CoursesStore from '../../store/Rate/RateCourses';


interface TeacherActivitiesOwnProps {
    teacherId: string
    passSelectedTeacherActivities?: (value: TeacherActivitiesStore.TeacherActivity | undefined) => void;
};

type TeacherActivitiesProps =
    {
    activities: TeacherActivitiesStore.RateTeacherActivitesState,
    courses: CoursesStore.CoursesState
    } &
    typeof TeacherActivitiesStore.actionCreators &
    typeof CoursesStore.actionCreators;

class RateTeacherActivities extends React.PureComponent<TeacherActivitiesProps & TeacherActivitiesOwnProps> {
    public setProp () {
    }

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
        this.props.requestCourses(this.props.teacherId);
        this.props.requestTeacherActivities(this.props.teacherId);
    }

    private renderTeacherActivites() {

        enum LectureType {
                Lecture,
                Practice,
                Seminar
            }

        return (
            <div>
                <h1>Activities</h1>
                {this.props.activities.isLoading && <span>Loading...</span>}
                <Table className='table table-striped' aria-labelledby="tabelLabel" size="sm">
                    <thead>
                        <tr>
                            <th>Course</th>
                            <th>Type</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.props.activities.teacherActivites.map((activity: TeacherActivitiesStore.TeacherActivity) =>
                            <tr onClick={() => this.props.setSelectedTeacherActivity(activity)}>
                                <td>{this.getCourseName(activity.courseId)}</td>
                                <td>{LectureType[activity.lectureType]}</td>
                            </tr>
                        )}
                    </tbody>
                </Table>
                {this.props.passSelectedTeacherActivities != undefined ? this.props.passSelectedTeacherActivities(this.props.activities.selectedTeacherActivity): this.props.passSelectedTeacherActivities}
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
        teacherId: ownProps.teacherId,
    }
};

const actions = {
    ...TeacherActivitiesStore.actionCreators,
    ...CoursesStore.actionCreators
}

export default connect(
    mapStateToProps,
    actions
)(RateTeacherActivities as any);

