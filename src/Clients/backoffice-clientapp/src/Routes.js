import React from 'react';
import { Switch, Route } from 'react-router-dom';
import Home from './pages/HomePage';
import Products from './pages/ProductsPage';

const Routes = () => (
  <Switch>
    <Route exact path="/" component={Home} />
    <Route exact path="/products" component={Products} />
    <Route exact path="/customers" component={Home} />
    <Route exact path="/orders" component={Home} />
    <Route exact path="/profile" component={Home} />
  </Switch>
);

export default Routes