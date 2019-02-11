import { HOME_BLOG_INIT } from './constants';

const type = HOME_BLOG_INIT;

//调用入口
export function homeBlogInit(blogList) {
  return {
    type,
    blogList,
  };
}

//更新state
export function reducer(state, action) {
  switch (action.type) {
    case type: {
      return {
        ...state,
        ...action,
      };
    }
    default:
      return state;
  }
}
