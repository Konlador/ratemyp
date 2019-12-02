import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { connect } from 'react-redux';
import TeacherInfo from './TeacherInfo';
import TeacherRatings from './TeacherRatings';
import TeacherActivities from './TeacherActivities';
import * as TeachersStore from '../../store/Teachers';
import TeacherStatistics from './TeacherStatistics';
import CustomStarShowcase from '../CustomStar/CustomStarShowcase'

type Props =
    typeof TeachersStore.actionCreators &
    RouteComponentProps<{ teacherId: string }>;

class TeacherProfile extends React.PureComponent<Props> {
    public componentWillUnmount(){
        this.props.clearSelectedTeacher();
    }

    public render() {
        return (
            <React.Fragment>
                <TeacherInfo teacherId={this.props.match.params.teacherId}/>
                <TeacherStatistics teacherId={this.props.match.params.teacherId}/>
                <TeacherActivities teacherId={this.props.match.params.teacherId}/>
                <TeacherRatings teacherId={this.props.match.params.teacherId}/>
                <CustomStarShowcase teacherId={this.props.match.params.teacherId} showItems = {3}/>
            </React.Fragment>
        );
    }
}


export default connect(
    undefined,
    TeachersStore.actionCreators
)(TeacherProfile as any);
