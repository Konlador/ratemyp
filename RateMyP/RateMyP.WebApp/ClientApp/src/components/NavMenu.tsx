import * as React from 'react';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import { Container, Navbar, NavbarBrand, NavItem, NavLink, Card, CardTitle } from 'reactstrap';
import { Link } from 'react-router-dom';
import LeaderboardLogo from "../images/leaderboard.png";
import BrowseLogo from "../images/browse.png";
import LoginLogo from "../images/login.png";
import StoreLogo from "../images/store.png";
import HomeLogo from "../images/home.png";

import './NavMenu.css';

class NavMenu extends React.PureComponent<{}, { isOpen: boolean }> {
    public render() {
        return (
            <header>
                <Navbar className="navbarstyle">
                    <Container>
                        <NavbarBrand tag={Link} to="/"><div className="title"><h1>RateMyP.WebApp</h1></div></NavbarBrand>
                        <ul className="NavMenu">
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/">
                                    <div className="nav-menu-item">
                                        <img src={HomeLogo} />
                                        <p className="text">
                                            Home
                                        </p>
                                    </div>
                                </NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/leaderboard">
                                    <div className="nav-menu-item">
                                        <img src={LeaderboardLogo} />
                                        <p className="text">
                                            Leaderboard
                                        </p>
                                    </div>
                                </NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/browse">
                                    <div className="nav-menu-item">
                                        <img src={BrowseLogo} />
                                        <p className="text">
                                            Browse
                                        </p>
                                    </div>
                                </NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/shop">
                                    <div className="nav-menu-item">
                                        <img src={StoreLogo} />
                                        <p className="text">
                                            Store
                                        </p>
                                    </div>
                                </NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/student">
                                    <div className="nav-menu-item">
                                        <img src={LoginLogo} />
                                        <p className="text">
                                            Login
                                        </p>
                                    </div>
                                </NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/reports">
                                    <div className="nav-menu-item">
                                        <img src={LoginLogo} />
                                        <p className="text">
                                            Admin
                                        </p>
                                    </div>
                                </NavLink>
                            </NavItem>
                        </ul>
                    </Container>
                </Navbar>
            </header>
        );
    }
}

function mapStateToProps(state: ApplicationState) {
    return {
        
    }
};

export default connect(
    mapStateToProps
)(NavMenu as any);

