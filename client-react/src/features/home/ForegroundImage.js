import React, { Component } from 'react';
import * as THREE from 'three';
import * as Stats from 'stats.js';
import WEBGL from '../../common/WebGL';
import styles from './ForegroundImage.module.scss';

export default class ForegroundImage extends Component {
  constructor(props) {
    super(props);
    this.foregroundImageRef = React.createRef();
  }
  componentDidMount() {
    // this.initThree();
    this.initThree2();
  }
  initThree() {
    if (WEBGL.isWebGL2Available() === false) {
      document.body.appendChild(WEBGL.getWebGL2ErrorMessage());
    }

    let stats = new Stats();
    stats.showPanel(0); // 0: fps, 1: ms, 2: mb, 3+: custom
    this.foregroundImageRef.current.appendChild(stats.dom);

    let scene = new THREE.Scene();
    var camera = new THREE.PerspectiveCamera(
      75,
      window.innerWidth / window.innerHeight,
      0.1,
      1000
    );
    camera.position.x = 0;
    camera.position.y = 0;
    camera.position.z = 0;

    let renderer = new THREE.WebGLRenderer({
      antialias: false,
      alpha: true,
    });
    renderer.setSize(window.innerWidth, window.innerHeight);
    renderer.setClearColor(0x000000, 0.0);
    this.foregroundImageRef.current.appendChild(renderer.domElement);
    // scene = new THREE.Scene();
    // scene.fog = new THREE.FogExp2(0x000000, 0.0008); //雾
    var geometry = new THREE.BufferGeometry();
    var vertices = [];
    let materials = [];
    let mouseX = 0;
    let mouseY = 0;
    let windowHalfX = window.innerWidth / 2;
    let windowHalfY = window.innerHeight / 2;
    var textureLoader = new THREE.TextureLoader();
    var sprite1 = textureLoader.load(require('../../static/p.png'));
    var sprite2 = textureLoader.load(require('../../static/p.png'));
    var sprite3 = textureLoader.load(require('../../static/p.png'));
    var sprite4 = textureLoader.load(require('../../static/p.png'));
    var sprite5 = textureLoader.load(require('../../static/p.png'));
    for (let i = 0; i < 10000; i++) {
      var x = Math.random() * 2000 - 1000;
      var y = Math.random() * 2000 - 1000;
      var z = Math.random() * 2000 - 1000;
      vertices.push(x, y, z);
    }
    geometry.addAttribute(
      'position',
      new THREE.Float32BufferAttribute(vertices, 3)
    );
    let parameters = [
      [[1.0, 1, 0.5], sprite2, 20],
      [[0.95, 1, 0.5], sprite3, 15],
      [[0.9, 1, 0.5], sprite1, 10],
      [[0.85, 1, 0.5], sprite5, 8],
      [[0.8, 1, 0.5], sprite4, 5],
    ];
    for (let i = 0; i < parameters.length; i++) {
      var color = parameters[i][0];
      var sprite = parameters[i][1];
      var size = parameters[i][2];
      materials[i] = new THREE.PointsMaterial({
        size: size,
        map: sprite,
        blending: THREE.AdditiveBlending,
        depthTest: false,
        transparent: true,
      });
      materials[i].color.setHSL(color[0], color[1], color[2]);
      var particles = new THREE.Points(geometry, materials[i]);
      particles.rotation.x = Math.random() * 6;
      particles.rotation.y = Math.random() * 6;
      particles.rotation.z = Math.random() * 6;
      scene.add(particles);
    }

    var animate = function() {
      requestAnimationFrame(animate);
      render();
      stats.update();
    };
    function onWindowResize() {
      camera.aspect = window.innerWidth / window.innerHeight;
      camera.updateProjectionMatrix();
      renderer.setSize(window.innerWidth, window.innerHeight);
    }
    function onDocumentMouseMove(event) {
      mouseX = event.clientX - windowHalfX;
      mouseY = event.clientY - windowHalfY;
    }
    function onDocumentTouchStart(event) {
      if (event.touches.length === 1) {
        event.preventDefault();
        mouseX = event.touches[0].pageX - windowHalfX;
        mouseY = event.touches[0].pageY - windowHalfY;
      }
    }
    function onDocumentTouchMove(event) {
      if (event.touches.length === 1) {
        event.preventDefault();
        mouseX = event.touches[0].pageX - windowHalfX;
        mouseY = event.touches[0].pageY - windowHalfY;
      }
    }
    window.addEventListener('resize', onWindowResize, false);
    document.addEventListener('mousemove', onDocumentMouseMove, false);
    document.addEventListener('touchstart', onDocumentTouchStart, false);
    document.addEventListener('touchmove', onDocumentTouchMove, false);
    function render() {
      var time = Date.now() * 0.00005;
      camera.position.x += (mouseX - camera.position.x) * 0.05;
      camera.position.y += (-mouseY - camera.position.y) * 0.05;
      camera.lookAt(scene.position);
      for (let i = 0; i < scene.children.length; i++) {
        var object = scene.children[i];
        if (object instanceof THREE.Points) {
          object.rotation.y = time * (i < 4 ? i + 1 : -(i + 1));
        }
      }
      for (let i = 0; i < materials.length; i++) {
        var color = parameters[i][0];
        var h = ((360 * (color[0] + time)) % 360) / 360;
        materials[i].color.setHSL(h, color[1], color[2]);
      }
      renderer.render(scene, camera);
    }

    animate();
  }
  initThree2() {
    if (WEBGL.isWebGL2Available() === false) {
      document.body.appendChild(WEBGL.getWebGL2ErrorMessage());
    }

    let stats = new Stats();
    stats.showPanel(0); // 0: fps, 1: ms, 2: mb, 3+: custom
    this.foregroundImageRef.current.appendChild(stats.dom);

    let scene = new THREE.Scene();
    var camera = new THREE.PerspectiveCamera(
      90,
      window.innerWidth / window.innerHeight,
      0.1,
      1000
    );
    camera.position.copy(new THREE.Vector3(0, 0, 0)); //设置摄像机位置
    camera.up.copy(new THREE.Vector3(0, 1, 0)); //设置y正方向为上
    camera.lookAt(new THREE.Vector3(0, 0, 1)); //设置视点

    let renderer = new THREE.WebGLRenderer({
      antialias: false,
      alpha: true,
    });
    renderer.setSize(window.innerWidth, window.innerHeight);
    renderer.setClearColor(0x000000, 0.0);
    this.foregroundImageRef.current.appendChild(renderer.domElement);

    var geometry = new THREE.BoxGeometry(1, 1, 1);
    var material = new THREE.MeshBasicMaterial({ color: 0x00ff00 });
    var cube = new THREE.Mesh(geometry, material);
    cube.position.copy(new THREE.Vector3(0, 0, 1));
    scene.add(cube);

    function onWindowResize() {
      camera.aspect = window.innerWidth / window.innerHeight;
      camera.updateProjectionMatrix();
      renderer.setSize(window.innerWidth, window.innerHeight);
    }
    window.addEventListener('resize', onWindowResize, false);
    var animate = function() {
      requestAnimationFrame(animate);
      renderer.render(scene, camera);
      stats.update();
    };
    animate();
  }

  render() {
    return (
      <div ref={this.foregroundImageRef} className={styles.foregroundImage} />
    );
  }
}
