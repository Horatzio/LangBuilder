import Vue from 'vue'
import vuetify from '@/plugins/vuetify'
import VueAnime from 'vue-animejs'

Vue.config.productionTip = false
Vue.use(VueAnime);

Vue.component('app-header', require('./components/Header.component.vue').default);

window.Vue = Vue;

new Vue({
    vuetify
}).$mount('#app');