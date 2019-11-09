import * as React from 'react';
import { RouteComponentProps, withRouter } from 'react-router';
import { connect } from 'react-redux';
import { Table } from 'reactstrap';
import { ApplicationState } from '../../store';
import * as TeacherActivitiesStore from '../../store/Teacher/TeacherActivities';
import * as CoursesStore from '../../store/Rate/RateCourses';


interface OwnProps {
    teacherId: string;
    passSelectedTeacherActivities: (value: string) => void;
};

type Props =
    {
    activities: TeacherActivitiesStore.TeacherActivitesState,
    courses: CoursesStore.RateCoursesState
    } &
    typeof TeacherActivitiesStore.actionCreators &
    typeof CoursesStore.actionCreators&
    RouteComponentProps<{}>;

class RateTeacherActivities extends React.PureComponent<Props & OwnProps> {
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
        this.props.requestTeacherCourses(this.props.teacherId);
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
                <Table className='table table-striped' aria-labelledby="tabelLabel" size="sm" hover>
                    <thead>
                        <tr>
                            <th>Course</th>
                            <th>Id</th>
                            <th>Type</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.props.activities.teacherActivites.map((activity: TeacherActivitiesStore.TeacherActivity) =>
                            <tr onClick={() => {this.props.setSelectedRowId(activity.id) ; this.props.passSelectedTeacherActivities(activity.courseId);}}  style={{background: activity.id===this.props.activities.selectedRowId? '#6091ff':'white'}}>
                                <td>{this.getCourseName(activity.courseId)}</td>
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
}

function mapStateToProps(state: ApplicationState, ownProps: OwnProps) {
    return {
        activities: state.teacherActivites,
        courses: state.rateCourses,
        teacherId: ownProps.teacherId,
    }
};

const actions = {
    ...TeacherActivitiesStore.actionCreators,
    ...CoursesStore.actionCreators
}

export default withRouter(
    connect(
        mapStateToProps,
        actions
    )(RateTeacherActivities as any) as React.ComponentType<any>
);