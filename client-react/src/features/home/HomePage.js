import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { Icon } from 'antd';
import * as actions from './redux/actions';
import styles from './HomePage.module.scss';

export class HomePage extends Component {
  static get propTypes() {
    return {
      typeList: PropTypes.array.isRequired,
      breadcrumbList: PropTypes.array.isRequired,
      nowTypeKey: PropTypes.string.isRequired,
      actions: PropTypes.object.isRequired,
    };
  }
  constructor(props) {
    super(props);
    this.props.history.listen(location => {
      let nowKey = location.pathname.substring(0, location.pathname.length - 1);
      nowKey = nowKey.substring(nowKey.lastIndexOf('/') + 1);
      this.props.actions.homeTypeListInit(nowKey);
    });
    let nowKey = this.props.location.pathname.substring(
      0,
      this.props.location.pathname.length - 1
    );
    nowKey = nowKey.substring(nowKey.lastIndexOf('/') + 1);
    this.props.actions.homeTypeListInit(nowKey);
  }

  render() {
    let nowTypeKey = this.props.nowTypeKey;
    let typeList = this.props.typeList;
    let breadcrumbList = this.props.breadcrumbList;
    return (
      <div className={styles.homePage}>
        <div className={styles.breadcrumb}>
          当前位置：首页
          {breadcrumbList.map(item => {
            return (
              <span key={item.key}>
                {' '}
                > <Link to={item.key + '/'}>{item.value}</Link>
              </span>
            );
          })}
        </div>
        <div className={styles.typeList}>
          <div className={styles.typeListTitle}>
            <Icon type="bars" className={styles.icon} />
            分类
          </div>
          <div className={styles.typeListContent}>
            {typeList.map(item => {
              if (item.pkey != nowTypeKey) return null;
              return (
                <div className={styles.item} key={item.key}>
                  <div className={styles.triangleLeft} />
                  <Link to={this.props.location.pathname + item.key + '/'}>
                    {item.value}
                  </Link>
                  <div className={styles.triangleRight} />
                </div>
              );
            })}
            {/* <div className={styles.item}>
              <div className={styles.triangleLeft} />
              人工智能
              <div className={styles.triangleRight} />
            </div>
            <div className={styles.item}>
              <div className={styles.triangleLeft} />
              WPF
              <div className={styles.triangleRight} />
            </div>
            <div className={styles.item}>
              <div className={styles.triangleLeft} />
              WinForm
              <div className={styles.triangleRight} />
            </div> */}
          </div>
        </div>
        <div className={styles.blogList}>
          <div className={styles.blogListTitle}>
            {/* <span className={styles.icon} /> */}
            <Icon type="book" className={styles.icon} />
            博客文章
          </div>
          <div className={styles.blogListContent}>文章1</div>
        </div>
      </div>
    );
  }
}

/* istanbul ignore next */
function mapStateToProps(state) {
  return {
    typeList: state.home.typeList,
    breadcrumbList: state.home.breadcrumbList,
    nowTypeKey: state.home.nowTypeKey,
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
)(HomePage);
