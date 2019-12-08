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
        onRowClick: (rowData: string[], rowState: {rowIndex: number, dataIndex: number}) => {
            !this.props.isLoading && this.props.history.push(`/course-profile/${rowData[3]}`);
          },
    };
    
    public componentDidMount() {
        this.ensureDataFetched();
    }

    public componentDidUpdate() {
        this.ensureDataFetched();
    }

    public ensureDataFetched() {
        this.props.requestAllTimeCourseLeaderboard();
        this.props.requestThisYearCourseLeaderboard();
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
        return this.state.tab === 0 ? this.renderAllTimeTable() : this.renderThisYearTable();
    }

    private renderAllTimeTable() {
        return (
            <MUIDataTable
                title={"Course All Time"}
                data={this.props.allTimeEntries.map((entry: CourseLeaderboardStore.CourseLeaderboardEntry) => {
                    return [
                        entry.allTimePosition,
                        entry.course.name,
                        entry.allTimeAverage.toFixed(2),
                        entry.courseId
                    ]})}
                columns={[
                    {name: 'Rank', options: {sort: true}},
                    {name: 'Name', options: {sort: false}},
                    {name: 'Rating'},
                    {name: 'Id', options: {display: 'excluded'}}  
                ]}
                options={this.tableOptions}/>
        )
    }

    private renderThisYearTable() {
        return (
            <MUIDataTable
                title={"Course This Year"}
                data={this.props.thisYearEntries.map((entry: CourseLeaderboardStore.CourseLeaderboardEntry) => {
                    return [
                        entry.thisYearPosition,
                        entry.course.name,
                        entry.thisYearAverage.toFixed(2),
                        entry.courseId
                    ]})}
                columns={[
                    {name: 'Rank', options: {sort: false}},
                    {name: 'Name', options: {sort: false}},
                    {name: 'Rating'},
                    {name: 'Id', options: {display: 'excluded'}}  
                ]}
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