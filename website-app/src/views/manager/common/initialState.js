// 初始化state
const initialState = {
    webLoader: {
        // 数据
        list: [],
        // 总记录数
        count: 0,
        status: 'loading',
        errorMsg: null,
        // 当前页
        page: 1,
        // 总记录数
        rows: 10,
        // 总页数
        pageCount: 1,
        // 临时新ID，用于删除
        newId: 0,
    },
};
export default initialState;