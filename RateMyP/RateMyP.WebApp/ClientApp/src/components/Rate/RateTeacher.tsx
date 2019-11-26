import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { Button, Form, FormGroup, Label, Input, CustomInput, UncontrolledTooltip, UncontrolledAlert } from 'reactstrap';
import { Multiselect } from 'react-widgets'
import Rating from 'react-rating';
import 'react-widgets/dist/css/react-widgets.css';
import RateTeacherActivities from './RateTeacherActivities';
import * as TagStore from '../../store/Tags'
import { ApplicationState } from '../../store';
import * as RateStore from '../../store/Rate/Rate';

type Props =
    {
    tags: TagStore.TagsState
    rating: RateStore.RateState
    } &
    typeof RateStore.actionCreators &
    typeof TagStore.actionCreators &
    RouteComponentProps<{ teacherId: string }>

class RateTeacher extends React.PureComponent<Props> {
    public componentDidMount() {
        this.props.requestTags();
    }

    public componentDidUpdate() {
        this.props.requestTags();
    }

    public componentWillUnmount() {
        this.props.clearStore();
    }

    public render() {
        return (
            <React.Fragment>
                {this.renderForm()}
            </React.Fragment>
        );
    }

    private renderForm() {
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
                <RateTeacherActivities teacherId={this.props.match.params.teacherId} passSelectedTeacherActivities = {(value: string) => this.props.setCourseId(value)} />
                <Button color="primary" onClick={() => this.props.submitReview() && this.onSubmitButtonPush() } >Submit</Button>{' '}
            </Form>
        );
    }

    private onSubmitButtonPush() {
        this.props.setTeacherId(this.props.match.params.teacherId)
        this.props.setRatingType(0)
        if(this.props.rating.rating.comment.length >= 30 &&
            this.props.rating.rating.overallMark !== 0 &&
            this.props.rating.rating.tags.length < 6 &&
            this.props.rating.rating.levelOfDifficulty > 0 &&
            this.props.rating.rating.courseId !== ''){
            this.props.sendRating()
            this.props.history.push(`/teacher-profile/${this.props.match.params.teacherId}`)
        }
    }

      private renderStarRating() {
        return(
          <FormGroup>
            <Label>Overall rating</Label>
              <div style={{fontSize: 26}}>
                <Rating
                onClick={event => this.props.setOverallMark(event)}
                initialRating={this.props.rating.rating.overallMark}
                emptySymbol={<img src={"api/images/w_50,h_50,f_png,o_30,bo_2px_solid_white/teacher=" + this.props.match.params.teacherId} className="icon" />}
                fullSymbol={<img src={"api/images/w_50,h_50,f_png,bo_2px_solid_white/teacher=" + this.props.match.params.teacherId} className="icon" />}
              />
            </div>
        </FormGroup>
      );        
    }

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
      enum DifficultyMessage {
          'Select one',
          'Paltry effort',
          'Some work',
          'Not great, not terrible',
          'Heavy lifting',
          'Fight for life',
      }
      return (
        <FormGroup check inline>
            <input type="range" className="custom-range0" id="customRange" 
            min="1" max="5" step="1"
            value={this.props.rating.rating.levelOfDifficulty}
            onChange={event => this.props.setLevelOfDifficulty(event.target.valueAsNumber)} >
            </input>
            <UncontrolledTooltip placement="right" target="customRange">
                {DifficultyMessage[this.props.rating.rating.levelOfDifficulty]}
            </UncontrolledTooltip>
        </FormGroup>
      );   
    }    

    private renderTagsMultiselect() {
        var tags = this.props.tags.tags.filter(function (item) {
            return (item.type & TagStore.TagType.Teacher) ===  TagStore.TagType.Teacher
        });
        return (
            <FormGroup>
                <Label>Tags</Label>
                <div>
                    <Multiselect
                    dropUp
                    data={tags}
                    value={this.props.rating.rating.tags}
                    textField='text'
                    placeholder='Your tags'
                    disabled={this.props.rating.rating.tags.length>=5 ? tags.filter(x => !this.props.rating.rating.tags.includes(x)) : []}
                    onChange={value => this.props.changeTags(value)} />
                </div>
            </FormGroup>
        );   
    } 
    
    private renderAlerts() {
        var minCommentLength = 30;
        var maxTagsCount = 5;
        return (
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
                <UncontrolledAlert color="info" fade={false} isOpen = {this.props.rating.rating.courseId === '' && this.props.rating.submitButtonClicked} toggle = {false}>
                    You must select an activity.
                </UncontrolledAlert>
            </div>
      );
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


export default connect(mapStateToProps, actions)(RateTeacher as any);