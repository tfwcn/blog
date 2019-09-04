// 初始化state
const initialState = {
    news: {
        // 数据
        list: null,
        // 总记录数
        count: 0,
        status: 'loading',
        errorMsg: null,
        // 当前页
        page: 1,
        // 总记录数
        rows : 10,
        // 总页数
        pageCount: 1,
    },
};
export default initialState;