import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Browse from './components/Browse';
import Login from './components/Login';
import TeacherProfile from './components/TeacherProfile/TeacherProfile';
import CourseProfile from './components/CourseProfile/CourseProfile';
import RateTeacher from './components/Rate/RateTeacher';
import RateCourse from './components/RateCourse';

import './custom.css'

export default () => (
    <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/browse' component={Browse} />
        <Route path='/login' component={Login} />
        <Route path='/teacher-profile/:teacherId' component={TeacherProfile} />
        <Route path='/course-profile/:courseId' component={CourseProfile} />
        <Route path='/rate-teacher/:teacherId' component={RateTeacher} />
        <Route path='/rate-course/:coureId' component={RateCourse} />
    </Layout>
);
