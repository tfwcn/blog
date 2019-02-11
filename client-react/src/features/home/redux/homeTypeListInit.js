import { HOME_TYPELIST_INIT } from './constants';

function getBreadcrumbList(list, key) {
  let result = [];
  list.map(item => {
    if (item.key == key) {
      let pItem = getBreadcrumbList(list, item.pkey);
      result = [...pItem, item];
      return;
    }
  });
  return result;
}

//调用入口
export function homeTypeListInit(key) {
  let typeList = [
    { key: 'ai', value: '人工智能', pkey: '' },
    { key: 'dotNet', value: '.NET技术', pkey: '' },
    { key: 'wpf', value: 'WPF', pkey: 'dotNet' },
    { key: 'aspNet', value: 'ASP.NET', pkey: 'dotNet' },
    { key: 'aspNet2', value: 'ASP.NET2', pkey: 'aspNet' },
  ];
  let breadcrumbList = getBreadcrumbList(typeList, key);
  return {
    type: HOME_TYPELIST_INIT,
    typeList,
    breadcrumbList,
  };
}

//执行逻辑
export function reducer(state, action) {
  switch (action.type) {
    case HOME_TYPELIST_INIT: {
      return {
        ...state,
        ...action,
      };
    }
    default:
      return state;
  }
}
