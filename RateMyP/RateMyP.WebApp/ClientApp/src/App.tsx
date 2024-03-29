import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home/Home';
import Browse from './components/Browse/Browse';
import Student from './components/Student';
import Leaderboard from './components/Leaderboard/Leaderboard';
import TeacherProfile from './components/TeacherProfile/TeacherProfile';
import CourseProfile from './components/CourseProfile/CourseProfile';
import RateTeacher from './components/Rate/RateTeacher';
import RateCourse from './components/Rate/RateCourse';
import RatingReport from './components/RatingReport/RatingReport';
import CustomStarUpload from './components/CustomStar/CustomStarUpload';
import CustomStarLeaderboard from './components/CustomStar/CustomStarShowcase';
import CustomStarReport from './components/CustomStarReport/CustomStarReport';
import ReportManager from './components/ReportManager/ReportManager';
import Shop from './components/Shop/Shop';
import firebase from "firebase";
import './custom.css';

firebase.initializeApp({
    apiKey: "AIzaSyATzXFbDyQYupqUw_va7NKEgPVOGfCFWb4",
    authDomain: "ratemyp-44d4c.firebaseapp.com"
});

class App extends React.Component<{}> {
    public render() {
        return (
            <div>
                <Layout>
                    <Route exact path='/' component={Home} />
                    <Route path='/browse' component={Browse} />
                    <Route path='/student' component={Student} />
                    <Route path='/leaderboard' component={Leaderboard} />
                    <Route path='/teacher-profile/:teacherId' component={TeacherProfile} />
                    <Route path='/course-profile/:courseId' component={CourseProfile} />
                    <Route path='/rate-teacher/:teacherId' component={RateTeacher} />
                    <Route path='/rate-course/:courseId' component={RateCourse} />
                    <Route path='/rating-report/:ratingId' component={RatingReport} />
                    <Route path='/add-custom-star/:teacherId' component={CustomStarUpload} />
                    <Route path='/custom-star/:teacherId' component={CustomStarLeaderboard} />
                    <Route path='/custom-star-report/:customStarId' component={CustomStarReport} />
                    <Route path='/reports' component={ReportManager} />
                    <Route path='/shop' component={Shop} />
                </Layout>
            </div>
        );
    }
}

export default App;