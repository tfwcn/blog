import React from 'react';
import ReactDOM from 'react-dom';
// import 'amfe-flexible/index.js';//移动端适配方案
import './index.scss';
import IndexLayer from './IndexLayer';
import * as serviceWorker from './serviceWorker';

ReactDOM.render(
  <IndexLayer name="hello" value={50} />,
  document.getElementById('root')
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: http://bit.ly/CRA-PWA
serviceWorker.unregister();
