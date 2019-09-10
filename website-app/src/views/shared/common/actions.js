import actionTypes from './actionTypes';

export const setValue = function (values) {
    return {
        type: actionTypes.SET_VALUE,
        ...values,
    }
}