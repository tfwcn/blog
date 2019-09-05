import React from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import * as actions from '../common/actions'
import style from '../styles/news.module.scss'
import { postData } from '@/common/fetchHelper'

class News extends React.Component {
    // 组件加载完成
    componentDidMount() {
        this.upPage = this.upPage.bind(this);
        this.downPage = this.downPage.bind(this);
        this.showNewsList(1, 10);
    }
    // 加载新闻列表
    showNewsList(page, rows) {
        this.props.actions.showNewsList(null, 0, 'loading', null, page, rows);
        postData('/api/News/list', { page: this.props.news.page, rows: this.props.news.rows })
            .then(response => {
                console.log(response);
                if (response.code === 0) {
                    let count = response.data.count > 100 ? 100 : response.data.count;
                    this.props.actions.showNewsList(response.data.dataList, count, 'success', null, this.props.news.page, this.props.news.rows);
                } else {
                    this.props.actions.showNewsList(null, 0, 'error', response.errorMsg, this.props.news.page, this.props.news.rows);
                }
            })
            .catch(error => {
                console.log(error);
                this.props.actions.showNewsList(null, 0, 'error', error, this.props.news.page, this.props.news.rows);
            });
    }
    // 上一页
    upPage() {
        if (this.props.news.page > 1) {
            this.showNewsList(this.props.news.page - 1, this.props.news.rows);
        }
    }
    // 下一页
    downPage() {
        if (this.props.news.page < this.props.news.pageCount) {
            this.showNewsList(this.props.news.page + 1, this.props.news.rows);
        }
    }
    // 渲染
    render() {
        // 新闻列表
        let newsListElement = "";
        if (this.props.news.status === 'success') {
            newsListElement = (
                <ul>
                    {this.props.news.list.map(m => (
                        <li key={m.id}><span className={style.dot}></span><a href={m.link} target="_blank" rel="noopener noreferrer">{m.title}</a></li>
                    ))}
                </ul>
            );
        }
        else if (this.props.news.status === 'loading') {
            newsListElement = (<div className={style.loading}>加载中</div>);
        }
        else if (this.props.news.status === 'error') {
            newsListElement = (<div className={style.loading}>{this.props.news.errorMsg}</div>);
        }
        // 分页
        return (
            <div className={style.newsItem}>
                <div className={style.title}>新闻</div>
                {newsListElement}
                <div className={style.page}>
                    <span className={style.link} onClick={this.upPage}>上一页</span>
                    {' ' + this.props.news.page + ' / ' + this.props.news.pageCount + ' '}
                    <span className={style.link} onClick={this.downPage}>下一页</span>
                </div>
            </div>
        );
    }
}
News.propTypes = {
    news: PropTypes.object,
};

/* istanbul ignore next */
function mapStateToProps(state) {
    return {
        news: state.home.news,
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
)(News);