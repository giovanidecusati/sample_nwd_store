import React, { Component } from 'react';
import { NavLink } from 'react-router-dom';
import Welcome from './Welcome';

class Menu extends Component {
  render() {
    return (
      <div id="navbarExampleTransparentExample" className="navbar-menu">
        <div className="navbar-start">
          <NavLink to="/" className="navbar-item">
            Home
          </NavLink>
          <NavLink to="/categories" className="navbar-item">
            Categories
          </NavLink>
          <NavLink to="/products" className="navbar-item">
            Products
          </NavLink>
          <div className="navbar-item has-dropdown is-hoverable">
            <NavLink to="/" className="navbar-link">
              Reports
            </NavLink>
            <div className="navbar-dropdown is-boxed">
              <NavLink to="/" className="navbar-item">
                Open Orders
              </NavLink>
              <NavLink to="/" className="navbar-item">
                Delivered Orders
              </NavLink>
              <hr className="navbar-divider" />
              <NavLink to="/" className="navbar-item">
                Stock
              </NavLink>
            </div>
          </div>
        </div>
        <Welcome {...this.props} />
      </div>
    );
  }
}

export default Menu;
