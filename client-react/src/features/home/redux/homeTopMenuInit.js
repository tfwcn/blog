import { HOME_TOPMENU_INIT } from './constants';

export function homeTopMenuInit() {
  return {
    type: HOME_TOPMENU_INIT,
  };
}

export function reducer(state, action) {
  switch (action.type) {
    case HOME_TOPMENU_INIT:
      return {
        ...state,
        topMenuList: {
          '': '首页',
          dotnet: '.Net',
          java: 'Java',
          python: 'Python',
          other: '其它',
        },
      };

    default:
      return state;
  }
}
