import * as React from 'react';
import { Link } from 'react-router-dom';
import { Button } from 'reactstrap';
import CustomStarShowcase from '../CustomStar/CustomStarShowcase';
import './TeacherCustomStarShowcase.css';

export default class TeacherCustomStarShowcase extends React.PureComponent<{ teacherId: string }> {
    public render() {
        return (
            <React.Fragment>
                <CustomStarShowcase teacherId={this.props.teacherId} showItems = {3}/>
                <Button className="see-all-custom-stars" color="primary" tag={Link} to={`/custom-star/${this.props.teacherId}`}>See all custom stars</Button>
            </React.Fragment>
        );
    }
}
