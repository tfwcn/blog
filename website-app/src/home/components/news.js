import React from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import * as actions from '../common/actions'
import style from '../styles/index.module.scss'

class News extends React.Component {
    // 组件加载完成
    componentDidMount() {
        this.getNewsList();
    }
    getNewsList(){
        this.props.actions.getNewsList([])
    }
    render() {
        return (
            <div className={style.newsItem}>
                <div className={style.title}>新闻</div>
                {if(this.props.newsList!=null){
                    return (
                <ul>
                    {this.props.newsList.map(m => (
                        <li><span className={style.dot}></span><a href="/">{m.title}</a></li>
                    ))}
                </ul>);
            }
                else{
                    return '加载中';
                }
                }
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