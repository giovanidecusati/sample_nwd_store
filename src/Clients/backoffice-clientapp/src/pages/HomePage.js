import React, { Component } from 'react';

class HomePage extends Component {
  constructor(props) {
    super(props);
    this.state = { user: { name: 'Giovani Decusati' } };
  }

  render() {
    return (
      <section className="section">
        <div className="container" />
      </section>
    );
  }
}

export default HomePage;
