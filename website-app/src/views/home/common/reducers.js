import actionTypes from './actionTypes';
import initialState from './initialState';

function getMenuList(state, action) {
    return { ...state, msg: action.msg };
}
function showNewsList(state, action) {
    return { ...state, news: action.news };
}
// 执行对应的action
export default function homeActions(state = initialState, action) {
    switch (action.type) {
        case actionTypes.HOME_MENU_SELECT:
            return getMenuList(state, action)
        case actionTypes.HOME_NEWS_SHOW:
            return showNewsList(state, action)
        default:
            return state
    }
};