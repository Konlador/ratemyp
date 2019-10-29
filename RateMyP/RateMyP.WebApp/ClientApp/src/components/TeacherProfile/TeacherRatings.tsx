import * as React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators, Dispatch } from 'redux';
import { Table } from 'reactstrap';
import { ApplicationState } from '../../store';
import * as RatingsStore from '../../store/Ratings';
import * as CoursesStore from '../../store/Courses';

interface TeacherRatingsOwnProps {
    teacherId: string
};

type TeacherRatingsProps =
    {
    ratings: RatingsStore.RatingsState,
    courses: CoursesStore.CoursesState
    } &
    typeof RatingsStore.actionCreators &
    typeof CoursesStore.actionCreators;

class TeacherRatings extends React.PureComponent<TeacherRatingsProps & TeacherRatingsOwnProps> {
    public componentDidMount() {
        this.ensureDataFetched();
    }

    public componentDidUpdate() {
        this.ensureDataFetched();
    }

    private ensureDataFetched() {
        this.props.requestTeacherCourses(this.props.teacherId);
        this.props.requestTeacherRatings(this.props.teacherId);
    }

    public render() {
        return (
            <React.Fragment>
                {this.renderTeacherInfo()}
            </React.Fragment>
        );
    }

    private renderTeacherInfo() {
        return (
            <div>
                <h1>Ratings</h1>
                {this.props.ratings.isLoading && <span>Loading...</span>}
                <Table className="table table-striped" aria-labelledby="tabelLabel" size="sm">
                    <thead>
                        <tr>
                            <th>Courses</th>
                            <th>Overall mark</th>
                            <th>Level of difficulty</th>
                            <th>Would take again</th>
                            <th>Date created</th>
                            <th>Comment</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.props.ratings.ratings.map((rating: RatingsStore.Rating) =>
                            <tr>
                                <td>{this.getCourseName(rating.CourseId)}</td>
                                <td>{rating.OverallMark}</td>
                                <td>{rating.LevelOfDifficulty}</td>
                                <td>{rating.WouldTakeTeacherAgain ? "Yes" : "No"}</td>
                                <td>{new Date(rating.DateCreated).toISOString().split('T')[0]}</td>
                                <td>{rating.Comment}</td>
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

function mapStateToProps(state: ApplicationState, ownProps: TeacherRatingsOwnProps) {
    return {
        ratings: state.ratings,
        courses: state.courses,
        teacherId: ownProps.teacherId
    }
};

const actions = {
    ...RatingsStore.actionCreators,
    ...CoursesStore.actionCreators
}

export default connect(mapStateToProps, actions)(TeacherRatings as any);

