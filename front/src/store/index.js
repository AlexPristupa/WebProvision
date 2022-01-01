import config from "@/config";
import Vue from "vue";
import Vuex from "vuex";
import axios from "axios";
import { pages } from "@/public/data"; // временное хранилище данных
import {
  logTypes,
  logs,
  models,
  vendors,
  nodes,
  objects,
  permissions,
} from "../public/data";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    pages: [],
    servers: [],
    servers2: [],
    vendors: [],
    models: [],
    logs: [],
    logTypes: [],
    users: { items: [], totalCount: null },
    roleUsers: { items: [], totalCount: null },
    roles: { items: [], totalCount: null },
    phones: [],
    tasksPool: { items: [], totalCount: null },
    tasksList: [],
    taskCurrent: {},
    taskDone: {},
    history: { items: [], totalCount: null },
    aboutProduct: {},
    token: localStorage.getItem("Token") || "",
    user: { username: localStorage.getItem("login") || "" } ,
    nodes: [],
    objects: [],
    permissions: [],
  },
  mutations: {
    SET_TOKEN: (state, token) => {
      state.token = token;
    },
    SET_USER: (state, user) => {
      state.user = user;
    },
    SET_PAGES: (state, pages) => {
      state.pages = pages;
    },
    SET_SERVERS: (state, servers) => {
      state.servers = servers;
    },
    SET_SERVERS2: (state, servers) => {
      state.servers2 = servers;
    },
    SET_NODES: (state, nodes) => {
      state.nodes = nodes;
    },
    UPDATE_NODE: (state, payload) => {
      // Меняет приоритет каждой ноды на индекс в массиве +1
      Object.assign(state.nodes[payload.index], {
        ...payload.item,
        priority: payload.index + 1,
      });
    },
    SET_VENDORS: (state, vendors) => {
      state.vendors = vendors;
    },
    SET_MODELS: (state, models) => {
      state.models = models;
    },
    SET_LOGS: (state, logs) => {
      state.logs = logs;
    },
    SET_LOG_TYPES: (state, logTypes) => {
      state.logTypes = logTypes;
    },
    SET_USERS: (state, users) => {
      state.users = users;
    },
    SET_ROLE_USERS: (state, roleUsers) => {
      state.roleUsers = roleUsers;
    },
    SET_ROLES: (state, roles) => {
      state.roles = roles;
    },
    SET_PHONES: (state, phones) => {
      state.phones = phones;
    },
    SET_TASKS_POOL: (state, tasks) => {
      state.tasksPool = tasks;
    },
    SET_TASKS_LIST: (state, items) => {
      state.tasksList = items.items;
    },
    SET_TASK_CURRENT: (state, items) => {
      state.taskCurrent = items;
    },
    SET_TASK_DONE: (state, items) => {
      state.taskDone = items;
    },
    SET_COMPLETED_TASKS: (state, tasks) => {
      state.history = tasks;
    },
    SET_ABOUT_PRODUCT: (state, product) => {
      state.aboutProduct = product;
    },
    SET_ABOUT_PRODUCT_LICENCE: (state, licence) => {
      state.aboutProduct.serialNumber = licence;
    },
    SET_OBJECTS: (state, objects) => {
      state.objects = objects;
    },
    SET_PERMISSIONS: (state, permissions) => {
      state.permissions = permissions;
    },
    // SERVER
    ADD_SERVER: (state, formData) => {
      state.servers = [...state.servers, formData];
    },
    CHANGE_SERVER: (state, server) => {
      state.servers = state.servers.map((item) => {
        if (item.idr === server.idr) {
          return { ...item, ...server };
        } else {
          return item;
        }
      });
    },
    REMOVE_SERVER: (state, server) => {
      state.servers = state.servers.filter((item) => item.idr !== server.idr);
    },
    // NODE
    ADD_NODE: (state, formData) => {
      state.nodes = [...state.nodes, formData];
    },
    CHANGE_NODE: (state, node) => {
      state.nodes = state.nodes.map((item) => {
        if (item.idr === node.idr) {
          return { ...item, ...node };
        } else {
          return item;
        }
      });
    },
    REMOVE_NODE: (state, node) => {
      state.nodes = state.nodes.filter((item) => item.idr !== node.idr);
    },
    // USER
    ADD_USER: (state, formData) => {
      state.users = [...state.users, formData];
    },
    CHANGE_USER: (state, user) => {
      state.users = state.users.map((item) => {
        if (item.idr === user.idr) {
          return { ...item, ...user };
        } else {
          return item;
        }
      });
    },
    REMOVE_USER: (state, user) => {
      state.users = state.users.filter((item) => item.idr !== user.idr);
    },
    // ROLE
    ADD_ROLE: (state, formData) => {
      state.roles = [...state.roles, formData];
    },
    CHANGE_ROLE: (state, role) => {
      state.roles = state.roles.map((item) => {
        if (item.idr === role.idr) {
          return { ...item, ...role };
        } else {
          return item;
        }
      });
    },
    REMOVE_ROLE: (state, role) => {
      state.roles = state.roles.filter((item) => item.idr !== role.idr);
    },
    // PHONE
    ADD_PHONE: (state, formData) => {
      state.phones = [...state.phones, formData];
    },
    CHANGE_PHONE: (state, phone) => {
      state.phones = state.phones.map((item) => {
        if (item.idr === phone.idr) {
          return { ...item, ...phone };
        } else {
          return item;
        }
      });
    },
    REMOVE_PHONE: (state, phone) => {
      state.phones = state.phones.filter((item) => item.idr !== phone.idr);
    },
    // TASK_POOL
    ADD_TASK_IN_POOL: (state, formData) => {
      state.tasksPool = [...state.tasksPool, formData];
    },
    CHANGE_TASK_IN_POOL: (state, task) => {
      state.tasksPool = state.tasksPool.map((item) => {
        if (item.idr === task.idr) {
          return { ...item, ...task };
        } else {
          return item;
        }
      });
    },
    REMOVE_TASK_IN_POOL: (state, task) => {
      state.tasksPool = state.tasksPool.filter((item) => item.idr !== task.idr);
    },
  },
  actions: {
    LOGIN(ctx, formData) {
      const axiosConfig = {};
      // const data = { ...formData } // body не нужен т.к логин и пароль передается в queryParams
      console.log("Form-data", formData);
      return new Promise((resolve, reject) => {
        console.log(formData)
        axios
          .post(
            `${config.baseUrl}/api/token`, formData,
            axiosConfig
          )
          .then((res) => {
            if (res.status == 200) {
              localStorage.setItem("Token", res.data.accessToken);
              localStorage.setItem("login", formData.login);
              ctx.commit("SET_TOKEN", res.data.accessToken);
              ctx.commit("SET_USER", { username: formData.login });
              ctx.dispatch("CHECK_IS_VALID_TOKEN"); // отправляем запрос на проверку токена
              resolve(res);
              window.location.href = ""; // редирект на текущую страницу(чтобы vue-router сработал)
            } else if (res.status == 401) {
              ctx.dispatch("LOGOUT"); // делаем logout
              reject(res);
            } else {
              reject(res);
            }
          })
          .catch((res) => reject(res));
      });
    },
    async CHECK_IS_VALID_TOKEN(ctx) {
      const token = localStorage.getItem("Token");
      const data = {};
      const axiosConfig = {
        headers: {
          Authorization: "Bearer " + token,
        },
      };
      return new Promise((resolve, reject) => {
        axios
          .post(`${config.baseUrl}/api/Token`, data, axiosConfig)
          .then((res) => {
            console.log("response", res);
            if (res.status == 200) {
              ctx.commit("SET_USER", { username: res.data.userName });
              resolve(res);
            } else {
              // с помощью .catch можно обработать полученные ошибки
              reject(res);
            }
          });
      });
    },
    LOGOUT(ctx) {
      localStorage.removeItem("Token");
      localStorage.removeItem("User");
      ctx.commit("SET_TOKEN", "");
    },
    async FETCH_PAGES(ctx, parentId) {
      return new Promise((resolve) => {
        /* eslint-disable */
        // axios.get(`${config.baseUrl}/api/Pages").then((res) => {
        //   if (res.status == 200) {
        //     const pages = res.data
        //     ctx.commit("SET_PAGES", pages)
        //     resolve(res)
        //   } else {
        //     reject(res)
        //   }
        // })
        const parentPages = pages.filter((item) => item.ParentId === parentId);
        ctx.commit("SET_PAGES", parentPages);
        resolve();
      });
    },
    async FETCH_SERVERS(ctx, { limit, offset, search, orderedColumn, sortDesc }) {
      return new Promise((resolve, reject) => {
        const reqArr = [];
        if (sortDesc !== undefined) reqArr.push(`orderDesc=${sortDesc}`)
        if (orderedColumn) reqArr.push(`tableColumn=${orderedColumn}`)
        if (offset || offset === 0 ) reqArr.push(`offset=${offset}`)
        if (limit) reqArr.push(`limit=${limit}`)
        if (search) reqArr.push(`search=${search}`)
        const string = reqArr.join('&');
        axios({
          method: 'get',
          url: `${config.baseUrl}/api/servers/selection?${string}`,
          headers: {
            Authorization: `Bearer ${this.state.token}`,
          }
        }).then((res) => {
            if (res.status == 200) {
              let stringForTotal = '';
              if (search && search !== '') stringForTotal = `?search=${search}`
              if (orderedColumn) reqArr.push(`tableColumn=${orderedColumn}`)
              axios({
                method: 'get',
                url: `${config.baseUrl}/api/servers/totalCount${stringForTotal}`,
                headers: {
                  Authorization: `Bearer ${this.state.token}`,
                }
              }).then((responce) => {
                  const totalCount = responce.data.meta.count;
                  const result = {
                    items: res.data,
                    totalCount: totalCount,
                  }
                  console.log(result)
                  ctx.commit("SET_SERVERS", result);
                  resolve(result);
                });
            } else {
              const result = {
                items: [],
                totalCount: 0,
              }
              ctx.commit("SET_SERVERS", result);
              resolve(result);
            }
          }).catch((e)=>{
            reject(e);
          });
        // axios.get(`${config.baseUrl}/servers`).then((res) => {
        //   if (res.status == 200) {
        //     const servers = res.data
        //     ctx.commit("SET_SERVERS", servers)
        //     resolve(res)
        //   } else {
        //     reject(res)
        //   }
        // })
        // console.log(offset, limit) // чтобы linter не ругался
      });
    },
    async FETCH_SERVERS2(ctx, { limit, offset, search, orderedColumn, sortDesc }) {
      return new Promise((resolve, reject) => {
        const reqArr = [];
        if (sortDesc !== undefined) reqArr.push(`orderDesc=${sortDesc}`)
        if (orderedColumn) reqArr.push(`tableColumn=${orderedColumn}`)
        if (offset || offset === 0 ) reqArr.push(`offset=${offset}`)
        if (limit) reqArr.push(`limit=${limit}`)
        if (search) reqArr.push(`search=${search}`)
        const string = reqArr.join('&');
        axios({
          method: 'get',
          url: `${config.baseUrl}/api/servers/selection?${string}`,
          headers: {
            Authorization: `Bearer ${this.state.token}`,
          }
        }).then((res) => {
            if (res.status == 200) {
              let stringForTotal = '';
              if (search && search !== '') stringForTotal = `?search=${search}`
              if (orderedColumn) reqArr.push(`tableColumn=${orderedColumn}`)
              axios({
                method: 'get',
                url: `${config.baseUrl}/api/servers/totalCount${stringForTotal}`,
                headers: {
                  Authorization: `Bearer ${this.state.token}`,
                }
              }).then((responce) => {
                  const totalCount = responce.data.meta.count;
                  const result = {
                    items: res.data,
                    totalCount: totalCount,
                  }
                  console.log(result)
                  ctx.commit("SET_SERVERS2", result);
                  resolve(result);
                });
            } else {
              const result = {
                items: [],
                totalCount: 0,
              }
              ctx.commit("SET_SERVERS2", result);
              resolve(result);
            }
          }).catch((e)=>{
            reject(e);
          });
        // axios.get(`${config.baseUrl}/servers`).then((res) => {
        //   if (res.status == 200) {
        //     const servers = res.data
        //     ctx.commit("SET_SERVERS", servers)
        //     resolve(res)
        //   } else {
        //     reject(res)
        //   }
        // })
        // console.log(offset, limit) // чтобы linter не ругался
      });
    },
    async FETCH_NODES(ctx, { serverId }) {
      // await axios
      //   .get(`${config.baseUrl}/nodes?serverId=${serverId}`)
      //   .then((res) => {
      //     const nodes = res.data
      //     ctx.commit("SET_NODES", nodes)
      //   })
      const filteredNodes = nodes.filter((item) => item.serverId === serverId);
      ctx.commit("SET_NODES", filteredNodes);
    },
    async FETCH_VENDORS(ctx) {
      // await axios
      //   .get(`${config.baseUrl}/api/Users?offset=0&limit=1001`)
      //   .then((res) => {
      //     const vendors = res.data
      //     ctx.commit("SET_VENDORS", vendors)
      //   })
      ctx.commit("SET_VENDORS", vendors);
    },
    async FETCH_MODELS(ctx) {
      // await axios.get(`${config.baseUrl}/models").then((res) => {
      //   const models = res.data
      //   ctx.commit("SET_MODELS", models)
      // })
      ctx.commit("SET_MODELS", models);
    },
    async FETCH_LOGS(ctx) {
      // await axios.get(`${config.baseUrl}/logs").then((res) => {
      //   const logs = res.data
      //   ctx.commit("SET_LOGS", logs)
      // })
      ctx.commit("SET_LOGS", { items: logs, totalCount: logs.length });
    },
    async FETCH_LOG_TYPES(ctx) {
      // await axios.get(`${config.baseUrl}/log-types").then((res) => {
      //   const logTypes = res.data
      //   ctx.commit("SET_LOG_TYPES", logTypes)
      // })
      ctx.commit("SET_LOG_TYPES", logTypes);
    },
    async FETCH_USERS(ctx, { limit, offset, search, orderedColumn, sortDesc }) {
      return new Promise((resolve, reject) => {
        const reqArr = [];
        if (limit) reqArr.push(`Limit=${limit}`)
        if (offset) reqArr.push(`Offset=${offset}`)
        if (search) reqArr.push(`Search=${search}`)
        if (orderedColumn) reqArr.push(`OrderedColumn=${orderedColumn}`)
        if (sortDesc !== undefined) reqArr.push(`OrderDesc=${sortDesc}`)
        const string = reqArr.join('&');
        axios
          .get(`${config.baseUrl}/api/Users/selection?${string}`)
          .then((res) => {
            if (res.status == 200) {
              let stringForTotal = '';
              if (search && search !== '') stringForTotal = `?Search=${search}`
              axios
                .get(`${config.baseUrl}/api/Users/totalCount${stringForTotal}`)
                .then((responce) => {
                  const totalCount = responce.data;
                  ctx.commit("SET_USERS", {
                    items: res.data,
                    totalCount: totalCount,
                  });
                  resolve(res);
                });
            } else {
              reject(res);
            }
          });
      });
    },
    async FETCH_USERS_WITHOUT_ROLE(
      ctx,
      { limit, offset, filteredColumns, orderedDesc, roleName }
    ) {
      const columnData = [{ roleName: roleName }];
      return new Promise((resolve, reject) => {
        axios
          .post(
            `${
              config.baseUrl
            }/api/Users?Limit=${limit}&Offset=${offset}&FiltersColumn=${filteredColumns ||
              null}&OrderDesc=${orderedDesc || false}`
          )
          .then((res) => {
            if (res.status == 200) {
              ctx.commit("SET_USERS", {
                items: res.data.users,
              });
              alert(res.data);
              resolve(res);
            } else {
              reject(res);
            }
          });
      });
    },
    async FETCH_USERS_BY_ROLE(ctx, { limit, offset, roleName, orderedDesc }) {
      const columnsData = JSON.stringify([{ login: "admin" }]);
      const body = { ff: "ff" };
      const reqConfig = { headers: { "Content-Type": "application/json" } };
      await axios
        .get(
          `${
            config.baseUrl
          }/api/Users?Limit=${limit}&Offset=${offset}&OrderedColumn=Login&OrderDesc=${orderedDesc ||
            true}&Search=admin`,
          body,
          reqConfig
        )
        .then((res) => {
          const users = res.data.users;
          const totalCount = res.data.totalCount;
          ctx.commit("SET_ROLE_USERS", {
            items: users,
            totalCount: totalCount,
          });
        })
        .catch(() => {
          ctx.commit("SET_ROLE_USERS", { items: [] });
        });
    },
    async FETCH_ROLES(ctx, { limit, offset, search, orderedColumn, sortDesc }) {
      const reqArr = [];
      if (limit) reqArr.push(`Limit=${limit}`)
      if (offset) reqArr.push(`Offset=${offset}`)
      if (search) reqArr.push(`Search=${search}`)
      if (orderedColumn) reqArr.push(`OrderedColumn=${orderedColumn}`)
      if (sortDesc !== undefined) reqArr.push(`OrderDesc=${sortDesc}`)
      const string = reqArr.join('&');
      await axios
        .get(`${config.baseUrl}/api/Roles/selection?${string}`)
        .then((res) => {
          console.log(res)
          const roles = res.data;
          ctx.commit("SET_ROLES", { items: roles });
        });
    },
    async FETCH_PHONES(ctx, { limit, offset, search, orderedColumn, sortDesc }) {
      return new Promise((resolve, reject) => {
        const reqArr = [];
        if (limit) reqArr.push(`limit=${limit}`)
        if (offset || offset === 0) reqArr.push(`offset=${offset}`)
        if (search) reqArr.push(`search=${search}`)
        if (orderedColumn) reqArr.push(`tableColumn=${orderedColumn}`)
        if (sortDesc !== undefined) reqArr.push(`orderDesc=${sortDesc}`)
        const string = reqArr.join('&');
        axios
          .get(
            `${config.baseUrl}/api/Devices/selection?${string}`
          )
          .then((res) => {
            if (res.status == 200) {
              const phones = res.data;
              let stringForTotal = '';
              if (search && search !== '') stringForTotal = `?Search=${search}`
              axios
                .get(`${config.baseUrl}/api/Devices/totalCount${stringForTotal}`)
                .then((res) => {
                  const totalCount = res.data;
                  const result = {
                    items: phones.devices,
                    totalCount: totalCount,
                  }
                  ctx.commit("SET_PHONES", result);
                  resolve(result);
                });
            }
          }).catch((e) =>{
            console.log(e)
            reject(e);
          });
      });
    },
    async DEVICE_ACTIONS(ctx, data) {
      console.log('store0')
      return new Promise((resolve, reject) => {
        console.log('store1')
        axios({
          method: 'post',
          url: `${config.baseUrl}/api/tasks`,
          data,
          headers: {
            Authorization: `Bearer ${this.state.token}`,
          }
        }).then((res) => {
          console.log('res')
              console.log(res)
              resolve(res);
          }).catch((e) => {
            console.log(e.response.data)
            reject(e);
          });
      });
    },

    async FETCH_TASKS_LISTS(ctx, data) {
      return new Promise((resolve, reject) => {
        axios({
          method: 'get',
          url:  `${config.baseUrl}/api/TasksLists`,
          headers: {
            Authorization: `Bearer ${this.state.token}`,
          }
        }).then((res) => {
            console.log(res)
            if (res.status == 200) {
              console.log(res.data.data)
              ctx.commit("SET_TASKS_LIST", {
                items: res.data.data,
              });
              resolve(res);
            } else {
              reject(res);
            }
          });
      });
    },

    async FETCH_TASK(ctx, data) {
      return new Promise((resolve, reject) => {
        axios({
          method: 'get',
          url:   `${config.baseUrl}/api/tasks/detailTask?taskId=${data.taskNumber}&taskStatus=${data.type}`,
          headers: {
            Authorization: `Bearer ${this.state.token}`,
          }
        }).then((res) => {
            console.log(res.data)
            if (res.data.length === 0) {
              throw Error('empty response');
            }
            if (data.tupe === 'current') {
              ctx.commit("SET_TASK_CURRENT", res.data);
            } else if (data.tupe === 'done') {
              ctx.commit("SET_TASK_DONE", res.data);
            }
            resolve(res.data);
          }).catch((e) => {
            console.dir(e)
            reject(e);
          })
      });
    },


    async FETCH_CURRENT_TASKS(ctx, { limit, offset }) {
      return new Promise((resolve, reject) => {
        axios
          .get(`${config.baseUrl}/api/Tasks?limit=${limit}&offset=${offset}`)
          .then((res) => {

            if (res.status == 200) {
              ctx.commit("SET_TASKS_POOL", {
                items: res.data.tasks,
                totalCount: res.data.totalCount,
              });
              resolve(res);
            } else {
              reject(res);
            }
          });
      });
    },
    async FETCH_CURRENT_TASKS_SELECTION(ctx, { limit, offset, search, orderedColumn, orderDesc, tableColumn }) {
      return new Promise((resolve, reject) => {
        const reqArr = [];
        //console.log(orderedColumn)
        if (limit) reqArr.push(`limit=${limit}`)
        if (offset || offset ===0) reqArr.push(`offset=${offset}`)
        if (search) reqArr.push(`search=${search}`)
        if (tableColumn) reqArr.push(`tableColumn=${tableColumn}`)
        if (orderedColumn) reqArr.push(`tableColumn=${orderedColumn}`)
        if (orderDesc !== undefined) reqArr.push(`orderDesc=${orderDesc}`)
        const string = reqArr.join('&');
        //console.log(string)
        axios({
          method: 'get',
          url: `${config.baseUrl}/api/tasks/selection?${string}`,
          headers: {
             Authorization: `Bearer ${this.state.token}`,
          }
        }).then((res) => {
          //console.log(res)
          let totalString = '';
          if (search && orderedColumn) {
            totalString = `?orderedColumn=${orderedColumn}&search=${search}`
          } else if (search) {
            totalString = `?search=${search}`
          }
          console.log(totalString)
            axios({
              method: 'get',
              url: `${config.baseUrl}/api/tasks/totalCount${totalString}`,
              headers: {
                 Authorization: `Bearer ${this.state.token}`,
              }
            }).then((responce) => {
              //console.log(responce)
              //console.log(res.data.tasks)
              //console.log(responce.data.meta.count)
              ctx.commit("SET_TASKS_POOL", {
                items: res.data.tasks,
                totalCount: responce.data.meta.count,
              });
              resolve({
                items: res.data.tasks,
                totalCount: responce.data.meta.count,
              });
            }).catch((e) => {
              reject(e);
            })

        });
      });
    },
    FETCH_COMPLETED_TASKS(ctx, { limit, offset, search, orderedColumn, orderDesc}) {
      return new Promise((resolve, reject) => {
        const reqArr = [];
        if (limit) reqArr.push(`limit=${limit}`)
        if (offset || offset === 0) reqArr.push(`offset=${offset}`)
        if (search) reqArr.push(`search=${search}`)
        if (orderedColumn) reqArr.push(`tableColumn=${orderedColumn}`)
        if (orderDesc !== undefined) reqArr.push(`orderDesc=${orderDesc}`)
        const string = reqArr.join('&');
        axios
          .get(`${config.baseUrl}/api/tasks/completed/selection?${string}`)
          .then((res) => {
            if (res.status == 200) {
              let stringForTotal = [];
              if (search && search !== '') stringForTotal.push(`search=${search}`)
              if (search && search !== '' && orderedColumn) stringForTotal.push(`tableColumn=${orderedColumn}`)
              axios
              .get(`${config.baseUrl}/api/tasks/completed/totalCount?${stringForTotal.join('&')}`)
              .then((responce) => {
                console.log(responce)
                const totalCount = responce.data.meta.count;
                console.log(res.data.tasks)
                ctx.commit("SET_COMPLETED_TASKS", {
                  items: res.data.tasks,
                  totalCount: totalCount,
                });
                resolve({
                  items: res.data.tasks,
                  totalCount: totalCount,
                });
              });
            } else if (res.status == 204) {
              ctx.commit("SET_COMPLETED_TASKS", {
                items: [],
                totalCount: 0,
              });
              resolve({
                items: [],
                totalCount: 0,
              });
            } else {
              reject(res);
            }
          });
      });
    },
    CANCEL_COMPLETED_ACTION(ctx, data) {
      return new Promise((resolve, reject) => {
        console.log('store1')
        axios({
          method: 'post',
          url: `${config.baseUrl}/api/tasks/return`,
          data,
          headers: {
            Authorization: `Bearer ${this.state.token}`,
          }
        }).then((res) => {
          console.log('res')
              console.log(res)
              resolve(res);
          }).catch((e) => {
            console.log(e.response.data)
            reject(e);
          });
      });
    },
    FETCH_COMPLETED_TASKS_DATE(ctx, { dateFrom, dateTo, limit, offset, orderedColumn, orderDesc }) {
      return new Promise((resolve, reject) => {
        const reqArr = [];
        if (dateFrom) reqArr.push(`dateFrom=${dateFrom}`)
        if (dateTo) reqArr.push(`dateTo=${dateTo}`)
        if (limit) reqArr.push(`limit=${limit}`)
        if (offset || offset === 0) reqArr.push(`offset=${offset}`)
        if (orderedColumn) reqArr.push(`tableColumn=${orderedColumn}`)
        if (orderDesc !== undefined) reqArr.push(`orderDesc=${orderDesc}`)
        const string = reqArr.join('&');
        axios
          .get(`${config.baseUrl}/api/tasks/completed/selectionDate?${string}`)
          .then((res) => {
            if (res.status == 200) {
              let stringForTotal = [];
              if (dateFrom) stringForTotal.push(`dateFrom=${dateFrom}`)
              if (dateTo) stringForTotal.push(`dateTo=${dateTo}`)
              if (orderedColumn) stringForTotal.push(`tableColumn=${orderedColumn}`)
              axios
              .get(`${config.baseUrl}/api/tasks/completed/totalCountDate?${stringForTotal.join('&')}`)
              .then((responce) => {
                console.log(responce)
                const totalCount = responce.data.meta.count;
                console.log(res.data.tasks)
                ctx.commit("SET_COMPLETED_TASKS", {
                  items: res.data.tasks,
                  totalCount: totalCount,
                });
                resolve({
                  items: res.data.tasks,
                  totalCount: totalCount,
                });
              });
            } else if (res.status == 204) {
              ctx.commit("SET_COMPLETED_TASKS", {
                items: [],
                totalCount: 0,
              });
              resolve({
                items: [],
                totalCount: 0,
              });
            } else {
              reject(res);
            }
          });
      });
    },
    async FETCH_OBJECTS(ctx) {
      ctx.commit("SET_OBJECTS", objects);
    },
    async FETCH_PERMISSIONS(ctx) {
      ctx.commit("SET_PERMISSIONS", permissions);
    },
    async FETCH_ABOUT_PRODUCT(ctx) {
      return new Promise((resolve, reject) => {
        console.log(config.baseUrl)
        axios({
          method: 'get',
          url: `${config.baseUrl}/api/about`,
          headers: {
            Authorization: `Bearer ${this.state.token}`,
          }
        }).then((res) => {
          ctx.commit("SET_ABOUT_PRODUCT", res.data)
          resolve(res.data)
        }).catch((e) => {
          reject(e)
        })
    })
      // await axios.get(`${config.baseUrl}/aboutProduct").then((res) => {
      //   const product = res.data[0] // первый элемент массива
      //   ctx.commit("SET_ABOUT_PRODUCT", product)
      // })
    },
    FETCH_LICENCE(ctx) {
      return new Promise((resolve, reject) => {
        axios({
          method: 'GET',
          url: `/static/license.xml`,
        }).then((res) => {
          const parser = new DOMParser();
          const xmlDoc = parser.parseFromString(res.data, "text/xml");
          const license = xmlDoc.getElementsByTagName("SerialNumber")[0].childNodes[0].nodeValue;
          resolve(license)
          ctx.commit("SET_ABOUT_PRODUCT_LICENCE", license)
        }).catch((e) => {
          ctx.commit("SET_ABOUT_PRODUCT_LICENCE", 'Файл лицензии не найден')
          reject(e)
        })
    })
      // await axios.get(`${config.baseUrl}/aboutProduct").then((res) => {
      //   const product = res.data[0] // первый элемент массива
      //   ctx.commit("SET_ABOUT_PRODUCT", product)
      // })
    },
    // SERVER INTERACTION
    async ADD_SERVER(ctx, data) {
      return new Promise((resolve, reject) => {
        console.log(this)
        axios({
          method: 'post',
          url:   `${config.baseUrl}/api/servers`,
          data,
          headers: {
            Authorization: `Bearer ${this.state.token}`,
          }
        }).then((res) => {
            console.log(res.data)
            resolve(res.data);
          }).catch((e) => {
            console.dir(e)
            reject(e);
          })
      });
    },
    async REMOVE_SERVER_NODE(ctx, node) {
      return new Promise((resolve, reject) => {
        console.log('111')
        axios({
          method: 'delete',
          url:   `${config.baseUrl}/api/servers/nodes/${node}`,
          headers: {
            Authorization: `Bearer ${this.state.token}`,
          }
        }).then((res) => {
            console.log(res.data)
            resolve(res.data);
          }).catch((e) => {
            console.dir(e)
            reject(e);
          })
      });
    },
    async ADD_SERVER_NODE(ctx, data) {
      return new Promise((resolve, reject) => {
        axios({
          method: 'post',
          url:   `${config.baseUrl}/api/servers/nodes`,
          headers: {
            Authorization: `Bearer ${this.state.token}`,
          },
          data
        }).then((res) => {
            console.log(res.data)
            resolve(res.data);
          }).catch((e) => {
            console.dir(e)
            reject(e);
          })
      });
    },
    async CHANGE_SERVER(ctx, server) {
      return new Promise((resolve, reject) => {
        axios({
          method: 'put',
          url:   `${config.baseUrl}/api/servers/`,
          data: server,
          headers: {
            Authorization: `Bearer ${this.state.token}`,
          }
        }).then((res) => {
            console.log(res.data)
            resolve(res.data);
          }).catch((e) => {
            console.dir(e)
            reject(e);
          })
      });
    },
    async REMOVE_SERVER(ctx, server) {
      return new Promise((resolve, reject) => {
        axios({
          method: 'delete',
          url:   `${config.baseUrl}/api/servers/${server}`,
          headers: {
            Authorization: `Bearer ${this.state.token}`,
          }
        }).then((res) => {
            console.log(res.data)
            resolve(res.data);
          }).catch((e) => {
            console.dir(e)
            reject(e);
          })
      });
    },
    async FETCH_SERVER(ctx, server) {
      return new Promise((resolve, reject) => {
        axios({
          method: 'get',
          url:   `${config.baseUrl}/api/servers/${server}`,
          headers: {
             Authorization: `Bearer ${this.state.token}`,
          }
        }).then((res) => {
            console.log(res.data)
            resolve(res.data);
          }).catch((e) => {
            console.dir(e)
            reject(e);
          })
      });
    },
    // NODE INTERACTION
    async ADD_NODE(ctx, formData) {
      const data = {
        node: {
          name: "dfsfsdf",
        },
      };
      console.log("node add data", data);
      const axiosConfig = {
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + localStorage.getItem("Token"),
        },
      };
      return new Promise((resolve, reject) => {
        axios
          .post(`${config.baseUrl}/nodes`, formData, axiosConfig)
          .then((res) => {
            if (res.status == 201) {
              ctx.commit("ADD_NODE", res.data);
              resolve(res);
            } else {
              reject(res);
            }
          });
      });
    },
    async CHANGE_NODE(ctx, node) {
      const data = {
        ...node,
      };
      const axiosConfig = {};
      return new Promise((resolve, reject) => {
        axios
          .put(`${config.baseUrl}/nodes/${node.idr}`, data, axiosConfig)
          .then((res) => {
            console.log("response", res);
            if (res.status == 200) {
              ctx.commit("CHANGE_NODE", node);
              resolve(res);
            } else {
              // с помощью .catch можно обработать полученные ошибки
              reject(res);
            }
          });
      });
    },
    async REMOVE_NODE(ctx, node) {
      ctx.commit("REMOVE_NODE", node);
    },
    // USER INTERACTION
    async ADD_USER(ctx, formData) {
      await axios.get(`${config.baseUrl}/users`, formData).then((res) => {
        const users = res.data;
        if (users) {
          ctx.commit("ADD_USER", formData);
        }
      });
    },
    async CHANGE_USER(ctx, user) {
      ctx.commit("CHANGE_USER", user);
    },
    async REMOVE_USER(ctx, user) {
      ctx.commit("REMOVE_USER", user);
    },
    // ROLE INTERACTION
    async ADD_ROLE(ctx, formData) {
      await axios.get(`${config.baseUrl}/users`, formData).then((res) => {
        const roles = res.data;
        if (roles) {
          ctx.commit("ADD_ROLE", formData);
        }
      });
    },
    async CHANGE_ROLE(ctx, role) {
      ctx.commit("CHANGE_ROLE", role);
    },
    async REMOVE_ROLE(ctx, role) {
      ctx.commit("REMOVE_ROLE", role);
    },
    // PHONE INTERACTION
    async ADD_PHONE(ctx, formData) {
      await axios.get(`${config.baseUrl}/phones`, formData).then((res) => {
        const phones = res.data;
        if (phones) {
          ctx.commit("ADD_PHONE", formData);
        }
      });
    },
    async CHANGE_PHONE(ctx, phone) {
      ctx.commit("CHANGE_PHONE", phone);
    },
    async REMOVE_PHONE(ctx, phone) {
      ctx.commit("REMOVE_PHONE", phone);
    },
    // TASK_IN_POOL INTERACTION
    async ADD_TASK_IN_POOL(ctx, formData) {
      await axios.get(`${config.baseUrl}/tasks-pool`, formData).then((res) => {
        const tasks = res.data;
        if (tasks) {
          ctx.commit("ADD_TASK_IN_POOL", formData);
        }
      });
    },
    async CHANGE_TASK_IN_POOL(ctx, task) {
      ctx.commit("CHANGE_TASK_IN_POOL", task);
    },
    async REMOVE_TASK_IN_POOL(ctx, task) {
      ctx.commit("REMOVE_TASK_IN_POOL", task);
    },
  },
  getters: {
    USER: (state) => {
      return state.user;
    },
    IS_AUTH: (state) => {
      return !!state.token; //token присутсвует
    },
    TOKEN: (state) => {
      return state.token;
    },
    PAGES: (state) => {
      return state.pages;
    },
    SERVERS: (state) => {
      return state.servers;
    },
    SERVERS2: (state) => {
      return state.servers2;
    },
    NODES: (state) => {
      return state.nodes;
    },
    VENDORS: (state) => {
      return state.vendors;
    },
    MODELS: (state) => {
      return state.models;
    },
    LOGS: (state) => {
      return state.logs;
    },
    LOG_TYPES: (state) => {
      return state.logTypes;
    },
    USERS: (state) => {
      return state.users;
    },
    TASK_CURRENT: (state) => {
      return state.taskCurrent;
    },
    TASK_DONE: (state) => {
      return state.taskDone;
    },
    ROLE_USERS: (state) => {
      return state.roleUsers;
    },
    ROLES: (state) => {
      return state.roles;
    },
    PHONES: (state) => {
      return state.phones;
    },
    CURRENT_TASKS: (state) => {
      return state.tasksPool;
    },
    TASKS_LIST: (state) => {
      return state.tasksList;
    },
    COMPLETED_TASKS: (state) => {
      return state.history;
    },
    OBJECTS: (state) => {
      return state.objects;
    },
    PERMISSIONS: (state) => {
      return state.permissions;
    },
    ABOUT_PRODUCT: (state) => {
      return state.aboutProduct;
    },
    CONFIG: () => {
      return config;
    },
  },
  modules: {},
});
