import React, { Component } from 'react';
// import ReactDOM from 'react-dom';
import * as THREE from 'three';
// import PropTypes from 'prop-types';
// import { bindActionCreators } from 'redux';
// import { connect } from 'react-redux';
// import * as actions from './redux/actions';
import styles from './BackgroundImage.module.scss';

export default class BackgroundImage extends Component {
  static get propTypes() {
    return {
      //   home: PropTypes.object.isRequired,
      //   actions: PropTypes.object.isRequired,
    };
  }
  constructor(props) {
    super(props);
    console.log(props);
  }
  componentDidMount() {
    this.initThree();
  }
  initThree() {
    var scene = new THREE.Scene();
    var camera = new THREE.PerspectiveCamera(
      75,
      window.innerWidth / window.innerHeight,
      0.1,
      1000
    );

    var renderer = new THREE.WebGLRenderer();
    renderer.setSize(window.innerWidth, window.innerHeight);
    renderer.setClearColor(0x000000, 0.0);
    document.getElementById('backgroundImage').appendChild(renderer.domElement);

    var geometry = new THREE.BoxGeometry(1, 1, 1);
    var material = new THREE.MeshBasicMaterial({ color: 0x00ff00 });
    var cube = new THREE.Mesh(geometry, material);
    scene.add(cube);

    camera.position.z = 5;

    var animate = function() {
      requestAnimationFrame(animate);

      cube.rotation.x += 0.01;
      cube.rotation.y += 0.01;

      renderer.render(scene, camera);
    };
    function onWindowResize() {
      camera.aspect = window.innerWidth / window.innerHeight;
      camera.updateProjectionMatrix();
      renderer.setSize(window.innerWidth, window.innerHeight);
    }
    window.addEventListener('resize', onWindowResize, false);

    animate();
  }

  render() {
    this.bgElement = (
      <div id={'backgroundImage'} className={styles.backgroundImage} />
    );
    return this.bgElement;
  }
}

// /* istanbul ignore next */
// function mapStateToProps(state) {
//   return {
//     home: state.home,
//   };
// }

// /* istanbul ignore next */
// function mapDispatchToProps(dispatch) {
//   return {
//     actions: bindActionCreators({ ...actions }, dispatch),
//   };
// }

// export default connect(
//   mapStateToProps,
//   mapDispatchToProps
// )(BackgroundImage);
