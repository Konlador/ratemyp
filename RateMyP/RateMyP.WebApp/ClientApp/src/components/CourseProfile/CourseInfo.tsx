import * as React from 'react';
import { connect } from 'react-redux';
import { ApplicationState } from '../../store';
import * as CoursesStore from '../../store/Courses';
import { CourseType } from '../../store/Courses';

interface OwnProps {
    courseId: string
};

type Props =
    CoursesStore.CoursesState &
    typeof CoursesStore.actionCreators;

class CourseInfo extends React.PureComponent<Props & OwnProps> {
    public componentDidMount() {
        this.ensureDataFetched();
    }

    public componentDidUpdate() {
        this.ensureDataFetched();
    }

    public render() {
        return (
            <React.Fragment>
                {this.renderCourseInfo()}
            </React.Fragment>
        );
    }

    private ensureDataFetched() {
        this.props.requestCourse(this.props.courseId);
    }

    private renderCourseInfo() {
        const course = this.props.selectedCourse;
        if (!course)
            return;

        return (
            <React.Fragment>
                <div>
                    Course
                    <h1>
                        {course.name}
                    </h1>
                </div>
                
                <p>
                    <p><strong>Faculty: </strong>{course.faculty}</p>
                    <p><strong>Credits: </strong>{course.credits}</p>
                    <p><strong>Course type: </strong>{CourseType[course.courseType]}</p>
                </p>
            </React.Fragment>
        );
    }
}

function mapStateToProps(state: ApplicationState, ownProps: OwnProps) {
    return {
        ...state.courses,
        courseId: ownProps.courseId
    }
};

export default connect(
    mapStateToProps, // Selects which state properties are merged into the component's props
    CoursesStore.actionCreators // Selects which action creators are merged into the component's props
)(CourseInfo as any);

