import actionTypes from '@/common/actionTypes';

export const webLoaderShow = function (action) {
    return {
        type: actionTypes.MANAGER_WEBLOADER_SHOW,
        webLoader: action.webLoader,
    }
}
