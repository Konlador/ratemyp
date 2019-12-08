import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { Button, Form, FormGroup, Label, Input, UncontrolledAlert, Table } from 'reactstrap';
import { ApplicationState } from '../../store';
import * as CustomStarReportCreationStore from '../../store/Reports/CustomStarReportCreation';
import { CustomStar } from '../../store/CustomStar/CustomStar';

type Props =
    CustomStarReportCreationStore.CustomStarReportCreationState &
    typeof CustomStarReportCreationStore.actionCreators &
    RouteComponentProps<{ customStarId: string }>

class CustomStarReport extends React.PureComponent<Props> {
    public componentDidMount() {
        this.props.requestCustomStar(this.props.match.params.customStarId);
    }

    public render() {      
        return (
            <React.Fragment>
                <Form>
                    {this.renderAlerts()}
                    {this.renderCustomStar()}
                    <FormGroup>
                        <Label style={{fontSize: 30}}>Report a Student's star</Label>
                    </FormGroup>
                    <FormGroup>
                        <Label for="report">What is wrong with this image?</Label>
                        <Input type="textarea" name="text" id="report" onChange={event => this.props.changeReason(event.target.value)}/>
                    </FormGroup>
                    <Button color="primary" onClick={() => this.props.submitReport() && this.onSubmitButtonPush() }>Submit</Button>{' '}
                </Form>
            </React.Fragment>
        );
      }

    private onSubmitButtonPush() {
        this.props.setCustomStarId(this.props.match.params.customStarId);
        this.props.setStudentId(this.props.customStar ? this.props.customStar.studentId : "Undefined");
        if(this.props.report.reason.length >= 30){
            this.props.sendReport();
            this.props.history.goBack();
        }
    }
      
    private renderAlerts() {
        var minReasonLength = 30;
        var maxReasonLength = 300;
        return(
            <div>
                <UncontrolledAlert color="info" fade={false} isOpen = {this.props.report.reason.length < minReasonLength && this.props.submitButtonClicked} toggle = {false}>
                    Reason length too short. It must be at least {minReasonLength} characters long.
                </UncontrolledAlert>
                <UncontrolledAlert color="info" fade={false} isOpen = {this.props.report.reason.length > maxReasonLength && this.props.submitButtonClicked} toggle = {false}>
                    Reason length too long. It must be at least {maxReasonLength} characters long.
                </UncontrolledAlert>
            </div>
        );
    }

    private renderCustomStar() {
        var customStar = this.props.customStar;
        if (!customStar)
            return;

        return (
            <div>
                <h2>Image</h2>
                <Table className="table table-striped" aria-labelledby="tabelLabel" size="sm">
                    <thead>
                        <tr>
                            <th className="information">Image information</th>
                            <th className="image">Image</th>
                            <th className="ratings">Thumbs</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr key={customStar.id}>
                            <td>{this.renderImageInfo(customStar)}</td>
                            <td><img className = "imgStar" src={"https://res.cloudinary.com/drodzj9pr/image/upload/_" + customStar.id}/></td>
                            <td>{this.renderThumbs(customStar)}</td>
                        </tr>
                    </tbody>
                </Table>
            </div>
        );
    }

    private renderImageInfo(customStar: CustomStar) {
        return (
            <div>
                <p>Date added: {new Date(customStar.dateCreated).toISOString().split('T')[0]}</p>
                <p>Student Id: {customStar.studentId}</p>
            </div>
        );
    }

    private renderThumbs(customStar: CustomStar) {
        return (
            <div>
                <div>
                    {customStar.thumbUps} Like this image
                </div>
                <div>
                    {customStar.thumbDowns} Dislike this image
                </div>
            </div>
        );
    }
}

export default connect(
    (state: ApplicationState) => state.customStarReportCreation,
    CustomStarReportCreationStore.actionCreators
  )(CustomStarReport as any);