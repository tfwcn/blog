import actionTypes from './actionTypes';
import initialState from './initialState';

function loginUser(state, action) {
    return { ...state, userId: action.userId };
}
// 执行对应的action
export default function homeActions(state = initialState, action) {
    switch (action.type) {
        case actionTypes.LOGIN_USER:
            return loginUser(state, action)
        default:
            return state
    }
};