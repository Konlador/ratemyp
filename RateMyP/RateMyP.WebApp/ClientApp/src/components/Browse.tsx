import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { Table } from 'reactstrap';
import { ApplicationState } from '../store';
import * as TeachersStore from '../store/Teachers';

type TeachersProps =
    TeachersStore.TeachersState &
    typeof TeachersStore.actionCreators &
    RouteComponentProps<{}>;

class Browse extends React.PureComponent<TeachersProps> {
    // This method is called when the component is first added to the document
    public componentDidMount() {
        this.props.requestTeachers();
    }

    // This method is called when the route parameters change
    public componentDidUpdate() {
        this.props.requestTeachers();
    }

    public render() {
        return (
            <React.Fragment>
                <h1 id="tabelLabel">Browse</h1>
                <p>This component demonstrates fetching data from the server and working with URL parameters.</p>
                {this.renderTeachersTable()}
            </React.Fragment>
        );
    }

    private renderTeachersTable() {
        return (
            <div>
                {this.props.isLoading && <span>Loading...</span>}
                <Table className='table table-striped' aria-labelledby="tabelLabel" hover size="sm">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Last name</th>
                            <th>Rank</th>
                            <th>Faculty</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.props.teachers.map((teacher: TeachersStore.Teacher) =>
                            <tr onClick={() => this.props.history.push(`/profile/${teacher.id}`)}>
                                <td>{teacher.firstName}</td>
                                <td>{teacher.lastName}</td>
                                <td>{teacher.rank}</td>
                                <td>{teacher.faculty}</td>
                            </tr>
                        )}
                    </tbody>
                </Table>
            </div>
        );
    }
}

export default connect(
    (state: ApplicationState) => state.teachers, // Selects which state properties are merged into the component's props
    TeachersStore.actionCreators // Selects which action creators are merged into the component's props
)(Browse as any);
