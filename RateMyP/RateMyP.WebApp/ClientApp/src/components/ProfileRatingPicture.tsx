import * as React from 'react';
import { RouteComponentProps, withRouter } from 'react-router';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import { Col, Button, Form, FormGroup, Label, UncontrolledAlert, FormText } from 'reactstrap';
import * as ProfileRatingPictureStore from '../store/ProfileRatingPicture'

type Props =
    {
    picture: ProfileRatingPictureStore.ProfileRatingPictureStore
    } &
    typeof ProfileRatingPictureStore.actionCreators &
    RouteComponentProps<{ teacherId: string }>;

class ProfileRatingPicture extends React.PureComponent<Props> {
    public render() {
        var fileReader: FileReader;

        const handleFileRead = () => {
            const content = fileReader.result;
                this.props.setPicture(content);   
        }

        const handleFileChosen = (file: Blob) => {
            fileReader = new FileReader();
            fileReader.onloadend = handleFileRead;
            fileReader.readAsDataURL(file)
        }

        return (
            <Form>
                {this.renderAlerts()}
                <FormGroup>
                    <Col sm={10}>
                        <Label style={{fontSize: 32}}>Upload a teacher's rating pictures</Label>
                    </Col>
                </FormGroup>
                <FormGroup>
                    <Label for="exampleFile" sm={2}>Rating image</Label>
                    <Col sm={10}>
                        <input type="file" name="file" accept="image/*" onChange={event => event.target.files?handleFileChosen(event.target.files[0]):console.log('event null')} />
                        <FormText color="muted">
                            Please select a rating image file to upload.
                        </FormText>
                    </Col>
                </FormGroup>
                <FormGroup>
                    <Col sm={10}>
                        <Button onClick={() => this.onSubmitButtonPush()}>Submit</Button> 
                    </Col>
                </FormGroup>                
            </Form>
        );
    }

    private onSubmitButtonPush(){
        this.props.setTeacherId(this.props.match.params.teacherId);
        this.props.submitButtonClicked();
        if(this.props.picture.picture !== null){
            this.props.uploadPicture();
        }
    }

    private renderAlerts() {
        return(
          <div>
            <UncontrolledAlert color="info" fade={false} isOpen = {!this.props.picture.picture && this.props.picture.submitButtonClicked} toggle={false}>
              You must select a picture.
            </UncontrolledAlert>
            <UncontrolledAlert color="success" fade={false} isOpen = {this.props.picture.picture && this.props.picture.submitButtonClicked} toggle={false}>
              You have successfully uploaded the picture
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
        picture: state.profileRatingPicture
    }
  };
  
  const actions = {
    ...ProfileRatingPictureStore.actionCreators,
  }
  
  
  export default withRouter(
      connect(mapStateToProps, actions)(ProfileRatingPicture as any) as React.ComponentType<any>
  );
