import { combineReducers } from 'redux';
import { connectRouter } from 'connected-react-router';
import homeReducer from './features/home/redux/reducer';

export default history =>
  combineReducers({
    router: connectRouter(history), //整合router与redux
    home: homeReducer,
  });
