import * as React from 'react';
import { connect } from 'react-redux';
import TeacherLadder from './TeacherLadder';
import '../../extensions/StringExtensions';

class Leaderboard extends React.PureComponent {

    public componentDidMount() {
        this.setState({teacherLeaderboardPage: 0});
        this.setState({courseLeaderboardPage: 0});
    }

    state = {
        teacherLeaderboardPage: 0,
        courseLeaderboardPage: 0,
    }

    public render() {
        return (
            <React.Fragment>
                <TeacherLadder/>
            </React.Fragment>
        )
    }
}

export default connect()(Leaderboard);