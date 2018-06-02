import React from 'react';
import { Switch, Route } from 'react-router-dom';
import importedComponent from 'react-imported-component';
// import HomePage from './pages/HomePage';
// import ProductsPage from './pages/ProductsPage';
import NotFoundPage from './pages/NotFoundPage';
import Topbar from './components/common/Topbar';
import Footer from './components/common/Footer';
import Loading from './components/common/Loading';

const HomePage = importedComponent(() => import('./pages/HomePage'), {
  LoadingComponent: Loading,
});

const ProductsPage = importedComponent(() => import('./pages/ProductsPage'), {
  LoadingComponent: Loading,
});

// import { runWithAdal } from 'react-adal';
// import { authContext } from './services/AuthService';
// runWithAdal(authContext, () => {
//   console.log(authContext);
// });

class Routes extends React.Component {
  constructor(props) {
    super(props);
    this.state = { user: { name: 'Giovani Decusati' } };
  }

  render() {
    return (
      <div>
        <Topbar user={this.state.user.name} />
        <Switch>
          <Route exact path="/" component={HomePage} />
          <Route path="/products" component={ProductsPage} />
          <Route component={NotFoundPage} />
        </Switch>
        <Footer />
      </div>
    );
  }
}

export default Routes;
