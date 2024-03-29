import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { Button, Form, FormGroup, Label, Input, UncontrolledAlert, Table } from 'reactstrap';
import { ApplicationState } from '../../store';
import * as RatingReportCreationStore from '../../store/Reports/RatingReportCreation';
import * as TeacherCoursesStore from '../../store/Teacher/TeacherCourses';
import * as RatingsStore from '../../store/Ratings'
import * as TagsStore from '../../store/Tags';

type Props =
    {
    report: RatingReportCreationStore.RatingReportCreationState,
    courses: TeacherCoursesStore.TeacherCoursesState
    } &
    typeof RatingReportCreationStore.actionCreators &
    typeof TeacherCoursesStore.actionCreators &
    RouteComponentProps<{ ratingId: string }>

class RatingReport extends React.PureComponent<Props> {
    public componentDidMount() {
        this.props.requestRating(this.props.match.params.ratingId);
    }

    public componentDidUpdate() {
        if (this.props.report.rating)
            this.props.requestTeacherCourses(this.props.report.rating.teacherId);
    }

    public render() {      
        return (
            <React.Fragment>
                <Form>
                    {this.renderAlerts()}
                    {this.renderRating()}
                    <FormGroup>
                        <Label style={{fontSize: 30}}>Report a Student's rating</Label>
                    </FormGroup>
                    <FormGroup>
                        <Label for="report">What is wrong with this rating?</Label>
                        <Input type="textarea" name="text" id="report" onChange={event => this.props.changeReason(event.target.value)}/>
                    </FormGroup>
                    <Button color="primary" onClick={() => this.props.submitReport() && this.onSubmitButtonPush() }>Submit</Button>{' '}
                </Form>
            </React.Fragment>
        );
      }

    private onSubmitButtonPush() {
        this.props.setRatingId(this.props.match.params.ratingId);
        if(this.props.report.report.reason.length >= 30){
            this.props.sendReport();
            this.props.history.goBack();
        }
    }
      
    private renderAlerts() {
        var minReasonLength = 30;
        var maxReasonLength = 300;
        return(
            <div>
                <UncontrolledAlert color="info" fade={false} isOpen = {this.props.report.report.reason.length < minReasonLength && this.props.report.submitButtonClicked} toggle = {false}>
                    Reason length too short. It must be at least {minReasonLength} characters long.
                </UncontrolledAlert>
                <UncontrolledAlert color="info" fade={false} isOpen = {this.props.report.report.reason.length > maxReasonLength && this.props.report.submitButtonClicked} toggle = {false}>
                    Reason length too long. It must be at least {maxReasonLength} characters long.
                </UncontrolledAlert>
            </div>
        );
    }

    private renderRating() {
        var rating = this.props.report.rating;
        if (!rating)
            return;

        return (
            <div>
                <h2>Rating</h2>
                <Table className="table table-striped" aria-labelledby="tabelLabel" size="sm">
                    <thead>
                        <tr>
                            <th className="course">Course</th>
                            <th className="rating">Rating</th>
                            <th className="comment">Comment</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>{this.getCourseName(rating.courseId)}</td>
                            <td>{this.renderRatingInfo(rating)}</td>
                            <td>{this.renderRatingComment(rating)}</td>
                        </tr>
                    </tbody>
                </Table>
            </div>
        );
    }

    private getCourseName(courseId: string): string {
        const course = this.props.courses.courses.find(x => x.id === courseId);
        return course ? course.name : "Unknown";
    }

    private renderRatingInfo(rating: RatingsStore.Rating) {
        return (
            <div>
                <p>{new Date(rating.dateCreated).toISOString().split('T')[0]}</p>
                <p>Overall mark: {rating.overallMark}</p>
                <p>Level of difficulty: {rating.levelOfDifficulty}</p>
                <p>Would take again: {rating.wouldTakeTeacherAgain ? "Yes" : "No"}</p>
            </div>
        );
    }

    private renderRatingComment(rating: RatingsStore.Rating) {
        return (
            <div>
                <div className="tagbox">
                    {rating.tags.map((tag: TagsStore.Tag) =>
                        <span>
                            {tag.text}
                        </span>
                    )}
                </div>
                <p>{rating.comment}</p>
            </div>
        );
    }
}

function mapStateToProps(state: ApplicationState) {
    return {
        report: state.ratingReportCreation,
        courses: state.teacherCourses,
    }
};

const actions = {
  ...RatingReportCreationStore.actionCreators,
  ...TeacherCoursesStore.actionCreators,
}


export default connect(mapStateToProps, actions)(RatingReport as any);