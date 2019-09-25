import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import './styles/reset.scss';
import './styles/index.scss';
import Login from '@/views/login/components/index';
import Home from '@/views/home/components/index';
import Manager from '@/views/manager/components/index';
import * as serviceWorker from './serviceWorker';
import store from './common/reducers';

// 在root标签显示App组件
ReactDOM.render(
    <Provider store={store}>
        <BrowserRouter>
            <Switch>
                <Route path="/login" component={Login} />
                <Route path="/manager" component={Manager} />
                <Route exact path="/" component={Home} />
            </Switch>
        </BrowserRouter>
    </Provider>,
    document.getElementById('root')
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
// 客户端渲染
serviceWorker.unregister();
// 同构，服务端渲染，加载更快
// serviceWorker.register();
