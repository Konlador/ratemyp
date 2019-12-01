import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps, withRouter } from 'react-router';
import { ApplicationState } from '../../store';
import * as CoursesStore from '../../store/Courses';
import { Input, Button, Spinner, Table } from 'reactstrap';
import '../../extensions/StringExtensions';

interface OwnProps {
    courseId: string,
};

type Props =
    CoursesStore.CoursesState &
    typeof CoursesStore.actionCreators &
    RouteComponentProps<{}>;

class Courses extends React.PureComponent<Props & OwnProps> {

    state = {
        currentIndex: 20,
        canLoadMore: true,
        searchString: "",
    }

    public componentDidMount() {
        this.props.requestAllCourses();
        window.addEventListener("scroll", () => this.handleScroll());
    }

    public componentWillUnmount() {
        window.removeEventListener("scroll", () => this.handleScroll());
    }

    private handleScroll = () => {
        const windowHeight = "innerHeight" in window ? window.innerHeight : document.documentElement.offsetHeight;
        const body = document.body;
        const html = document.documentElement;
        const docHeight = Math.max(body.scrollHeight, body.offsetHeight, html.clientHeight, html.scrollHeight, html.offsetHeight);
        const windowBottom = windowHeight + window.pageYOffset;
        console.log("windowBottom: " + windowBottom + ", docHeight: " + docHeight);
        if (windowBottom >= docHeight && !this.props.isLoading)
            this.loadMoreCourses()
    }

    private loadMoreCourses() {
        if (this.state.canLoadMore)
            this.setState({currentIndex: this.state.currentIndex + 20});
        if (this.state.currentIndex >= this.props.courses.length)
            this.setState({canLoadMore: false})
    }

    public render() {
        return (
            <React.Fragment>
                {this.renderTable()}
                {this.props.courses.length == 0 && <h4 style={{textAlign: 'center'}}>No results</h4>}
                {this.state.canLoadMore && 
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

    private search() {
        this.props.clearAllCourses();
        if (this.state.searchString == "") {
            this.setState({canLoadMore: true})
            this.props.requestAllCourses()
        } else {
            this.setState({canLoadMore: false})
            this.props.searchCourse(this.state.searchString);
        }
    }

    private searchChanged(searchString: string) {
        this.setState({searchString: searchString})
    }

    private renderTable() {
        return (
            <div>
                <div>
                    <h2>Courses
                        {this.props.isLoading && <Spinner type="grow" color= "primary" style={{display: 'inline'}}></Spinner>}
                        <Input name="Search" id="searchBox" placeholder="Type here..." onChange={(e) => this.searchChanged(`${e.target.value}`)} 
                               style={{width: '20%', display:'inline', marginInlineStart:'59%'}}/>
                        <Button onClick={() => this.search()} color="primary" style={{display: 'inline', marginLeft: '24px'}}>Search</Button>
                    </h2>
                </div>
                <Table className="table table-striped" aria-labelledby="tabelLabel" size="sm">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Type</th>
                            <th>Credits</th>
                            <th>Faculty</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.props.courses.slice(0, this.state.currentIndex).map((course: CoursesStore.Course) =>
                            <tr key={course.id} onClick={() => this.props.history.push(`/course-profile/${course.id}`)}>
                                <td>{course.name}</td>
                                <td>{CoursesStore.CourseType[course.courseType]}</td>
                                <td>{course.credits}</td>
                                <td>{course.faculty}</td>
                            </tr>
                        )}
                    </tbody>
                </Table>
            </div>
        )
    }
}

function mapStateToProps(state: ApplicationState, ownProps: OwnProps) {
    return {
        ...state.courses,
        courseId: ownProps.courseId
    }
};

export default withRouter(
    connect(
        mapStateToProps,
        CoursesStore.actionCreators
    )(Courses as any) as React.ComponentType<any>
);
