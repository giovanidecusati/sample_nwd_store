import React, { Component } from 'react';
import AdalService from '../../services/AdalService';

class Welcome extends Component {
  constructor(props) {
    super(props);

    adalService = new AdalService();
  }

  render() {
    return (
      <div id="navbarExampleTransparentExample" className="navbar-menu">
        <div className="navbar-end">
          <div className="navbar-item">
            <div className="field is-grouped">
              <p className="control">
                <a
                  className="button is-primary"
                  href="https://github.com/jgthms/bulma/releases/download/0.7.1/bulma-0.7.1.zip"
                >
                  <span>Welcome, {this.props.user}</span>
                </a>
              </p>

              <p className="control">
                <a
                  className="button is-primary"
                  href="https://github.com/jgthms/bulma/releases/download/0.7.1/bulma-0.7.1.zip"
                >
                  <span className="icon">
                    <i className="fas fa-power-off" />
                  </span>
                  <span>Sign Out</span>
                </a>
              </p>
            </div>
          </div>
        </div>
      </div>
    );
  }
}

export default Welcome;
