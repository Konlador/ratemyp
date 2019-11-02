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

type RateProps =
    RateStore.RateState &
    typeof RateStore.actionCreators &
    RouteComponentProps<{ teacherId: string}>

class Rate extends React.PureComponent<RateProps> {
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
              <Label>{this.props.LevelOfDifficulty}</Label>
            </FormGroup>
            {this.renderLevelOfDifficultySlider()}
            <FormGroup>
              <Label for="comment">Comment</Label>
              <Input type="textarea" name="text" id="comment" onChange={event => this.props.changeComment(event.target.value)}/>
            </FormGroup>
            {this.renderTagsMultiselect()}
            <RateTeacherActivities teacherId={this.props.match.params.teacherId} passSelectedTeacherActivities = {value => this.props.setTeacherActivity(value)} />
            <Button color="primary" onClick={() => this.props.submitReview() && this.onSubmitButtonPush() } >Submit</Button>{' '}
          </Form>
        );
      }
    
      private onSubmitButtonPush() {
        this.props.setTeacherId(this.props.match.params.teacherId)
        if(this.props.Comment.length >= 30 && this.props.OverallMark != 0 && this.props.Tags.length < 6 && this.props.LevelOfDifficulty > 0 && this.props.TeacherActivity != undefined){
          this.props.sendRating()
          this.props.history.push(`/profile/${this.props.match.params.teacherId}`)
        }
      }

      private renderStarRating() {
        return(
          <FormGroup>
            <Label>Overall rating</Label>
              <div style={{fontSize: 32}}>
                <StarRatingComponent 
                  name="rate1" 
                  starCount={5}
                  value={this.props.OverallMark}
                  renderStarIcon={() => <span>♡</span>}
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
              value={this.props.LevelOfDifficulty}
              onChange={event => this.props.setLevelOfDifficulty(event.target.valueAsNumber)} >
              </input>
              <UncontrolledTooltip placement="right" target="customRange">
                {this.difficultyMessage()}
              </UncontrolledTooltip>
          </FormGroup>
        );   
      }    

      private difficultyMessage() {
        switch (this.props.LevelOfDifficulty) {
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
                  data={this.props.AllTags}
                  value={this.props.Tags}
                  textField='text'
                  placeholder='Your tags'
                  disabled={this.props.Tags.length>=5 ? this.props.AllTags.filter(x => !this.props.Tags.includes(x)) : []}
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
            <UncontrolledAlert color="info" fade={false} isOpen = {this.props.Comment.length < minCommentLength && this.props.SubmitButtonClicked} toggle = {false}>
              Comment length too short. It must be at least {minCommentLength} characters long.
            </UncontrolledAlert>
            <UncontrolledAlert color="info" fade={false} isOpen = {this.props.OverallMark == 0 && this.props.SubmitButtonClicked} toggle = {false}>
              You must select a rating.
            </UncontrolledAlert>
            <UncontrolledAlert color="info" fade={false} isOpen = {this.props.Tags.length > maxTagsCount && this.props.SubmitButtonClicked} toggle = {false}>
              You cannot select more than {maxTagsCount} tags.
            </UncontrolledAlert>
            <UncontrolledAlert color="info" fade={false} isOpen = {this.props.LevelOfDifficulty == 0 && this.props.SubmitButtonClicked} toggle = {false}>
              You must select a difficulty.
            </UncontrolledAlert>
            <UncontrolledAlert color="info" fade={false} isOpen = {this.props.TeacherActivity == undefined && this.props.SubmitButtonClicked} toggle = {false}>
              You must select an activity.
            </UncontrolledAlert>
          </div>
        )
      }
}
export default connect(
    (state: ApplicationState) => state.rate, // Selects which state properties are merged into the component's props
    RateStore.actionCreators // Selects which action creators are merged into the component's props
)(Rate as any);
