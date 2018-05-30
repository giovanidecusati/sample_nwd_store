import React, { Component } from 'react';
import { NavLink } from 'react-router-dom';

class Menu extends Component {
  state = { user: null };

  render() {
    return (
      <div className="nav-right nav-menu">
        <NavLink to="/" className="nav-item">
          Home
        </NavLink>
        <NavLink to="products" className="nav-item">
          Products
        </NavLink>
        <NavLink to="customers" className="nav-item">
          Customers
        </NavLink>
        <NavLink to="orders" className="nav-item">
          Orders
        </NavLink>
        <NavLink to="profile" className="nav-item">
          Welcome, {this.state.user}
        </NavLink>
        <span className="nav-item">
          <NavLink to="logout" className="button is-primary is-inverted">
            <span className="icon">
              <i className="fa fa-power-off" />
            </span>
            <span>Sign Out</span>
          </NavLink>
        </span>
      </div>
    );
  }
}

export default Menu;
