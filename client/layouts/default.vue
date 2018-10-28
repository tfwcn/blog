<template>
  <div class="c-layout">
    <header class="header">
      <div class="fixed">

        <nuxt-link tag="div" class="brand" to="/">
          <img class="logo" src="@/assets/images/logo.png" alt="HYH" />
          <img class="title" src="@/assets/images/title.png" alt="个人博客" />
        </nuxt-link>

        <nav class="nav">
          <ul>
            <nuxt-link
              v-for="(item, index) in navList"
              :key="item.path"
              :to="item.path"
              :class="{ active: currentNavItem.index === index }"
              tag="li"
              ref="navItem"
            >{{ item.name }}</nuxt-link>
          </ul>

          <div class="underline" :style="{
            width: currentNavItem.width + 'px',
            left: currentNavItem.left + 'px'
          }" />
        </nav>

        <form @submit.prevent="search" class="search">
          <input
            class="input"
            type="text"
            placeholder="请输入标题关键字"
            autocomplete="off"
            v-model="keyword"
          />
          <button type="submit" class="icon">
            <i class="g-icon icon-search" />
          </button>
        </form>

        <nuxt-link to="/admin/login" class="login">登录</nuxt-link>
      </div>
    </header>

    <main>
      <nuxt/>
    </main>

    <footer>
      <p>Copyright © {{ year }} HYH's Blog. All rights reserved.</p>
    </footer>
  </div>
</template>

<script>
export default {
  data() {
    return {
      year: new Date().getFullYear(),
      navList: [],
      currentNavItem: {
        index: 0,
        width: 0,
        left: 0
      },
      keyword: ''
    };
  },

  watch: {
    $route() {
      this.updateNavItem();
    }
  },

  methods: {
    search() {
      console.log(this.keyword);
    },
    updateNavItem() {
      let result = {};
      let path = this.$route.path;
      let current = this.navList.find(item => {
        return item.path >= path && new RegExp(`^${ item.path }`).test(path);
      });

      if (current) {
        let index = this.navList.indexOf(current);
        let $navItem = (this.$refs.navItem || [])[index];

        result = {
          index,
          width: $navItem ? $navItem.$el.clientWidth : 0,
          left: $navItem ? $navItem.$el.offsetLeft : 0
        };
      }

      return this.currentNavItem = result;
    }
  },

  mounted() {
    this.updateNavItem();
  },

  created() {
    this.$store.dispatch('getNavList').then(res => this.navList = res);
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
      padding: 0 30px;
      display: flex;
      align-items: center;
      background: #fff;
      box-sizing: border-box;
      box-shadow: 0 2px 6px rgba(0, 0, 0, 0.1);
      border-bottom: 1px solid rgba(0, 0, 0, 0.1);
    }

    .brand,
    .nav {
      line-height: 60px;
    }

    .brand {
      cursor: pointer;
      transition: filter 300ms linear;

      &:hover {
        filter: drop-shadow(1px 1px 3px #ccc);
      }

      .logo {
        height: 30px;
        margin-right: 5px;
      }

      .title {
        height: 26px;
      }
    }

    .nav {
      position: relative;
      height: 100%;
      margin-left: 30px;
      flex: 1;

      ul {
        height: 100%;

        li {
          height: 100%;
          display: inline-block;
          padding: 0 15px;
          margin: 0 5px;
          transition: all 100ms linear;
          color: #999;
          cursor: pointer;

          &.active {
            color: #000;
          }

          &:hover {
            background: #f3f3f3;
          }
        }
      }

      .underline {
        position: absolute;
        bottom: 0;
        height: 2px;
        background: #000;
        transition: all 100ms linear;
      }
    }

    .search {
      position: relative;
      margin: 0 15px;

      .input {
        width: 180px;
        height: 30px;
        border: 1px solid #ddd;
        border-radius: 15px;
        background: #efefef;
        box-sizing: border-box;
        padding: 0 30px 0 10px;
        outline: none;
        transition: background 100ms linear;

        &:focus {
          background: #fff;
        }
      }

      .icon {
        position: absolute;
        top: 0;
        right: 0;
        width: 30px;
        background: transparent;
        border: 0;
        line-height: 30px;
        text-align: center;
        cursor: pointer;
      }
    }
  }
}
</style>