import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps, withRouter } from 'react-router';
import { ApplicationState } from '../../store';
import * as TeachersStore from '../../store/Teachers';
import { Button, Spinner } from 'reactstrap';
import MUIDataTable, { SelectableRows } from 'mui-datatables';
import '../../extensions/StringExtensions'

interface OwnProps {
    teacherId: string
};

type Props =
    TeachersStore.TeachersState &
    typeof TeachersStore.actionCreators &
    RouteComponentProps<{}>;

class Teachers extends React.PureComponent<Props & OwnProps> {

    tableOptions = {
        print: false,
        download: false,
        viewColumns: false,
        selectableRows: "none" as SelectableRows,
        pagination: false,
        sort: false,
        onRowClick: (rowData: string[], rowState: {rowIndex: number, dataIndex: number}) => {
            console.log(rowData, rowState);
            !this.props.isLoading && this.props.history.push(`/teacher-profile/${rowData[4]}`);
          },
        customSearch: (searchQuery:string, currentRow:any[], columns:any[]) => {
            let isFound = false;
            let matchString = currentRow[0].toString().concat(" ", currentRow[1].toString());
            console.log(searchQuery, currentRow, columns, matchString);
            if (matchString.toUpperCase().denationalize().includes(searchQuery.toUpperCase().denationalize())) {
                isFound = true;
            }       
            return isFound;
        }
    };
    
    state = {
        data: []
    }

    public componentDidMount() {
        if (this.props.teachers.length === 0) this.props.requestTeachers();
        window.addEventListener("scroll", this.onScroll, false);
    }

    public componentWillUnmount() {
        window.removeEventListener("scroll", this.onScroll, false);
    }

    onScroll = () => {
        if (this.hasReachedBottom() && !this.props.isLoading) {
            this.loadMoreTeachers()
        }
    };

    hasReachedBottom() {
        console.log(document.body.offsetHeight, document.body.scrollTop, document.body.scrollHeight)
        return (
            document.body.getBoundingClientRect().bottom < window.innerHeight
        );
    }

    private loadMoreTeachers() {
        this.props.requestTeachers();
    }

    public render() {
        return (
            <React.Fragment>
                {this.renderTable()}
                {this.props.canLoadMore &&
                <Button onClick={() => this.loadMoreTeachers()} color="primary" style={{
                    position: 'absolute', 
                    left: '46%', 
                    marginTop: '16px', 
                    marginBottom: '16px'
                }}>Load More!
                </Button>}
                {this.props.isLoading && <Spinner type="grow" color ="primary" style={{
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
                    title={"Academic Staff"}
                    data={this.props.teachers.map((teacher: TeachersStore.Teacher) => {
                    return [
                        teacher.firstName,
                        teacher.lastName,
                        teacher.rank,
                        teacher.faculty,
                        teacher.id
                    ]})
                    }
                    columns={
                        [
                            {name: 'Name', options: { filter: false }},
                            {name: 'Surname', options: { filter: false }},
                            {name: 'Rank'},
                            {name: 'Faculty'},
                            {name: 'Id', options: { display: 'excluded', filter: false}}
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
        ...state.teachers,
        teacherId: ownProps.teacherId
    }
};

export default withRouter(
    connect(
        mapStateToProps,
        TeachersStore.actionCreators
    )(Teachers as any) as React.ComponentType<any>
);