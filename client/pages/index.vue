<template>
  <div class="p-home">
    <div class="logo">
      <div class="left item"></div>
      <div class="middle item"></div>
      <div class="right item"></div>
    </div>

    <div class="title">
      <img src="@/assets/images/title.png" alt="个人博客" />
    </div>

    <div class="buttons">
      <nuxt-link class="left button" to="/cv">简历</nuxt-link>
      <nuxt-link class="middle button" to="/articles">进入</nuxt-link>
      <a class="right button" href="https://github.com/hanyonghao" target="_blank">
        <i class="g-icon icon-github" />
        <span>GITHUB</span>
      </a>
    </div>

    <footer class="footer">
      <p>Copyright © {{ year }} HYH's Blog. All rights reserved.</p>
    </footer>
  </div>
</template>

<script>

export default {
  layout: 'index',

  head() {
    return {
      title: '首页'
    };
  },

  data() {
    return {
      navList: []
    };
  }
};
</script>

<style lang="scss" scoped>
.p-home {
  position: relative;
  min-height: 100vh;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  background: #f8f8f8;
  padding-bottom: 30px;
  box-sizing: border-box;

  .logo {
    width: 176px;
    height: 82px;
    display: flex;
    justify-content: space-between;

    .item {
      width: 46px;
      height: 100%;
      background-size: auto 100%;
      background-repeat: no-repeat;
    }

    .left {
      background-image: url(~assets/images/logo.png);
      background-position: 0 0;
      animation: logo 1500ms infinite;
    }

    .middle {
      background-image: url(~assets/images/logo.png);
      background-position: 50% 0;
      animation: logo 1500ms infinite 400ms;
    }

    .right {
      background-image: url(~assets/images/logo.png);
      background-position: 100% 0;
      animation: logo 1500ms infinite 800ms;
    }
  }

  .title {
    width: 250px;
    margin-top: 30px;

    img {
      width: 100%;
    }
  }

  .buttons {
    margin-top: 60px;

    .button {
      display: inline-block;
      padding: 15px 35px;
      margin: 0 15px;
      border-radius: 22px;
      font-weight: bold;
      cursor: pointer;
      transition: all 100ms linear;

      &:hover {
        box-shadow: 0 0 6px #aaa;
      }
    }

    .left,
    .right {
      background: #fff;
      border: 1px solid #aaa;
      color: #222;
    }

    .middle {
      background: #41b883;
      border: 1px solid #36966b;
      color: #fff;
    }

    .right {
      position: relative;
      padding-left: 45px;
      padding-right: 25px;

      i {
        position: absolute;
        top: 50%;
        left: 8px;
        transform: translateY(-50%);
        font-size: 28px;
      }
    }
  }

  .footer {
    position: absolute;
    left: 0;
    bottom: 0;
    width: 100%;
    line-height: 30px;
    text-align: center;
    font-size: 12px;
    color: #aaa;
  }

  @keyframes logo {
    0% {
      transform: translateY(0px);
    }
    50% {
      transform: translateY(-30px);
    }
    100% {
      transform: translateY(0px);
    }
  }
}
</style>