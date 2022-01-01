
<template>
  <v-card elevation="0" height="100%" style="padding-top: 1px">
    <v-card-actions class="ps-card-actions-top side ps-card-actions-top__margins" style="margin-top: 9px; margin-bottom: 9px;">
      <div style="width: 800px; font-size: 16px">
        <p>В предварительной версии не функционально</p>
      </div>
      <v-spacer></v-spacer>
      <div class="top-btn-width-2">
        <v-btn
          disabled
          max-height="42"
          class="ps-btn-add ps-add-btn-2 font-size-16 "
          @click.stop.prevent="saveSelectedPermissions()"
        >{{$vuetify.lang.t('$vuetify.save')}}</v-btn>
      </div>
    </v-card-actions>
    <v-card-text class="ps-card-text"> 
      <v-data-table
        class="side-table"
        calculate-widths
        :hide-default-footer="CONFIG.table.hideDefaultFooter"
        :header-props="CONFIG.table.headerProps"
        :fixed-header="CONFIG.table.fixedHeader"
        :class="CONFIG.table.class"
        :footer-props="CONFIG.table.footerProps"
        :dense="CONFIG.table.dense"
        :height="windowHeight - 279"
        :items="OBJECTS"
        :headers="headers"
        :page.sync="page"
        :items-per-page.sync="pagination.rowsPerPage"
        :search="search"
        @page-count="pagination.totalPages = $event"
      >
        <template v-slot:item="props">
          <tr>
            <td style="font-size: 16px">{{ props.item.text }}</td>
            <td style="font-size: 16px">
              <ps-page-permissions-item
                :pageIdr="props.item.idr"
                @updatePermissions="updatePermissions"
              />
            </td>
            <td>
            </td>
          </tr>
        </template>
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
      </v-data-table>
    </v-card-text>
  </v-card>
</template>

<script>
import psPagePermissionsItem from "@/components/tables/computed-rows/roles/ps-page-permissions-item";
import { mapGetters, mapActions } from "vuex";
export default {
  name: "ps-permitted-actions-table",
  data() {
    return {
      pageSetErrors: [],
      pagination: {
        page: 1,
        rowsPerPage: 25,
        totalItems: 0,
        totalPages: 0,
      },
      search: "",
      headers: [
        {
          text: this.$vuetify.lang.t("$vuetify.object"),
          value: "text",
          width: "200",
        },
        {
          text: this.$vuetify.lang.t("$vuetify.permissions"),
          value: "permissions",
          width: "200",
        },
        {
          text: '',
          value: '',
          width: "200",
        },
      ],
      isSelectedNowPermissions: false, // изменены ли изначальные права?
      permissions: [], // права
    };
  },
  components: { psPagePermissionsItem },
  computed: {
    ...mapGetters(["CONFIG", "OBJECTS", "PERMISSIONS"]),
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
  methods: {
    ...mapActions(["FETCH_OBJECTS", "FETCH_PERMISSIONS"]),
    setPage(page) {
      this.pagination.page = parseInt(page);
      this.pageSetErrors = [];
    },
    handlePageSetErrors(errors) {
      this.pageSetErrors = errors;
    },
    updatePermissions({ oldValue, newValue, pageIdr }) {
      // обрабатывает $emit из дочернего компонента
      // console.log(this.permissions);
      // this.permissions.push({ pageIdr, oldValue, newValue });
      // this.permissions = [
      //   ...new Map(
      //     this.permissions.map((item) => [item.pageIdr, item])
      //   ).values(),
      // ]; // очищаем массив от дубликатов по idr

      // let arr = []; // массив куда добавляем булево значение(есть изменение или нет)
      // this.permissions.map((item) => {
      //   // добавляем булево значение на каждой итерации
      //   arr.push(item.oldValue !== item.newValue);
      // });
      // if (arr.includes(true)) {
      //   // если массив содержит хотя бы одно значение true, то кнопка становится активной.
      //   this.isSelectedNowPermissions = true;
      // } else {
      //   this.isSelectedNowPermissions = false;
      // }
      console.log(oldValue, newValue, pageIdr) // нужно будет сделать проверку на новое значение когда будет готов api
    },
    saveSelectedPermissions(){
      console.log("SAVE_NEW_PERMISSIONS", this.permissions)
    }
  },
  mounted() {
    this.FETCH_PERMISSIONS();
    this.FETCH_OBJECTS();
  },
};
</script>
<style scoped>

</style>

