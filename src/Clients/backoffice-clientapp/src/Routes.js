import React from 'react';
import { Switch, Route } from 'react-router-dom';
import HomePage from './pages/HomePage';
import ProductsPage from './pages/ProductsPage';
import Topbar from './components/common/Topbar';
import Footer from './components/common/Footer';

// import { runWithAdal } from 'react-adal';
// import { authContext } from './services/AuthService';
// runWithAdal(authContext, () => {
//   console.log(authContext);
// });

const DefaultLayout = ({ component: Component, ...rest }) => {
  return (
    <Route
      exact
      path="/"
      render={matchProps => (
        <div>
          <Topbar {...matchProps} />
          <Component {...matchProps} />
          <Footer />
        </div>
      )}
    />
  );
};

class Routes extends React.Component {
  constructor(props) {
    super(props);
    this.state = { user: { name: 'Giovani Decusati' } };
  }

  render() {
    return (
      <Switch>
        <DefaultLayout exact path="/" {...this.state} component={HomePage} />        
        <DefaultLayout
          exact
          path="/products"
          state={this.state}
          component={ProductsPage}
        />
      </Switch>
    );
  }
}

export default Routes;
