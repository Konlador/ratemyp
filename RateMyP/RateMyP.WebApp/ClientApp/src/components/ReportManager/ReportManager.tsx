import * as React from 'react';
import RatingReports from "./RatingReports";
import CustomStarReports from "./CustomStarReports";

export default class ReportManager extends React.PureComponent {
    public render() {
        return (
            <React.Fragment>
                <RatingReports />
                <CustomStarReports />
            </React.Fragment>
        );
    }
}
