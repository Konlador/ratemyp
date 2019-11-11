import * as React from 'react';
import { Spinner } from 'reactstrap';
import { connect } from 'react-redux';
import { ApplicationState } from '../../store';
import * as TeacherStatisticsStore from '../../store/TeacherStatistics';
import * as TeacherRatingsStore from "../../store/Teacher/TeacherRatings";
import * as TagsStore from "../../store/Tags";
import { Card, CardTitle, CardText, CardBody, UncontrolledCollapse, Row, Col, Button } from 'reactstrap';
import { Chart } from "react-google-charts";
import { RouteComponentProps } from 'react-router';

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

    public componentWillUnmount()
    {
        this.props.clearSelectedStatistics();
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
        this.props.requestTeacherStatistics(this.props.teacherId);
        this.props.requestTags();
    }


    private renderTeacherStatistics() {
        return (
            <div>
                <h1>Statistics</h1>
                <Row>
                    <Col sm="4">
                        {this.renderMark("Average Mark", "25px", "170px", this.props.statistics.teacherStatistics.averageMark)}
                    </Col>
                    <Col sm="4">
                        {this.renderMark("Level of difficulty", "12px", "70px", this.props.statistics.teacherStatistics.averageLevelOfDifficulty)}
                        {this.renderMark("Ratio of Students Who Would Take This Teacher Again", "10px", "70px", this.props.statistics.teacherStatistics.wouldTakeAgainRatio)}
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
                            {this.renderTeacherMarkChart()}
                        </CardBody>
                    </Card>
                </UncontrolledCollapse>
            </div>
        );
    }

    private renderMark(title: string, titleFontSize: string, bodyTextFontSize: string, mark: number) {
        return (
            <Card>
                <CardBody>
                    <CardTitle
                        style={{ fontSize: titleFontSize }}
                        body className="text-center">
                        <strong>{title}</strong>
                    </CardTitle>

                    <CardText
                        style={{ fontSize: bodyTextFontSize }}
                        body className="text-center">
                        <strong>{Number((mark).toFixed(1))}</strong>
                    </CardText>
                </CardBody>
            </Card>
        )
    }

    private renderTeacherMarkChart() {
        var teacherStat: TeacherStatisticsStore.TeacherStatistic;
        teacherStat = this.props.statistics.teacherStatistics;
        var teacherMarks = teacherStat.averageMarkList as Array<TeacherStatisticsStore.DateMark>;
        var data = [];
        data.push(['Part', 'Rating']);

        for (var i = 0; i < teacherMarks.length; i++) {
            const teacherStatistic: TeacherStatisticsStore.DateMark = teacherMarks[i];
            let statDate: Date = new Date(teacherStatistic.date);
            var dateString = statDate.getFullYear() + '/' + (statDate.getMonth() + 1) + '/' + (statDate.getDate());
            data.push([dateString, teacherStatistic.mark]);
        }
        return (
            <div className={"my-pretty-chart-container"}>
                {(data.length > 1) ? <Chart
                    width={'1000px'}
                    height={'400px'}
                    chartType="LineChart"
                    loader={<div>Loading Chart</div>}
                    data={data}
                    options={{
                        series: {
                            0: { color: '#6f9654' }
                        },
                        hAxis: {
                            title: 'Part',
                        },
                        vAxis: {
                            title: 'Rating',
                            minValue: 0,
                            maxValue: 10,
                        },
                        width: 1000,
                        height: 400,
                    }}
                    rootProps={{ 'data-testid': '1' }}
                /> : <Spinner type="grow" color="success" />}
            </div>
        )
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

