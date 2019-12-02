import * as React from 'react';
import { connect } from 'react-redux';
import { Table, Spinner, Button } from 'reactstrap';
import { ApplicationState } from '../../store';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';
import * as ImageStore from '../../store/CustomStar/CustomStarLeaderboard';
import './CustomStarLeaderboard.css'

interface OwnProps {
    teacherId: string,
    showItems: number
};

type Props =
    {
    images: ImageStore.CustomStarLeaderboardState,
    } &
    typeof ImageStore.actionCreators &
    RouteComponentProps<{ teacherId: string }>;

class StarImageLeaderboard extends React.PureComponent<Props & OwnProps> {
    public componentDidMount() {
        this.ensureDataFetched();
    }

    public componentDidUpdate() {
    }

    private ensureDataFetched() {
        this.props.requestCustomStars(this.teacherId())
    }

    private teacherId() {
        if(this.props.teacherId && this.props.teacherId.length > 5) {
            return this.props.teacherId
        }
        else {
            return this.props.match.params.teacherId
        }
    }

    public render() {
        return (
            <React.Fragment>                
                <div>
                    <div className="images-head">
                        <h2 className="images">Custom star showcase</h2>
                        <Button className="add-image" color="primary" tag={Link} to={`/add-custom-star/${this.teacherId()}`}>Add an image</Button>{' '}
                        {this.props.images.isLoading && <Spinner type="grow" color="success" />}
                    </div>
                    <Table className="table table-striped" aria-labelledby="tabelLabel" size="sm">
                        <thead>
                            <tr>
                                <th className="information">Image information</th>
                                <th className="image">Image</th>
                                <th className="ratings">Thumbs</th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.props.images.images.slice(0, this.props.showItems? this.props.showItems : 100).map((image: ImageStore.Image, index: number) =>
                                <tr key={image.id}>
                                    <td>{this.renderImageInfo(image)}</td>
                                    <td><img className="imgStar" src={"https://res.cloudinary.com/drodzj9pr/image/upload/_" + image.id}/></td>
                                    <td>{this.renderThumbs(image)}</td>
                                </tr>
                            )}
                        </tbody>
                    </Table>
                    <div className="add-image-container">
                        <Button className="add-image2" color="primary" tag={Link} to={`/add-custom-star/${this.teacherId()}`}>Add an image</Button>{' '}
                    </div>                   
                </div>
            </React.Fragment>
        );
    }

    private renderImageInfo(image: ImageStore.Image) {
        return (
            <div>
                <p>Date added: {new Date(image.dateCreated).toISOString().split('T')[0]}</p>
                <p>Student Id: {image.studentId}</p>
            </div>
        );
    }

    private renderThumbs(image: ImageStore.Image) {
        return (
            <div>
                <div>
                <div>
                    <a className = "rating-item" onClick={() => this.props.sendCustomStarThumb(image.id, true)}>
                        <img src="http://free-icon-rainbow.com/i/icon_01693/icon_016930_256.png" className="thumbs" id="thumbsUp"/>
                        {image.thumbUps + " "} 
                        Like this star
                    </a>
                </div>
                <div>
                    <a className = "rating-item" onClick={() => this.props.sendCustomStarThumb(image.id, false)}>
                        <img src="http://free-icon-rainbow.com/i/icon_01693/icon_016930_256.png" className="thumbs" id="thumbsDown"/>
                        {image.thumbDowns + " "} 
                        Dislike this star
                    </a>
                </div>
                    <div className = "report-container">
                        <Button className = "report-button" color="primary" tag={Link} to={`/custom-star-report/${image.id}`} size='sm'>Report image</Button>{' '}
                    </div>
                </div>
            </div>
        );
    }
}

function mapStateToProps(state: ApplicationState, ownProps: OwnProps) {
    return {
        images: state.customStarLeaderboard,
        teacherId: ownProps.teacherId
    }
};

const actions = {
    ...ImageStore.actionCreators
}

export default connect(mapStateToProps, actions)(StarImageLeaderboard as any);

