import {
  HOME_BLOG_LIST_LOADING,
  HOME_BLOG_LIST_SUCCESS,
  HOME_BLOG_LIST_ERROR,
} from './constants';

//调用入口
export function homeBlogListLoading() {
  return {
    type: HOME_BLOG_LIST_LOADING,
    blogList: null,
    blogStatus: 'loading',
    error: null,
  };
}
export function homeBlogListSuccess(response) {
  let blogList = response.result.list;
  return {
    type: HOME_BLOG_LIST_SUCCESS,
    blogStatus: 'success',
    blogList,
  };
}
export function homeBlogListError(error) {
  return {
    type: HOME_BLOG_LIST_ERROR,
    blogStatus: 'error',
    error: error,
  };
}

//更新state
export function reducer(state, action) {
  switch (action.type) {
    case HOME_BLOG_LIST_LOADING:
    case HOME_BLOG_LIST_SUCCESS:
    case HOME_BLOG_LIST_ERROR: {
      return {
        ...state,
        ...action,
      };
    }
    default:
      return state;
  }
}
