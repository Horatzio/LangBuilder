import Vue from 'vue'
import vuetify from '@/plugins/vuetify'
import VueAnime from 'vue-animejs'

Vue.config.productionTip = false
Vue.use(VueAnime);

Vue.component('hello-world', require('./components/HelloWorld.vue').default);
Vue.component('test-list', require('./components/TestList.component.vue').default);

window.Vue = Vue;

new Vue({
    vuetify
}).$mount('#app');