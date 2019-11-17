import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps, withRouter } from 'react-router';
import { ApplicationState } from '../../store';
import * as TeachersStore from '../../store/Teachers';
import { Button, Spinner } from 'reactstrap';
import MUIDataTable, { SelectableRows } from 'mui-datatables';
import '../../extensions/StringExtensions'

interface OwnProps {
    teacherId: string,
    search: string
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
        searchText: this.props.search,
        onRowClick: (rowData: string[], rowState: {rowIndex: number, dataIndex: number}) => {
            !this.props.isLoading && this.props.history.push(`/teacher-profile/${rowData[4]}`);
          },
        onSearchOpen: () => {
            this.setState({previousIndex: this.state.currentIndex});
             if (this.state.canLoadMore) this.setState({currentIndex: this.props.teachers.length});
            this.setState({canLoadMore: false});
        },
        onSearchClose: () => {
            //this.setState({currentIndex: this.state.previousIndex});
            //if (this.state.currentIndex < this.props.teachers.length) this.setState({canLoadMore: true});
        },
        customSearch: (searchQuery:string, currentRow:any[], columns:any[]) => {
            let isFound = false;
            let matchString = currentRow[0].toString().concat(" ", currentRow[1].toString());
            if (matchString.toUpperCase().denationalize().includes(searchQuery.toUpperCase().denationalize()))
                isFound = true;
            return isFound;
        }
    };
    
    state = {
        currentIndex: 20,
        previousIndex: 20,
        canSearch: true,
        canLoadMore: true,
    }

    public componentDidMount() {
        this.props.requestAllTeachers();
        //window.addEventListener("scroll", () => this.handleScroll());
    }

    public componentWillUnmount() {
        //window.removeEventListener("scroll", () => this.handleScroll());
    }

    // private handleScroll = () => {
    //     const windowHeight = "innerHeight" in window ? window.innerHeight : document.documentElement.offsetHeight;
    //     const body = document.body;
    //     const html = document.documentElement;
    //     const docHeight = Math.max(body.scrollHeight, body.offsetHeight, html.clientHeight, html.scrollHeight, html.offsetHeight);
    //     const windowBottom = windowHeight + window.pageYOffset;
    //     console.log("windowBottom: " + windowBottom + ", docHeight: " + docHeight);
    //     if (windowBottom >= docHeight && !this.props.isLoading)
    //         this.loadMoreTeachers()
    // }

    private loadMoreTeachers() {
        if (this.state.canLoadMore)
            this.setState({currentIndex: this.state.currentIndex + 20});
        if (this.state.currentIndex >= this.props.teachers.length)
            this.setState({canLoadMore: false})
        //this.props.requestTeachers();
    }

    public render() {
        return (
            <React.Fragment>
                {this.renderTable()}
                {this.state.canLoadMore &&
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
                    data={this.props.teachers.slice(0, this.state.currentIndex).map((teacher: TeachersStore.Teacher,) => {
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