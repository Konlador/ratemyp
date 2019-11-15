import * as React from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink, Card, CardTitle, CardBody } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';

export default class NavMenu extends React.PureComponent<{}, { isOpen: boolean }> {
    public state = {
        isOpen: false
    };

    public render() {
        return (
            <header>
                <Navbar className="default" light style={{ backgroundColor: '#100E17', width: '100vw' }}>
                    <Container>
                        <NavbarBrand style={{ color: '#F66A27' }} tag={Link} to="/">RateMyP.WebApp</NavbarBrand>
                        <ul className="NavMenu">
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/">
                                    <Card style={{ backgroundColor: '#100E17' }}>
                                        <img width="50px" height="50px" src={require('../images/house.svg')}/>
                                            <CardTitle style={{ color: 'white' }}>
                                                <strong>Home</strong>
                                            </CardTitle>
                                    </Card>
                                </NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/browse">
                                <Card style={{ backgroundColor: '#100E17' }}>
                                        <img width="50px" height="50px" src={require('../images/clipboard.svg')}/>
                                            <CardTitle style={{ color: 'white' }}>
                                                <strong>Browse</strong>
                                            </CardTitle>
                                    </Card>
                                </NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/student">
                                <Card style={{ backgroundColor: '#100E17' }}>
                                        <img width="50px" height="50px" src={require('../images/profile.svg')}/>
                                            <CardTitle style={{ color: 'white' }}>
                                                <strong>Login</strong>
                                            </CardTitle>
                                    </Card>
                                </NavLink>
                            </NavItem>
                        </ul>
                    </Container>
                </Navbar>
            </header>
        );
    }

    private toggle = () => {
        this.setState({
            isOpen: !this.state.isOpen
        });
    }
}
