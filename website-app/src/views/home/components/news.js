import React from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import * as actions from '../common/actions'
import style from '../styles/news.module.scss'
import {postData} from '../../../common/fetchHelper'

class News extends React.Component {
    // 组件加载完成
    componentDidMount() {
        this.getNewsList();
    }
    getNewsList() {
        this.props.actions.getNewsList(null);
        
        postData('http://localhost/api/News/list',{page:1,rows:100})
          .then(response => {
            console.log(response);
            if(response.code===0){
                this.props.actions.getNewsList(response.data.dataList);
            }
          })
          .catch(error => {
            console.log(error);
          });
    }
    render() {
        let newsElement = "";
        if (this.props.newsList != null) {
            newsElement = (
                <ul>
                    {this.props.newsList.map(m => (
                        <li key={m.id}><span className={style.dot}></span><a href={m.link} target="_blank" rel="noopener noreferrer">{m.title}</a></li>
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