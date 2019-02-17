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
      actions: PropTypes.object.isRequired,
    };
  }
  constructor(props) {
    super(props);
  }
  //查询博客列表
  selectBlogs() {
    this.props.actions.homeBlogListLoading();
    fetch('/api/note/list', {
      method: 'POST',
      mode: 'cors',
      credentials: 'include',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ page: 1, rows: 10 }),
    })
      .then(res => res.json())
      .catch(error => {
        this.props.actions.homeBlogListError(error);
      })
      .then(response => {
        console.log(response);
        this.props.actions.homeBlogListSuccess(response);
      });
  }

  componentDidMount() {
    // console.log('componentDidMount');
    let nowKey = this.props.location.pathname.substring(
      0,
      this.props.location.pathname.length - 1
    );
    nowKey = nowKey.substring(nowKey.lastIndexOf('/') + 1);
    this.nowTypeKey = nowKey;
    this.props.actions.homeTypeListInit(nowKey);
    this.selectBlogs();
  }

  componentWillUnmount() {
    // console.log('componentWillUnmount');
  }

  render() {
    let nowTypeKey = this.nowTypeKey;
    let typeList = this.props.typeList;
    let breadcrumbList = this.props.breadcrumbList;
    let hasTypeItem = typeList.find(i => i.pkey == nowTypeKey);
    return (
      <div className={styles.homePage}>
        <div className={styles.breadcrumb}>
          当前位置：<Link to={'/'}>首页</Link>
          {breadcrumbList.map(item => {
            let nowPath = this.props.location.pathname;
            nowPath = nowPath.substring(0, nowPath.indexOf(item.key));
            nowPath = nowPath + item.key + '/';
            if (this.props.location.pathname == nowPath) {
              return (
                <span key={item.key}>
                  {' '}
                  > <span className={styles.nowItem}>{item.value}</span>
                </span>
              );
            } else {
              return (
                <span key={item.key}>
                  {' '}
                  > <Link to={nowPath}>{item.value}</Link>
                </span>
              );
            }
          })}
        </div>
        {hasTypeItem ? (
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
            </div>
          </div>
        ) : null}
        <div className={styles.blogList}>
          <div className={styles.blogListTitle}>
            {/* <span className={styles.icon} /> */}
            <Icon type="book" className={styles.icon} />
            博客文章
          </div>
          <div className={styles.blogListContent}>
            <div className={styles.blogListItem}>
              <div className={styles.title}>
                <Link to={''}>
                  Mask RCNN训练自己的数据集(修正及windows下使用labelme)
                </Link>
              </div>
              <div className={styles.content}>
                原文参考：https：//blog.csdn.net/l297969586/article/details/79140840/
                labelme编译并使用： 源码：https：//github.com/tfwcn/labelme
                源码已改成批量转Json的文件 Logs 2018/11/8...
              </div>
              <div className={styles.info}>
                <span className={styles.time}>2018-10-29 09:22:21</span> 阅读数
                22 评论数 0
              </div>
            </div>
            <div className={styles.blogListItem}>
              <div className={styles.title}>
                <Link to={''}>
                  Mask RCNN训练自己的数据集(修正及windows下使用labelme)
                </Link>
              </div>
              <div className={styles.content}>
                原文参考：https：//blog.csdn.net/l297969586/article/details/79140840/
                labelme编译并使用： 源码：https：//github.com/tfwcn/labelme
                源码已改成批量转Json的文件 Logs 2018/11/8...
              </div>
              <div className={styles.info}>
                <span className={styles.time}>2018-10-29 09:22:21</span> 阅读数
                22 评论数 0
              </div>
            </div>
            <div className={styles.blogListItem}>
              <div className={styles.title}>
                <Link to={''}>
                  Mask RCNN训练自己的数据集(修正及windows下使用labelme)
                </Link>
              </div>
              <div className={styles.content}>
                原文参考：https：//blog.csdn.net/l297969586/article/details/79140840/
                labelme编译并使用： 源码：https：//github.com/tfwcn/labelme
                源码已改成批量转Json的文件 Logs 2018/11/8...
              </div>
              <div className={styles.info}>
                <span className={styles.time}>2018-10-29 09:22:21</span> 阅读数
                22 评论数 0
              </div>
            </div>
            <div className={styles.blogListItem}>
              <div className={styles.title}>
                <Link to={''}>
                  Mask RCNN训练自己的数据集(修正及windows下使用labelme)
                </Link>
              </div>
              <div className={styles.content}>
                原文参考：https：//blog.csdn.net/l297969586/article/details/79140840/
                labelme编译并使用： 源码：https：//github.com/tfwcn/labelme
                源码已改成批量转Json的文件 Logs 2018/11/8...
              </div>
              <div className={styles.info}>
                <span className={styles.time}>2018-10-29 09:22:21</span> 阅读数
                22 评论数 0
              </div>
            </div>
          </div>
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
