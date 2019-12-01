import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps, withRouter } from 'react-router';
import { ApplicationState } from '../../store';
import * as TeachersStore from '../../store/Teachers';
import { Input, Table, Button, Spinner } from 'reactstrap';
import '../../extensions/StringExtensions'

interface OwnProps {
    teacherId: string,
};

type Props =
    TeachersStore.TeachersState &
    typeof TeachersStore.actionCreators &
    RouteComponentProps<{}>;

class Teachers extends React.PureComponent<Props & OwnProps> {

    state = {
        currentIndex: 20,
        canLoadMore: true,
        searchString: "",
    }

    public componentDidMount() {
        this.props.requestAllTeachers();
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
            this.loadMoreTeachers()
    }

    private loadMoreTeachers() {
        if (this.state.canLoadMore)
            this.setState({currentIndex: this.state.currentIndex + 20});
        if (this.state.currentIndex >= this.props.teachers.length)
            this.setState({canLoadMore: false})
    }

    private search() {
        this.props.clearAllTeachers();
        if (this.state.searchString == "") {
            this.setState({canLoadMore: true})
            this.props.requestAllTeachers()
        } else {
            this.setState({canLoadMore: false})
            this.props.searchTeacher(this.state.searchString)
        }
    }

    private searchChanged(searchString: string) {
        this.setState({searchString: searchString})
    }

    public render() {
        return (
            <React.Fragment>
                {this.renderTable()}
                {this.props.teachers.length == 0 && <h4 style={{textAlign: 'center'}}>No results</h4>}
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
                <div>
                    <h2>Academic Staff
                        {this.props.isLoading && <Spinner type="grow" color= "primary" style={{display: 'inline'}}></Spinner>}
                        <Input name="Search" id="searchBox" placeholder="Type here..." onChange={(e) => this.searchChanged(`${e.target.value}`)} 
                               style={{width: '20%', display:'inline', marginInlineStart:'50%'}}/>
                        <Button onClick={() => this.search()} color="primary" style={{display: 'inline', marginLeft: '24px'}}>Search</Button>
                    </h2>
                </div>
                <Table className="table table-striped" aria-labelledby="tabelLabel" size="sm">
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