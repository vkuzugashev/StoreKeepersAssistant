import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { InventoryMovies } from './components/InventoryMovies';
import { Invoice } from './components/Invoice';
//import { RemainsReport } from './components/RemainsReport';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
            <Route exact path='/' component={Home} />
            <Route path='/InventoryMovies' component={InventoryMovies} />
            <Route path='/invoice' component={Invoice} />
            {/*<Route path='/remains-report' component={RemainsReport} />*/}
      </Layout>
    );
  }
}
