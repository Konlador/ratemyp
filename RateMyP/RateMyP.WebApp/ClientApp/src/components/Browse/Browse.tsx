import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import Teachers from './Teachers';
import Courses from './Courses';
import { Button, ButtonGroup } from "reactstrap";
import '../../extensions/StringExtensions';


type Props = RouteComponentProps<{}>;

class Browse extends React.PureComponent<Props> {

    public componentDidMount() {
        this.setState({ page: 0 })
    }

    state = {
        page: 0,
    }

    renderComponents() {
        if (this.props.location.state !== undefined) {

            if (this.props.location.state.searchType === "teacher") {

                if (this.props.location.state.search !== undefined) {
                    return <Teachers search={this.props.location.state.search} />
                }
                else return <Teachers/>
            }

            else if (this.props.location.state.searchType == "course") {

                if (this.props.location.state.search !== undefined) {
                    return <Courses search={this.props.location.state.search} />
                }
                else return <Courses/>
            }
        }
        else {
            if (this.state.page === 0) return <Teachers />
            else return <Courses />
        }
    }

    switchPage() {
        if (this.state.page === 0) this.setState({ page: 1 })
        else this.setState({ page: 0 })
    }

    setButtonStatus() {
        return (this.state.page === 0) ? true : false
    }

    public render() {
        return (
            <React.Fragment>
                <div>
                    <h1 id="tabelLabel" style={{ marginBottom: '16px' }}>Browse
                    <ButtonGroup style={{ left: '75%' }}>
                            <Button color="primary" disabled={this.setButtonStatus()} onClick={() => this.switchPage()}>Staff</Button>
                            <Button color="primary" disabled={!this.setButtonStatus()} onClick={() => this.switchPage()}>Courses</Button>
                        </ButtonGroup>
                    </h1>
                </div>
                {this.renderComponents()}
            </React.Fragment>
        );
    }
}

export default connect()(Browse);
