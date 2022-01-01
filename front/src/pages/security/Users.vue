
<template>
  <v-row style="height: 100%" justify="center">
    <v-dialog
      v-if="deleteConfirmDialog"
      no-click-animation
      persistent
      v-model="deleteConfirmDialog"
      width="750px"
      @keydown.esc="deleteConfirmDialog = false"
    >
      <v-card elevation="0" height="100%" class="d-flex flex-column">
        <div class="popup-header">
          <v-card-title>Удалить пользователя</v-card-title>
          <v-icon
            class="white--text"
            @click="deleteConfirmDialog = false"
            :color="CONFIG.colors.white"
          >mdi-close</v-icon>
        </div>
        <v-row no-gutters class="pl-7 pr-7">
          <v-card-title class="pl-0 pb-2">{{editedItem.name}}</v-card-title>
          <v-spacer></v-spacer>
        </v-row>
        <v-divider class="ml-7 mr-7"></v-divider>
        <v-card-text
          class="pt-2 pb-2 pr-7 pl-7"
        >{{$vuetify.lang.t('$vuetify.areYouSureYouWantToDeleteTheUser')}}</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            class="ps-btn-delete white--text"
            :color="CONFIG.colors.primary"
            @click="deleteItem"
          >{{$vuetify.lang.t('$vuetify.delete')}}</v-btn>
          <v-btn
            autofocus
            class="cancel-btn-delete"
            :color="CONFIG.colors.secondary"
            @click="closeDeleteForm"
          >{{$vuetify.lang.t('$vuetify.close')}}</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-dialog
      v-if="dialog"
      no-click-animation
      persistent
      v-model="dialog"
      width="750px"
      @keydown.esc="dialog = false"
    >
      <v-card elevation="0" height="100%" class="d-flex flex-column">
        <div class="popup-header">
          <v-card-title>{{formTitle}}</v-card-title>
          <v-icon class="white--text" @click="dialog = false" :color="CONFIG.colors.white">mdi-close</v-icon>
        </div>
        <v-row no-gutters class="pl-7 pr-7">
          <v-card-title class="pl-0 pb-2">{{$vuetify.lang.t('$vuetify.enterAccompanyingInformation')}}</v-card-title>
          <v-spacer></v-spacer>
        </v-row>
        <v-divider class="ml-7 mr-7"></v-divider>
        <v-card-text>
          <v-form ref="form" lazy-validation class="pr-1 pl-1">
            <v-row>
              <v-col class="pr-5">
                <v-row :class="CONFIG.form.rowClass">
                  <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass">
                    <v-subheader
                      :class="CONFIG.form.labelClass"
                    >{{$vuetify.lang.t('$vuetify.login')}}</v-subheader>
                  </v-col>
                  <v-col :class="CONFIG.form.fieldColClass">
                    <v-text-field
                      dense
                      outlined
                      v-model="editedItem.login"
                      :placeholder="$vuetify.lang.t('$vuetify.login')"
                    ></v-text-field>
                  </v-col>
                </v-row>
                <v-row :class="CONFIG.form.rowClass">
                  <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass">
                    <v-subheader
                      :class="CONFIG.form.labelClass"
                    >{{$vuetify.lang.t('$vuetify.username')}}</v-subheader>
                  </v-col>
                  <v-col :class="CONFIG.form.fieldColClass">
                    <v-text-field
                      :rules="displayNameRules"
                      dense
                      outlined
                      v-model="editedItem.DisplayName"
                      :placeholder="$vuetify.lang.t('$vuetify.username')"
                    ></v-text-field>
                  </v-col>
                </v-row>
                <v-row :class="CONFIG.form.rowClass">
                  <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass">
                    <v-subheader
                      :class="CONFIG.form.labelClass"
                    >{{$vuetify.lang.t('$vuetify.position')}}</v-subheader>
                  </v-col>
                  <v-col :class="CONFIG.form.fieldColClass">
                    <v-text-field
                      dense
                      outlined
                      v-model="editedItem.Provider"
                      :placeholder="$vuetify.lang.t('$vuetify.position')"
                    ></v-text-field>
                  </v-col>
                </v-row>
                <v-row :class="CONFIG.form.rowClass">
                  <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass">
                    <v-subheader
                      :class="CONFIG.form.labelClass"
                    >{{$vuetify.lang.t('$vuetify.role')}}</v-subheader>
                  </v-col>
                  <v-col :class="CONFIG.form.fieldColClass">
                    <v-select
                      dense
                      outlined
                      v-model="editedItem.RoleId"
                      :placeholder="$vuetify.lang.t('$vuetify.role')"
                    ></v-select>
                  </v-col>
                </v-row>
                <v-row :class="CONFIG.form.rowClass">
                  <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass">
                    <v-subheader
                      :class="CONFIG.form.labelClass"
                    >{{$vuetify.lang.t('$vuetify.password')}}</v-subheader>
                  </v-col>
                  <v-col :class="CONFIG.form.fieldColClass">
                    <v-text-field
                      dense
                      outlined
                      :rules="passwordRules"
                      v-model="editedItem.password"
                      :placeholder="$vuetify.lang.t('$vuetify.password')"
                    ></v-text-field>
                  </v-col>
                </v-row>
                <v-row :class="CONFIG.form.rowClass">
                  <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass">
                    <v-subheader
                      :class="CONFIG.form.labelClass"
                    >{{$vuetify.lang.t('$vuetify.email')}}</v-subheader>
                  </v-col>
                  <v-col :class="CONFIG.form.fieldColClass">
                    <v-text-field
                      dense
                      outlined
                      v-model="editedItem.Email"
                      :placeholder="$vuetify.lang.t('$vuetify.email')"
                    ></v-text-field>
                  </v-col>
                </v-row>
                <v-row :class="CONFIG.form.rowClass">
                  <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass">
                    <v-subheader :class="CONFIG.form.labelClass">SID</v-subheader>
                  </v-col>
                  <v-col :class="CONFIG.form.fieldColClass">
                    <v-text-field
                      dense
                      outlined
                      v-model="editedItem.SID"
                      placeholder="SID"
                    ></v-text-field>
                  </v-col>
                </v-row>
              </v-col>
            </v-row>
          </v-form>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            class="white--text ps-btn-ok"
            :color="CONFIG.colors.primary"
            @click="saveForm"
          >{{okBtnText}}</v-btn>
          <v-btn
            autofocus
            class="white--text ps-btn-close cancel-btn-delete"
            :color="CONFIG.colors.grey"
            @click="closeAddOrEditForm"
          >Закрыть</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-col cols="12" style="height: 100%" class="ps-col-wrapper">
      <v-card-title class="ps-card-title-top">
        <h1 class="page-header">{{$vuetify.lang.t('$vuetify.users')}}</h1>
      </v-card-title>
      <v-card elevation="0" height="100%" class="d-flex flex-column">
        <v-card-actions class="ps-card-actions-top ps-card-actions-top__margins">
          <!-- Поле поиска -->
          <div class="search-width">
            <v-text-field
              @keyup="searchTimeOut()"
              hide-details="auto"
              class="ps-search-input font-size-16"
              v-model="search"
              :placeholder="$vuetify.lang.t('$vuetify.searchText')"
              append-icon="mdi-magnify"
              :background-color="CONFIG.colors.white"
            ></v-text-field>
          </div>
          <v-spacer></v-spacer>
          <div class="width-210">
            <v-btn
              class="ps-btn-add ps-add-btn font-size-16"
              max-height="42"
              :color="CONFIG.colors.primary"
              @click="showAddForm()"
            >Добавить</v-btn>
          </div>
        </v-card-actions>
        <v-card-text class="ps-card-text">
          <div :style="{ width: `${tableWidth}px`, position: 'relative'}">
            <div class="table-width-setter" @mousedown="dragMouseDown(0,$event)" :style="{ left: `${columns.first}%`}"></div> 
            <div class="table-width-setter" @mousedown="dragMouseDown(1,$event)" :style="{ left: `${columns.first + columns.second}%`}"></div>
            <div class="table-width-setter" @mousedown="dragMouseDown(2, $event)" :style="{ left: `${columns.first + columns.second + columns.third}%`}"></div> 
            <div class="table-width-setter" @mousedown="dragMouseDown(3, $event)" :style="{ left: `${columns.first + columns.second + columns.third + columns.fourth}%`}"></div> 
          </div> 
          <v-data-table
            :options.sync="options"
            :hide-default-footer="CONFIG.table.hideDefaultFooter"
            :header-props="CONFIG.table.headerProps"
            :class="CONFIG.table.class"
            :footer-props="CONFIG.table.footerProps"
            :server-items-length="USERS.totalCount"
            :dense="CONFIG.table.dense"
            :height="windowHeight - 225"
            :fixed-header="CONFIG.table.fixedHeader"
            :headers="headers"
            :items="USERS.items"
            :page.sync="pagination.page"
            :items-per-page.sync="pagination.rowsPerPage"
            @page-count="pagination.totalPages = $event"
          >
            <template v-slot:footer="{props}">
              <v-row justify="start" align="center" no-gutters class="pagination-section">
                <v-pagination
                  class="ps-pagination pagination-block"
                  :total-visible="CONFIG.table.pagination.totalVisible"
                  v-model="pagination.page"
                  :length="pagination.totalPages"
                ></v-pagination>
                <div class="pagination-block r-off">
                  <div class="ps-page-set">
                    Перейти на страницу
                    <div class="ps-page-set-wrapper">
                      <ps-pagination-page-set
                        @onValidationError="handlePageSetErrors"
                        @onSetPage="setPage"
                        :totalPages="pagination.totalPages"
                      />
                    </div>
                  </div>
                </div>
                <v-spacer></v-spacer>
                <div class="pagination-block">
                  <div class="pagination-items-per-page-select">
                    Записей на странице
                    <div class="pagination-items-per-page-select-wrapper">
                      <v-select
                        :menu-props="{ top: true, offsetY: true }"
                        append-icon=""
                        prepend-inner-icon="mdi-chevron-down"
                        reverse
                        item-value="value"
                        :items="itemsPerPageOptions"
                        item
                        dense
                        outlined
                        hide-details
                        v-model="pagination.rowsPerPage"
                      />
                    </div>
                  </div>
                </div>
                <div class="pagination-block">
                  <div class="pagination-step">
                    <div
                      class="current"
                    >{{props.pagination.pageStart + 1}}-{{props.pagination.pageStop}}</div>
                    из {{props.pagination.itemsLength}}
                  </div>
                </div>
                <!-- <div class="page-set-errors">
                  <p
                    class="red--text pl-10 pt-2"
                    style="width: 100%; font-size: 8px; text-align: center !important"
                    v-for="(err, index) in pageSetErrors"
                    :key="index"
                  >{{err}}</p>
                </div>-->
              </v-row>
            </template>
            <template v-slot:item="props">
              <tr
                ref="tablecontainer"
                style="cursor: pointer"
                @click="handleRowClick(props)"
                :class="{ 'v-data-table__selected':selectedPhone === props.item }"
              >
                <td :style="{width: `${columns.first}%`}">{{ props.item.login }}</td>
                <td :style="{width: `${columns.second}%`}">{{ props.item.displayName }}</td>
                <td :style="{width: `${columns.third}%`}">{{ props.item.roleName }}</td>
                <td :style="{width: `${columns.fourth}%`}">{{ props.item.email }}</td>
                <td :style="{width: `${columns.fifth}%`}">
                  <div class="text-truncate">
                    <img
                      class="mr-1"
                      style="cursor: pointer"
                      :src="require('../../assets/media/edit.png')"
                      alt="user"
                      @click="showEditForm(props.item)"
                    />
                    <img
                      style="cursor: pointer"
                      :src="require('../../assets/media/trash.png')"
                      alt="user"
                      @click="showDeleteForm(props.item)"
                    />
                  </div>
                </td>
              </tr>
            </template>

            <template
              v-slot:item.role="{ item }"
            >{{ROLES.length && ROLES.find(role => role.idr === item.RoleId) && ROLES.find(role => role.idr === item.RoleId).name}}</template>
            <template v-slot:item.actions="props">
              <div class="text-truncate">
                <img
                  class="mr-1"
                  style="cursor: pointer"
                  :src="require('../../assets/media/edit.png')"
                  alt="user"
                  @click="showEditForm(props.item)"
                />
                <img
                  style="cursor: pointer"
                  :src="require('../../assets/media/trash.png')"
                  alt="user"
                  @click="showDeleteForm(props.item)"
                />
              </div>
            </template>
          </v-data-table>
        </v-card-text>
      </v-card>
    </v-col>
  </v-row>
</template>

<script>
import { mapGetters, mapActions } from "vuex";
import { required } from "@/helpers/validators";
import psPaginationPageSet from "@/components/pagination/ps-pagination-page-set";

export default {
  name: "Users",
  components: {
    psPaginationPageSet,
  },
  data() {
    return {
      pagination: {
        page: 1,
        rowsPerPage: 25,
        totalItems: 0,
        totalPages: 0,
      },
      tableWidth: 0,
      point: 0,
      activeBar: 0,
      columns: {
        first: 20,
        second: 20,
        third: 20,
        fourth: 20,
        fifth: 20,
      },
      search: "",
      timer: null,
      options: {sortBy: [], sortDesc: []},
      headers: [
        { text: this.$vuetify.lang.t("$vuetify.login"), value: "login" },
        {
          text: this.$vuetify.lang.t("$vuetify.username"),
          value: "displayName",
        },
        { text: this.$vuetify.lang.t("$vuetify.role"), value: "roleName" },
        { text: this.$vuetify.lang.t("$vuetify.email"), value: "email" },
        {
          text: this.$vuetify.lang.t("$vuetify.actions"),
          value: "actions",
          sortable: false,
        },
      ],
      dialog: false,
      windowHeight: window.innerHeight,
      deleteConfirmDialog: false,
      editedIndex: -1,
      editedItem: {
        login: "",
        DisplayName: "",
        Provider: "",
        RoleId: "",
        password: "",
        Email: "",
        SID: "",
      },
      defaultItem: {
        login: "",
        DisplayName: "",
        Provider: "",
        RoleId: "",
        password: "",
        Email: "",
        SID: "",
      },
      loginRules: [() => required(this.editedItem.login)],
      displayNameRules: [() => required(this.editedItem.DisplayName)],
      passwordRules: [() => required(this.editedItem.password)],
      pageSetErrors: [],
    };
  },
  async mounted() {
    this.FETCH_ROLES();
    this.FETCH_USERS({
      limit: this.pagination.rowsPerPage,
      offset: this.pagination.page - 1,
    });
    this.pagination.totalItems = await this.USERS.items.length;
    this.pagination.totalPages =
      (await Math.floor(
        this.USERS.items.length / this.pagination.rowsPerPage
      )) + 1;
    setInterval(() => {
      if (window.innerHeight !== this.windowHeight) {
         this.windowHeight = window.innerHeight;
         this.tableWidth = this.$refs.tablecontainer.offsetWidth;
      }
      if (this.tableWidth !== this.$refs.tablecontainer.offsetWidth) {
        this.tableWidth = this.$refs.tablecontainer.offsetWidth;
      }
    }, 1000);
  },
  watch: {
    "pagination.page": {
      handler() {
        this.FETCH_USERS({
          limit: this.pagination.rowsPerPage,
          offset: this.pagination.page - 1,
        });
      },
    },
    "pagination.rowsPerPage": {
      handler() {
        this.FETCH_USERS({
          limit: this.pagination.rowsPerPage,
          offset: this.pagination.page - 1,
        });
      },
    },
    options: {
      handler ({sortBy, sortDesc}) {
         console.log(this.options)
         this.FETCH_USERS({ limit: this.pagination.rowsPerPage, offset: this.pagination.page - 1, orderedColumn: sortBy[0], sortDesc: sortDesc[0], search: this.search});
      },
    },
  },
  computed: {
    ...mapGetters(["USERS", "ROLES", "CONFIG"]),
    itemsPerPageOptions() {
      return [
        { value: 25, text: "25" },
        { value: 50, text: "50" },
        { value: 100, text: "100" },
        { value: 200, text: "200" },
        { value: 500, text: "500" },
      ];
    },
    formTitle() {
      return this.editedIndex === -1
        ? this.$vuetify.lang.t("$vuetify.addUser")
        : this.$vuetify.lang.t("$vuetify.editUser");
    },
    okBtnText() {
      return this.editedIndex === -1
        ? this.$vuetify.lang.t("$vuetify.add")
        : this.$vuetify.lang.t("$vuetify.save");
    },
  },
  methods: {
    ...mapActions([
      "FETCH_USERS",
      "FETCH_ROLES",
      "ADD_USER",
      "CHANGE_USER",
      "REMOVE_USER",
    ]),
        searchTimeOut() {  
      if (this.timer) {
        clearTimeout(this.timer);
        this.timer = null;
      }
      this.timer = setTimeout(() => {
        this.FETCH_USERS({ limit: this.pagination.rowsPerPage, offset: this.pagination.page - 1, search: this.search, orderedColumn: this.options.sortBy[0], sortDesc: this.options.sortDesc[0]});
      }, 1000);
    },
    showEditForm(item) {
      this.editedIndex = 1;
      this.editedItem = Object.assign({}, item);
      this.dialog = true;
    },
    dragMouseDown(bar, e) {
      e.preventDefault();
      this.point = e.clientX;
      this.activeBar = bar;
      document.onmousemove = this.elementDrag;
      document.onmouseup = this.closeDragElement;
    },
    elementDrag(e) {
      e = e || window.event;
      e.preventDefault();
      const activeBar = this.activeBar;
      const columns = {...this.columns};
      const width=this.$refs.tablecontainer.offsetWidth;
      this.tableWidth = width;
      const pos = this.point - 35;
      const position = +(((pos/width)*100).toFixed(3));
      const keys = Object.keys(columns);
      let diffTotal = position;
      for (let i = 0; i <= activeBar; i += 1) {
        diffTotal = diffTotal - columns[keys[i]];
      }
      let result = position;
      for (let i = activeBar -1; i >= 0; i -= 1) {
        result = result - columns[keys[i]]
      }
      columns[keys[activeBar]] = result;
      columns[keys[activeBar + 1]] = columns[keys[activeBar + 1]] - diffTotal;
      if (columns.first < 5.3 || columns.second < 9.8 || columns.third < 3.4 || columns.fourth < 4.8 || columns.fifth < 5.7) {
        this.point = e.clientX;
        return;
      }
      this.columns = columns;
      this.point = e.clientX;
    },
    closeDragElement() {
      document.onmouseup = null;
      document.onmousemove = null;
    },
    showAddForm() {
      this.editedIndex = -1;
      this.editedItem = Object.assign({}, this.defaultItem);
      this.dialog = true;
    },
    showDeleteForm(item) {
      this.editedIndex = null;
      this.editedItem = Object.assign({}, item);
      this.deleteConfirmDialog = true;
    },
    closeAddOrEditForm() {
      this.dialog = false;
    },
    closeDeleteForm() {
      this.deleteConfirmDialog = false;
    },
    deleteItem() {
      this.REMOVE_USER(this.editedItem);
      this.closeDeleteForm();
    },
    saveForm() {
      if (this.$refs.form.validate()) {
        if (this.editedIndex > -1) {
          this.CHANGE_USER(this.editedItem);
        } else {
          this.ADD_USER({ ...(this.editedItem || null) });
        }
        this.closeAddOrEditForm();
      }
    },
    setPage(page) {
      this.pagination.page = parseInt(page);
      this.pageSetErrors = [];
    },
    handlePageSetErrors(errors) {
      this.pageSetErrors = errors;
    },
  },
};
</script>
