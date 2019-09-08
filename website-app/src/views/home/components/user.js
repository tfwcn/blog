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
    }
    // 渲染
    render() {
        if (this.props.userId === null) {
            return (
                <Link className={style.user} to='/login'>登录</Link>
            );
        } else {
            return (<div className={style.user}>欢迎</div>);
        }
    }
}
User.propTypes = {
    userId: PropTypes.string,
};

/* istanbul ignore next */
function mapStateToProps(state) {
    return {
        userId: state.login.userId,
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