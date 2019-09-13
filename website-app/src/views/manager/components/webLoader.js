import React from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import * as actions from '../common/actions'
import * as sharedActions from '@/views/shared/common/actions'
import style from '../styles/webLoader.module.scss';
import { postData } from '@/common/fetchHelper';
import ManagerWebLoaderItem from "./webLoaderItem";

class ManagerWebLoader extends React.Component {
    // 构造函数
    constructor(props) {
        super(props);
        document.title = 'TFW - 爬虫配置';
        this.add = this.add.bind(this);
        this.showList(1, 10);
    }
    // 组件加载完成
    componentDidMount() {

    }

    // 加载列表
    showList(page, rows) {
        let tmpWebLoader = {
            ...this.props.webLoader,
            list: [],
            count: 0,
            status: 'loading',
            errorMsg: null,
            page: page,
            rows: rows,
            pageCount: 0,
        };
        this.props.actions.setValue({ webLoader: tmpWebLoader });
        postData('/api/WebLoader/list', { page: this.props.webLoader.page, rows: this.props.webLoader.rows })
            .then(response => {
                console.log(response);
                if (response.code === 0) {
                    let count = response.data.count;
                    let tmpWebLoader = {
                        ...this.props.webLoader,
                        list: response.data.dataList,
                        count: count,
                        status: 'success',
                        errorMsg: null,
                        pageCount: Math.ceil(count / this.props.webLoader.rows),
                    };
                    this.props.actions.setValue({ webLoader: tmpWebLoader });
                } else {
                    let tmpWebLoader = {
                        ...this.props.webLoader,
                        list: [],
                        count: 0,
                        status: 'error',
                        errorMsg: response.errorMsg,
                        pageCount: 0,
                    };
                    this.props.actions.setValue({ webLoader: tmpWebLoader });
                }
            })
            .catch(error => {
                console.error(error);
                let tmpWebLoader = {
                    ...this.props.webLoader,
                    list: [],
                    count: 0,
                    status: 'error',
                    errorMsg: error.message,
                    pageCount: 0,
                };
                this.props.actions.setValue({ webLoader: tmpWebLoader });
            });
    }

    // 新增记录
    add() {
        let tmpWebLoader = { ...this.props.webLoader };
        console.log(tmpWebLoader);
        tmpWebLoader.list.splice(0, 0, { id: tmpWebLoader.newId++, remark: '', javascript: '', url: '' });
        this.props.actions.setValue({ webLoader: tmpWebLoader });
    }

    // 渲染
    render() {
        return (
            <div className={style.webLoader}>
                <div className={style.title}>爬虫配置</div>
                <ul className={style.list}>
                    <li className={style.add} onClick={this.add}>
                        <svg className={style.icon} t="1567915192785" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="3713"><path d="M448 448H192v128h256v256h128V576h256V448H576V192H448v256z m64 576A512 512 0 1 1 512 0a512 512 0 0 1 0 1024z" fill="#ffffff" p-id="3714"></path></svg>
                    </li>
                    {this.props.webLoader.list.map(m => {
                        if (m.id.length !== 36) {
                            return (
                                <ManagerWebLoaderItem key={m.id} item={{ ...m }} showType="add" />
                            );
                        } else {
                            return (
                                <ManagerWebLoaderItem key={m.id} item={{ ...m }} showType="show" />
                            );
                        }
                    })}
                </ul>
            </div>
        );
    }
}
ManagerWebLoader.propTypes = {
    webLoader: PropTypes.object,
};

/* istanbul ignore next */
function mapStateToProps(state) {
    return {
        webLoader: state.manager.webLoader,
    };
}

/* istanbul ignore next */
function mapDispatchToProps(dispatch) {
    return {
        actions: bindActionCreators({ ...actions, ...sharedActions }, dispatch),
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(ManagerWebLoader);