import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps, withRouter } from 'react-router';
import { ApplicationState } from '../../store';
import * as CourseLeaderboardStore from '../../store/Leaderboard/CourseLeaderboard';
import { Button, ButtonGroup, Spinner } from 'reactstrap';
import MUIDataTable, { SelectableRows } from 'mui-datatables';
import './CourseLeaderboard.css';

type Props =
    CourseLeaderboardStore.CourseLeaderboardState &
    typeof CourseLeaderboardStore.actionCreators &
    RouteComponentProps<{}>;

interface State {
    tab: number
}

class CourseLeaderboard extends React.PureComponent<Props, State> {

    constructor(props: Props) {
        super(props);
        this.state = { tab: 0 };
    };

    tableOptions = {
        print: false,
        download: false,
        viewColumns: false,
        selectableRows: "none" as SelectableRows,
        pagination: false,
        sort: true,
        filter: false,
        search: false,
    };
    
    tableColumns = [
        {name: 'Rank', options: {sort: false}},
        {name: 'Subject', options: {sort: false}},
        {name: 'Rating'},
    ];

    public componentDidMount() {
    }

    private switchTab() {
        this.setState({ tab: (this.state.tab + 1) % 2 });
    }

    private getButtonStatus() {
        return this.state.tab == 0;
    }

    public render() {
        return (
            <React.Fragment>
                <ButtonGroup  id="leaderboard-courses-scope-buttons">
                    <Button color="primary" disabled={this.getButtonStatus()} onClick={() => this.switchTab()}>All-Time</Button>
                    <Button color="primary" disabled={!this.getButtonStatus()} onClick={() => this.switchTab()}>This Year</Button>
                </ButtonGroup>
                {this.renderComponents()}
                {this.props.isLoading && <Spinner type="grow" color ="primary" style={{
                    position: 'absolute', 
                    left: '40%', 
                    marginTop: '16px'
                }}/>}
            </React.Fragment>          
        );
    }

    private renderComponents() {
        if (this.state.tab === 0) return this.renderAllTimeTable()
        else return this.renderThisYearTable()
    }

    private renderAllTimeTable() {
        return (
            <MUIDataTable
                title={"Course All Time"}
                data={[]}
                columns={this.tableColumns}
                options={this.tableOptions}/>
        )
    }

    private renderThisYearTable() {
        return (
            <MUIDataTable
                title={"Course This Year"}
                data={[]}
                columns={this.tableColumns}
                options={this.tableOptions}/>
        )

    }
}

export default withRouter(
    connect(
        (state: ApplicationState) => state.courseLeaderboard,
        CourseLeaderboardStore.actionCreators
    )(CourseLeaderboard as any) as React.ComponentType<any>
);