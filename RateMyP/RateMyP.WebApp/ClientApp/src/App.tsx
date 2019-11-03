import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import Browse from './components/Browse/Browse';
import TeacherProfile from './components/TeacherProfile/TeacherProfile';
import Rate from './components/Rate';

import './custom.css'

export default () => (
    <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/browse' component={Browse} />
        <Route path='/fetch-data/:startDateIndex?' component={FetchData} />
        <Route path='/profile/:teacherId' component={TeacherProfile} />
        <Route path='/rate/:teacherId' component={Rate} />
    </Layout>
);
