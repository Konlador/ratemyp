import * as React from 'react';
import { connect } from 'react-redux';
import { ApplicationState } from '../../store';
import * as TeacherStatisticsStore from '../../store/TeacherStatistics';
import { Row, Col } from 'reactstrap';
import {
    Card, CardTitle, CardText, CardBody  } from 'reactstrap';

interface OwnProps {
    teacherId: string
};

type Props =
    {
    statistics: TeacherStatisticsStore.TeacherStatisticsState
    } &
    typeof TeacherStatisticsStore.actionCreators;

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
                </Row>
            </div>
        );
    }
}

function mapStateToProps(state: ApplicationState, ownProps: OwnProps) {
    return {
        statistics: state.teacherStatistics,
        teacherId: ownProps.teacherId
    }
};

const actions = {
    ...TeacherStatisticsStore.actionCreators
}

export default connect(
    mapStateToProps,
    actions
)(TeacherStatistics as any);

