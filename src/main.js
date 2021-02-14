import Vue from 'vue'

Vue.config.productionTip = false

Vue.component('hello-world', require('./components/HelloWorld.vue').default);

window.Vue = Vue;