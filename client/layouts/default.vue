<template>
  <div class="c-layout">
    <header class="header">
      <div class="fixed">

        <div class="brand">
          <img
            class="logo"
            src="@/assets/images/logo.png"
          >
          <img
            class="title"
            src="@/assets/images/title.png"
          >
        </div>

        <nav class="nav">
          <ul>
            <li
              v-for="item in nav"
              :key="item.path"
            >{{ item.name }}</li>
          </ul>
        </nav>

        <div class="search">
          <input
            class="input"
            type="text"
            placeholder="请输入标题关键字"
            autocomplete="off"
          >
        </div>

        <div class="login">登录</div>
      </div>
    </header>

    <main>
      <nuxt/>
    </main>

    <footer>
      <p>Copyright © 2018 HYH's Blog. All rights reserved.</p>
    </footer>
  </div>
</template>

<script>
export default {
  data() {
    return {
      nav: [
        {
          name: '全部',
          path: '/articles'
        },
        {
          name: '前端开发',
          path: '/articles/frontend'
        },
        {
          name: 'Node.js开发',
          path: '/articles/node'
        },
        {
          name: '其他开发',
          path: '/articles/other'
        },
        {
          name: '工作生活',
          path: '/articles/life'
        }
      ]
    };
  }
};
</script>

<style lang="scss" scoped>
.c-layout {
  background: #f8f8f8;

  .header {
    height: 60px;

    .fixed {
      position: fixed;
      top: 0;
      left: 0;
      width: 100%;
      height: 60px;
      line-height: 60px;
      padding: 0 30px;
      background: #fff;
      box-sizing: border-box;
      box-shadow: 0 2px 6px rgba(0, 0, 0, 0.1);
      border-bottom: 1px solid rgba(0, 0, 0, 0.1);
    }

    .brand {
      display: inline-block;

      .logo {
        height: 40px;
        margin-right: 10px;
      }

      .title {
        height: 40px;
      }
    }

    .nav,
    .search {
      margin-left: 40px;
    }

    .nav {
      height: 100%;
      display: inline-block;

      ul {
        height: 100%;

        li {
          height: 100%;
          display: inline-block;
          padding: 0 15px;
        }
      }
    }

    .search {
      position: relative;
      height: 100%;
      display: inline-block;

      .input {
        width: 180px;
        height: 30px;
        border: 1px solid #ddd;
        border-radius: 15px;
        background: #fff;
        box-sizing: border-box;
        padding: 0 26px 0 10px;
        outline: none;
      }
    }

    .login {
      float: right;
    }
  }
}
</style>
