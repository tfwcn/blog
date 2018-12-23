<template>
  <div class="c-cloud" ref="$canvas"></div>
</template>

<script>
import {
  PerspectiveCamera,
  Scene,
  Geometry,
  TextureLoader,
  LinearMipMapLinearFilter,
  Fog,
  ShaderMaterial,
  Mesh,
  PlaneGeometry,
  WebGLRenderer,
  CameraHelper,
  BoxGeometry,
  MeshBasicMaterial,
  MeshLambertMaterial,
  AmbientLight,
  ImageUtils
} from 'three';

export default {
  data() {
    return {
      window: {
        width: 0,
        heigth: 0
      },
      mouse: {
        x: 0,
        y: 0
      },
      scene: null,
      camera: null,
      renderer: null,
      nowTime: Date.now()
    };
  },

  methods: {
    init() {
      return this.loadImage().then(texture => {

        let camera = new PerspectiveCamera(
          30,
          this.window.width / this.window.heigth,
          1,
          3000
        );

        camera.position.z = 6000;

        let scene = new Scene();

        let geometry = new Geometry();
        let textureLoader = new TextureLoader();

        texture.magFilter = LinearMipMapLinearFilter;
        texture.minFilter = LinearMipMapLinearFilter;

        let fog = new Fog(0x4584b4, -100, 3000);

        let material = new ShaderMaterial({
          uniforms: {
            'map': {
              type: 't',
              value: texture
            },
            'fogColor': {
              type: 'c',
              value: fog.color
            },
            'fogNear': {
              type: 'f',
              value: fog.near
            },
            'fogFar': {
              type: 'f',
              value: fog.far
            }
          },
          depthTest: false,
          transparent: true,
          vertexShader: `

            varying vec2 vUv;

            void main() {

            	vUv = uv;
            	gl_Position = projectionMatrix * modelViewMatrix * vec4( position, 1.0 );

            }

          `,
          fragmentShader: `

            uniform sampler2D map;

            uniform vec3 fogColor;
            uniform float fogNear;
            uniform float fogFar;

            varying vec2 vUv;

            void main() {

            	float depth = gl_FragCoord.z / gl_FragCoord.w;
            	float fogFactor = smoothstep( fogNear, fogFar, depth );

            	gl_FragColor = texture2D( map, vUv );
            	gl_FragColor.w *= pow( gl_FragCoord.z, 20.0 );
            	gl_FragColor = mix( gl_FragColor, vec4( fogColor, gl_FragColor.w ), fogFactor );

            }

          `
        });

        let plane = new Mesh(new PlaneGeometry(64, 64));

        for (let i = 0; i < 8000; i++) {
          plane.position.x = Math.random() * 1000 - 500;
          plane.position.y = -Math.random() * Math.random() * 200 - 15;
          plane.position.z = i;
          plane.rotation.z = Math.random() * Math.PI;
          plane.scale.x = plane.scale.y =
            Math.random() * Math.random() * 1.5 + 0.5;

          geometry.mergeMesh(plane);
        }

        let mesh;

        mesh = new Mesh(geometry, material);
        scene.add(mesh);

        mesh = new Mesh(geometry, material);
        mesh.position.z = -8000;
        scene.add(mesh);

        let renderer = new WebGLRenderer({
          antialias: false,
          alpha: true
        });
        renderer.setSize(this.window.width, this.window.heigth);
        renderer.setClearColor(0x000000, 0.0);

        this.scene = scene;
        this.camera = camera;
        this.renderer = renderer;

        return renderer.domElement;
      });
    },

    loadImage() {
      return new Promise((reslove, reject) => {
        let textureLoader = new TextureLoader();
        textureLoader.load(
          require('@/assets/images/cloud.png'),
          texture => reslove(texture),
          undefined,
          err => reject(err)
        );
      });
    },

    start() {
      (
        window.requestAnimationFrame ||
        window.webkitRequestAnimationFrame ||
        window.mozRequestAnimationFrame ||
        window.oRequestAnimationFrame ||
        window.msRequestAnimationFrame ||
        function(callback) {
          window.setTimeout(callback, 1000 / 60);
        }
      )(() => {
        this.render();
        this.start();
      });
    },

    render() {
      let position = ((new Date().getTime() - this.nowTime) * 0.03) % 8000;

      if (this.camera && this.renderer) {
        this.camera.position.z = -position + 8000;
        this.renderer.render(this.scene, this.camera);
      }
    },

    resize() {
      this.window.width = window.innerWidth;
      this.window.heigth = window.innerHeight;

      if (this.camera && this.render) {
        this.camera.aspect = this.window.width / this.window.heigth;
        this.camera.updateProjectionMatrix();
        this.renderer.setSize(this.window.width, this.window.heigth);
      }
    }
  },

  mounted() {
    this.window.width = window.innerWidth;
    this.window.heigth = window.innerHeight;

    window.addEventListener('resize', this.resize.bind(this));

    this.init().then(domElement => {
      this.$refs.$canvas.appendChild(domElement);
      this.$emit('ready');
      this.start();
    });
  },

  destroyed() {
    window.removeEventListener('resize', this.resize.bind(this));
  }
};
</script>

<style lang="scss" scoped>
.c-cloud {
  background: linear-gradient(#1e4877 0%, #4584b4 50%);
}
</style>