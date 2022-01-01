import Vue from "vue"
import VueRouter from "vue-router"
import Home from "../pages/Home.vue"
import Login from "../pages/Login.vue"
import Servers from "../pages/configuration/Servers"
import Logs from "../pages/security/Logs"
import Users from "../pages/security/Users"
import Roles from "../pages/security/Roles"
import Phones from "../pages/devices/Phones"
import CurrentTasks from "../pages/tasks/CurrentTasks"
import HistoryTasks from "../pages/tasks/HistoryTasks"
import store from "../store"
import config from "@/config"

Vue.use(VueRouter)

const routes = [
  {
    path: "/",
    name: "home",
    component: Home,
    meta: { requiresAuth: true },
  },
  {
    path: "/login",
    name: "login",
    component: Login,
    meta: {
      hideForAuth: true,
    },
  },
  {
    path: "/devices/phones",
    name: "devices.phones",
    component: Phones,
    meta: { requiresAuth: true },
  },
  {
    path: "/configuration/servers",
    name: "configuration.servers",
    component: Servers,
    meta: { requiresAuth: true },
  },
  {
    path: "/security/logs",
    name: "security.logs",
    component: Logs,
    meta: { requiresAuth: true },
  },
  {
    path: "/security/users",
    name: "security.users",
    component: Users,
    meta: { requiresAuth: true },
  },
  {
    path: "/security/roles",
    name: "security.roles",
    component: Roles,
    meta: { requiresAuth: true },
  },
  {
    path: "/tasks/current",
    name: "tasks.current",
    component: CurrentTasks,
    meta: { requiresAuth: true },
  },
  {
    path: "/tasks/history",
    name: "tasks.history",
    component: HistoryTasks,
    meta: { requiresAuth: true },
  },
]

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
})

router.afterEach((to) => {
  Vue.nextTick(() => {
    document.title = to.meta.title || config.DEFAULT_TITLE
  })
})

router.beforeEach((to, from, next) => {
  let isAuth = store.getters.IS_AUTH
  if (to.matched.some((record) => record.meta.requiresAuth)) {
    // this route requires auth, check if logged in
    // if not, redirect to login page.
    if (!isAuth) {
      next({
        path: "/login",
        query: { redirect: to.fullPath },
      })
    } else {
      next()
    }
  } else if (to.matched.some((record) => record.meta.hideForAuth)) {
    if (!isAuth) {
      next()
    } else {
      next({
        path: "/",
        query: { redirect: to.fullPath },
      })
    }
  } else {
    next() // make sure to always call next()!
  }
})

export default router
