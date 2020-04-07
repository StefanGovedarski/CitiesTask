import React, { Component } from 'react';
import { Cities } from './components/Cities';
import { Roads } from './components/Roads';
import { LogisticCenter } from './components/LogisticCenter';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
        <div>
            <h3 class="display-3">Cities Map</h3>
            <Cities></Cities>
            <Roads></Roads>
            <LogisticCenter></LogisticCenter>
      </div>
    );
  }
}
