import React, { Component } from 'react';
import Menu from './Menu';

class Topbar extends Component {
  render() {
    return (
      <nav className="navbar is-mobile is-transparent is-primary is-small animated slideInDown">
        <div className="navbar-brand">
          <a className="navbar-item">
            <img src="logo.png" width="120" height="34" alt="Northwind" />
          </a>
          <div
            className="navbar-burger burger"
            data-target="navbarExampleTransparentExample"
          >
            <span />
            <span />
            <span />
          </div>
        </div>
        <Menu user={this.props} />
      </nav>
    );
  }
}

export default Topbar;
