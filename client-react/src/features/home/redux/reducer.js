import initialState from './initialState';
import { reducer as homeTopMenuInit } from './homeTopMenuInit';
import { reducer as homeTypeListInit } from './homeTypeListInit';

const reducers = [homeTopMenuInit, homeTypeListInit];

export default function reducer(state = initialState, action) {
  let newState;
  switch (action.type) {
    // Handle cross-topic actions here
    default:
      newState = state;
      break;
  }
  return reducers.reduce((s, r) => r(s, action), newState);
}
