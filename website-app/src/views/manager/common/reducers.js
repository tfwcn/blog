import actionTypes from '@/common/actionTypes';
import initialState from './initialState';

function webLoaderShow(state, action) {
    return { ...state, webLoader: action.webLoader };
}

// 执行对应的action
export default function homeActions(state = initialState, action) {
    switch (action.type) {
        case actionTypes.MANAGER_WEBLOADER_SHOW:
            return webLoaderShow(state, action)
        default:
            return state
    }
};