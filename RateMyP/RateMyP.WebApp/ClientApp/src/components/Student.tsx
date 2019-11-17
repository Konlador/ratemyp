import * as React from 'react';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as StudentStore from '../store/Student';
import firebase from "firebase";
import StyledFirebaseAuth from "react-firebaseui/StyledFirebaseAuth";

type Props =
    StudentStore.StudentState &
    typeof StudentStore.actionCreators;

class Student extends React.PureComponent<Props> {
    private uiConfig: firebaseui.auth.Config = {
        signInFlow: "popup",
        signInOptions: [
          firebase.auth.GoogleAuthProvider.PROVIDER_ID,
          firebase.auth.EmailAuthProvider.PROVIDER_ID
        ],
        callbacks: ({
            signInSuccess: () => false
        }) as any
      }

    public componentDidMount() {
        firebase.auth().onAuthStateChanged(user => {
            if (!!user){
                this.props.login(user);
            } else {
                this.props.logout();
            }
        });
    }

    public render() {
        return (
            <div>
                {this.props.isLoggedIn ? (
                    <span>
                        <div>Signed In!</div>
                        <button onClick={() => firebase.auth().signOut()}>Sign out!</button>
                        <h1>Welcome {firebase.auth().currentUser!.displayName}</h1>
                        <img
                        alt="profile picture"
                        src={firebase.auth().currentUser!.photoURL !== null ? firebase.auth().currentUser!.photoURL! : undefined}
                        />
                    </span>
                ) : (
                    <StyledFirebaseAuth
                        uiConfig={this.uiConfig!}
                        firebaseAuth={firebase.auth()}
                    />
                )}
            </div>
        );
    }
}
 
export default connect(
    (state: ApplicationState) => state.student,
    StudentStore.actionCreators
)(Student as any);
