import * as React from 'react';
import { connect } from 'react-redux';
import TeacherLeaderboard from './TeacherLeaderboard';
import CourseLeaderboard from './CourseLeaderboard';
import './Leaderboard.css';

class Leaderboard extends React.PureComponent {
    public render() {
        return (
            <React.Fragment>
            <div>
                <div className="teacher-leaderboard"><TeacherLeaderboard/></div>
                <div className="course-leaderboard"><CourseLeaderboard/></div>
            </div>
            </React.Fragment>
        )
    }
}

export default connect()(Leaderboard);