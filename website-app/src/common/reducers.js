import { combineReducers, createStore, applyMiddleware } from 'redux';
import { createLogger } from 'redux-logger';
import homeReducers from '../views/home/common/reducers';
import loginReducers from '../views/login/common/reducers';
import managerReducers from '../views/manager/common/reducers';
import sharedReducers from '../views/shared/common/reducers';

// 整合所有reducer
const reducers = {
  home: homeReducers,
  login: loginReducers,
  manager: managerReducers,
  shared: sharedReducers,
};

// 返回一个整合的reducer
const appReducers = combineReducers(reducers);

// 中间件
const middleware = []
if (process.env.NODE_ENV !== 'production') {
  middleware.push(createLogger())
}

// 创建store
const store = createStore(appReducers,
  applyMiddleware(...middleware)
);

export default store;