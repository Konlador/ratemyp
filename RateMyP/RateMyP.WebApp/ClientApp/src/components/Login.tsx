import * as React from 'react';
import { connect } from 'react-redux';
import ReactDOM from 'react-dom';
import { GoogleLogin, GoogleLoginResponse, GoogleLoginResponseOffline } from 'react-google-login';

const responseGoogle = (response: GoogleLoginResponse | GoogleLoginResponseOffline) => {
    console.log(response);
}

class Login extends React.PureComponent<{}> {

    public render() {
        return (
            <div>
                <h1>Log In to Rate My Professors</h1>
                <GoogleLogin
                    clientId="658977310896-knrl3gka66fldh83dao2rhgbblmd4un9.apps.googleusercontent.com"
                    buttonText="Login"
                    onSuccess={responseGoogle}
                    onFailure={responseGoogle}
                    cookiePolicy={'single_host_origin'}
                />
            </div>
        );
    }
}


 
 

 
export default connect()(Login);