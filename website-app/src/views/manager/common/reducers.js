import actionTypes from './actionTypes';
import initialState from './initialState';

function setValue(state, action) {
    let newState = { ...state, ...action };
    delete newState.type;
    return newState;
}
// 执行对应的action
export default function homeActions(state = initialState, action) {
    switch (action.type) {
        case actionTypes.SET_VALUE:
            return setValue(state, action)
        default:
            return state
    }
};