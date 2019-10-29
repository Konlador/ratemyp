import * as React from 'react';
import { connect } from 'react-redux';
import { Table } from 'reactstrap';
import { ApplicationState } from '../../store';
import * as TeacherActivitiesStore from '../../store/TeacherActivities';

interface TeacherActivitiesOwnProps {
    teacherId: string
};

type TeacherActivitiesProps =
    TeacherActivitiesStore.TeacherActivitesState &
    typeof TeacherActivitiesStore.actionCreators;

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
        this.props.requestTeacherActivities(this.props.teacherId);
    }

    private renderTeacherActivites() {
        return (
            <div>
                <h1>Activities</h1>
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
                        {this.props.teacherActivites.map((rating: TeacherActivitiesStore.TeacherActivity) =>
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

function mapStateToProps(state: ApplicationState, ownProps: TeacherActivitiesOwnProps) {
    return {
        ...state.teacherActivites,
        teacherId: ownProps.teacherId
    }
};

export default connect(
    mapStateToProps, // Selects which state properties are merged into the component's props
    TeacherActivitiesStore.actionCreators // Selects which action creators are merged into the component's props
)(TeacherActivities as any);

