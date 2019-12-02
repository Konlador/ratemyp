import * as React from 'react';
import { connect } from 'react-redux';
import { Table, Spinner, Button } from 'reactstrap';
import { ApplicationState } from '../../store';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';
import * as CustomStarShowcaseStore from '../../store/CustomStar/CustomStarShowcase';
import './CustomStarShowcase.css';
import { CustomStar } from '../../store/CustomStar/CustomStar';

interface OwnProps {
    teacherId: string,
    showItems: number
};

type Props =
    CustomStarShowcaseStore.CustomStarShowcaseState &
    typeof CustomStarShowcaseStore.actionCreators &
    RouteComponentProps<{ teacherId: string }>;

class CustomStarShowcase extends React.PureComponent<Props & OwnProps> {
    public componentDidMount() {
        this.props.requestCustomStars(this.teacherId())
    }

    private teacherId() {
        if (this.props.teacherId && this.props.teacherId.length > 0)
            return this.props.teacherId;
        return this.props.match.params.teacherId;
    }

    public render() {
        return (
            <React.Fragment>                
                <div>
                    <div className="images-head">
                        <h2 className="images">Custom star showcase</h2>
                        <Button className="add-image" color="primary" tag={Link} to={`/add-custom-star/${this.teacherId()}`}>Add an image</Button>{' '}
                        {this.props.isLoading && <Spinner type="grow" color="success" />}
                    </div>
                    <Table className="table table-striped" aria-labelledby="tabelLabel" size="sm">
                        <thead>
                            <tr>
                                <th className="information">Info</th>
                                <th className="image">Star</th>
                                <th className="ratings">Thumbs</th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.props.customStars.slice(0, this.props.showItems ? this.props.showItems : 100).map((customStar: CustomStar) =>
                                <tr key={customStar.id}>
                                    <td>{this.renderImageInfo(customStar)}</td>
                                    <td><img className="imgStar" src={"https://res.cloudinary.com/drodzj9pr/image/upload/_" + customStar.id}/></td>
                                    <td>{this.renderThumbs(customStar)}</td>
                                </tr>
                            )}
                        </tbody>
                    </Table>
                </div>
            </React.Fragment>
        );
    }

    private renderImageInfo(customStar: CustomStar) {
        return (
            <div className="custom-star-create-date">
                <strong>Date added</strong>
                <p>{new Date(customStar.dateCreated).toISOString().split('T')[0]}</p>
            </div>
        );
    }

    private renderThumbs(customStar: CustomStar) {
        return (
            <div>
                <div>
                <div>
                    <a className = "rating-item" onClick={() => this.props.sendCustomStarThumb(customStar.id, true)}>
                        <img className="thumbs" id="thumbsUp" src="http://free-icon-rainbow.com/i/icon_01693/icon_016930_256.png"/>
                        {customStar.thumbUps + " "} 
                        Likes
                    </a>
                </div>
                <div>
                    <a className = "rating-item" onClick={() => this.props.sendCustomStarThumb(customStar.id, false)}>
                        <img className="thumbs" id="thumbsDown" src="http://free-icon-rainbow.com/i/icon_01693/icon_016930_256.png"/>
                        {customStar.thumbDowns + " "} 
                        Dislikes
                    </a>
                </div>
                    <div className = "report-container">
                        <Button className = "report-button" color="primary" tag={Link} to={`/custom-star-report/${customStar.id}`} size='sm'>Report star</Button>{' '}
                    </div>
                </div>
            </div>
        );
    }
}

function mapStateToProps(state: ApplicationState, ownProps: OwnProps) {
    return {
        ...state.customStarShowcase,
        teacherId: ownProps.teacherId
    }
};

const actions = {
    ...CustomStarShowcaseStore.actionCreators
}

export default connect(mapStateToProps, actions)(CustomStarShowcase as any);

