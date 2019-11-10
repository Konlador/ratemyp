import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps, withRouter } from 'react-router';
import { Button, Form, FormGroup, Label, Input, UncontrolledAlert } from 'reactstrap';
import { ApplicationState } from '../../store';
import * as ReportRatingStore from '../../store/Rate/RatingReport';

type Props =
    {
      report: ReportRatingStore.RatingReportState
    } &
    typeof ReportRatingStore.actionCreators &
    RouteComponentProps<{ ratingId: string }>

class RatingReport extends React.PureComponent<Props> {
    render() {      
        return (
          <React.Fragment>
            {this.renderForm()}
          </React.Fragment>
        );
      }
    private renderForm() {
      return(
      <Form>
          {this.renderAlerts()}
          <FormGroup>
            <Label  style={{fontSize: 30}}>Report a Student's rating</Label>
          </FormGroup>
          <FormGroup>
            <Label for="report">What is wrong with this rating?</Label>
            <Input type="textarea" name="text" id="report" onChange={event => this.props.changeReason(event.target.value)}/>
          </FormGroup>
          <Button color="primary" onClick={() => this.props.submitReport() && this.onSubmitButtonPush() }>Submit</Button>{' '}
      </Form>
      );
    }
    
      private onSubmitButtonPush() {
        this.props.setRatingId(this.props.match.params.ratingId)
        if(this.props.report.report.reason.length >= 30){
          this.props.sendReport()
          this.props.history.goBack()
        }
      }
      
      private renderAlerts() {
        var minReasonLength = 30
        var maxReasonLength = 300
        return(
          <div>
            <UncontrolledAlert color="info" fade={false} isOpen = {this.props.report.report.reason.length < minReasonLength && this.props.report.submitButtonClicked} toggle = {false}>
              Reason length too short. It must be at least {minReasonLength} characters long.
            </UncontrolledAlert>
            <UncontrolledAlert color="info" fade={false} isOpen = {this.props.report.report.reason.length > maxReasonLength && this.props.report.submitButtonClicked} toggle = {false}>
              Reason length too long. It must be at least {maxReasonLength} characters long.
            </UncontrolledAlert>
          </div>
        )
      }
      componentWillUnmount() {
        this.props.clearStore();
    }
}

function mapStateToProps(state: ApplicationState) {
  return {
      report: state.ratingReport
  }
};

const actions = {
  ...ReportRatingStore.actionCreators,
}


export default connect(mapStateToProps, actions)(RatingReport as any);