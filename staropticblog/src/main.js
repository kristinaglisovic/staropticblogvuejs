import "bootstrap/dist/css/bootstrap.css"
import "bootstrap/dist/js/bootstrap.js"
import "./axios"
import Vuelidate from 'vuelidate'
import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store/vuex'
import vuetify from './plugins/vuetify'
import ScrollAnimation from './directives/scrollanimation'
import Carousel3d  from "vue-carousel-3d"

import moment from 'moment';

import Paginate from 'vuejs-paginate'
Vue.component('paginate', Paginate)

Vue.filter('formatDate', function(value) {
    if (value) {
        return moment(String(value)).format('DD/MM/YYYY')
    }
});

Vue.filter('formatDate2', function(value) {
  if (value) {
      return moment(String(value)).format('DD/MM/YYYY hh:mm')
  }
});


Vue.use(Vuelidate)
Vue.use(Carousel3d)
Vue.directive('scrollanimation',ScrollAnimation);

Vue.config.productionTip = false

store.dispatch('auth/attempt',localStorage.getItem('token')).then(()=>{
  new Vue({
    router,
    store,
    vuetify,
    render: h => h(App)
  }).$mount('#app')
})


