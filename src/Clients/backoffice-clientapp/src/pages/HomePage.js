import React, { Component } from 'react';
import TopBar from '../components/TopBar';
import Footer from '../components/Footer';

class HomePage extends Component {
  render() {
    return (
      <div>
        <TopBar />
        <section className="section">
          <div className="container" />
        </section>
        <Footer />
      </div>
    );
  }
}

export default HomePage;
