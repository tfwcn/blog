import actionTypes from './actionTypes';

export const getMenuList = function (msg) {
    return {
        type: actionTypes.HOME_MENU_SELECT,
        msg: 'aaa',
    }
}

export const showNewsList = function (list, count, status, errorMsg, page, rows) {
    let pageCount = Math.ceil(count / rows);
    return {
        type: actionTypes.HOME_NEWS_SHOW,
        news: {
            list: list,
            count: count,
            status: status,
            errorMsg: errorMsg,
            page: page,
            rows: rows,
            pageCount: pageCount,
        },
    }
}