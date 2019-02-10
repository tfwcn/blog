import React, { Component } from 'react';
import PropTypes from 'prop-types';
import * as THREE from 'three';
import * as Stats from 'stats.js';
import WEBGL from '../../common/WebGL';
import styles from './ForegroundImage.module.scss';

class PointSprite {
  static get propTypes() {
    return {
      spriteMap: PropTypes.object.isRequired,
      spriteMaterial: PropTypes.object.isRequired,
      sprite: PropTypes.object.isRequired,
    };
  }
  constructor(props) {
    this.spriteMap = props.spriteMap;

    this.spriteMaterial = new THREE.SpriteMaterial({
      map: this.spriteMap,
      color: 0xffffff,
    });

    this.sprite = new THREE.Sprite(this.spriteMaterial);
    this.sprite.position.copy(props.position);
    this.sprite.scale.x = 0.4;
    this.sprite.scale.y = 0.4;
  }
  run() {
    if (this.sprite.scale.x > 0) {
      this.sprite.position.y -= (0.4 - this.sprite.scale.x) * 0.1;
      this.sprite.scale.x -= 0.01;
      this.sprite.scale.y -= 0.01;
    }
  }
  CheckEnd() {
    return this.sprite.scale.x <= 0;
  }
}

export default class ForegroundImage extends Component {
  constructor(props) {
    super(props);
    this.foregroundImageRef = React.createRef();
  }
  componentDidMount() {
    // this.initThree2();
    this.initThree3();
  }
  //鼠标坐标映射到三维空间
  convertTo3DCoordinate(camera, clientX, clientY) {
    // console.log('cx: ' + clientX + ', cy: ' + clientY);
    let mv = new THREE.Vector3(
      (clientX / window.innerWidth) * 2 - 1,
      -(clientY / window.innerHeight) * 2 + 1,
      0
    );
    // console.log('mx: ' + mv.x + ', my: ' + mv.y + ', mz:' + mv.z);
    // mv.unproject(this.camera);
    // console.log('x: ' + mv.x + ', y: ' + mv.y + ', z:' + mv.z);
    return mv.unproject(camera);
  }
  //鼠标坐标映射到三维空间并映射到某个平面
  convertTo3DCoordinateForZIndex(camera, clientX, clientY, z) {
    let vector = this.convertTo3DCoordinate(camera, clientX, clientY);
    //作一条射线，与某个平面的交点作为坐标
    let ray = new THREE.Ray(camera.position, vector.sub(camera.position));

    let planeNormal = camera.position.clone();
    planeNormal.sub(new THREE.Vector3(0, 0, 0).unproject(this.camera));
    planeNormal.normalize();
    let plane = new THREE.Plane(planeNormal, z);
    let point = new THREE.Vector3(0, 0, 0);
    ray.intersectPlane(plane, point);
    return point;
  }
  initThree2() {
    if (WEBGL.isWebGL2Available() === false) {
      document.body.appendChild(WEBGL.getWebGL2ErrorMessage());
    }

    let stats = new Stats();
    stats.showPanel(0); // 0: fps, 1: ms, 2: mb, 3+: custom
    this.foregroundImageRef.current.appendChild(stats.dom);

    let scene = new THREE.Scene();
    this.camera = new THREE.PerspectiveCamera(
      45,
      window.innerWidth / window.innerHeight,
      0.1,
      1000
    );
    this.camera.position.copy(new THREE.Vector3(0, 0, 0)); //设置摄像机位置
    this.camera.up.copy(new THREE.Vector3(0, 1, 0)); //设置y正方向为上
    this.camera.lookAt(new THREE.Vector3(0, 0, 1)); //设置视点

    this.renderer = new THREE.WebGLRenderer({
      antialias: false,
      alpha: true,
    });
    this.renderer.setSize(window.innerWidth, window.innerHeight);
    this.renderer.setClearColor(0x000000, 0.0);
    this.renderer.shadowMap.enabled = true; //阴影
    this.renderer.shadowMap.type = THREE.PCFSoftShadowMap; // default THREE.PCFShadowMap
    this.foregroundImageRef.current.appendChild(this.renderer.domElement);
    //全局光
    var light = new THREE.AmbientLight(0xffffff, 0.5); // soft white light
    scene.add(light);
    //平行光
    var directionalLight = new THREE.DirectionalLight(0xffffff, 1);
    // directionalLight.castShadow = true; //产生阴影

    //Set up shadow properties for the light
    directionalLight.shadow.mapSize.width = 512; // default
    directionalLight.shadow.mapSize.height = 512; // default
    directionalLight.shadow.camera.near = 0.5; // default
    directionalLight.shadow.camera.far = 500; // default

    directionalLight.position.copy(new THREE.Vector3(1, 1, 0)); //位置
    directionalLight.target.position.copy(new THREE.Vector3(0, 0, 1)); //方向
    scene.add(directionalLight);
    scene.add(directionalLight.target);

    let geometry = new THREE.BoxGeometry(1, 1, 1);
    var edges = new THREE.EdgesGeometry(geometry); //显示边缘
    let material = new THREE.MeshStandardMaterial({ color: 0x00ff00 });
    this.cube = new THREE.Mesh(geometry, material);
    // this.cube.castShadow = true; //产生阴影
    // this.cube.receiveShadow = true; //接收阴影
    this.cube.position.copy(new THREE.Vector3(0, 0, 10));
    this.cube.add(new THREE.LineSegments(edges, material));
    scene.add(this.cube);

    window.addEventListener(
      'resize',
      () => {
        this.camera.aspect = window.innerWidth / window.innerHeight;
        this.camera.updateProjectionMatrix();
        this.renderer.setSize(window.innerWidth, window.innerHeight);
      },
      false
    );
    document.addEventListener(
      'mousemove',
      () => {
        this.convertTo3DCoordinate(event.clientX, event.clientY);
      },
      false
    );
    let animate = () => {
      requestAnimationFrame(animate);
      this.renderer.render(scene, this.camera);
      stats.update();
    };
    animate();
  }
  initThree3() {
    if (WEBGL.isWebGL2Available() === false) {
      document.body.appendChild(WEBGL.getWebGL2ErrorMessage());
    }

    // let stats = new Stats();
    // stats.showPanel(0); // 0: fps, 1: ms, 2: mb, 3+: custom
    // this.foregroundImageRef.current.appendChild(stats.dom);

    this.scene = new THREE.Scene();
    this.camera = new THREE.PerspectiveCamera(
      45,
      window.innerWidth / window.innerHeight,
      0.1,
      1000
    );
    this.camera.position.copy(new THREE.Vector3(0, 0, 0)); //设置摄像机位置
    this.camera.up.copy(new THREE.Vector3(0, 1, 0)); //设置y正方向为上
    this.camera.lookAt(new THREE.Vector3(0, 0, 1)); //设置视点

    this.renderer = new THREE.WebGLRenderer({
      antialias: false,
      alpha: true,
    });
    this.renderer.setSize(window.innerWidth, window.innerHeight);
    this.renderer.setClearColor(0x000000, 0.0);
    this.foregroundImageRef.current.appendChild(this.renderer.domElement);

    let textureLoader = new THREE.TextureLoader();
    this.pointSpriteMap = textureLoader.load(require('../../static/p.png'));
    this.pointSpriteList = [];

    window.addEventListener(
      'resize',
      () => {
        this.camera.aspect = window.innerWidth / window.innerHeight;
        this.camera.updateProjectionMatrix();
        this.renderer.setSize(window.innerWidth, window.innerHeight);
      },
      false
    );
    document.addEventListener(
      'mousemove',
      event => {
        let position = this.convertTo3DCoordinateForZIndex(
          this.camera,
          event.clientX,
          event.clientY,
          10
        );
        if (position != null) {
          let tmpPointSprite = new PointSprite({
            spriteMap: this.pointSpriteMap,
            position: position,
          });
          this.scene.add(tmpPointSprite.sprite);
          this.pointSpriteList.push(tmpPointSprite);
        }
      },
      false
    );
    let animate = () => {
      requestAnimationFrame(animate);
      this.pointSpriteList.map(item => {
        item.run();
        if (item.CheckEnd()) {
          this.pointSpriteList.splice(this.pointSpriteList.indexOf(item), 1);
          this.scene.remove(item.sprite);
        }
      });
      this.renderer.render(this.scene, this.camera);
      // stats.update();
    };
    animate();
  }

  render() {
    return (
      <div ref={this.foregroundImageRef} className={styles.foregroundImage} />
    );
  }
}
