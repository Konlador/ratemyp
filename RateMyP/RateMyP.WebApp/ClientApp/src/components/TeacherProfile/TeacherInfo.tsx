import * as React from 'react';
import { connect } from 'react-redux';
import { ApplicationState } from '../../store';
import * as TeachersStore from '../../store/Teachers';

interface OwnProps {
    teacherId: string
};

type Props =
    TeachersStore.TeachersState &
    typeof TeachersStore.actionCreators;

class TeacherInfo extends React.PureComponent<Props & OwnProps> {
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
                <div>
                    <h1>
                        {`${teacher.firstName} ${teacher.lastName}`}
                    </h1>
                </div>
                
                <div>
                    <p><strong>Faculty: </strong>{teacher.faculty}</p>
                    <p><strong>Rank: </strong>{teacher.rank}</p>
                    <p>{teacher.description && ("Description:" + teacher.description)}</p>
                </div>
            </React.Fragment>
        );
    }
}

function mapStateToProps(state: ApplicationState, ownProps: OwnProps) {
    return {
        ...state.teachers,
        teacherId: ownProps.teacherId
    }
};

export default connect(
    mapStateToProps, // Selects which state properties are merged into the component's props
    TeachersStore.actionCreators // Selects which action creators are merged into the component's props
)(TeacherInfo as any);

