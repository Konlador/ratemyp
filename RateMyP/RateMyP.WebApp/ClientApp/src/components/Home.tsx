import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';
import Button from '@material-ui/core/Button';
import ButtonGroup from '@material-ui/core/ButtonGroup';
import Grid from '@material-ui/core/Grid';
import { Parallax } from 'react-parallax';
import "./footer.css";


type Props = RouteComponentProps<{}>;

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    container: {
      display: 'flex',
      flexWrap: 'wrap',
      justifyContent: 'center',
    },
    textField: {
      marginLeft: theme.spacing(1),
      marginRight: theme.spacing(1),
      width: "500px"
    },
    resize: {
      fontSize: 50
    },
  }),
);

const backgroundImage = require('../images/image.jpg');

class Home extends React.PureComponent<Props> {

  state = {
    inputString: undefined,
    searchToggle: true,
    inputPlaceholder: "Search for teachers",
    staffButtonStyle: { backgroundColor: '#F66A27', color: '#100E17' },
    courseButtonStyle: { backgroundColor: '#100E17', color: '#F66A27' }
  }

  textChangeHandler = (e: React.ChangeEvent<HTMLInputElement>) => this.setState({
    inputString: e.target.value
  })

  changeButtonStyle() {
    var tmp = this.state.courseButtonStyle;
    this.setState({
      courseButtonStyle: this.state.staffButtonStyle,
      staffButtonStyle: tmp
    })
  }

  changeSearchToggle() {
    if (this.state.searchToggle) {
      this.setState({ searchToggle: false });
      this.setPlaceholderText();
    }
    else {
      this.setState({ searchToggle: true });
      this.setPlaceholderText();
    }
    this.changeButtonStyle();
  }

  setButtonStatus() {
    return (this.state.searchToggle === true) ? true : false
  }
  setPlaceholderText() {
    if (this.state.searchToggle) {
      this.setState({
        inputPlaceholder: "Search for courses",
      });
    }
    else {
      this.setState({
        inputPlaceholder: "Search for teachers",
      });
    }
  }

  redirectSearchToBrowsePage() {
    if (this.state.searchToggle) {
      this.props.history.push(`/browse/teacher${(this.state.inputString === undefined) ? "" : "=" + this.state.inputString}`);
    }
    else {
      this.props.history.push(`/browse/course${(this.state.inputString === undefined) ? "" : "=" + this.state.inputString}`);
    }
  }

  public render() {
    return (
      <div style={{ backgroundColor: '#100E17', width: '100vw', height: 'auto', display: 'flex', position: 'absolute', left: 0 }}>
        <div style={{display: 'flex', position: 'absolute'}}>
          <Parallax bgImage={backgroundImage} strength={300}>
            <div style={{ height: '30vw', width: '100vw'}} />
          </Parallax>
        </div>

        <div className="container">

            <Grid
              container
              spacing={2}
              direction="column"
              alignItems="center"
              justify="center"
              style={{
                minHeight: '60vh',
              }}
            >

              <Grid item>
                <ButtonGroup>
                  <Button variant="contained" color="default" style={this.state.staffButtonStyle} disabled={this.state.searchToggle} onClick={() => this.changeSearchToggle()}><strong>Staff</strong></Button>
                  <Button variant="contained" color="primary" style={this.state.courseButtonStyle} disabled={!this.state.searchToggle} onClick={() => this.changeSearchToggle()}><strong>Courses</strong></Button>
                </ButtonGroup>
              </Grid>

              <Grid item>
                <this.SearchTextField />
              </Grid>

              <Grid item>
                <Button variant="contained" color="primary" style={{ backgroundColor: '#F66A27', color: '#100E17' }} onClick={() => { this.redirectSearchToBrowsePage() }}>
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
              }}
            >

              <Grid item>
                <div style={{
                  width: '100vw',
                  height: '600px',
                  left: 0,
                }}>
                </div>
              </Grid>
            </Grid>

            <Grid
              container
              spacing={0}
              direction="row"
              justify="center"
            >

              <Grid item>
                <div>
                  <footer className="footer">
                    <p>Komanda, kuri kažką padaro, Inc</p>
                    <p>Vilnius University, MIF</p>
                    <p>Our GitHub page: <a href="https://github.com/Konlador/ratemyp" target="_blank">https://github.com/Konlador/ratemyp</a></p>
                  </footer>
                </div>
              </Grid>

            </Grid>
        </div>
      </div>
    );
  }

  private SearchTextField = () => {
    const classes = useStyles();
    return (
      <form className={classes.container} noValidate autoComplete="off">
        <div>
          <TextField
            value={this.state.inputString}
            placeholder={this.state.inputPlaceholder}
            onChange={(event: React.ChangeEvent<HTMLInputElement>) => this.textChangeHandler(event)}
            id="standard-basic"
            className={classes.textField}
            margin="normal"
            InputProps={{
              classes: {
                input: classes.resize,
              },
              spellCheck: false
            }}
          />
        </div>
      </form>
    );
  }

};

export default connect()(Home);