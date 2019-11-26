import * as React from 'react';
import { RouteComponentProps, withRouter } from 'react-router';
import { connect } from 'react-redux';
import { ApplicationState } from '../../store';
import { Col, Button, Form, FormGroup, Label, UncontrolledAlert, FormText } from 'reactstrap';
import * as CustomStarStore from '../../store/CustomStar/CustomStarStore'

type Props = 
    CustomStarStore.CustomStarState &
    typeof CustomStarStore.actionCreators &
    RouteComponentProps<{ teacherId: string }>;

class CustomStarUpload extends React.PureComponent<Props> {
    public render() {
        var fileReader: FileReader;
        var img: HTMLImageElement;

        const handleFileRead = () => {
            const content = fileReader.result;
                this.props.setImage(content);   
                checkImageSize(content);

        }

        const checkImageSize = (image: any) => {
            img = new Image();
            img.onloadend = handleImageSize;
            img.src = String(image);
        }

        const handleImageSize = () => {
            this.props.setImageSize(img.width, img.height)
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
                <Col sm={10}>
                <Label>Current rating image:‎‎‎‏‏‎‏‏‎‏‏‎</Label>
                <FormGroup>                    
                    <img src={"api/images/teacher=" + this.props.match.params.teacherId} className="icon"/>
                </FormGroup>  
                </Col>
            </Form>
        );
    }

    private onSubmitButtonPush(){
        this.props.submitButtonClick();
        if(this.props.image !== null && !(this.props.width > 256 || this.props.height > 256)){
            this.props.uploadCustomStar(this.props.match.params.teacherId);
        }
    }

    private renderAlerts() {
        return(
          <div>
            <UncontrolledAlert color="info" fade={false} isOpen = {!this.props.image && this.props.submitButtonClicked} toggle={false}>
              You must select an image.
            </UncontrolledAlert>
            <UncontrolledAlert color="info" fade={false} isOpen = {(this.props.width > 256 || this.props.height > 256) && this.props.submitButtonClicked} toggle={false}>
              Your image is too large, please select one in the margins 256x256.
            </UncontrolledAlert>
            <UncontrolledAlert color="success" fade={false} isOpen = {!(this.props.width > 256 || this.props.height > 256) && this.props.image && this.props.submitButtonClicked} toggle={false}>
              You have successfully uploaded the picture
            </UncontrolledAlert>
          </div>
        )
      }
      componentWillUnmount() {
        this.props.clearStore();
    }
}
  
  export default withRouter(
      connect((state: ApplicationState) => state.customStarUpload, CustomStarStore.actionCreators)(CustomStarUpload as any) as React.ComponentType<any>
  );
