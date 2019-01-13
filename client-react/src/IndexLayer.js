import React from 'react';
import { Icon } from 'antd';
// import MenuComponent from './MenuComponent';
import styles from './styles/IndexLayer.module.scss';

class IndexLayer extends React.Component {
  //构造函数
  constructor(props) {
    super();
    this.state = {
      value: props.value,
      text: ''
    };
    this.test = this.test.bind(this);
    this.handleChange = this.handleChange.bind(this);
  }
  componentDidMount() {
    console.log('componentDidMount');
  }

  // componentWillUnmount() {
  //   console.log('componentWillUnmount');
  // }
  // componentWillUpdate() {
  //   console.log('componentWillUpdate');
  // }
  // componentDidUpdate() {
  //   console.log('componentDidUpdate');
  // }
  //测试方法
  test() {
    this.setState(prevState => ({
      value: prevState.value + 1
    }));
    // console.log('test');
  }
  handleChange(event) {
    this.setState({ text: event.target.text });
  }
  // test2() {
  //   return [1, 2, 3, 5].map(num => <span key={num}>{num}</span>);
  // }
  render() {
    return (
      <div className={styles.IndexLayer} onClick={this.test}>
        <div className={styles.header}>
          <div className={styles.empty} />
          <div className={styles.content}>
            <span className={styles.logo} />
            {/* 菜单 */}
            <span className={styles.menu}>
              <ul>
                <li className={styles.select}>.Net</li>
                <li>Java</li>
                <li>Python</li>
              </ul>
            </span>
            {/* 右边按钮 */}
            <span className={styles.right}>
              <span className={styles.triangle} />
              <Icon className={styles.icon} type="form" />
              写博客
            </span>
          </div>
          <div className={styles.emptyRight} />
        </div>
        <div className={styles.main}>
          <div className={styles.empty} />
          <div className={styles.content}>
            {/* <div className={styles.left}>
              <ul>
                <li className={styles.select}>.Net</li>
                <li>Java</li>
                <li>Python</li>
              </ul>
            </div> */}
            <div className={styles.right}>
              {this.state.value}
              <form>
                <label>
                  Name:
                  <input
                    type="text"
                    value={this.state.text}
                    onChange={this.handleChange}
                  />
                </label>
                <input type="submit" value="Submit" />
              </form>
            </div>
          </div>
          <div className={styles.empty} />
        </div>
        <div className={styles.footer}>footer</div>
      </div>
    );
  }
}

// IndexLayer.defaultProps = {
//   name: ''
// };

export default IndexLayer;
