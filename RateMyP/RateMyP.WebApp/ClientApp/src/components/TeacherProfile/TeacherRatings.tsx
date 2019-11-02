import * as React from 'react';
import { connect } from 'react-redux';
import { Table, Spinner } from 'reactstrap';
import { ApplicationState } from '../../store';
import * as RatingsStore from '../../store/Ratings';
import * as TeacherRatingsStore from '../../store/Teacher/TeacherRatings';
import * as TeacherCoursesStore from '../../store/Teacher/TeacherCourses';

interface OwnProps {
    teacherId: string
};

type Props =
    {
    ratings: TeacherRatingsStore.TeacherRatingsState,
    courses: TeacherCoursesStore.TeacherCoursesState
    } &
    typeof TeacherRatingsStore.actionCreators &
    typeof TeacherCoursesStore.actionCreators;

class TeacherRatings extends React.PureComponent<Props & OwnProps> {
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
                <h2>Ratings</h2>
                {this.props.ratings.isLoading && <Spinner type="grow" color="success" />}
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

function mapStateToProps(state: ApplicationState, ownProps: OwnProps) {
    return {
        ratings: state.ratings,
        courses: state.teacherCourses,
        teacherId: ownProps.teacherId
    }
};

const actions = {
    ...TeacherRatingsStore.actionCreators,
    ...TeacherCoursesStore.actionCreators
}

export default connect(mapStateToProps, actions)(TeacherRatings as any);

