import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps, withRouter } from 'react-router';
import { ApplicationState } from '../../store';
import * as TeacherLeaderboardStore from '../../store/TeacherLeaderboardEntry';
import * as TeachersStore from '../../store/Teachers';
import { Button, Spinner } from 'reactstrap';
import MUIDataTable, { SelectableRows } from 'mui-datatables';

interface OwnProps {
    teacherId: string
};

type Props =
    {
    leaderboard: TeacherLeaderboardStore.TeacherLeaderboardState
    teachers: TeachersStore.TeachersState
    } &
    typeof TeacherLeaderboardStore.actionCreators &
    typeof TeachersStore.actionCreators & 
    RouteComponentProps<{}>;

class TeacherLadder extends React.PureComponent<Props & OwnProps> {

    tableOptions = {
        print: false,
        download: false,
        viewColumns: false,
        selectableRows: "none" as SelectableRows,
        pagination: false,
        sort: true,
        filter: false,
    };
    
    state = {
    }

    public componentDidMount() {
        if (this.props.leaderboard.entries.length === 0) this.props.requestTeacherLeaderboard();
    }

    public render() {
        return (
            <React.Fragment>
                {this.renderTable()}
                {this.props.leaderboard.isLoading && <Spinner type="grow" color ="primary" style={{
                    position: 'absolute', 
                    left: '40%', 
                    marginTop: '16px'
                }}/>}
            </React.Fragment>          
        );
    }

    private renderTable() {
        return (
            <div>
                <MUIDataTable
                    title={"Teacher Leaderboard"}
                    data={this.props.leaderboard.entries.map((entry: TeacherLeaderboardStore.TeacherLeaderboardEntry) => {
                    return [
                        entry.allTimePosition,
                        entry.id,
                        entry.allTimeAverage,
                    ]})
                    }
                    columns={
                        [
                            {name: 'Rank'},
                            {name: 'Name'},
                            {name: 'Rating'},
                        ]
                    }
                    options={this.tableOptions}
                />
            </div>
        )
    }
}

function mapStateToProps(state: ApplicationState, ownProps: OwnProps) {
    return {
        ...state.teacherLeaderboardEntries,
        teacherId: ownProps.teacherId
    }
};

export default withRouter(
    connect(
        mapStateToProps,
        TeacherLeaderboardStore.actionCreators
    )(TeacherLadder as any) as React.ComponentType<any>
);