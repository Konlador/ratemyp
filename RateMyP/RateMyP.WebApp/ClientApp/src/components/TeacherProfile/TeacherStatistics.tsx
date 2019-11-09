import * as React from 'react';
import { connect } from 'react-redux';
import { ApplicationState } from '../../store';
import * as TeacherStatisticsStore from '../../store/TeacherStatistics';
import * as TeacherRatingsStore from "../../store/Teacher/TeacherRatings";
import * as TagsStore from "../../store/Tags";
import { Row, Col } from 'reactstrap';
import { Card, CardTitle, CardText, CardBody  } from 'reactstrap';

interface OwnProps {
    teacherId: string
};

type Props =
    {
    statistics: TeacherStatisticsStore.TeacherStatisticsState,
    ratings: TeacherRatingsStore.TeacherRatingsState,
    tags: TagsStore.TagsState
    } &
    typeof TeacherStatisticsStore.actionCreators &
    typeof TeacherRatingsStore.actionCreators &
    typeof TagsStore.actionCreators;

class TeacherStatistics extends React.PureComponent<Props & OwnProps> {
    public componentDidMount() {
        this.ensureDataFetched();
    }

    public componentDidUpdate() {
        this.ensureDataFetched();
    }

    public render() {
        return (
            <React.Fragment>
                {this.renderTeacherStatistics()}
            </React.Fragment>
        );
    }

    private ensureDataFetched() {
        this.props.requestTeacherStatistics(this.props.teacherId);
        this.props.requestTeacherRatings(this.props.teacherId);
        this.props.requestTags();
    }

    private renderTeacherStatistics() {
        return (
        <div>
                <h1>Statistics</h1>
                <Row>
                    <Col sm="4">
                        <Card>
                            <CardBody>
                                <CardTitle
                                    style={{ fontSize: "25px" }}
                                    body className="text-center">
                                    <strong>Average Rating</strong>
                                </CardTitle>

                                {this.props.statistics.teacherStatistics.map((statistic: TeacherStatisticsStore.TeacherStatistic) =>
                                    <CardText
                                        style={{ fontSize: "170px" }}
                                        body className="text-center">
                                        <strong>{statistic.averageMark}</strong>
                                    </CardText>
                                )}
                            </CardBody>
                        </Card>
                    </Col>
                    <Col sm="4">
                        <Card>
                            <CardBody>
                            <CardTitle
                                    style={{ fontSize: "12px" }}
                                    body className="text-center">
                                    <strong>Average Level of Difficulty Rating</strong>
                                </CardTitle>

                                {this.props.statistics.teacherStatistics.map((statistic: TeacherStatisticsStore.TeacherStatistic) =>
                                    <CardText
                                        style={{ fontSize: "70px" }}
                                        body className="text-center">
                                        <strong>{statistic.averageLevelOfDifficulty}</strong>
                                    </CardText>
                                )}
                            </CardBody>
                        </Card>
                        <Card>
                        <CardBody>
                            <CardTitle
                                    style={{ fontSize: "10px" }}
                                    body className="text-center">
                                    <strong>Ratio of Students Who Would Take This Teacher Again</strong>
                                </CardTitle>

                                {this.props.statistics.teacherStatistics.map((statistic: TeacherStatisticsStore.TeacherStatistic) =>
                                    <CardText
                                        style={{ fontSize: "70px" }}
                                        body className="text-center">
                                        <strong>{statistic.averageWouldTakeAgainRatio}</strong>
                                    </CardText>
                                )}
                            </CardBody>
                        </Card>
                    </Col>
                    <Col>
                    {this.renderTags()}
                    </Col>
                </Row>
            </div>
        );
    }

    private renderTags() {
        const distinctTeacherTags = this.getDistinctTeacherTags();
        if (typeof distinctTeacherTags !== 'undefined') {
            return (
                <div>
                    <div className="tagbox">
                        {distinctTeacherTags.map((tag: TagsStore.Tag) =>
                            <span>
                                {tag.text} ({this.countTags(tag)})
                            </span>)}
                    </div>
                </div>
            )
        }
    }

    private getAllTeacherTags() {
        var tags = [];
        var ratings = this.props.ratings.ratings;
        if (this.props.ratings.ratings.length > 0)
        {
            for (let val of ratings)
            {
                for(let value of val.tags)
                {
                    tags.push(value);
                }
            }
            return tags;
        }
    }

    private getDistinctTeacherTags() {
        var allTeachers = this.getAllTeacherTags();
        if(typeof allTeachers !== 'undefined')
        {
            allTeachers = allTeachers.filter((elem, index, self) => 
            self.findIndex((t) => {return (t.id === elem.id && t.text === elem.text)}) === index);
            return allTeachers;
        }
    }
    
    private countTags(tag: TagsStore.Tag) {
        var allTeacherTags = this.getAllTeacherTags();
        if (typeof allTeacherTags !== 'undefined') {
            var count = 0;
            allTeacherTags.forEach((v) => (v.id === tag.id && count++));
            return count;
        }
    }
}

function mapStateToProps(state: ApplicationState, ownProps: OwnProps) {
    return {
        statistics: state.teacherStatistics,
        ratings: state.teacherRatings,
        tags: state.tags,
        teacherId: ownProps.teacherId
    }
};

const actions = {
    ...TeacherStatisticsStore.actionCreators,
    ...TeacherRatingsStore.actionCreators,
    ...TagsStore.actionCreators
}

export default connect(
    mapStateToProps,
    actions
)(TeacherStatistics as any);

