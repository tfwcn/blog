import React from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import * as actions from '../common/actions'
import * as sharedActions from '@/views/shared/common/actions'
import style from '../styles/webLoaderItem.module.scss';
import { postData } from '@/common/fetchHelper';

// 单个爬虫配置，无需使用redux
class ManagerWebLoaderItem extends React.Component {
    // 构造函数
    constructor(props) {
        super(props);
        console.log(this);
        // 局部state
        this.state = {
            showType: this.props.showType,
            id: this.props.item.id,
            remark: this.props.item.remark,
            javascript: this.props.item.javascript,
            url: this.props.item.url,
            status: 'success',
            errorMsg: '',
        };
        this.addCancel = this.addCancel.bind(this);
        this.editCancel = this.editCancel.bind(this);
        this.edit = this.edit.bind(this);
        this.addSave = this.addSave.bind(this);
        this.editSave = this.editSave.bind(this);
        this.delete = this.delete.bind(this);
    }
    // 组件加载完成
    componentDidMount() {

    }
    //新增取消
    addCancel() {
        let tmpWebLoader = { ...this.props.webLoader };
        console.log(tmpWebLoader);
        tmpWebLoader.list.map((m, i) => {
            if (m.id === this.props.item.id)
                tmpWebLoader.list.splice(i, 1);
            return m;
        });
        // tmpWebLoader.list.remove(this.props.item);
        this.props.actions.setValue({ webLoader: tmpWebLoader });
    }
    //编辑取消
    editCancel() {
        this.setState({
            id: this.props.item.id,
            remark: this.props.item.remark,
            javascript: this.props.item.javascript,
            url: this.props.item.url,
            showType: 'show',
        });
    }
    //编辑
    edit() {
        this.setState({
            showType: 'edit',
        });
    }

    // 新增保存
    addSave() {
        this.setState({ status: 'loading' });
        let tmpData = {
            id: this.state.id,
            remark: this.state.remark,
            javascript: this.state.javascript,
            url: this.state.url,
        }
        postData('/api/WebLoader/add', tmpData)
            .then(response => {
                console.log(response);
                if (response.code === 0) {
                    //更新旧值
                    // this.props.item.id = response.data.id;
                    // this.props.item.remark = this.state.remark;
                    // this.props.item.javascript = this.state.javascript;
                    // this.props.item.url = this.state.url;
                    // // 更新状态
                    // this.setState({ id: response.data.id, status: 'success', showType: 'show' });
                    let tmpWebLoader = { ...this.props.webLoader };
                    console.log(tmpWebLoader);
                    tmpWebLoader.list.map((m) => {
                        if (m.id === this.state.id) {

                            m.id = response.data.id;
                            m.remark = this.state.remark;
                            m.javascript = this.state.javascript;
                            m.url = this.state.url;
                        }
                        return m;
                    });
                    this.props.actions.setValue({ webLoader: tmpWebLoader });
                } else {
                    // 失败
                    this.setState({ status: 'error', errorMsg: response.errorMsg });
                }
            })
            .catch(error => {
                console.error(error);
                // 失败
                this.setState({ status: 'error', errorMsg: error.message });
            });
    }

    // 编辑保存
    editSave() {
        this.setState({ status: 'loading' });
        let tmpData = {
            id: this.state.id,
            remark: this.state.remark,
            javascript: this.state.javascript,
            url: this.state.url,
        }
        postData('/api/WebLoader/update', tmpData)
            .then(response => {
                console.log(response);
                if (response.code === 0) {
                    // 更新状态
                    this.setState({ id: response.data.id, status: 'success', showType: 'show' });
                } else {
                    // 失败
                    this.setState({ status: 'error', errorMsg: response.errorMsg });
                }
            })
            .catch(error => {
                console.error(error);
                // 失败
                this.setState({ status: 'error', errorMsg: error.message });
            });
    }

    // 删除
    delete() {
        this.setState({ status: 'loading' });
        let tmpData = {
            id: this.state.id,
        }
        postData('/api/WebLoader/delete', tmpData)
            .then(response => {
                console.log(response);
                if (response.code === 0) {
                    let tmpWebLoader = { ...this.props.webLoader };
                    console.log(tmpWebLoader);
                    tmpWebLoader.list.map((m, i) => {
                        if (m.id === this.state.id)
                            tmpWebLoader.list.splice(i, 1);
                        return m;
                    });
                    this.props.actions.setValue({ webLoader: tmpWebLoader });
                } else {
                    // 失败
                    this.setState({ status: 'error', errorMsg: response.errorMsg });
                }
            })
            .catch(error => {
                console.error(error);
                // 失败
                this.setState({ status: 'error', errorMsg: error.message });
            });
    }

    // 渲染
    render() {
        let btnList = '';
        if (this.state.showType === 'add' || this.state.showType === 'edit') {
            btnList = (
                <div className={style.btnList}>
                    <span className={style.message}>{this.state.errorMsg}</span>
                    {/* 保存 */}
                    <span title="保存">
                        <svg onClick={this.state.showType === 'add' ? this.addSave : this.editSave} className={style.icon} t="1567927843131" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="4171" width="200" height="200"><path d="M512 1024A512 512 0 1 1 512 0a512 512 0 0 1 0 1024z m3.008-92.992a416 416 0 1 0 0-832 416 416 0 0 0 0 832zM448 605.12L746.688 320 832 401.472 448 768 192 523.648l85.312-81.472L448 605.12z" fill="#ffffff" p-id="4172"></path></svg>
                    </span>
                    {/* 取消 */}
                    <span title="取消">
                        <svg onClick={this.state.showType === 'add' ? this.addCancel : this.editCancel} className={style.icon} t="1567927816186" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="4018" width="200" height="200"><path d="M595.392 504.96l158.4 158.336-90.496 90.496-158.4-158.4-158.4 158.4L256 663.296l158.4-158.4L256 346.496 346.496 256 504.96 414.4 663.296 256l90.496 90.496L595.392 504.96zM512 1024A512 512 0 1 1 512 0a512 512 0 0 1 0 1024z m3.008-92.992a416 416 0 1 0 0-832 416 416 0 0 0 0 832z" fill="#ffffff" p-id="4019"></path></svg>
                    </span>
                </div>
            );
            return (
                <li className={style.item + ' ' + (this.state.showType === 'add' ? style.add : style.edit)}>
                    {btnList}
                    <div className={style.line}>链接</div>
                    <div className={style.line}><input className={style.text} spellCheck="false" type="text" value={this.state.url} onChange={(e) => { this.setState({ url: e.target.value }); }} /></div>
                    <div className={style.line}>描述</div>
                    <div className={style.line}><input className={style.text} spellCheck="false" type="text" value={this.state.remark} onChange={(e) => { this.setState({ remark: e.target.value }); }} /></div>
                    <div className={style.line}>JS脚本</div>
                    <div className={style.line}><textarea className={style.textarea} spellCheck="false" type="text" value={this.state.javascript} onChange={(e) => { this.setState({ javascript: e.target.value }); }} /></div>
                </li>
            );
        } else if (this.state.showType === 'show') {
            btnList = (
                <div className={style.btnList}>
                    {/* 修改 */}
                    <span title="修改">
                        <svg onClick={this.edit} className={style.icon} t="1567927969674" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="4324" width="200" height="200"><path d="M512 1024A512 512 0 1 1 512 0a512 512 0 0 1 0 1024z m0-85.312A426.688 426.688 0 1 0 512 85.312a426.688 426.688 0 0 0 0 853.376zM320 576a64 64 0 1 1 0-128 64 64 0 0 1 0 128z m192 0a64 64 0 1 1 0-128 64 64 0 0 1 0 128z m192 0a64 64 0 1 1 0-128 64 64 0 0 1 0 128z" fill="#ffffff" p-id="4325"></path></svg>
                    </span>
                    {/* 删除 */}
                    <span title="删除">
                        <svg onClick={this.delete} className={style.icon} t="1567928072988" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="4477" width="200" height="200"><path d="M199.04 289.472a384 384 0 0 0 535.488 535.488L198.976 289.536zM289.408 199.04l535.552 535.552A384 384 0 0 0 289.536 199.04zM512 1024A512 512 0 1 1 512 0a512 512 0 0 1 0 1024z" fill="#ffffff" p-id="4478"></path></svg>
                    </span>
                </div>
            );

            return (
                <li className={style.item}>
                    {btnList}
                    <div className={style.line}>链接</div>
                    <div className={style.line}><div className={style.text}>{this.state.url}</div></div>
                    <div className={style.line}>描述</div>
                    <div className={style.line}><div className={style.text}>{this.state.remark}</div></div>
                    <div className={style.line}>JS脚本</div>
                    <div className={style.line}><div className={style.textarea}>{this.state.javascript}</div></div>
                    {this.state.status === 'loading' && (
                        <div className={style.loading}>
                            <svg className={style.icon} t="1567951326745" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="4630" width="200" height="200"><path d="M876.864 782.592c3.264 0 6.272-3.2 6.272-6.656 0-3.456-3.008-6.592-6.272-6.592-3.264 0-6.272 3.2-6.272 6.592 0 3.456 3.008 6.656 6.272 6.656z m-140.544 153.344c2.304 2.432 5.568 3.84 8.768 3.84a12.16 12.16 0 0 0 8.832-3.84 13.76 13.76 0 0 0 0-18.56 12.224 12.224 0 0 0-8.832-3.84 12.16 12.16 0 0 0-8.768 3.84 13.696 13.696 0 0 0 0 18.56zM552.32 1018.24c3.456 3.648 8.32 5.76 13.184 5.76a18.368 18.368 0 0 0 13.184-5.76 20.608 20.608 0 0 0 0-27.968 18.368 18.368 0 0 0-13.184-5.824 18.368 18.368 0 0 0-13.184 5.76 20.608 20.608 0 0 0 0 28.032z m-198.336-5.76c4.608 4.8 11.072 7.68 17.6 7.68a24.448 24.448 0 0 0 17.536-7.68 27.456 27.456 0 0 0 0-37.248 24.448 24.448 0 0 0-17.536-7.68 24.448 24.448 0 0 0-17.6 7.68 27.52 27.52 0 0 0 0 37.184z m-175.68-91.84c5.76 6.08 13.824 9.6 21.952 9.6a30.592 30.592 0 0 0 22.016-9.6 34.368 34.368 0 0 0 0-46.592 30.592 30.592 0 0 0-22.016-9.6 30.592 30.592 0 0 0-21.952 9.6 34.368 34.368 0 0 0 0 46.592z m-121.152-159.36c6.912 7.36 16.64 11.648 26.368 11.648a36.736 36.736 0 0 0 26.432-11.584 41.28 41.28 0 0 0 0-55.936 36.736 36.736 0 0 0-26.432-11.584 36.8 36.8 0 0 0-26.368 11.52 41.28 41.28 0 0 0 0 56zM12.736 564.672a42.88 42.88 0 0 0 30.784 13.44 42.88 42.88 0 0 0 30.784-13.44 48.128 48.128 0 0 0 0-65.216 42.88 42.88 0 0 0-30.72-13.44 42.88 42.88 0 0 0-30.848 13.44 48.128 48.128 0 0 0 0 65.216z m39.808-195.392a48.96 48.96 0 0 0 35.2 15.36 48.96 48.96 0 0 0 35.2-15.36 54.976 54.976 0 0 0 0-74.56 48.96 48.96 0 0 0-35.2-15.424 48.96 48.96 0 0 0-35.2 15.424 54.976 54.976 0 0 0 0 74.56zM168.32 212.48c10.368 11.008 24.96 17.408 39.68 17.408 14.592 0 29.184-6.4 39.552-17.408a61.888 61.888 0 0 0 0-83.84 55.104 55.104 0 0 0-39.616-17.408c-14.656 0-29.248 6.4-39.616 17.408a61.888 61.888 0 0 0 0 83.84zM337.344 124.8c11.52 12.16 27.712 19.264 43.968 19.264 16.256 0 32.448-7.04 43.968-19.264a68.672 68.672 0 0 0 0-93.184 61.248 61.248 0 0 0-43.968-19.264 61.248 61.248 0 0 0-43.968 19.264 68.736 68.736 0 0 0 0 93.184z m189.632-1.088c12.672 13.44 30.528 21.248 48.448 21.248s35.712-7.808 48.384-21.248a75.584 75.584 0 0 0 0-102.464A67.392 67.392 0 0 0 575.36 0c-17.92 0-35.776 7.808-48.448 21.248a75.584 75.584 0 0 0 0 102.464z m173.824 86.592c13.824 14.592 33.28 23.104 52.736 23.104 19.584 0 39.04-8.512 52.8-23.104a82.432 82.432 0 0 0 0-111.744 73.472 73.472 0 0 0-52.8-23.168c-19.52 0-38.912 8.512-52.736 23.168a82.432 82.432 0 0 0 0 111.744z m124.032 158.528c14.976 15.872 36.032 25.088 57.216 25.088 21.12 0 42.24-9.216 57.152-25.088a89.344 89.344 0 0 0 0-121.088 79.616 79.616 0 0 0-57.152-25.088c-21.184 0-42.24 9.216-57.216 25.088a89.344 89.344 0 0 0 0 121.088z m50.432 204.032c16.128 17.088 38.784 27.008 61.632 27.008 22.784 0 45.44-9.92 61.568-27.008a96.256 96.256 0 0 0 0-130.432 85.76 85.76 0 0 0-61.568-27.072c-22.848 0-45.44 9.984-61.632 27.072a96.192 96.192 0 0 0 0 130.432z" fill="#ffffff" p-id="4631"></path></svg>
                        </div>
                    )}
                </li>
            );
        } else {
            return null;
        }
    }
}
ManagerWebLoaderItem.propTypes = {
    showType: PropTypes.oneOf(['add', 'edit', 'show']),
    item: PropTypes.object,
    webLoader: PropTypes.object,
    webLoaderItem: PropTypes.object,
};

/* istanbul ignore next */
function mapStateToProps(state) {
    return {
        webLoader: state.manager.webLoader,
        webLoaderItem: state.manager.webLoaderItem,
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
)(ManagerWebLoaderItem);