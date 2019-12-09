import * as React from 'react';
import { Table } from 'reactstrap';
import * as RatingsStore from '../../store/Ratings'
import * as TagsStore from '../../store/Tags';

export default class RatingReport extends React.PureComponent<{ rating: RatingsStore.Rating }> {
    public render() {      
        return (
            <React.Fragment>
                <div>
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
                                <td>{this.props.rating.courseId}</td>
                                <td>{this.renderRatingInfo(this.props.rating)}</td>
                                <td>{this.renderRatingComment(this.props.rating)}</td>
                            </tr>
                        </tbody>
                    </Table>
                </div>
            </React.Fragment>
        );
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
