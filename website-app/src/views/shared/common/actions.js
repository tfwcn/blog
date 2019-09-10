import actionTypes from './actionTypes';

export const setValue = function (value) {
    return {
        type: actionTypes.CONTROL_SET_VALUE,
        value: value,
    }
}