import * as React from 'react';
import { connect } from 'react-redux';
import { Table, Spinner, Button } from 'reactstrap';
import { ApplicationState } from '../../store';
import { Link } from 'react-router-dom';
import * as RatingsStore from '../../store/Ratings';
import * as TagsStore from '../../store/Tags';
import * as CourseRatingsStore from '../../store/Course/CourseRatings';
import * as CourseTeachersStore from '../../store/Course/CourseTeachers';
import './CourseRatings.css';

interface OwnProps {
    courseId: string
};

type Props =
    {
    ratings: CourseRatingsStore.CourseRatingsState,
    teachers: CourseTeachersStore.CourseTeachersState
    } &
    typeof CourseRatingsStore.actionCreators &
    typeof CourseTeachersStore.actionCreators;

class CourseRatings extends React.PureComponent<Props & OwnProps> {
    public componentDidMount() {
        this.ensureDataFetched();
    }

    public componentDidUpdate() {
        this.ensureDataFetched();
    }

    private ensureDataFetched() {
        this.props.requestCourseTeachers(this.props.courseId);
        this.props.requestCourseRatings(this.props.courseId);
    }

    public render() {
        return (
            <React.Fragment>
                <div>
                    <div className="ratings-head">
                        <h2 className="ratings">Ratings ({this.props.ratings.ratings.length})</h2>
                        <Button className="add-rating" color="primary" tag={Link} to={`/rate-course/${this.props.courseId}`}>Add a rating</Button>{' '}
                        {this.props.ratings.isLoading && <Spinner type="grow" color="success" />}
                    </div>
                    <Table className="table table-striped" aria-labelledby="tabelLabel" size="sm">
                        <thead>
                            <tr>
                                <th className="course">Teacher</th>
                                <th className="rating">Rating</th>
                                <th className="comment">Comment</th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.props.ratings.ratings.map((rating: RatingsStore.Rating, index: number) =>
                                <tr key={rating.id}>
                                    <td>{this.getTeacherName(rating.teacherId)}</td>
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

    private getTeacherName(teacherId: string): string {
        const teacher = this.props.teachers.teachers.find(x => x.id === teacherId);
        return teacher ? teacher.firstName + " " + teacher.lastName : "Unknown";
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
        ratings: state.courseRatings,
        teachers: state.courseTeachers,
        courseId: ownProps.courseId
    }
};

const actions = {
    ...CourseRatingsStore.actionCreators,
    ...CourseTeachersStore.actionCreators
}

export default connect(mapStateToProps, actions)(CourseRatings as any);

