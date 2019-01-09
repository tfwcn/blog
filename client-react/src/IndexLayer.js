import React from 'react';
import styles from './styles/IndexLayer.module.scss';

class IndexLayer extends React.Component {
  //构造函数
  constructor() {
    super();
    this.state = {
      value: 0
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
        {this.state.value}

        {this.test2()}
      </div>
    );
  }
}

// IndexLayer.defaultProps = {
//   name: ''
// };

export default IndexLayer;
