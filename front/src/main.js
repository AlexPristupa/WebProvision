import 'babel-polyfill'  
import Vue from "vue"
import App from "./App.vue"
import router from "./router"
import store from "./store"
import vuetify from "@/plugins/vuetify"
import { VueMaskDirective } from 'v-mask'

Vue.directive('mask', VueMaskDirective);

Vue.config.productionTip = false


new Vue({
  vuetify: vuetify,
  router: router,
  store: store,
  render: (h) => h(App),
}).$mount("#app")
