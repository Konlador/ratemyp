import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps, withRouter } from 'react-router';
import { ApplicationState } from '../../store';
import * as TeacherLeaderboardStore from '../../store/TeacherLeaderboardEntry';
import { Button, ButtonGroup, Spinner } from 'reactstrap';
import MUIDataTable, { SelectableRows } from 'mui-datatables';

interface OwnProps {
    teacherId: string
};

type Props =
    TeacherLeaderboardStore.TeacherLeaderboardState &
    typeof TeacherLeaderboardStore.actionCreators &
    RouteComponentProps<{}>;

class TeacherLeaderboard extends React.PureComponent<Props & OwnProps> {

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
            console.log(rowData, rowState);
            !this.props.isLoading && this.props.history.push(`/teacher-profile/${rowData[3]}`);
          },
    };
    state = {
        tab: 0
    }

    public componentDidMount() {
        this.setState({tab: 0});
        this.ensureDataFetched();
    }

    public componentDidUpdate() {
        this.ensureDataFetched();
    }

    private ensureDataFetched() {
        this.props.requestAllTimeTeacherLeaderboard();
        this.props.requestThisYearTeacherLeaderboard();
    }

    switchTab() {
        this.setState({ tab: (this.state.tab + 1) % 2 });
    }

    getButtonStatus() {
        return this.state.tab === 0;
    }

    public render() {
        return (
            <React.Fragment>
                <ButtonGroup style={{left: '35%', marginBottom: '16px'}}>
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
        let data = this.props.allTimeEntries.map((entry: TeacherLeaderboardStore.TeacherLeaderboardEntry) => {
            return [
                entry.allTimePosition,
                entry.name,
                entry.allTimeAverage.toFixed(2),
                entry.id
            ]
        });

        return (
            <MUIDataTable
                title={"Teacher All Time"}
                data={this.props.allTimeEntries.map((entry: TeacherLeaderboardStore.TeacherLeaderboardEntry) => {
                    return [
                        entry.allTimePosition,
                        entry.name,
                        entry.allTimeAverage.toFixed(2),
                        entry.id
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

    private renderThisYearTable() {
        return (
            <MUIDataTable
                title={"Teacher This Year"}
                data={this.props.thisYearEntries.map((entry: TeacherLeaderboardStore.TeacherLeaderboardEntry) => {
                    return [
                        entry.thisYearPosition,
                        entry.name,
                        entry.thisYearAverage.toFixed(2),
                        entry.id,
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
    )(TeacherLeaderboard as any) as React.ComponentType<any>
);