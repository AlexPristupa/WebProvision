
<template>
  <v-card elevation="0" class="d-flex flex-column pa-0">
    <v-dialog v-model="deleteConfirmDialog" width="750px">
      >
      <v-card elevation="0" height="100%" class="d-flex flex-column">
        <div class="popup-header">
          <v-card-title>{{formTitle}}</v-card-title>
          <v-icon class="white--text" @click="dialog = false" :color="CONFIG.colors.white">mdi-close</v-icon>
        </div>
        <v-row no-gutters class="pl-7 pr-7">
          <v-card-title class="pl-0 pb-2">{{$vuetify.lang.t('$vuetify.areYouSureYouWantToDeleteTheUser')}}</v-card-title>
          <v-spacer></v-spacer>
        </v-row>
        <v-divider class="ml-7 mr-7"></v-divider>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            class="ps-btn-close"
            :color="CONFIG.colors.secondary"
            @click="closeDeleteForm"
          >{{$vuetify.lang.t('$vuetify.close')}}</v-btn>
          <v-btn
            class="ps-btn-delete"
            :color="CONFIG.colors.primary"
            @click="deleteItem"
          >{{$vuetify.lang.t('$vuetify.delete')}}</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <ps-users-set-role-dialog
      :roleName="roleName"
      :isShow="usersSetRoleDialog"
      @close="usersSetRoleDialogCloseHandler()"
    />
    <v-card-actions class="ps-card-actions-top side ps-card-actions-top__margins mt-2" style="margin-bottom: 11px;">
      <div class="width-363">
        <!-- Поле поиска -->
        <v-text-field
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
          max-height="42"
          class="ps-btn-add ps-add-btn font-size-16 add-left"
          @click.stop.prevent="showUsersSetRoleDialog()"
        >{{$vuetify.lang.t('$vuetify.add')}}</v-btn>
      </div>
    </v-card-actions>
    <v-card-text class="ps-card-text">
      <div :style="{ width: `${tableWidth}px`, position: 'relative'}">
         <div class="table-width-setter" ref="firstBar" @mousedown="dragMouseDown(0,$event)" :style="{ left: `${columns.first}%`}"></div> 
         <div class="table-width-setter" ref="secondBar" @mousedown="dragMouseDown(1,$event)" :style="{ left: `${columns.first + columns.second}%`}"></div>
         <div class="table-width-setter" ref="thirdBar" @mousedown="dragMouseDown(2, $event)" :style="{ left: `${columns.first + columns.second + columns.third}%`}"></div> 
        </div>
      <v-data-table
        fixed-header
        :hide-default-footer="CONFIG.table.hideDefaultFooter"
        :header-props="CONFIG.table.headerProps"
        :class="CONFIG.table.class"
        :footer-props="CONFIG.table.footerProps"
        :dense="CONFIG.table.dense"
        :headers="headers"
        :height="windowHeight - 279"
        :items="ROLE_USERS.items"
        :page.sync="pagination.page"
        :items-per-page.sync="pagination.rowsPerPage"
        :search="search"
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
            <v-spacer></v-spacer>
            <div class="pagination-block">
              <div class="pagination-items-per-page-select">
                Записей на странице
                <div class="pagination-items-per-page-select-wrapper">
                  <v-select
                    hide-details
                    :menu-props="{ top: true, offsetY: true }"
                    append-icon=""
                    prepend-inner-icon="mdi-chevron-down"
                    reverse
                    item-value="value"
                    :items="itemsPerPageOptions"
                    item
                    dense
                    outlined
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
              >
                <td :style="{width: `${columns.first}%`}">{{ props.item.login }}</td>
                <td :style="{width: `${columns.second}%`}">{{ props.item.DisplayName }}</td>
                <td :style="{width: `${columns.third}%`}">{{ props.item.Email }}</td>
                <td :style="{width: `${columns.fourth}%`}">{{ props.item.actions }}</td>
              </tr>
            </template>

      </v-data-table>
    </v-card-text>
  </v-card>
</template>

<script>
import { mapGetters, mapActions } from "vuex";
import { required } from "@/helpers/validators";
import psUsersSetRoleDialog from "@/components/dialogs/ps-users-set-role-dialog";

export default {
  name: "ps-users-table",
  props: {
    roleName: {
      type: String,
      required: true,
    },
  },
  components: {
    psUsersSetRoleDialog,
  },
  mounted() {
    this.FETCH_USERS_BY_ROLE({
      limit: 1000,
      offset: 0,
      roleName: this.roleName,
    });
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
  data() {
    return {
      pageSetErrors: [],
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
        first: 25,
        second: 25,
        third: 25,
        fourth: 25,
      },
      search: "",
      headers: [
        { text: this.$vuetify.lang.t("$vuetify.login"), value: "login" },
        {
          text: this.$vuetify.lang.t("$vuetify.username"),
          value: "DisplayName",
        },
        { text: this.$vuetify.lang.t("$vuetify.email"), value: "Email" },
        {
          text: this.$vuetify.lang.t("$vuetify.actions"),
          value: "actions",
          sortable: false,
        },
      ],
      windowHeight: window.innerHeight,
      dialog: false,
      deleteConfirmDialog: false,
      usersSetRoleDialog: false,
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
      passwordRules: [() => required(this.editedItem.password)],
    };
  },
  computed: {
    ...mapGetters(["ROLE_USERS", "ROLES", "CONFIG"]),
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
      "FETCH_USERS_BY_ROLE",
      "ADD_USER",
      "CHANGE_USER",
      "REMOVE_USER",
    ]),
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
      const pos = this.point - this.$refs.tablecontainer.getBoundingClientRect().left;
      const position = +(((pos/width)*100).toFixed(3));
      const keys = Object.keys(columns);
      let diffTotal = position;
      for (let i = 0; i <= activeBar; i += 1) {
        diffTotal = diffTotal - columns[keys[i]];
      }
      console.log(diffTotal)
      let result = position;
      for (let i = activeBar -1; i >= 0; i -= 1) {
        result = result - columns[keys[i]]
      }
      columns[keys[activeBar]] = result;
      columns[keys[activeBar + 1]] = columns[keys[activeBar + 1]] - diffTotal;
      if (columns.first < 9.9 || columns.second < 20 || columns.third < 8 || columns.fourth < 11) {
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
    setPage(page) {
      this.pagination.page = parseInt(page);
      this.pageSetErrors = [];
    },
    handlePageSetErrors(errors) {
      this.pageSetErrors = errors;
    },
    showEditForm(item) {
      this.editedIndex = 1;
      this.editedItem = Object.assign({}, item);
      this.dialog = true;
    },
    showUsersSetRoleDialog() {
      this.usersSetRoleDialog = true;
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
    usersSetRoleDialogCloseHandler() {
      this.usersSetRoleDialog = false;
    },
  },
};
</script>
