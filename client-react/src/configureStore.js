import { createBrowserHistory } from 'history';
import { applyMiddleware, compose, createStore } from 'redux';
import { routerMiddleware } from 'connected-react-router';
import { createLogger } from 'redux-logger';
import createRootReducer from './reducers';

const loggerMiddleware = createLogger();

export const history = createBrowserHistory();

function configureStore(preloadedState) {
  const store = createStore(
    createRootReducer(history), // root reducer with router state
    preloadedState,
    compose(
      applyMiddleware(
        //中间件
        routerMiddleware(history), // for dispatching history actions
        loggerMiddleware // 一个很便捷的 middleware，用来打印 action 日志
        // ... other middlewares ...
      )
    )
  );

  return store;
}

export const store = configureStore();
