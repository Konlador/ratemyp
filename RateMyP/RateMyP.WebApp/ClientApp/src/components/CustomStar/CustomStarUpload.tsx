import * as React from 'react';
import { RouteComponentProps, withRouter } from 'react-router';
import { connect } from 'react-redux';
import { ApplicationState } from '../../store';
import { CustomInput, Col, Button, Form, FormGroup, Label, UncontrolledAlert, FormText } from 'reactstrap';
import * as CustomStarStore from '../../store/CustomStar/CustomStarStore'
import AvatarEditor from 'react-avatar-editor'
import './CustomStarUpload.css'

type Props = 
    CustomStarStore.CustomStarState &
    typeof CustomStarStore.actionCreators &
    RouteComponentProps<{ teacherId: string }>;

class CustomStarUpload extends React.PureComponent<Props> {
    editor: any;
    fileReader!: FileReader;
    img!: HTMLImageElement;

    private handleFileChosen = (file: Blob) => {
        this.fileReader = new FileReader();
        this.fileReader.onloadend = this.handleFileRead;
        this.fileReader.readAsDataURL(file)
    }
    private handleFileRead = () => {
        const content = this.fileReader.result;
            this.props.setImage(content);   
            // this.checkImageSize(content);
    }
    private checkImageSize = (image: any) => {
        this.img = new Image();
        this.img.onloadend = this.handleImageSize;
        this.img.src = String(image);
    }

    private handleImageSize = () => {
        this.props.setImageSize(this.img.width, this.img.height)
    }
    public render() {

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
                        <input type="file" name="file" accept="image/*" onChange={event => event.target.files?this.handleFileChosen(event.target.files[0]):console.log('event null')} />
                        <FormText color="muted">
                            Please select a rating image file to upload.
                        </FormText>
                    </Col>
                </FormGroup>
                {this.props.image? this.renderImageEditor():undefined}
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
        if (this.editor) {
            this.props.setImage(this.editor.getImageScaledToCanvas().toDataURL());
        }
        this.props.submitButtonClick();
        if(this.props.image !== null){
            this.props.uploadCustomStar(this.props.match.params.teacherId);
        }
    }
    
    private setEditorRef = (editor: any) => this.editor = editor

    private renderImageEditor() {
        return (
            <div>
                <div>
                    <AvatarEditor
                        ref={this.setEditorRef}
                        image = {"" + this.props.image}
                        width={256}
                        height={256}
                        border={25}
                        color={[0, 0, 0, 0.6]}
                        scale={this.props.zoomLevel}
                        rotate={0}
                    />
                </div>
                <div>
                    <div>Zoom level:</div>
                    <CustomInput type="range" className="avatar-zoom-slider" id="none"
                                onChange={event => this.props.setZoomLevel(event.target.valueAsNumber)} 
                                min="1" max="5" step="0.2" value={this.props.zoomLevel}/>
                </div>
            </div>
        )
    }
    private renderAlerts() {
        return(
          <div>
            <UncontrolledAlert color="info" fade={false} isOpen = {!this.props.image && this.props.submitButtonClicked && !this.props.imageUploaded} toggle={false}>
              You must select an image.
            </UncontrolledAlert>
            <UncontrolledAlert color="info" fade={false} isOpen = {(this.props.width > 256 || this.props.height > 256) && this.props.submitButtonClicked} toggle={false}>
              Your image is too large, please select one in the margins 256x256.
            </UncontrolledAlert>
            <UncontrolledAlert color="success" fade={false} isOpen = {this.props.imageUploaded && this.props.submitButtonClicked} toggle={false}>
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
