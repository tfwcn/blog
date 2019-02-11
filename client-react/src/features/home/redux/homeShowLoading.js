import { HOME_SHOW_LOADING } from './constants';

const type = HOME_SHOW_LOADING;

//调用入口
export function homeShowLoading(isLoading) {
  return {
    type,
    isLoading,
  };
}

//执行逻辑
export function reducer(state, action) {
  switch (action.type) {
    case type: {
      let isLoading = action.isLoading;
      return {
        ...state,
        isLoading,
      };
    }
    default:
      return state;
  }
}
