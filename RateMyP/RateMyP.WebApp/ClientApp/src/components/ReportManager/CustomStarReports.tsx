import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { Button, Spinner, Jumbotron } from 'reactstrap';
import { ApplicationState } from '../../store';
import * as CustomStarReportsStore from '../../store/Reports/CustomStarReports';
import CustomStarDisplay from '../CustomStar/CustomStarDisplay';

type Props =
    CustomStarReportsStore.CustomStarReportsState &
    typeof CustomStarReportsStore.actionCreators &
    RouteComponentProps<{}>;

class CustomStarReports extends React.PureComponent<Props> {
    public componentDidMount() {
        this.props.requestCustomStarReports();
    }
    
    public render() {
        return (
            <React.Fragment>
                <h1>Custom star reports</h1>
                {this.props.isLoading && <Spinner type="grow" color="success" />}
                {this.props.reports.map((report, index: number) => {
                    return (
                        <Jumbotron key={index} className="custom-star-report">
                            <CustomStarDisplay customStar={report.customStar!}/>
                            {this.renderReport(report)}
                            {this.renderActions(report)}
                        </Jumbotron >
                    );
                })}
            </React.Fragment>
        );
    }

    private renderReport(report: CustomStarReportsStore.CustomStarReport) {
        return (
            <div>
                <p><strong>Student: </strong>{report.studentId}</p>
                <p><strong>Reason: </strong>{report.reason}</p>
            </div>
        );
    }

    private renderActions(report: CustomStarReportsStore.CustomStarReport) {
        return (
            <div>
                <p>What's your verdict?</p>
                <Button className="add-rating" color="success" onClick={() => this.props.deleteCustomStarReport(report.id)}>Custom star is ok</Button>{' '}
                <Button className="add-rating" color="danger" onClick={() => this.props.deleteCustomStarReport(report.id)}>Remove custom star</Button>{' '}
            </div>
        );
    }
}

function mapStateToProps(state: ApplicationState) {
    return {
        ...state.customStarReports
    }
};

const actions = {
  ...CustomStarReportsStore.actionCreators,
}

export default connect(mapStateToProps, actions)(CustomStarReports as any);
