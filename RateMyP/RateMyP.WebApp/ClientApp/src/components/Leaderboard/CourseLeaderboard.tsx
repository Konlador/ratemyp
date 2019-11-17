import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps, withRouter } from 'react-router';
import { ApplicationState } from '../../store';
import * as CourseLeaderboardStore from '../../store/CourseLeaderboardEntry';
import { Button, ButtonGroup, Spinner } from 'reactstrap';
import MUIDataTable, { SelectableRows } from 'mui-datatables';

interface OwnProps {
    courseId: string
};

type Props =
    CourseLeaderboardStore.CourseLeaderboardState &
    typeof CourseLeaderboardStore.actionCreators &
    RouteComponentProps<{}>;

class CourseLeaderboard extends React.PureComponent<Props & OwnProps> {

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
    
    state = {
        tab: 0
    }

    tableColumns = [
            {name: 'Rank', options: {sort: false}},
            {name: 'Subject', options: {sort: false}},
            {name: 'Rating'},
        ]

    public componentDidMount() {
        this.setState({tab: 0});
    }

    switchTab() {
        this.setState({ tab: (this.state.tab + 1) % 2 });
    }

    getButtonStatus() {
        return (this.state.tab == 0) ? true : false
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

function mapStateToProps(state: ApplicationState, ownProps: OwnProps) {
    return {
        ...state.courseLeaderboardEntries,
        courseId: ownProps.courseId
    }
};

export default withRouter(
    connect(
        mapStateToProps,
        CourseLeaderboardStore.actionCreators
    )(CourseLeaderboard as any) as React.ComponentType<any>
);