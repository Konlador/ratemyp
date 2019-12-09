import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { Button, Spinner, Jumbotron } from 'reactstrap';
import { ApplicationState } from '../../store';
import * as RatingReportsStore from '../../store/Reports/RatingReports';
import Rating from '../Rating/Rating';

type Props =
    RatingReportsStore.RatingReportsState &
    typeof RatingReportsStore.actionCreators &
    RouteComponentProps<{}>;

class RatingReports extends React.PureComponent<Props> {
    public componentDidMount() {
        this.props.requestRatingReports();
    }

    public render() {
        return (
            <React.Fragment>
                <h1>Rating reports</h1>
                {this.props.isLoading && <Spinner type="grow" color="success" />}
                {this.props.reports.map((report, index: number) => {
                    return (
                        <Jumbotron key={index} className="rating-report">
                            <Rating rating={report.rating!}/>
                            {this.renderReport(report)}
                            {this.renderActions(report)}
                        </Jumbotron >
                    );
                })}
            </React.Fragment>
        );
    }

    private renderReport(report: RatingReportsStore.RatingReport) {
        return (
            <div>
                <p><strong>Student: </strong>{report.studentId}</p>
                <p><strong>Reason: </strong>{report.reason}</p>
            </div>
        );
    }

    private renderActions(report: RatingReportsStore.RatingReport) {
        return (
            <div>
                <p>What's your verdict?</p>
                <Button className="add-rating" color="success" onClick={() => this.props.deleteRatingReport(report.id)}>Rating is ok</Button>{' '}
                <Button className="add-rating" color="danger" onClick={() => this.props.deleteRatingReport(report.id)}>Remove rating</Button>{' '}
            </div>
        );
    }
}

function mapStateToProps(state: ApplicationState) {
    return {
        ...state.ratingReports
    }
};

const actions = {
  ...RatingReportsStore.actionCreators,
}

export default connect(mapStateToProps, actions)(RatingReports as any);
