import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import TextField from '@material-ui/core/TextField';
import Button from '@material-ui/core/Button';
import ButtonGroup from '@material-ui/core/ButtonGroup';
import Grid from '@material-ui/core/Grid';
import { Parallax } from 'react-parallax';
import "./Home.css";

const backgroundImage = require('../../images/image.jpg');

enum SearchTypeButton {
    Staff,
    Course
}

type Props = RouteComponentProps<{}>;

interface State {
    search: string | undefined,
    searchType: "teacher" | "course",
    searchPlaceholder: string,
    staffButtonStyle: any,
    courseButtonStyle: any,
    searchToggle: string
};

class Home extends React.Component<Props, State> {

    constructor(props: Props) {
        super(props);

        this.state = {
            search: undefined,
            searchType: "teacher",
            searchPlaceholder: "Search for teachers",
            staffButtonStyle: { background: 'linear-gradient(90deg, rgba(131,58,180,1) 0%, rgba(253,29,29,1) 0%, rgba(252,176,69,1) 100%)', color: '#100E17' },
            courseButtonStyle: { backgroundColor: '#100E17', color: '#F66A27' },
            searchToggle: SearchTypeButton[0],
        };
    };

    private textChangeHandler = (e: React.ChangeEvent<HTMLInputElement>) => this.setState({
        search: e.target.value
    })

    private changeButtonStyle() {
        this.setState({
            courseButtonStyle: this.state.staffButtonStyle,
            staffButtonStyle: this.state.courseButtonStyle
        })
    }

    private changeSearchToggle() {
        this.setPlaceholderText();
        this.changeSearchTypeButtonToggle();
        this.changeSearchType();
        this.changeButtonStyle();
    }

    private changeSearchTypeButtonToggle() {
        if (this.state.searchToggle === SearchTypeButton[0]) {
            this.setState({ searchToggle: SearchTypeButton[1] });
        }
        else if (this.state.searchToggle === SearchTypeButton[1]) {
            this.setState({ searchToggle: SearchTypeButton[0] });
        }
    }

    private changeSearchType() {
        if (this.state.searchType === "teacher")
            this.setState({ searchType: "course" });
        else if (this.state.searchType === "course")
            this.setState({ searchType: "teacher" });
    }

    private setPlaceholderText() {
        if (this.state.searchToggle === SearchTypeButton[1]) {
            this.setState({
                searchPlaceholder: "Search for teachers",
            });
        }
        else if (this.state.searchToggle === SearchTypeButton[0]) {
            this.setState({
                searchPlaceholder: "Search for courses",
            });
        }
    }

    private redirectSearchToBrowsePage() {
        this.props.history.push({
            pathname: '/browse',
            state: {
                search: this.state.search,
                searchType: this.state.searchType,
            }
        })
    }

    public render() {
        return (
            <div className="home-container">
                <div className="home-parallax" style={{ zIndex: 0 }}>
                    <Parallax bgImage={backgroundImage} strength={300}>
                        <div className="home-parallax-dimensions" />
                    </Parallax>
                </div>
                <div className="nzn-container">
                    <Grid
                        container
                        spacing={1}
                        direction="column"
                        alignItems="center"
                        justify="center"
                        style={{
                            minHeight: '60vh',
                        }}>
                        <Grid item>
                            <ButtonGroup>
                                <Button
                                    variant="contained"
                                    color="default"
                                    style={this.state.staffButtonStyle}
                                    disabled={this.state.searchToggle === SearchTypeButton[0]}
                                    onClick={() => this.changeSearchToggle()}>
                                        <strong>Staff</strong>
                                </Button>
                                <Button variant="contained" color="primary" style={this.state.courseButtonStyle} disabled={this.state.searchToggle === SearchTypeButton[1]} onClick={() => this.changeSearchToggle()}><strong>Courses</strong></Button>
                            </ButtonGroup>
                        </Grid>
                        <Grid item style={{ zIndex: 1 }}>
                            {this.randerSearchTextField()}
                        </Grid>
                        <Grid item>
                            <Button variant="contained" color="primary" style={{ background: 'linear-gradient(90deg, rgba(131,58,180,1) 0%, rgba(253,29,29,1) 0%, rgba(252,176,69,1) 100%)', color: '#100E17' }} onClick={() => { this.redirectSearchToBrowsePage() }}>
                                <strong>SEARCH</strong>
                            </Button>
                        </Grid>
                    </Grid>
                    <Grid
                        container
                        spacing={0}
                        direction="row"
                        alignItems="center"
                        justify="center"
                        style={{
                            minHeight: '600px',
                        }}>
                        <Grid item >
                            <div style={{
                                width: '100vw',
                                height: '600px',
                                left: 0,
                            }} />
                        </Grid>
                    </Grid>
                    <Grid
                        container
                        spacing={0}
                        direction="row"
                        justify="center">
                        <Grid item>
                            <footer className="footer">
                                <p>Komanda, kuri kažką padaro, Inc</p>
                                <p>Vilnius University, MIF</p>
                                <p>Our GitHub page: <a href="https://github.com/Konlador/ratemyp" target="_blank">https://github.com/Konlador/ratemyp</a></p>
                            </footer>
                        </Grid>
                    </Grid>
                </div>
            </div>
        );
    }

    private randerSearchTextField() {
        return (
            <form className="search-container" noValidate autoComplete="off">
                <div>
                    <TextField
                        className="search-text-field"
                        value={this.state.search}
                        placeholder={this.state.searchPlaceholder}
                        style={{ borderRadius: 25 }}
                        onChange={(event: React.ChangeEvent<HTMLInputElement>) => this.textChangeHandler(event)}
                        id="standard-basic"
                        margin="normal"
                        InputProps={{ spellCheck: false }}
                    />
                </div>
            </form>
        );
    }
};

export default Home;