import React from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import * as actions from '../common/actions'
import style from '../styles/news.module.scss'

class News extends React.Component {
    // 组件加载完成
    componentDidMount() {
        this.getNewsList();
    }
    getNewsList() {
        this.props.actions.getNewsList([{ id: 0, title: 'aaa' }, { id: 1, title: 'bbb' }]);
        // fetch('http://news.baidu.com/')
        //   .then(res => console.log(res));
        //   .then(response => {
        //     console.log(response);
        //     // this.props.actions.homeBlogListSuccess(response);
        //     this.setState({ blogList: response.result.list });
        //   })
        //   .catch(error => {
        //     // this.props.actions.homeBlogListError(error);
        //     console.log(error);
        //   });
    }
    render() {
        let newsElement = "";
        if (this.props.newsList != null) {
            newsElement = (
                <ul>
                    {this.props.newsList.map(m => (
                        <li key={m.id}><span className={style.dot}></span><a href="/">{m.title}</a></li>
                    ))}
                </ul>
            );
        }
        else {
            newsElement = (<div className={style.loading}>加载中</div>);
        }
        return (
            <div className={style.newsItem}>
                <div className={style.title}>新闻</div>
                {newsElement}
            </div>
        );
    }
}
News.propTypes = {
    newsList: PropTypes.array,
};

/* istanbul ignore next */
function mapStateToProps(state) {
    return {
        newsList: state.home.newsList,
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