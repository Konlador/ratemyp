import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { ApplicationState } from '../store';
import * as RatingsStore from '../store/Ratings';

type RateProps =
    RatingsStore.RatingsState &
    typeof RatingsStore.actionCreators &
    RouteComponentProps<{ teacherId: string}>;

class Rate extends React.PureComponent<RateProps> {
    public componentDidMount() {
        this.ensureDataFetched();
    }

    public componentDidUpdate() {
        this.ensureDataFetched();
    }

    public render() {
        return (
            <React.Fragment>
                Pakolkas nieko
            </React.Fragment>
        );
    }

    private ensureDataFetched() {
    }
}

export default connect(
    (state: ApplicationState) => state.ratings, // Selects which state properties are merged into the component's props
    RatingsStore.actionCreators // Selects which action creators are merged into the component's props
)(Rate as any);
