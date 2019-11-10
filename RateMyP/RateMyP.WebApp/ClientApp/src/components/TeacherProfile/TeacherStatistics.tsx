import * as React from 'react';
import { Spinner } from 'reactstrap';
import { connect } from 'react-redux';
import { ApplicationState } from '../../store';
import * as TeacherStatisticsStore from '../../store/TeacherStatistics';
import * as TeacherRatingsStore from "../../store/Teacher/TeacherRatings";
import * as TagsStore from "../../store/Tags";
import { Card, CardTitle, CardText, CardBody, UncontrolledCollapse, Row, Col, Button } from 'reactstrap';
import { Chart } from "react-google-charts";

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
        this.props.requestTeacherRatings(this.props.teacherId);
        var firstRating = this.getOldestRating();
        if (firstRating !== undefined) {
            this.props.requestTeacherStatistics(this.props.teacherId, new Date(firstRating.dateCreated).getTime() * 10000 + 621355968000000000, new Date().getTime() * 10000 + 621355968000000000);
        }
        this.props.requestTags();
    }


    private renderTeacherStatistics() {
        interface IStatisticDateAndMark {
            item1: number,
            item2: number
        };

        var teacherStat: TeacherStatisticsStore.TeacherStatistic;
        teacherStat = this.props.statistics.teacherStatistics;
        var teacherMarkList = teacherStat.averageMarkList as Array<IStatisticDateAndMark>;

        var data = [];
        data.push(['Part', 'Mark']);
        for (var i = 0; i < teacherMarkList.length; i++) {
            const teacherStatistic: IStatisticDateAndMark = teacherMarkList[i];
            let statDate: Date = new Date(teacherStatistic.item1);
            var dateString = statDate.getFullYear() + '/' + (statDate.getMonth() + 1) + '/' + (statDate.getDate());
            data.push([dateString, teacherStatistic.item2]);
        }

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

                                <CardText
                                    style={{ fontSize: "170px" }}
                                    body className="text-center">
                                    <strong>{Number((this.props.statistics.teacherStatistics.averageMark).toFixed(1))}</strong>
                                </CardText>
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

                                <CardText
                                    style={{ fontSize: "70px" }}
                                    body className="text-center">
                                    <strong>{Number((this.props.statistics.teacherStatistics.averageLevelOfDifficulty).toFixed(1))}</strong>
                                </CardText>
                            </CardBody>
                        </Card>
                        <Card>
                            <CardBody>
                                <CardTitle
                                    style={{ fontSize: "10px" }}
                                    body className="text-center">
                                    <strong>Ratio of Students Who Would Take This Teacher Again</strong>
                                </CardTitle>

                                <CardText
                                    style={{ fontSize: "70px" }}
                                    body className="text-center">
                                    <strong>{Number((this.props.statistics.teacherStatistics.averageWouldTakeAgainRatio).toFixed(1))}</strong>
                                </CardText>
                            </CardBody>
                        </Card>
                    </Col>
                    <Col>
                        {this.renderTags()}
                    </Col>
                </Row>

                <Button color="primary" id="toggler" style={{ marginBottom: '1rem' }}>
                    Show/Hide Statistics Graph
    </Button>
                <UncontrolledCollapse toggler="#toggler">
                    <Card>
                        <CardBody>
                            <div className={"my-pretty-chart-container"}>
                                {(data.length > 1) ? <Chart
                                    width={'1000px'}
                                    height={'400px'}
                                    chartType="LineChart"
                                    loader={<div>Loading Chart</div>}
                                    data={data}
                                    options={{
                                        hAxis: {
                                            title: 'Part',
                                        },
                                        vAxis: {
                                            title: 'Mark',
                                            minValue: 0,
                                            maxValue: 10,
                                        },
                                        width: 1000,
                                        height: 400,
                                    }}
                                    rootProps={{ 'data-testid': '1' }}
                                /> : <Spinner type="grow" color="success" />}
                            </div>
                        </CardBody>
                    </Card>
                </UncontrolledCollapse>
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
        if (this.props.ratings.ratings.length > 0) {
            for (let val of ratings) {
                for (let value of val.tags) {
                    tags.push(value);
                }
            }
            return tags;
        }
    }
    private getDistinctTeacherTags() {
        var allTeachers = this.getAllTeacherTags();
        if (typeof allTeachers !== 'undefined') {
            allTeachers = allTeachers.filter((elem, index, self) =>
                self.findIndex((t) => { return (t.id === elem.id && t.text === elem.text) }) === index);
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

    private getOldestRating() {
        var ratings = this.props.ratings.ratings;
        if (this.props.ratings.ratings.length > 0) {
            var min = ratings[0];
            for (let val of ratings) {
                if (new Date(val.dateCreated) < new Date(min.dateCreated))
                    min = val;
            }
            return min;
        }
    }
}

function mapStateToProps(state: ApplicationState, ownProps: OwnProps) {
    return {
        statistics: state.teacherStatistics,
        ratings: state.ratings,
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

