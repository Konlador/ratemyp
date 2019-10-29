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
                <Table className='table table-striped' aria-labelledby="tabelLabel" size="sm">
                    <thead>
                        <tr>
                            <th>Courese</th>
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
                                <td>{rating.courseId}</td>
                                <td>{rating.overallMark}</td>
                                <td>{rating.levelOfDifficulty}</td>
                                <td>{rating.wouldTakeTeacherAgain}</td>
                                <td>{rating.dateCreated}</td>
                                <td>{rating.comment}</td>
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

