import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { connect } from 'react-redux';
import * as ShopStore from '../../store/Shop/Shop';
import './Shop.css';

type Props =
    typeof ShopStore.actionCreators &
    RouteComponentProps<{ teacherId: string }>;

class Shop extends React.PureComponent<Props> {
    public render() {
        return (
            <React.Fragment>
                <div>
                    Shopas
                </div>
            </React.Fragment>
        );
    }
}


export default connect(
    undefined
)(Shop as any);

/*
public render() {
        return (
            <React.Fragment>
                <TeacherInfo teacherId={this.props.match.params.teacherId}/>
                <TeacherStatistics teacherId={this.props.match.params.teacherId}/>
                <TeacherActivities teacherId={this.props.match.params.teacherId}/>
                <TeacherRatings teacherId={this.props.match.params.teacherId}/>
                <TeacherCustomStarShowcase teacherId={this.props.match.params.teacherId}/>
            </React.Fragment>
        );
    }
*/