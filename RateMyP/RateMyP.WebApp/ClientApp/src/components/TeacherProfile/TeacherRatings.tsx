import * as React from 'react';
import { connect } from 'react-redux';
import { Table } from 'reactstrap';
import { ApplicationState } from '../../store';
import * as RatingsStore from '../../store/Ratings';

interface TeacherRatingsOwnProps {
    teacherId: string
};

type TeacherRatingsProps =
    RatingsStore.RatingsState &
    typeof RatingsStore.actionCreators;

class TeacherRatings extends React.PureComponent<TeacherRatingsProps & TeacherRatingsOwnProps> {
    public componentDidMount() {
        this.ensureDataFetched();
    }

    public componentDidUpdate() {
        this.ensureDataFetched();
    }

    public render() {
        return (
            <React.Fragment>
                {this.renderTeacherInfo()}
            </React.Fragment>
        );
    }

    private ensureDataFetched() {
        this.props.requestTeacherRatings(this.props.teacherId);
    }

    private renderTeacherInfo() {
        return (
            <div>
                <h1>Ratings</h1>
                {this.props.isLoading && <span>Loading...</span>}
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
                        {this.props.ratings.map((rating: RatingsStore.Rating) =>
                            <tr>
                                <td>{rating.CourseId}</td>
                                <td>{rating.OverallMark}</td>
                                <td>{rating.LevelOfDifficulty}</td>
                                <td>{rating.WouldTakeTeacherAgain}</td>
                                <td>{rating.DateCreated}</td>
                                <td>{rating.Comment}</td>
                            </tr>
                        )}
                    </tbody>
                </Table>
            </div>
        );
    }
}

function mapStateToProps(state: ApplicationState, ownProps: TeacherRatingsOwnProps) {
    return {
        ...state.ratings,
        teacherId: ownProps.teacherId
    }
};

export default connect(
    mapStateToProps, // Selects which state properties are merged into the component's props
    RatingsStore.actionCreators // Selects which action creators are merged into the component's props
)(TeacherRatings as any);

