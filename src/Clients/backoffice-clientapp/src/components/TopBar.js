import React, { Component } from 'react';
import Menu from './Menu';

class TopBar extends Component {
  render() {
    return (
      <section className="hero is-primary is-small animated slideInDown">
        <div className="hero-head">
          <header className="nav">
            <div className="container">
              <div className="nav-left">
                <a className="nav-item pacifico medium-text">
                  NorthWind Backoffice Module
                </a>
              </div>
              <span className="nav-toggle">
                <span />
                <span />
                <span />
              </span>
              <Menu />
            </div>
          </header>
        </div>
      </section>
    );
  }
}

export default TopBar;
