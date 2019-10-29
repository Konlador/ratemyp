import * as React from 'react';
import { connect } from 'react-redux';
import { ApplicationState } from '../../store';
import * as TeachersStore from '../../store/Teachers';

interface TeacherInfoOwnProps {
    teacherId: string
};

type TeacherInfoProps =
    TeachersStore.TeachersState &
    typeof TeachersStore.actionCreators;

class TeacherInfo extends React.PureComponent<TeacherInfoProps & TeacherInfoOwnProps> {
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
        this.props.requestTeacher(this.props.teacherId);
    }

    private renderTeacherInfo() {
        const teacher = this.props.selectedTeacher;
        if (!teacher)
            return;

        return (
            <React.Fragment>
                <h1>
                    {`${teacher.firstName} ${teacher.lastName}`}
                </h1>
                <p>
                    <p><strong>Faculty: </strong>{teacher.faculty}</p>
                    <p><strong>Rank: </strong>{teacher.rank}</p>
                </p>
                <p>
                    {teacher.description && ("Description:" + teacher.description)}
                </p>
            </React.Fragment>
        );
    }
}

function mapStateToProps(state: ApplicationState, ownProps: TeacherInfoOwnProps) {
    return {
        ...state.teachers,
        teacherId: ownProps.teacherId
    }
};

export default connect(
    mapStateToProps, // Selects which state properties are merged into the component's props
    TeachersStore.actionCreators // Selects which action creators are merged into the component's props
)(TeacherInfo as any);

