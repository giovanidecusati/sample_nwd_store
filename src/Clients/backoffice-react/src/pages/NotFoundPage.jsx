import React, { Component } from 'react';

class NotFoundPage extends Component {
  constructor(props) {
    super(props);
    this.state = { user: { name: 'Giovani Decusati' } };
  }

  render() {
    return (
      <section className="section">
        <div className="container">
          <h1 className="titel is-h3">Page Not Found.</h1>
        </div>
      </section>
    );
  }
}

export default NotFoundPage;
