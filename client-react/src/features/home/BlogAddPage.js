import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import * as actions from './redux/actions';
import { Editor } from '@tinymce/tinymce-react';
import styles from './BlogAddPage.module.scss';

export class BlogAddPage extends Component {
  constructor(props) {
    super(props);
    //初始化
    this.state = {
      loading: false,
      title: '',
      content: '',
      result: null,
    };

    this.handleTitleChange = e => {
      console.log('Title was updated:', e.target.value);
      this.setState({ title: e.target.value });
    };

    this.handleContentChange = e => {
      console.log('Content was updated:', e.target.getContent());
      this.setState({ content: e.target.getContent() });
    };

    this.handleSubmit = e => {
      //取消默认动作
      e.preventDefault();
      console.log(e);
      if (this.state.title.trim() == '' || this.state.content.trim() == '')
        return;
      fetch('/api/note/add', {
        method: 'POST',
        mode: 'cors',
        credentials: 'include',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          title: this.state.title,
          content: this.state.content,
          typeId: 1,
        }),
      })
        .then(res => res.json())
        .then(response => {
          console.log(response);
          // this.props.actions.homeBlogListSuccess(response);
          this.setState({ result: response.code });
        })
        .catch(error => {
          console.log(error);
          // this.props.actions.homeBlogListError(error);
        });
    };
  }

  componentDidMount() {}

  render() {
    return (
      <div className={styles.blogAddPage}>
        <div className={styles.blogForm}>
          <div className={styles.content}>
            {this.state.result == 0 ? (
              <div>新增成功,请等待审核</div>
            ) : (
              <form onSubmit={this.handleSubmit}>
                <div className={styles.item}>
                  <div className={styles.name}>标题:</div>
                  <div className={styles.value}>
                    <input
                      className={styles.text}
                      type="text"
                      name="title"
                      value={this.state.title}
                      onChange={this.handleTitleChange}
                    />
                  </div>
                </div>
                <div className={styles.item}>
                  <div className={styles.name}>内容:</div>
                  <div className={styles.value}>
                    <Editor
                      apiKey="7eh5wmpkjhy1e9b2i6zo6r661dztmxiw3nrhrla6tvd9i5jd"
                      initialValue=""
                      init={{
                        plugins: 'image link codesample table',
                        toolbar:
                          'undo redo | cut copy paste | formatselect | forecolor backcolor bold italic strikethrough | alignleft aligncenter alignright | link image codesample | removeformat',
                        height: 700,
                        language_url:
                          process.env.PUBLIC_URL +
                          '/libs/tinymce/lang/zh_CN.js',
                        language: 'zh_CN',
                      }}
                      value={this.state.content}
                      onChange={this.handleContentChange}
                    />
                  </div>
                </div>
                <div className={styles.bottom}>
                  <input
                    className={styles.button}
                    type="submit"
                    value="发布博客"
                  />
                </div>
              </form>
            )}
          </div>
        </div>
      </div>
    );
  }
}
BlogAddPage.propTypes = {
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
)(BlogAddPage);
