import * as React from 'react';
import { Table } from 'reactstrap';
import * as CustomStarStore from '../../store/CustomStar/CustomStar';
import './CustomStarShowcase.css';

export default class CustomStarShowcase extends React.PureComponent<{ customStar: CustomStarStore.CustomStar }> {
    public render() {
        return (
            <React.Fragment>                
                <div>
                    <Table className="table table-striped" aria-labelledby="tabelLabel" size="sm">
                        <thead>
                            <tr>
                                <th className="information">Info</th>
                                <th className="image">Star</th>
                                <th className="ratings">Thumbs</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr key={this.props.customStar.id}>
                                <td>{this.renderImageInfo(this.props.customStar)}</td>
                                <td><img className="imgStar" src={`https://res.cloudinary.com/drodzj9pr/image/upload/_${this.props.customStar.id}`}/></td>
                                <td>{this.renderThumbs(this.props.customStar)}</td>
                            </tr>
                        </tbody>
                    </Table>
                </div>
            </React.Fragment>
        );
    }

    private renderImageInfo(customStar: CustomStarStore.CustomStar) {
        return (
            <div className="custom-star-create-date">
                <strong>Date added</strong>
                <p>{new Date(customStar.dateCreated).toISOString().split('T')[0]}</p>
            </div>
        );
    }

    private renderThumbs(customStar: CustomStarStore.CustomStar) {
        return (
            <div>
                <div>
                    <a className = "rating-item">
                        <img className="thumbs" id="thumbsUp" src="http://free-icon-rainbow.com/i/icon_01693/icon_016930_256.png"/>
                        {customStar.thumbUps + " "} 
                        Likes
                    </a>
                </div>
                <div>
                    <a className = "rating-item">
                        <img className="thumbs" id="thumbsDown" src="http://free-icon-rainbow.com/i/icon_01693/icon_016930_256.png"/>
                        {customStar.thumbDowns + " "} 
                        Dislikes
                    </a>
                </div>
            </div>
        );
    }
}
