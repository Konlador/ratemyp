import * as React from 'react';
import { Container, Navbar, NavbarBrand, NavItem, NavLink, Card, CardTitle } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';


export default class NavMenu extends React.PureComponent<{}, { isOpen: boolean }> {
    public state = {
        isOpen: false
    };

    public render() {
        return (
            <header>
                <Navbar className="navbarstyle" light>
                    <Container>
                        <NavbarBrand tag={Link} to="/"><h1 className="text"><strong>RateMyP.WebApp</strong></h1></NavbarBrand>
                        <ul className="NavMenu">
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/">
                                    <Card className="cardstyle">
                                        <img width="50px" height="50px" src={require('../images/house.svg')} />
                                        <CardTitle className="text">
                                            Home
                                        </CardTitle>
                                    </Card>
                                </NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/browse">
                                    <Card className="cardstyle">
                                        <img width="50px" height="50px" src={require('../images/clipboard.svg')} />
                                        <CardTitle className="text">
                                            Browse
                                        </CardTitle>
                                    </Card>
                                </NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/student">
                                    <Card className="cardstyle">
                                        <img width="50px" height="50px" src={require('../images/profile.svg')} />
                                        <CardTitle className="text">
                                            Login
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
