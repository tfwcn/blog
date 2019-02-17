import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import * as actions from './redux/actions';
import { Editor } from '@tinymce/tinymce-react';
import styles from './BlogAddPage.module.scss';

export class BlogAddPage extends Component {
  static get propTypes() {
    return {
      home: PropTypes.object.isRequired,
      actions: PropTypes.object.isRequired,
    };
  }
  constructor(props) {
    super(props);
    this.editorRef = React.createRef();
    this.handleEditorChange = e => {
      console.log('Content was updated:', e.target.getContent());
    };

    this.state = {
      confirmDirty: false,
      autoCompleteResult: [],
    };

    this.handleSubmit = e => {
      e.preventDefault();
      this.props.form.validateFieldsAndScroll((err, values) => {
        if (!err) {
          console.log('Received values of form: ', values);
        }
      });
    };

    this.handleConfirmBlur = e => {
      const value = e.target.value;
      this.setState({ confirmDirty: this.state.confirmDirty || !!value });
    };

    this.compareToFirstPassword = (rule, value, callback) => {
      const form = this.props.form;
      if (value && value !== form.getFieldValue('password')) {
        callback('Two passwords that you enter is inconsistent!');
      } else {
        callback();
      }
    };

    this.validateToNextPassword = (rule, value, callback) => {
      const form = this.props.form;
      if (value && this.state.confirmDirty) {
        form.validateFields(['confirm'], { force: true });
      }
      callback();
    };

    this.handleWebsiteChange = value => {
      let autoCompleteResult;
      if (!value) {
        autoCompleteResult = [];
      } else {
        autoCompleteResult = ['.com', '.org', '.net'].map(
          domain => `${value}${domain}`
        );
      }
      this.setState({ autoCompleteResult });
    };
  }

  componentDidMount() {}

  render() {
    return (
      <div className={styles.blogAddPage}>
        <div className={styles.blogForm}>
          <form>
            <div>
              <div>标题:</div>
              <div>
                <input type="text" name="title" />
              </div>
            </div>
            <label>
              <div>内容:</div>
              <div>
                <Editor
                  apiKey="7eh5wmpkjhy1e9b2i6zo6r661dztmxiw3nrhrla6tvd9i5jd"
                  initialValue="<p>This is the initial content of the editor</p>"
                  init={{
                    plugins: 'link image code',
                    toolbar:
                      'undo redo | cut copy paste | formatselect | forecolor backcolor bold italic strikethrough | alignleft aligncenter alignright | link image code | removeformat',
                  }}
                  onChange={this.handleEditorChange}
                />
              </div>
            </label>
            <input type="submit" value="发布博客" />
          </form>
        </div>
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
  mapDispatchToProps
)(BlogAddPage);
