import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps, withRouter } from 'react-router';
import { ApplicationState } from '../../store';
import * as CoursesStore from '../../store/Courses';
import { Button, Spinner } from 'reactstrap';
import MUIDataTable, { SelectableRows } from 'mui-datatables';

type CoursesProps =
    CoursesStore.CoursesState &
    typeof CoursesStore.actionCreators &
    RouteComponentProps<{}>;

class Courses extends React.PureComponent<CoursesProps> {

    tableOptions = {
        print: false,
        download: false,
        viewColumns: false,
        selectableRows: "none" as SelectableRows,
        pagination: false,
        sort: false,
    };
    
    state = {
        data: [],
    }

    public componentDidMount() {
        if (this.props.courses.length === 0) this.props.requestCourses();
        window.addEventListener("scroll", this.onScroll, false);
    }

    private loadMoreCourses() {
        this.props.requestCourses();
    }

    public componentWillUnmount() {
        window.removeEventListener("scroll", this.onScroll, false);
    }

    onScroll = () => {
        if (this.hasReachedBottom() && !this.props.isLoading) {
            this.loadMoreCourses()
        }
    };

    hasReachedBottom() {
        console.log(document.body.offsetHeight, document.body.scrollTop, document.body.scrollHeight)
        return (
            document.body.getBoundingClientRect().bottom < window.innerHeight
        );
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
                    ]})
                    }
                    columns={
                        [
                            {name: 'Name', options: { sortDirection: 'asc', filter: false}},
                            {name: 'Type'},
                            {name: 'Credits'},
                            {name: 'Faculty'}
                        ]
                    }
                    options={this.tableOptions}
                />
            </div>
        )
    }

}

export default connect(
    (state: ApplicationState) => state.courses, // Selects which state properties are merged into the component's props
    CoursesStore.actionCreators // Selects which action creators are merged into the component's props
)(Courses as any);
