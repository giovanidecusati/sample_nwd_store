import React, { Component } from 'react';
import Menu from './Menu';

class Topbar extends Component {
  render() {
    return (
      <nav className="navbar is-mobile is-transparent is-primary is-small animated slideInDown">
        <div className="navbar-brand">
          <a className="navbar-item">
            <h3 className="title is-h3">Northwind</h3>
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
        <Menu user={this.props.user} />
      </nav>
    );
  }
}

export default Topbar;
