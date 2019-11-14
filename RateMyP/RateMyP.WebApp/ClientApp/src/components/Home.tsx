import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';
import Button from '@material-ui/core/Button';
import ButtonGroup from '@material-ui/core/ButtonGroup';
import Grid from '@material-ui/core/Grid';
import { Parallax, Background } from 'react-parallax';
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
    inputPlaceholder: "Search for teachers"
  }

  textChangeHandler = (e: React.ChangeEvent<HTMLInputElement>) => this.setState({
    inputString: e.target.value
  })

  changeSearchToggle() {
    if (this.state.searchToggle) {
      this.setState({ searchToggle: false });
      this.setPlaceholderText();
    }
    else {
      this.setState({ searchToggle: true });
      this.setPlaceholderText();
    }
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
      this.props.history.push(`/browse/${(this.state.inputString === undefined) ? "" : "teacher=" + this.state.inputString}`);
    }
    else {
      this.props.history.push(`/browse/${(this.state.inputString === undefined) ? "" : "course=" + this.state.inputString}`);
    }
  }

  public render() {
    return (
      <div>
        <div style={{ position: 'absolute', left: 0 }}>
          <Parallax
            blur={{ min: -10, max: 30 }}
            bgImage={backgroundImage}
            strength={200}
          >
            <div style={{
              height: '600px',
              width: '100vw',
            }} />
          </Parallax>
        </div>
        <Grid
          container
          spacing={0}
          direction="column"
          alignItems="center"
          justify="center"
          style={{
            minHeight: '60vh',
          }}
        >
          <Grid>
            <ButtonGroup>
              <Button variant="text" color="primary" disabled={this.state.searchToggle} onClick={() => this.changeSearchToggle()}><strong>Staff</strong></Button>
              <Button variant="text" color="primary" disabled={!this.state.searchToggle} onClick={() => this.changeSearchToggle()}><strong>Courses</strong></Button>
            </ButtonGroup>
          </Grid>

          <Grid item xs={6}>
            <this.TextFieldRender />
          </Grid>

          <Grid>
            <Button variant="contained" color="primary" onClick={() => { this.redirectSearchToBrowsePage() }}>
              SEARCH
    </Button>
          </Grid>
        </Grid>

        <div style={{
          width: '100vw',
          height: '60vw',
          position: 'absolute',
          left: 0
        }}>
          <div style={{ textAlign: 'center' }}>
            <Grid
              container
              spacing={0}
              direction="column"
              alignItems="center"
              justify="center"
              style={{
                minHeight: '30vh',
              }}
            >

            </Grid>
          </div>
          <div style={{ position: 'absolute', left: 0, bottom: 0 }}>
            <div>
              <footer className="footer">
                <p>Komanda, kuri kažką padaro, Inc</p>
                <p>Vilnius University, MIF</p>
                <p>Our GitHub page: <a href="https://github.com/Konlador/ratemyp" target="_blank">https://github.com/Konlador/ratemyp</a></p>
              </footer>
            </div>
          </div>
        </div>


      </div>
    );
  }

  private TextFieldRender = () => {
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