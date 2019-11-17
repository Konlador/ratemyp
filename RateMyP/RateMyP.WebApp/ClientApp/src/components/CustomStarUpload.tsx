import * as React from 'react';
import { RouteComponentProps, withRouter } from 'react-router';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import { Col, Button, Form, FormGroup, Label, UncontrolledAlert, FormText } from 'reactstrap';
import * as CustomStarStore from '../store/CustomStarUpload'

type Props =
    {
    image: CustomStarStore.CustomStar
    } &
    typeof CustomStarStore.actionCreators &
    RouteComponentProps<{ teacherId: string }>;

class CustomStarUpload extends React.PureComponent<Props> {
    public render() {
        var fileReader: FileReader;

        const handleFileRead = () => {
            const content = fileReader.result;
                this.props.setImage(content);   
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
                        <Label style={{fontSize: 32}}>Upload a rating image</Label>
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
        if(this.props.image.image !== null){
            this.props.uploadImage();
        }
    }

    private renderAlerts() {
        return(
          <div>
            <UncontrolledAlert color="info" fade={false} isOpen = {!this.props.image.image && this.props.image.submitButtonClicked} toggle={false}>
              You must select an image.
            </UncontrolledAlert>
            <UncontrolledAlert color="success" fade={false} isOpen = {this.props.image.image && this.props.image.submitButtonClicked} toggle={false}>
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
        image: state.customStarUpload
    }
  };
  
  const actions = {
    ...CustomStarStore.actionCreators,
  }
  
  
  export default withRouter(
      connect(mapStateToProps, actions)(CustomStarUpload as any) as React.ComponentType<any>
  );
