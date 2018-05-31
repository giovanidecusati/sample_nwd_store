import React from 'react';
import { Switch, Route } from 'react-router-dom';
import HomePage from './pages/HomePage';
import ProductsPage from './pages/ProductsPage';

class Routes extends React.Component {
  render () {
    return (
      <Switch>
        <Route exact path="/" component={HomePage} />
        <Route exact path="/products" component={ProductsPage}/>}/>
        <Route exact path="/customers" component={HomePage} />
        <Route exact path="/orders" component={HomePage} />
        <Route exact path="/profile" component={HomePage} />
      </Switch>)
  }
}

export default Routes