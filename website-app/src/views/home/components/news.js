import React from 'react';
import style from '../styles/news.module.scss'
import { postData } from '@/common/fetchHelper'

class News extends React.Component {
    // 构造函数
    constructor(props) {
        super(props);
        this.state = {
            news: {
                // 数据
                list: [],
                // 总记录数
                count: 0,
                status: 'loading',
                errorMsg: null,
                // 当前页
                page: 1,
                // 总记录数
                rows: 10,
                // 总页数
                pageCount: 1,
            },
        }
    }
    // 组件加载完成
    componentDidMount() {
        this.upPage = this.upPage.bind(this);
        this.downPage = this.downPage.bind(this);
        this.showNewsList(1, 10);
    }
    // 加载新闻列表
    showNewsList(page, rows) {
        this.setState({
            news: {
                ...this.state.news,
                list: [],
                count: 0,
                status: 'loading',
                errorMsg: null,
                page: page,
                rows: rows,
                pageCount: Math.ceil(0 / rows),
            }
        });
        // this.props.actions.showNewsList(null, 0, 'loading', null, page, rows);
        postData('/api/News/list', { page: page, rows: rows })
            .then(response => {
                console.log(response);
                if (response.code === 0) {
                    let count = response.data.count > 100 ? 100 : response.data.count;
                    this.setState({
                        news: {
                            ...this.state.news,
                            list: response.data.dataList,
                            count: count,
                            status: 'success',
                            errorMsg: null,
                            pageCount: Math.ceil(count / this.state.news.rows),
                        }
                    });
                    // this.props.actions.showNewsList(response.data.dataList, count, 'success', null, this.props.news.page, this.props.news.rows);
                } else {
                    this.setState({
                        news: {
                            ...this.state.news,
                            list: [],
                            count: 0,
                            status: 'error',
                            errorMsg: response.errorMsg,
                            pageCount: Math.ceil(0 / this.state.news.rows),
                        }
                    });
                    // this.props.actions.showNewsList(null, 0, 'error', response.errorMsg, this.props.news.page, this.props.news.rows);
                }
            })
            .catch(error => {
                console.error(error);
                this.setState({
                    news: {
                        ...this.state.news,
                        list: [],
                        count: 0,
                        status: 'error',
                        errorMsg: error.message,
                        pageCount: Math.ceil(0 / this.state.news.rows),
                    }
                });
                // this.props.actions.showNewsList(null, 0, 'error', error.message, this.props.news.page, this.props.news.rows);
            });
    }
    // 上一页
    upPage() {
        if (this.state.news.page > 1) {
            this.showNewsList(this.state.news.page - 1, this.state.news.rows);
        }
    }
    // 下一页
    downPage() {
        if (this.state.news.page < this.state.news.pageCount) {
            this.showNewsList(this.state.news.page + 1, this.state.news.rows);
        }
    }
    // 渲染
    render() {
        // 新闻列表
        let newsListElement = "";
        if (this.state.news.status === 'success') {
            newsListElement = (
                <ul>
                    {this.state.news.list.map(m => (
                        <li key={m.id}><span className={style.dot}></span><a href={m.link} target="_blank" rel="noopener noreferrer">{m.title}</a></li>
                    ))}
                </ul>
            );
        }
        else if (this.state.news.status === 'loading') {
            newsListElement = (<div className={style.loading}>加载中</div>);
        }
        else if (this.state.news.status === 'error') {
            newsListElement = (<div className={style.loading}>{this.state.news.errorMsg}</div>);
        }
        // 分页
        return (
            <div className={style.newsItem}>
                <div className={style.title}>新闻</div>
                {newsListElement}
                <div className={style.page}>
                    <span className={style.link} onClick={this.upPage}>上一页</span>
                    {' ' + this.state.news.page + ' / ' + this.state.news.pageCount + ' '}
                    <span className={style.link} onClick={this.downPage}>下一页</span>
                </div>
            </div>
        );
    }
}

export default News;