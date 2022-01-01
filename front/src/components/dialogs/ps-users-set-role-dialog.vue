<template>
  <v-dialog
    v-if="isShow"
    no-click-animation
    persistent
    width="1150"
    v-model="isShow"
    @keydown.esc="closeHandler()"
  >
    <v-card
      height="100%"
      :elevation="CONFIG.vuetify.elevation"
      class="d-flex flex-column"
    >
      <div class="popup-header">
        <v-card-title>Назначить роль</v-card-title>
        <v-icon class="white--text" @click="closeHandler()" :color="CONFIG.colors.white">mdi-close</v-icon>
      </div>
      <v-row no-gutters class="pl-7 pr-7">
        <v-card-title class="pl-0 pb-2">Назначить роль {{roleName}}</v-card-title>
        <v-spacer></v-spacer>
      </v-row>
      <v-divider class="ml-7 mr-7"></v-divider>
      <v-card-actions class="ps-card-actions-top ps-card-actions-top__margins">
        <v-col cols="5" class="pb-0 pt-0">
          <!-- Поле поиска -->
          <v-text-field
            autofocus
            hide-details="auto"
            class="ps-search-input"
            v-model="search"
            :placeholder="$vuetify.lang.t('$vuetify.searchText')"
            append-icon="mdi-magnify"
            :background-color="CONFIG.colors.white"
          ></v-text-field>
        </v-col>
        <v-col cols="4" class="pb-0 pt-0">
          <v-btn
            class="ps-btn-add ps-add-btn"
            :color="CONFIG.colors.primary"
            @click="alert()"
          >Сохранить</v-btn>
        </v-col>
      </v-card-actions>
      <v-card-text class="ps-card-text pb-5">
        <v-data-table
          :hide-default-footer="CONFIG.table.hideDefaultFooter"
          :header-props="CONFIG.table.headerProps"
          :class="CONFIG.table.class"
          :footer-props="CONFIG.table.footerProps"
          :server-items-length="USERS.totalCount"
          :dense="CONFIG.table.dense"
          :height="windowHeight - 280"
          :fixed-header="CONFIG.table.fixedHeader"
          :headers="headers"
          :items="USERS.items"
          :page.sync="pagination.page"
          :items-per-page.sync="pagination.rowsPerPage"
          :search="search"
          @page-count="pagination.totalPages = $event"
        >
          <template v-slot:footer="{props}">
            <v-row justify="start" align="center" no-gutters class="pagination-section">
              <div class="pagination-block">
                <div class="ps-pagination">
                  <v-pagination
                    :total-visible="CONFIG.table.pagination.totalVisible"
                    v-model="pagination.page"
                    :length="pagination.totalPages"
                  ></v-pagination>
                </div>
              </div>
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
        </v-data-table>
      </v-card-text>
    </v-card>
  </v-dialog>
</template>

<script>
import { mapGetters, mapActions } from "vuex";
import psPaginationPageSet from "@/components/pagination/ps-pagination-page-set";

export default {
  name: "psUsersSetRoleDialog",
  props: {
    isShow: {
      type: Boolean,
      required: true,
    },
    roleName: {
      type: String,
      required: true,
    },
  },
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
      search: "",
      selectedUsers: [],
      pageSetErrors: [],
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
    };
  },
  computed: {
    ...mapGetters(["USERS", "CONFIG"]),
    windowHeight() {
      return window.innerHeight;
    },
    itemsPerPageOptions() {
      return [
        { value: 25, text: "25" },
        { value: 50, text: "50" },
        { value: 100, text: "100" },
        { value: 200, text: "200" },
        { value: 500, text: "500" },
      ];
    },
  },
  async mounted() {
    this.FETCH_USERS({
      limit: this.pagination.rowsPerPage,
      offset: this.pagination.page - 1,
    });
    this.pagination.totalItems = await this.USERS.items.length;
    this.pagination.totalPages =
      (await Math.floor(
        this.USERS.items.length / this.pagination.rowsPerPage
      )) + 1;
    //   this.FETCH_USERS_WITHOUT_ROLE({
    //     limit: 1000,
    //     offset: 43,
    //     roleName: this.roleName,
    //   })
    //     .then((res) => alert(res))
    //     .catch((res) => alert(res));
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
  },
  methods: {
    ...mapActions(["FETCH_USERS_WITHOUT_ROLE", "FETCH_USERS"]),
    saveHandler() {
      alert(`Роль назначена ${this.selectedUsers.length} пользователям`);
      this.$emit("close");
    },
    closeHandler() {
      this.$emit("close");
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
