import actionTypes from './actionTypes';

export const getMenuList = function(msg){
    return {
        type:actionTypes.HOME_MENU_SELECT,
        msg:'aaa',
    }
}

export const getNewsList = function(newsList){
    return {
        type:actionTypes.HOME_TIME,
        newsList: newsList,
    }
}