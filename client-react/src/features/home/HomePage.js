import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Icon } from 'antd';
import { Route } from 'react-router';
import { Link } from 'react-router-dom';
import * as actions from './redux/actions';
import styles from './HomePage.module.scss';

export class HomePage extends Component {
  static get propTypes() {
    return {
      home: PropTypes.object.isRequired,
      actions: PropTypes.object.isRequired,
    };
  }
  constructor({ match }) {
    super();
    this.match = match;
  }

  componentWillMount() {
    this.props.actions.homeTopMenuInit();
  }

  render() {
    return (
      <div className={styles.homePage} onClick={this.test}>
        <div className={styles.header}>
          <div className={styles.empty} />
          <div className={styles.content}>
            <span className={styles.logo} />
            {/* 菜单 */}
            <span className={styles.menu}>
              <ul>
                {this.props.home.topMenuList.map((item, index) =>
                  index == 0 ? (
                    <li key={item} className={styles.select}>
                      <Link to={'/' + item}>{item}</Link>
                    </li>
                  ) : (
                    <li key={item}>
                      <Link to={'/' + item}>{item}</Link>
                    </li>
                  ),
                )}
                {/* <li className={styles.select}>.Net</li>
                <li>Java</li>
                <li>Python</li> */}
              </ul>
            </span>
            {/* 右边按钮 */}
            <span className={styles.right}>
              <span className={styles.triangle} />
              <Icon className={styles.icon} type="form" />
              写博客
            </span>
          </div>
          <div className={styles.emptyRight} />
        </div>
        <div className={styles.main}>
          <div className={styles.empty} />
          <div className={styles.content}>
            <div className={styles.left}>
              <ul>
                <li className={styles.select}>.Net</li>
                <li>Java</li>
                <li>Python</li>
              </ul>
            </div>
            <div className={styles.right}>
              <Route
                path="/:item"
                component={({ match }) => (
                  <div>
                    <h2>{match.params.item}</h2>
                  </div>
                )}
              />
            </div>
          </div>
          <div className={styles.empty} />
        </div>
        <div className={styles.footer}>footer</div>
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
  mapDispatchToProps,
)(HomePage);
