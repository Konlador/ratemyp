import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import Teachers from './Teachers';
import Courses from './Courses';
import { Button, ButtonGroup } from "reactstrap";
import '../../extensions/StringExtensions';

type Props = RouteComponentProps<{ searchString: string }>;

class Browse extends React.PureComponent<Props> {

    public componentDidMount() {
        this.setState({ page: 0 })
    }

    state = {
        page: 0,
    }

    renderComponents() {
        if (this.props.match.params.searchString !== undefined) {
            if (this.props.match.params.searchString.substring(0, 7) === "teacher") {

                if (this.props.match.params.searchString.substring(8) !== undefined) {
                    return <Teachers searchString={this.props.match.params.searchString.substring(8)} />
                }
                else return <Teachers />
            }

            else if (this.props.match.params.searchString.substring(0, 6) == "course") {

                if (this.props.match.params.searchString.substring(7) !== undefined) {
                    return <Courses searchString={this.props.match.params.searchString.substring(7)} />
                }
                else return <Courses />
            }
        }
        else return <Teachers />
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
