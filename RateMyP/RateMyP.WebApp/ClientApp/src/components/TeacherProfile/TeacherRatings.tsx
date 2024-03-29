import * as React from 'react';
import { connect } from 'react-redux';
import { Table, Spinner, Button } from 'reactstrap';
import { ApplicationState } from '../../store';
import { Link } from 'react-router-dom';
import * as RatingsStore from '../../store/Ratings';
import * as TagsStore from '../../store/Tags';
import * as TeacherRatingsStore from '../../store/Teacher/TeacherRatings';
import * as TeacherCoursesStore from '../../store/Teacher/TeacherCourses';
import './TeacherRatings.css';

interface OwnProps {
    teacherId: string
};

type Props =
    {
    ratings: TeacherRatingsStore.TeacherRatingsState,
    courses: TeacherCoursesStore.TeacherCoursesState
    } &
    typeof TeacherRatingsStore.actionCreators &
    typeof TeacherCoursesStore.actionCreators;

class TeacherRatings extends React.PureComponent<Props & OwnProps> {
    public componentDidMount() {
        this.ensureDataFetched();
    }

    public componentDidUpdate() {
        this.ensureDataFetched();
    }

    private ensureDataFetched() {
        this.props.requestTeacherCourses(this.props.teacherId);
        this.props.requestTeacherRatings(this.props.teacherId);
    }

    public render() {
        return (
            <React.Fragment>
                <div>
                    <div className="ratings-head">
                        <h2 className="ratings">Ratings ({this.props.ratings.ratings.length})</h2>
                        <Button className="add-rating" color="primary" tag={Link} to={`/rate-teacher/${this.props.teacherId}`}>Add a rating</Button>{' '}
                        {this.props.ratings.isLoading && <Spinner type="grow" color="success" />}
                    </div>
                    <Table className="table table-striped" aria-labelledby="tabelLabel" size="sm">
                        <thead>
                            <tr>
                                <th className="course">Course</th>
                                <th className="rating">Rating</th>
                                <th className="comment">Comment</th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.props.ratings.ratings.map((rating: RatingsStore.Rating, index: number) =>
                                <tr key={rating.id}>
                                    <td>{this.getCourseName(rating.courseId)}</td>
                                    <td>{this.renderRatingInfo(rating)}</td>
                                    <td>{this.renderComment(rating)}</td>
                                </tr>
                            )}
                        </tbody>
                    </Table>
                </div>
            </React.Fragment>
        );
    }

    private getCourseName(courseId: string): string {
        const course = this.props.courses.courses.find(x => x.id === courseId);
        return course ? course.name : "Unknown";
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

    private renderComment(rating: RatingsStore.Rating) {
        return (
            <div>
                <div className="tagbox">
                    {rating.tags.map((tag: TagsStore.Tag) =>
                        <span key={tag.id}>
                            {tag.text}
                        </span>
                    )}
                </div>
                <p>{rating.comment}</p>
                <div>
                    <div>
                        <a onClick={() => this.props.sendRatingThumb(rating.id, true)}>{rating.thumbUps} find this useful</a>
                    </div>
                    <div>
                        <a onClick={() => this.props.sendRatingThumb(rating.id, false)}>{rating.thumbDowns} find this not useful</a>
                    </div>
                    <div>
                        <Button color="primary" tag={Link} to={`/rating-report/${rating.id}`} size='sm'>Report rating</Button>{' '}
                    </div>
                </div>
            </div>
        );
    }
}

function mapStateToProps(state: ApplicationState, ownProps: OwnProps) {
    return {
        ratings: state.teacherRatings,
        courses: state.teacherCourses,
        teacherId: ownProps.teacherId
    }
};

const actions = {
    ...TeacherRatingsStore.actionCreators,
    ...TeacherCoursesStore.actionCreators
}

export default connect(mapStateToProps, actions)(TeacherRatings as any);

