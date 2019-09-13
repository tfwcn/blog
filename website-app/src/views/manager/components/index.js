import React from 'react';
import { Link, Route } from "react-router-dom";
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import * as actions from '../common/actions'
import * as sharedActions from '@/views/shared/common/actions'
import style from '../styles/index.module.scss';
import LayoutMain from '../../shared/components/layoutMain';
import ManagerWebLoader from './webLoader';
import { postData } from '@/common/fetchHelper';

class ManagerIndex extends React.Component {
    // 构造函数
    constructor(props) {
        super(props);
        console.log(this);
        document.title = 'TFW - 后台管理';
    }
    // 组件加载完成
    componentDidMount() {
        postData('/api/User/check', { token: this.props.userId })
            .catch(error => {
                this.props.actions.setValue({ userId: null });
                this.props.history.push('/login');
            });
    }

    // 渲染
    render() {
        return (
            <LayoutMain>
                <div className={style.main}>
                    <div className={style.left}>
                        <div className={style.title}>菜单</div>
                        <ul>
                            <li>
                                <span className={style.dot}></span>
                                <Link to={`${this.props.match.url}/webLoader`}>爬虫配置</Link>
                            </li>
                        </ul>
                    </div>
                    <div className={style.right}>
                        <Route path={`${this.props.match.url}/webLoader`} component={ManagerWebLoader} />
                    </div>
                </div>
            </LayoutMain>
        );
    }
}
ManagerIndex.propTypes = {
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
        actions: bindActionCreators({ ...actions,...sharedActions }, dispatch),
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(ManagerIndex);