import React from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Redirect } from "react-router-dom";
import * as actions from '../common/actions'
import * as sharedActions from '@/views/shared/common/actions'
import style from '../styles/index.module.scss';
import { postData } from '@/common/fetchHelper';
import md5 from 'js-md5';
import LayoutLogin from '../../shared/components/layoutLogin';

class LoginIndex extends React.Component {
    // 构造函数
    constructor(props) {
        super(props);
        document.title = 'TFW - 登录';
        this.state = {
            loginName: '',
            password: '',
            isLoading: false,
            message: '',
        }
        this.login = this.login.bind(this);
    }
    // 组件加载完成
    componentDidMount() {

    }

    login() {
        if (this.state.isLoading)
            return;
        this.setState({ isLoading: true });
        var tmpPassword = md5(this.state.password);
        postData('/api/User/login', { loginName: this.state.loginName, password: tmpPassword })
            .then(response => {
                console.log(response);
                if (response.code === 0) {
                    sessionStorage.setItem('access_token', response.data.id);
                    this.setState({ isLoading: false, userId: response.data.id, message: '' });
                    this.props.actions.setValue({ userId: response.data.id });
                } else
                    this.setState({ isLoading: false, message: '账号或密码有误！' });
            })
            .catch(error => {
                console.log(error);
                this.props.actions.setValue({ isLoading: false });
            });
    }

    // 渲染
    render() {
        if (this.props.userId === null) {
            return (
                <LayoutLogin>
                    <div className={style.item}>
                        <div className={style.line}>账号</div>
                        <div className={style.line}><input className={style.text} spellCheck="false" type="text" value={this.state.loginName} onChange={(e) => { this.setState({ loginName: e.target.value }) }} /></div>
                        <div className={style.line}>密码</div>
                        <div className={style.line}><input className={style.text} spellCheck="false" type="password" value={this.state.password} onChange={(e) => { this.setState({ password: e.target.value }) }} /></div>
                        <div className={style.line}><button className={style.button} onClick={this.login}>登录</button></div>
                        {this.state.message !== '' && (<div className={style.line}><span className={style.message}>{this.state.message}</span></div>)}
                    </div>
                </LayoutLogin>
            );
        } else {
            return (<Redirect to='/manager' />);
        }
    }
}
LoginIndex.propTypes = {
    userId: PropTypes.string,
};

/* istanbul ignore next */
function mapStateToProps(state) {
    return {
        userId: state.shared.userId,
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
)(LoginIndex);