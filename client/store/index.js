import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

export default () => new Vuex.Store({
  state: {
    navList: [{
      name: '全部',
      path: '/articles'
    }, {
      name: '前端开发',
      path: '/articles/frontend'
    }, {
      name: 'Node.js开发',
      path: '/articles/node'
    }, {
      name: '其他开发',
      path: '/articles/other'
    }, {
      name: '工作生活',
      path: '/articles/life'
    }]
  },

  actions: {
    getNavList({ state }) {
      if (state.navList) {
        return state.navList;
      } else {
        return this.$axios.get('/api/note_type/get_list').then(res => {
          state.navList = res;
          return res;
        });
      }
    }
  }
});