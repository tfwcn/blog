import React from 'react';
import styles from './styles/IndexLayer.module.scss';
import MenuComponent from './MenuComponent';

class IndexLayer extends React.Component {
  //构造函数
  constructor(props) {
    super();
    this.state = {
      value: props.value
    };
    this.test = this.test.bind(this);
  }
  componentDidMount() {
    console.log('componentDidMount');
  }

  componentWillUnmount() {
    console.log('componentWillUnmount');
  }
  componentWillUpdate() {
    console.log('componentWillUpdate');
  }
  componentDidUpdate() {
    console.log('componentDidUpdate');
  }
  //测试方法
  test() {
    this.setState(prevState => ({
      value: prevState.value + 1
    }));
    console.log('test');
  }
  test2() {
    return [1, 2, 3, 5].map(num => <span key={num}>{num}</span>);
  }
  render() {
    return (
      <div className={styles.IndexLayer} onClick={this.test}>
        <div className={styles.header}>
          <div className={styles.content}>{this.state.value}</div>
        </div>
        <div className={styles.main}>
          <div className={styles.content}>
            <div className={styles.left}>
              <MenuComponent />
            </div>
            <div className={styles.right}>{this.state.value}</div>
          </div>
        </div>
      </div>
    );
  }
}

// IndexLayer.defaultProps = {
//   name: ''
// };

export default IndexLayer;
