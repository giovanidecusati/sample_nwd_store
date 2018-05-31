import React, { Component } from 'react';
import Topbar from '../components/common/Topbar';
import Footer from '../components/common/Footer';

class HomePage extends Component {
  constructor(props) {
    super(props);
    this.state = { user: { name: 'Giovani Decusati' } };
  }

  render() {
    return (
      <div>
        <Topbar user={this.state.user} />
        <section className="section">
          <div className="container" />
        </section>
        <Footer />
      </div>
    );
  }
}

export default HomePage;
