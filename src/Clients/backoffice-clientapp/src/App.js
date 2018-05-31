import React, { Component } from 'react';
import Routes from './Routes';
import { BrowserRouter } from 'react-router-dom';

// import { runWithAdal } from 'react-adal';
// import { authContext } from './services/AuthService';
// runWithAdal(authContext, () => {
//   console.log(authContext);
// });

class App extends Component {
  render() {
    return (
      <BrowserRouter>
        <Routes />
      </BrowserRouter>
    );
  }
}

export default App;
