import actionTypes from '@/common/actionTypes';

export const loginUser = function (userId) {
    return {
        type: actionTypes.LOGIN_USER,
        userId: userId,
    }
}