import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { ApplicationState } from '../../store';
import * as RateStore from '../../store/Rate/Rate';
import { Button, Form, FormGroup, Label, Input, CustomInput, UncontrolledTooltip, UncontrolledAlert } from 'reactstrap';
import StarRatingComponent from 'react-star-rating-component';
import { Multiselect } from 'react-widgets'
import 'react-widgets/dist/css/react-widgets.css';
import RateTeacherActivities from './RateTeacherActivities';

type Props =
    RateStore.RateState &
    typeof RateStore.actionCreators &
    RouteComponentProps<{ teacherId: string}>

class RateTeacher extends React.PureComponent<Props> {
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
              <Label>{this.props.levelOfDifficulty}</Label>
            </FormGroup>
            {this.renderLevelOfDifficultySlider()}
            <FormGroup>
              <Label for="comment">Comment</Label>
              <Input type="textarea" name="text" id="comment" onChange={event => this.props.changeComment(event.target.value)}/>
            </FormGroup>
            {this.renderTagsMultiselect()}
            <RateTeacherActivities teacherId={this.props.match.params.teacherId} passSelectedTeacherActivities = {(value: string) => this.props.setCourseId(value)} />
            <Button color="primary" onClick={() => this.props.submitReview() && this.onSubmitButtonPush() } >Submit</Button>{' '}
          </Form>
        );
      }
    
      private onSubmitButtonPush() {
        this.props.setTeacherId(this.props.match.params.teacherId)
        if(this.props.comment.length >= 30 && this.props.overallMark !== 0 && this.props.tags.length < 6 && this.props.levelOfDifficulty > 0 && this.props.courseId !== ''){
          this.props.sendRating()
          this.props.history.push(`/teacher-profile/${this.props.match.params.teacherId}`)
        }
      }

      private renderStarRating() {
        return(
          <FormGroup>
            <Label>Overall rating</Label>
              <div style={{fontSize: 26}}>
                <StarRatingComponent 
                  name="rate1" 
                  starCount={5}
                  value={this.props.overallMark}
                  renderStarIcon={() => <span>☭</span>}
                  starColor="#f00"
                  onStarClick={event => this.props.setOverallMark(event)}
                />
              </div>
          </FormGroup>
        );        
      }//⛧

      private renderWouldTakeAgain() {
        return(
          <FormGroup tag="fieldset">
            <Label>Would you take this prof again?</Label>
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
        return(
          <FormGroup check inline>
              <input type="range" className="custom-range0" id="customRange" 
              min="1" max="5" step="1"
              value={this.props.levelOfDifficulty}
              onChange={event => this.props.setLevelOfDifficulty(event.target.valueAsNumber)} >
              </input>
              <UncontrolledTooltip placement="right" target="customRange">
                {this.difficultyMessage()}
              </UncontrolledTooltip>
          </FormGroup>
        );   
      }    

      private difficultyMessage() {
        switch (this.props.levelOfDifficulty) {
          case 0:
            return 'Select one'
          case 1: 
            return 'Very easy';
          case 2: 
            return 'Easy';
          case 3: 
            return 'Medium';
          case 4: 
            return 'Hard';
          case 5: 
            return 'Very hard';
        }
      }

      private renderTagsMultiselect() {
        return(
          <FormGroup>
              <Label>Tags</Label>
              <div>
                <Multiselect
                  dropUp
                  data={this.props.allTags}
                  value={this.props.tags}
                  textField='text'
                  placeholder='Your tags'
                  disabled={this.props.tags.length>=5 ? this.props.allTags.filter(x => !this.props.tags.includes(x)) : []}
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
            <UncontrolledAlert color="info" fade={false} isOpen = {this.props.comment.length < minCommentLength && this.props.submitButtonClicked} toggle = {false}>
              Comment length too short. It must be at least {minCommentLength} characters long.
            </UncontrolledAlert>
            <UncontrolledAlert color="info" fade={false} isOpen = {this.props.overallMark === 0 && this.props.submitButtonClicked} toggle = {false}>
              You must select a rating.
            </UncontrolledAlert>
            <UncontrolledAlert color="info" fade={false} isOpen = {this.props.tags.length > maxTagsCount && this.props.submitButtonClicked} toggle = {false}>
              You cannot select more than {maxTagsCount} tags.
            </UncontrolledAlert>
            <UncontrolledAlert color="info" fade={false} isOpen = {this.props.levelOfDifficulty === 0 && this.props.submitButtonClicked} toggle = {false}>
              You must select a difficulty.
            </UncontrolledAlert>
            <UncontrolledAlert color="info" fade={false} isOpen = {this.props.courseId === '' && this.props.submitButtonClicked} toggle = {false}>
              You must select an activity.
            </UncontrolledAlert>
          </div>
        )
      }
}
export default connect(
    (state: ApplicationState) => state.rate, // Selects which state properties are merged into the component's props
    RateStore.actionCreators // Selects which action creators are merged into the component's props
)(RateTeacher as any);
