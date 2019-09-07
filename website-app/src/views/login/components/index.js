import React from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from "react-router-dom";
import * as actions from '../common/actions'
import style from '../styles/index.module.scss';
import { postData } from '@/common/fetchHelper';
import md5 from 'js-md5';

class LoginIndex extends React.Component {
    // 构造函数
    constructor(props) {
        super(props);
        document.title = 'TFW - 登录';
        this.login = this.login.bind(this);
    }
    // 组件加载完成
    componentDidMount() {

    }

    login() {
        if (this.props.isLoading)
            return;
        this.props.actions.setValue({ isLoading: true });
        var tmpPassword = md5(this.props.password);
        postData('/api/User/login', { loginName: this.props.loginName, password: tmpPassword })
            .then(response => {
                console.log(response);
                this.props.actions.setValue({ isLoading: false });
            })
            .catch(error => {
                console.log(error);
                this.props.actions.setValue({ isLoading: false });
            });
    }

    // 渲染
    render() {
        // 分页
        return (
            <div className={style.body}>
                <div className={style.header}>
                    <Link className={style.title} to='/'>TFW</Link>
                </div>
                <div className={style.item}>
                    <div className={style.line}>账号</div>
                    <div className={style.line}><input className={style.text} type="text" value={this.props.loginName} onChange={(e) => { this.props.actions.setValue({ loginName: e.target.value }) }} /></div>
                    <div className={style.line}>密码</div>
                    <div className={style.line}><input className={style.text} type="password" value={this.props.password} onChange={(e) => { this.props.actions.setValue({ password: e.target.value }) }} /></div>
                    <div className={style.line}><button className={style.button} onClick={this.login}>登录</button></div>
                </div>
            </div>
        );
    }
}
LoginIndex.propTypes = {
    userId: PropTypes.string,
    loginName: PropTypes.string,
    password: PropTypes.string,
    isLoading: PropTypes.bool,
};

/* istanbul ignore next */
function mapStateToProps(state) {
    return {
        userId: state.login.userId,
        loginName: state.login.loginName,
        password: state.login.password,
        isLoading: state.login.isLoading,
    };
}

/* istanbul ignore next */
function mapDispatchToProps(dispatch) {
    return {
        actions: bindActionCreators({ ...actions }, dispatch),
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(LoginIndex);