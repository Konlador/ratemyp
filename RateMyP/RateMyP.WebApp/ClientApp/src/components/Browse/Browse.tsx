import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import Teachers from './Teachers';
import Courses from './Courses';
import { Button, ButtonGroup } from "reactstrap";

type Props = RouteComponentProps<{}>;

interface State {
    page: number
}

class Browse extends React.PureComponent<Props, State> {

    constructor(props: Props) {
        super(props);
        this.state = { page: 0 };
    };

    private renderComponents() {
        if (this.props.location.state !== undefined) {
            if (this.props.location.state.searchType === "teacher") {
                if (this.props.location.state.search !== undefined)
                    return <Teachers search={this.props.location.state.search} />
                else return <Teachers />
            }
            else if (this.props.location.state.searchType === "course") {
                if (this.props.location.state.search !== undefined)
                    return <Courses search={this.props.location.state.search} />
                else return <Courses />
            }
        }
        else
            return this.state.page === 0 ? <Teachers /> : <Courses />
    }

    private switchPage() {
        this.setState({ page: (this.state.page + 1) % 2 });
    }

    private getButtonStatus() {
        return this.state.page === 0;
    }

    public render() {
        return (
            <React.Fragment>
                <div>
                    <h1 id="tabelLabel" style={{ marginBottom: '16px' }}>Browse
                        <ButtonGroup style={{ left: '75%' }}>
                            <Button color="primary" disabled={this.getButtonStatus()} onClick={() => this.switchPage()}>Staff</Button>
                            <Button color="primary" disabled={!this.getButtonStatus()} onClick={() => this.switchPage()}>Courses</Button>
                        </ButtonGroup>
                    </h1>
                </div>
                {this.renderComponents()}
            </React.Fragment>
        );
    }
}

export default connect()(Browse);
