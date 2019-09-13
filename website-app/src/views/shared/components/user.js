import React from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from "react-router-dom";
import * as actions from '../common/actions'
import style from '../styles/user.module.scss'

class User extends React.Component {
    // 组件加载完成
    componentDidMount() {
        this.props.actions.setValue({ userId: sessionStorage.getItem('access_token') })
        console.log(this);
    }
    // 渲染
    render() {
        if (this.props.userId === null) {
            return (
                <Link className={style.user} to='/login'>登录</Link>
            );
        } else {
            return (
                <Link className={style.user} to='/manager'>欢迎</Link>
            );
        }
    }
}
User.propTypes = {
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
        actions: bindActionCreators({ ...actions }, dispatch),
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(User);