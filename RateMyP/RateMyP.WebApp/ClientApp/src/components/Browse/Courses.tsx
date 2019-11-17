import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps, withRouter } from 'react-router';
import { ApplicationState } from '../../store';
import * as CoursesStore from '../../store/Courses';
import { Button, Spinner } from 'reactstrap';
import MUIDataTable, { SelectableRows } from 'mui-datatables';
import '../../extensions/StringExtensions';

interface OwnProps {
    search: string
};

type Props =
    CoursesStore.CoursesState &
    typeof CoursesStore.actionCreators &
    RouteComponentProps<{}>;

class Courses extends React.PureComponent<Props & OwnProps> {

    tableOptions = {
        print: false,
        download: false,
        viewColumns: false,
        selectableRows: "none" as SelectableRows,
        pagination: false,
        sort: false,
        searchText: this.props.search,
        onRowClick: (rowData: string[], rowState: {rowIndex: number, dataIndex: number}) => {
            !this.props.isLoading && this.props.history.push(`/course-profile/${rowData[4]}`);
          },
        customSearch: (searchQuery:string, currentRow:any[], columns:any[]) => {
            let isFound = false;
            let matchString = currentRow[0];
            if (matchString.toUpperCase().denationalize().includes(searchQuery.toUpperCase().denationalize())) {
                isFound = true;
            }       
            return isFound;
        }
    };
    
    state = {
        data: [],
    }

    public componentDidMount() {
        if (this.props.courses.length === 0) this.props.requestCourses();
    }

    private loadMoreCourses() {
        this.props.requestCourses();
    }

    public render() {
        return (
            <React.Fragment>
                {this.renderTable()}
                {this.props.canLoadMore && 
                <div>
                    <Button onClick={() => this.loadMoreCourses()} color="primary" style={{ 
                        position: 'absolute', 
                        left: '46%', 
                        marginTop: '16px', 
                        marginBottom: '16px'}}>
                        Load More!
                    </Button>
                    {this.props.isLoading && <Spinner type="grow" color ="primary" style={{
                        position: 'absolute', 
                        left: '40%', 
                        marginTop: '16px'}}/>}                    
                </div>}
            </React.Fragment>
        );
    }

    private renderTable() {
        return (
            <div>
                <MUIDataTable
                    title={"Course List"}
                    data={this.props.courses.map((course: CoursesStore.Course) => {
                    return [
                        course.name,
                        CoursesStore.CourseType[course.courseType],
                        course.credits,
                        course.faculty,
                        course.id
                    ]})
                    }
                    columns={
                        [
                            {name: 'Name', options: { filter: false}},
                            {name: 'Type'},
                            {name: 'Credits'},
                            {name: 'Faculty'},
                            {name: 'Id', options: {filter: false, display: 'excluded'}}
                        ]
                    }
                    options={this.tableOptions}
                />
            </div>
        )
    }
}

export default withRouter(
    connect(
        (state: ApplicationState) => state.courses,
        CoursesStore.actionCreators
    )(Courses as any) as React.ComponentType<any>
);
