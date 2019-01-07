import React from 'react';
import styles from './styles/IndexLayer.module.scss';

class IndexLayer extends React.PureComponent {
  render() {
    return <div className={styles.IndexLayer}>{this.props.name}</div>;
  }
}

IndexLayer.defaultProps = {
  name: ''
};

export default IndexLayer;
