import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps, withRouter } from 'react-router';
import { Button, Form, FormGroup, Label, Input, CustomInput, UncontrolledTooltip, UncontrolledAlert } from 'reactstrap';
import { Multiselect } from 'react-widgets'
import Rating from 'react-rating';
import 'react-widgets/dist/css/react-widgets.css';
import { ApplicationState } from '../../store';
import * as RateStore from '../../store/Rate/RateTeacher';
import * as TagStore from '../../store/Tags'

type Props =
    {
    tags: TagStore.TagsState
    rating: RateStore.RateTeacherState
    } &
    typeof RateStore.actionCreators &
    typeof TagStore.actionCreators &
    RouteComponentProps<{ courseId: string}>

class RateCourse extends React.PureComponent<Props> {
    public componentDidMount() {
        this.props.requestTags();
    }

    public componentDidUpdate() {
        this.props.requestTags();
    }

    render() {      
        return (
          <Form>
            {this.renderAlerts()}
            {this.renderStarRating()}
            {this.renderWouldTakeAgain()}
            <FormGroup>
              <Label>Level of difficulty</Label>
            </FormGroup>
            <FormGroup check inline>
              <Label>{this.props.rating.rating.levelOfDifficulty}</Label>
            </FormGroup>
            {this.renderLevelOfDifficultySlider()}
            <FormGroup>
              <Label for="comment">Comment</Label>
              <Input type="textarea" name="text" id="comment" onChange={event => this.props.changeComment(event.target.value)}/>
            </FormGroup>
            {this.renderTagsMultiselect()}
            <Button color="primary" onClick={() => this.props.submitReview() && this.onSubmitButtonPush() } >Submit</Button>{' '}
          </Form>
        );
      }
    
      private onSubmitButtonPush() {
        this.props.setCourseId(this.props.match.params.courseId)
        if(this.props.rating.rating.comment.length >= 30 && this.props.rating.rating.overallMark !== 0 && this.props.rating.rating.tags.length < 6 && this.props.rating.rating.levelOfDifficulty > 0 && this.props.rating.rating.courseId !== ''){
          this.props.sendRating()
          this.props.history.push(`/course-profile/${this.props.match.params.courseId}`)
        }
      }

      private renderStarRating() {
        return(
          <FormGroup>
            <Label>Overall rating</Label>
              <div style={{fontSize: 26}}>
                <Rating
                onChange={event => this.props.setOverallMark(event)}
                initialRating={this.props.rating.rating.overallMark}
                // emptySymbol={<img src="assets/images/star-empty.png" className="icon" />}
                // fullSymbol={<img src="assets/images/star-full.png" className="icon" />}
                />
              </div>
          </FormGroup>
        );        
      }//⛧

      private renderWouldTakeAgain() {
        return(
          <FormGroup tag="fieldset">
            <Label>Would you take this course again?</Label>
            <div>
              <FormGroup check inline>
                <Label check>
                  <CustomInput type="radio" id="exampleCustomRadio" name="customRadio" label="Yeah" onClick={() => this.props.setWouldTakeTeacherAgainTrue()} />
                </Label>
              </FormGroup>
              <FormGroup check inline>
                <Label check>
                  <CustomInput type="radio" id="exampleCustomRadio2" name="customRadio" label="Nah" onClick={() => this.props.setWouldTakeTeacherAgainFalse()} />
                </Label>
              </FormGroup>
            </div>
          </FormGroup>
        );   
      }

      private renderLevelOfDifficultySlider() {
        enum difficultyMessage {
            'Select one',
            'Very easy',
            'Easy',
            'Medium',
            'Hard',
            'Very hard',
        }
        return(
          <FormGroup check inline>
              <input type="range" className="custom-range0" id="customRange" 
              min="1" max="5" step="1"
              value={this.props.rating.rating.levelOfDifficulty}
              onChange={event => this.props.setLevelOfDifficulty(event.target.valueAsNumber)} >
              </input>
              <UncontrolledTooltip placement="right" target="customRange">
                {difficultyMessage[this.props.rating.rating.levelOfDifficulty]}
              </UncontrolledTooltip>
          </FormGroup>
        );   
      }    

      private renderTagsMultiselect() {
        return(
          <FormGroup>
              <Label>Tags</Label>
              <div>
                <Multiselect
                  dropUp
                  data={this.props.tags.tags}
                  value={this.props.rating.rating.tags}
                  textField='text'
                  placeholder='Your tags'
                  disabled={this.props.rating.rating.tags.length>=5 ? this.props.tags.tags.filter(x => !this.props.rating.rating.tags.includes(x)) : []}
                  onChange={value => this.props.changeTags(value)}
                />
              </div>
          </FormGroup>
        );   
      } 
      
      private renderAlerts() {
        var minCommentLength = 30
        var maxTagsCount = 5
        return(
          <div>
            <UncontrolledAlert color="info" fade={false} isOpen = {this.props.rating.rating.comment.length < minCommentLength && this.props.rating.submitButtonClicked} toggle = {false}>
              Comment length too short. It must be at least {minCommentLength} characters long.
            </UncontrolledAlert>
            <UncontrolledAlert color="info" fade={false} isOpen = {this.props.rating.rating.overallMark === 0 && this.props.rating.submitButtonClicked} toggle = {false}>
              You must select a rating.
            </UncontrolledAlert>
            <UncontrolledAlert color="info" fade={false} isOpen = {this.props.rating.rating.tags.length > maxTagsCount && this.props.rating.submitButtonClicked} toggle = {false}>
              You cannot select more than {maxTagsCount} tags.
            </UncontrolledAlert>
            <UncontrolledAlert color="info" fade={false} isOpen = {this.props.rating.rating.levelOfDifficulty === 0 && this.props.rating.submitButtonClicked} toggle = {false}>
              You must select a difficulty.
            </UncontrolledAlert>
          </div>
        )
      }
}

function mapStateToProps(state: ApplicationState) {
  return {
      tags: state.tags,
      rating: state.rate
  }
};

const actions = {
  ...RateStore.actionCreators,
  ...TagStore.actionCreators
}


export default withRouter(
    connect(mapStateToProps, actions)(RateCourse as any) as React.ComponentType<any>
);