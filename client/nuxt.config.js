const pkg = require('./package');

module.exports = {
  mode: 'universal',

  /*
  ** Headers of the page
  */
  head: {
    titleTemplate: '%s | HYH\'s Blog',
    meta: [{
      charset: 'utf-8'
    }, {
      name: 'render',
      content: 'webkit'
    }, {
      'http-equiv': 'X-UA-Compatible',
      content: 'IE=edge,chrome=1'
    }, {
      name: 'viewport',
      content: 'width=device-width, initial-scale=1, minimum-scale=1, shrink-to-fit=no'
    }, {
      hid: 'description',
      name: 'description',
      content: pkg.description
    }],
    link: [{
      rel: 'icon',
      type: 'image/x-icon',
      href: '/favicon.ico'
    }]
  },

  /*
  ** Customize the progress-bar color
  */
  loading: {
    color: '#41b883'
  },

  /*
  ** Global CSS
  */
  css: [{
    src: '@/assets/scss/reset.scss',
    lang: 'scss'
  }, {
    src: '@/assets/scss/preset.scss',
    lang: 'scss'
  }, {
    src: '@/assets/font/iconfont.scss',
    lang: 'scss'
  }],

  /*
  ** Plugins to load before mounting the App
  */
  plugins: ['@/plugins/element-ui'],

  /*
  ** Nuxt.js modules
  */
  modules: [
    // Doc: https://github.com/nuxt-community/axios-module#usage
    '@nuxtjs/axios'
  ],
  /*
  ** Axios module configuration
  */
  axios: {
    // See https://github.com/nuxt-community/axios-module#options
  },

  /*
  ** Build configuration
  */
  build: {
    /*
    ** You can extend webpack config here
    */
    extend(config, ctx) {
      // Run ESLint on save
      if (ctx.isDev && ctx.isClient) {
        config.module.rules.push({
          enforce: 'pre',
          test: /\.(js|vue)$/,
          loader: 'eslint-loader',
          exclude: /(node_modules)/
        });
      }
    }
  },

  server: {
    port: 3000,
    host: '0.0.0.0'
  }
};