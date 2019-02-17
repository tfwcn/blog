import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Icon } from 'antd';
import { Route, Switch } from 'react-router';
import { Link } from 'react-router-dom';
import * as actions from './redux/actions';
// import TopMenu from './TopMenu';
import HomePage from './HomePage';
import BlogAddPage from './BlogAddPage';
import BackgroundImage from './BackgroundImage';
import ForegroundImage from './ForegroundImage';
import styles from './IndexPage.module.scss';

export class IndexPage extends Component {
  static get propTypes() {
    return {
      home: PropTypes.object.isRequired,
      actions: PropTypes.object.isRequired,
    };
  }
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <div className={styles.indexPage}>
        <BackgroundImage />
        <ForegroundImage />
        <div className={styles.header}>
          <div className={styles.empty} />
          <div className={styles.content}>
            <span className={styles.logo}>PPHT个人博客</span>
            {/* 菜单 */}
            {/* <TopMenu location={this.props.location} /> */}
            {/* 右边按钮 */}
            <span className={styles.right}>
              <span className={styles.triangle} />
              <Link to={'/blog/add'}>
                <Icon className={styles.icon} type="form" />
                写博客
              </Link>
            </span>
          </div>
          <div className={styles.emptyRight} />
        </div>
        <div className={styles.main}>
          <div className={styles.empty} />
          <div className={styles.content}>
            {/* <div className={styles.left}>
              <ul>
                <li className={styles.select}>.Net</li>
                <li>Java</li>
                <li>Python</li>
              </ul>
            </div> */}
            <Switch>
              <Route path={'/blog/add'} component={BlogAddPage} />
              <Route
                path={'/'}
                component={HomePage}
                key={this.props.location.pathname}
              />
            </Switch>
          </div>
          <div className={styles.empty} />
        </div>
        <div className={styles.footer}>&copy;2011-2019 PPHT个人版权所有</div>
      </div>
    );
  }
}

/* istanbul ignore next */
function mapStateToProps(state) {
  return {
    home: state.home,
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
)(IndexPage);
