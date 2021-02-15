import Vue from 'vue'
import vuetify from '@/plugins/vuetify'

Vue.config.productionTip = false

Vue.component('hello-world', require('./components/HelloWorld.vue').default);

window.Vue = Vue;

new Vue({
    vuetify
}).$mount('#app');