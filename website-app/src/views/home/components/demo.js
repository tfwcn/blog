import React from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import * as actions from '../common/actions'

class Demo extends React.Component {
    // 构造函数
    constructor(props) {
        super(props);
        this.getMsg = this.getMsg.bind(this);
        this.tick = this.tick.bind(this);
    }
    // 组件加载完成
    componentDidMount() {
        this.timerID = setInterval(
            () => this.tick(),
            1000
        );
    }
    // 组件卸载
    componentWillUnmount() {
        clearInterval(this.timerID);
    }

    tick() {
        this.props.actions.getTime();
    }

    getMsg() {
        this.props.actions.getMenuList('test');
    }

    render() {
        return (
            <div onClick={this.getMsg}>{this.props.title}_{this.props.msg}：{this.props.time.toLocaleTimeString()}</div>
        );
    }
}
// 指定类型，isRequired允许为空
Demo.propTypes = {
    msg: PropTypes.string.isRequired,
    time: PropTypes.object.isRequired,
};

/* istanbul ignore next */
function mapStateToProps(state) {
    return {
        msg: state.home.msg,
        time: state.home.time,
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
)(Demo);