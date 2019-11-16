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

    private ensureDataFetched() {
        this.props.requestTeacherRatings(this.props.teacherId);
        this.props.requestTeacherStatistics(this.props.teacherId);
        this.props.requestTags();
    }

    public render() {
        return (
            <React.Fragment>
                <div>
                    <h1>Statistics</h1>
                    <Row>
                        <Col sm="4">
                            {this.renderNumber("Average rating", "25px", "170px", this.props.statistics.teacherStatistics.averageMark)}
                        </Col>
                        <Col sm="4">
                            {this.renderNumber("Level of difficulty", "12px", "70px", this.props.statistics.teacherStatistics.averageLevelOfDifficulty)}
                            <Card>
                                <CardBody>
                                    <CardTitle
                                        style={{ fontSize: "10px" }}
                                        body className="text-center">
                                        <strong>Would take again</strong>
                                    </CardTitle>

                                    <CardText
                                        style={{ fontSize: "70px" }}
                                        body className="text-center">
                                        <strong>{(this.props.statistics.teacherStatistics.wouldTakeAgainRatio * 100).toFixed(0)}%</strong>
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
                                {!this.props.statistics.isLoading ? this.renderTeacherRatingHistory() : <Spinner type="grow" color="success" />}
                            </CardBody>
                        </Card>
                    </UncontrolledCollapse>
                </div>
            </React.Fragment>
        );
    }

    private renderNumber(title: string, titleFontSize: string, bodyTextFontSize: string, value: number) {
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
                        <strong>{value.toFixed(1)}</strong>
                    </CardText>
                </CardBody>
            </Card>
        )
    }

    private renderTeacherRatingHistory() {
        const averageMarks = this.props.statistics.teacherStatistics.averageMarks;
        if (!averageMarks || averageMarks.length === 0)
            return;

        var data = [];
        data.push(['Date', 'Rating']);
        averageMarks.forEach((dateMark) => {
            let statDate = new Date(dateMark.date);
            var dateString = `${statDate.getFullYear()}/${statDate.getMonth() + 1}/${statDate.getDate()}`;
            data.push([dateString, dateMark.mark]);
        });

        return (
            <div className="my-pretty-chart-container">
                <Chart
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
                            title: 'Rating',
                            minValue: 1,
                            maxValue: 5,
                        },
                        width: 1000,
                        height: 400,
                        legend: "none",
                    }}
                    rootProps={{ 'data-testid': '1' }}
                />
            </div>
        )
    }

    private renderTags() {
        const tagTextCounts = this.getTeacherTagTextCounts();
        return (
            <div className="tagbox">
                {Array.from(tagTextCounts).sort((a, b) => b[1] - a[1]).map((tagTextCount) =>
                    <span>
                        {tagTextCount[0]} ({tagTextCount[1]})
                    </span>)}
            </div>
        )
    }

    private getTeacherTagTextCounts(): Map<string, number> {
        let tagTextCounts = new Map<string, number>();
        var ratings = this.props.ratings.ratings;

        for (let rating of ratings)
            tagTextCounts = rating.tags.reduce((counter, tag) =>
                counter.set(tag.text, 1 + (counter.get(tag.text) || 0)), tagTextCounts);
        
        return tagTextCounts;
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

