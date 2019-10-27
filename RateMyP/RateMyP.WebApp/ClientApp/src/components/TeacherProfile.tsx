import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { ApplicationState } from '../store';
import * as TeachersStore from '../store/Teachers';
import * as RatingsStore from '../store/Ratings';

type TeacherProfileProps =
    TeachersStore.TeachersState &
    typeof RatingsStore.actionCreators &
    typeof TeachersStore.actionCreators &
    RouteComponentProps<{ teacherId: string }>;

class TeacherProfile extends React.PureComponent<TeacherProfileProps> {
    public componentDidMount() {
        this.ensureDataFetched();
    }

    public componentDidUpdate() {
        this.ensureDataFetched();
    }

    public render() {
        return (
            <React.Fragment>
                {this.renderTeacherProfile()}
                {this.renderTeacherRatings()}
            </React.Fragment>
        );
    }

    private ensureDataFetched() {
        //this.props.requestRatings();
        this.props.requestTeachers();
    }

    private renderTeacherProfile() {
        return (
            <div>
                mokytojo duomenys
            </div>
        );
    }

    private renderTeacherRatings() {
        return (
            <div>
                mokytojo reitingai
            </div>
        );
    }
}

export default connect(
    (state: ApplicationState) => state.teachers, // Selects which state properties are merged into the component's props
    TeachersStore.actionCreators // Selects which action creators are merged into the component's props
)(TeacherProfile as any);
