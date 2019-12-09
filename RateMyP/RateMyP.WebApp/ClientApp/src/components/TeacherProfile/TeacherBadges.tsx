import * as React from 'react';
import { connect } from 'react-redux';
import { ApplicationState } from '../../store';
import * as TeacherBadgesStore from '../../store/Teacher/TeacherBadges';

interface OwnProps {
    teacherId: string
};

type Props = 
    TeacherBadgesStore.TeacherBadgesState  &
    typeof TeacherBadgesStore.actionCreators

class TeacherBadges extends React.PureComponent<Props & OwnProps> {

    public componentDidMount() {
        this.ensureDataFetched();
    }

    public componentDidUpdate() {
        this.ensureDataFetched();
    }

    private ensureDataFetched() {
        this.props.requestTeacherBadges(this.props.teacherId);
    }

    public render() {
        return (
            <React.Fragment>
                {this.renderBadges()}
            </React.Fragment>
        );
    }

    private renderBadges() {
        return (
            <React.Fragment>
                {this.props.badges ? <img src={"/api/badges/" + this.props.badges[0].id}></img> : null}
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
    TeacherBadgesStore.actionCreators // Selects which action creators are merged into the component's props
)(TeacherBadges as any);

