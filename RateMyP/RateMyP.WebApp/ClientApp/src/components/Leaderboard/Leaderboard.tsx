import * as React from 'react';
import { connect } from 'react-redux';
import TeacherLeaderboard from './TeacherLeaderboard';
import CourseLeaderboard from './CourseLeaderboard';
import '../../extensions/StringExtensions';
import './Leaderboard.css';

class Leaderboard extends React.PureComponent {

    public render() {
        return (
            <React.Fragment>
            <div>
                <div style={{maxWidth: '48%', display: 'inline-block', marginRight: '2%'}}><TeacherLeaderboard/></div>
                <div style={{maxWidth: '48%', display: 'inline-block', verticalAlign: 'top'}}><CourseLeaderboard/></div>
            </div>
            </React.Fragment>
        )
    }
}

export default connect()(Leaderboard);