import React, { Component } from 'react';

class Footer extends Component {
  render() {
    return (
      <footer className="footer">
        <div className="container">
          <div className="content has-text-centered">
            <p>
              <strong>NorthWind Backoffice Module</strong> by{' '}
              <a href="https://github.com/giovanidecusati">Giovani Decusati</a>.
            </p>
            <p>
              <a
                className="icon"
                href="https://github.com/giovanidecusati/sample_nwd_store">
                <i className="fa fa-github" />
              </a>
            </p>
          </div>
        </div>
      </footer>
    );
  }
}

export default Footer;
