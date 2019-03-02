import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import * as actions from './redux/actions';
import { Editor } from '@tinymce/tinymce-react';
import { Icon } from 'antd';
import styles from './BlogContentPage.module.scss';

export class BlogContentPage extends Component {
  constructor(props) {
    super(props);
    //初始化
    this.state = {
      loading: false,
      title: '',
      content: '',
      result: null,
    };
    console.log(this);

    this.getModel = () => {
      //取消默认动作
      fetch('/api/note/model', {
        method: 'POST',
        mode: 'cors',
        credentials: 'include',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          num: this.props.match.params.id,
        }),
      })
        .then(res => res.json())
        .then(response => {
          console.log(response);
          // this.props.actions.homeBlogListSuccess(response);
          this.setState({
            result: response.code,
            title: response.result.title,
            content: response.result.content,
          });
          document.title = 'PPHT个人博客--' + response.result.title;
        })
        .catch(error => {
          console.log(error);
          // this.props.actions.homeBlogListError(error);
        });
    };
  }

  componentDidMount() {
    this.getModel();
  }

  render() {
    let contentHtml = '';
    if (this.state.result == null) {
      contentHtml = <div>文章加载中...</div>;
    } else if (this.state.title == '') contentHtml = <div>文章不存在</div>;
    else {
      contentHtml = (
        <div>
          <div className={styles.blogContentTitle}>
            <Icon type="book" className={styles.icon} />
            {this.state.title}
          </div>
          <div className={styles.blogContentContent}>
            <Editor
              apiKey="7eh5wmpkjhy1e9b2i6zo6r661dztmxiw3nrhrla6tvd9i5jd"
              value={this.state.content.replace(/<script [^>]+>/gi, '')}
              disabled
              // inline
              init={{
                toolbar: false,
                menubar: false,
                plugins: 'image link codesample table quickbars autoresize',
                autoresize_on_init: true,
                language_url:
                  process.env.PUBLIC_URL + '/libs/tinymce/lang/zh_CN.js',
                language: 'zh_CN',
                // height: 800,
              }}
            />
          </div>
        </div>
      );
    }
    return (
      <div className={styles.blogContentPage}>
        <div className={styles.blogContent}>{contentHtml}</div>
      </div>
    );
  }
}
BlogContentPage.propTypes = {
  home: PropTypes.object.isRequired,
  actions: PropTypes.object.isRequired,
};

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
)(BlogContentPage);
