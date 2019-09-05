import React from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from "react-router-dom";
import * as actions from '../common/actions'
import style from '../styles/index.module.scss';

class LoginIndex extends React.Component {
    // 构造函数
    constructor(props) {
        super(props);
        document.title = 'TFW - 登录';
    }
    // 组件加载完成
    componentDidMount() {
        
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
                </div>
            </div>
        );
    }
}
LoginIndex.propTypes = {
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
)(LoginIndex);