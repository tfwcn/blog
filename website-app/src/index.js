import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux'
import { BrowserRouter, Route } from 'react-router-dom'
import './styles/reset.scss';
import './styles/index.scss';
import Home from './views/home/components/index';
import * as serviceWorker from './serviceWorker';
import store from './common/reducers'

// 在root标签显示App组件
ReactDOM.render(
    <Provider store={store}>
        <BrowserRouter>
            <Route exact path="/" component={Home} />
        </BrowserRouter>
    </Provider>,
    document.getElementById('root')
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
// 客户端渲染
// serviceWorker.unregister();
// 同构，服务端渲染，加载更快
serviceWorker.register();
