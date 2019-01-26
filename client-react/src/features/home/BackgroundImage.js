import React, { Component } from 'react';
import * as THREE from 'three';
import * as Stats from 'stats.js';
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
    this.backgroundImageRef = React.createRef();
  }
  componentDidMount() {
    // this.initThree();
  }
  initThree() {
    var stats = new Stats();
    stats.showPanel(0); // 0: fps, 1: ms, 2: mb, 3+: custom
    this.backgroundImageRef.current.appendChild(stats.dom);
    var scene = new THREE.Scene();
    var camera = new THREE.PerspectiveCamera(
      75,
      window.innerWidth / window.innerHeight,
      0.1,
      1000
    );

    var renderer = new THREE.WebGLRenderer({
      antialias: false,
      alpha: true,
    });
    renderer.setSize(window.innerWidth, window.innerHeight);
    renderer.setClearColor(0x000000, 0.0);
    this.backgroundImageRef.current.appendChild(renderer.domElement);

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
      stats.update();
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
    return (
      <div ref={this.backgroundImageRef} className={styles.backgroundImage} />
    );
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
