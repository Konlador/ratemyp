import React from 'react';
import './Navigation.css';
import { Navbar, Nav, NavItem } from 'react-bootstrap';
import { NavLink } from 'react-router-dom';
import { LinkContainer } from 'react-router-bootstrap';
 
const navigation = () => {
    return (
<nav class="navbar navbar-default">

  <div class="container-fluid">
    <div class="navbar-header">
      <a class="navbar-brand" href="#">Brand</a>
    </div>

    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
      <ul class="nav navbar-nav">
        <li>
            <a>
            <LinkContainer to={'/'}>
                <NavItem eventKey={1}>
                    Home
                </NavItem>
            </LinkContainer>
            </a>
        </li>
        <li>
            <a>
            <LinkContainer to={'/browse'}>
                <NavItem eventKey={2}>
                    Browse
                </NavItem>
            </LinkContainer>
            </a>
        </li>
      </ul>
      <ul class="nav navbar-nav navbar-right">
        <li><a>
            <LinkContainer to={'/'}>
                <NavItem eventKey={3}>
                    nzn
                </NavItem>
            </LinkContainer>
        </a></li>
      </ul>
    </div>
  </div>
</nav>
    )
}
 
export default navigation;