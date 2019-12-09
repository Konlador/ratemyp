import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps, withRouter } from 'react-router';
import { ApplicationState } from '../../store';
import * as TeachersStore from '../../store/Teachers';
import { Input, Table, Button, Spinner } from 'reactstrap';
import './Teachers.css';

interface OwnProps {
    teacherId: string,
    search: string,
};

type Props =
    TeachersStore.TeachersState &
    typeof TeachersStore.actionCreators &
    RouteComponentProps<{}>;

class Teachers extends React.PureComponent<Props & OwnProps> {

    constructor(props: Props & OwnProps) {
        super(props);
        this.keyPressed = this.keyPressed.bind(this);
        this.state = {
            currentIndex: 20,
            canLoadMore: true,
            searchString: "",
        };
    }

    state = {
        currentIndex: 20,
        canLoadMore: true,
        searchString: "",
    }

    public componentDidMount() {
        if (this.props.search !== undefined) {
            this.props.clearAllTeachers();
            this.updateSearchText(this.props.search);
            this.props.searchTeacher(this.props.search);
            if (this.props.teachers.length < 20) this.setState({canLoadMore: false});
        } else {
            this.props.clearAllTeachers();
            this.props.requestAllTeachers();
        }
        window.addEventListener("scroll", () => this.handleScroll());
    }

    private updateSearchText(searchString: string) {
        this.setState({searchString: searchString});
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
        if (windowBottom >= docHeight && !this.props.isLoading)
            this.loadMoreTeachers()
    }

    private loadMoreTeachers() {
        if (this.state.canLoadMore)
            this.setState({currentIndex: this.state.currentIndex + 20});
        if (this.state.currentIndex >= this.props.teachers.length)
            this.setState({canLoadMore: false});
    }

    private search() {
        this.props.clearAllTeachers();
        if (this.state.searchString == "") {
            this.setState({canLoadMore: true});
            this.props.requestAllTeachers();
        } else {
            this.props.searchTeacher(this.state.searchString);
        }
    }

    private searchChanged(searchString: string) {
        this.setState({searchString: searchString});
    }

    keyPressed = (e: any) => {
        if (e.key === "Enter") {
            this.search();
        }
    }

    public render() {
        return (
            <React.Fragment>
                <div className="teachers-top">
                    <h2 id="academic-staff-label">
                        Academic Staff {this.props.isLoading && <Spinner type="grow" color= "primary" style={{display: 'inline'}}></Spinner>}
                    </h2>
                    <Input id="teacher-search-box" value={this.state.searchString} name="Search" placeholder="Type here..." 
                        onChange={(e) => this.searchChanged(`${e.target.value}`)} 
                        onKeyPress={this.keyPressed}/>
                    <Button id="teacher-search-button" onClick={() => this.search()} color="primary">Search</Button>
                </div>
                {this.renderTable()}
                {this.props.teachers.length == 0 && <h4 style={{textAlign: 'center'}}>No results</h4>}
                {this.state.canLoadMore && (this.props.teachers.length >= 20) &&
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
        )
    }

    private renderTable() {
        return (
            <div>
                <Table className="table table-striped" aria-labelledby="tabelLabel" size="sm" hover>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Surname</th>
                            <th>Rank</th>
                            <th>Faculty</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.props.teachers.slice(0, this.state.currentIndex).map((teacher: TeachersStore.Teacher) =>
                            <tr key={teacher.id} onClick={() => this.props.history.push(`/teacher-profile/${teacher.id}`)}>
                                <td>{teacher.firstName}</td>
                                <td>{teacher.lastName}</td>
                                <td>{teacher.rank}</td>
                                <td>{teacher.faculty}</td>
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