import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { connect } from 'react-redux';
import { Button } from 'reactstrap';
import { Link } from 'react-router-dom';
import TeacherInfo from './TeacherInfo';
import TeacherRatings from './TeacherRatings';
import TeacherActivities from './TeacherActivities';
import * as TeachersStore from '../../store/Teachers';


type TeacherProfileProps =
    typeof TeachersStore.actionCreators &
    RouteComponentProps<{ teacherId: string }>;

class TeacherProfile extends React.PureComponent<TeacherProfileProps> {
    public componentWillUnmount(){
        this.props.clearSelectedTeacher();
    }

    public render() {
        return (
            <React.Fragment>
                <TeacherInfo teacherId={this.props.match.params.teacherId}/>
                <div>TODO: Teacher statistics</div>
                <Button color="primary" tag={Link} to={`/rate/${this.props.match.params.teacherId}`}>Add a rating</Button>{' '}
                <TeacherActivities teacherId={this.props.match.params.teacherId}/>
                <TeacherRatings teacherId={this.props.match.params.teacherId}/>
            </React.Fragment>
        );
    }
}


export default connect(
    undefined,
    TeachersStore.actionCreators
)(TeacherProfile as any);
