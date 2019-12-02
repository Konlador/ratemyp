import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { connect } from 'react-redux';
import { Button } from 'reactstrap';
import { Link } from 'react-router-dom';
import CourseInfo from './CourseInfo';
import CourseRatings from './CourseRatings';
import CourseStatistics from './CourseStatistics';
import CourseTeacherActivities from './CourseTeacherActivities';
import * as CoursesStore from '../../store/Courses';


type Props =
    typeof CoursesStore.actionCreators &
    RouteComponentProps<{ courseId: string }>;

class CourseProfile extends React.PureComponent<Props> {
    public componentWillUnmount(){
        this.props.clearSelectedCourse();
    }

    public render() {
        return (
            <React.Fragment>
                <CourseInfo courseId={this.props.match.params.courseId}/>
                <CourseStatistics courseId={this.props.match.params.courseId}/>
                <Button color="primary" tag={Link} to={`/rate-course/${this.props.match.params.courseId}`}>Add a rating</Button>{' '}
                <CourseTeacherActivities courseId={this.props.match.params.courseId}/>
                <CourseRatings courseId={this.props.match.params.courseId}/>
            </React.Fragment>
        );
    }
}


export default connect(
    undefined,
    CoursesStore.actionCreators
)(CourseProfile as any);
