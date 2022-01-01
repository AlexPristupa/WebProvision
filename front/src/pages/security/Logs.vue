
<template>
  <!-- Журнал -->
  <v-row style="height: 100%" justify="center">
    <v-col cols="12" style="height: 100%" class="ps-col-wrapper">
      <v-card-title class="ps-card-title-top">
        <h1 class="page-header">{{$vuetify.lang.t("$vuetify.logs")}}</h1>
      </v-card-title>
      <v-card elevation="0" height="100%" class="d-flex flex-column">
        <v-card-actions class="ps-card-actions-top ps-card-actions-top__margins">
          <div class="search-width">
            <v-text-field
              hide-details="auto"
              class="ps-search-input"
              v-model="search"
              :placeholder="$vuetify.lang.t('$vuetify.searchText')"
              append-icon="mdi-magnify"
              :background-color="CONFIG.colors.white"
              single-line
            ></v-text-field>
          </div>
          <v-spacer></v-spacer>
        </v-card-actions>

        <v-card-text class="ps-card-text">
          <div :style="{ width: `${tableWidth}px`, position: 'relative'}" v-if="tableNotEmpty">
            <div class="table-width-setter" @mousedown="dragMouseDown(0,$event)" :style="{ left: `${columns.first}%`}"></div> 
            <div class="table-width-setter" @mousedown="dragMouseDown(1,$event)" :style="{ left: `${columns.first + columns.second}%`}"></div>
            <div class="table-width-setter" @mousedown="dragMouseDown(2, $event)" :style="{ left: `${columns.first + columns.second + columns.third}%`}"></div> 
            <div class="table-width-setter" @mousedown="dragMouseDown(3, $event)" :style="{ left: `${columns.first + columns.second + columns.third + columns.fourth}%`}"></div> 
          </div> 
          <v-data-table
            :hide-default-footer="CONFIG.table.hideDefaultFooter"
            :header-props="CONFIG.table.headerProps"
            :class="CONFIG.table.class"
            :footer-props="CONFIG.table.footerProps"
            :dense="CONFIG.table.dense"
            :height="windowHeight - 225"
            :fixed-header="CONFIG.table.fixedHeader"
            :headers="headers"
            :items="LOGS.items"
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
                  :length="pagination.totalItems"
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
              >
                <td :style="{width: `${columns.first}%`}">{{ props.item.Date }}</td>
                <td :style="{width: `${columns.second}%`}">{{ props.item.Author }}</td>
                <td :style="{width: `${columns.third}%`}">{{ LOG_TYPES.find(Type => Type.idr === props.item.TypeId).name }}</td>
                <td :style="{width: `${columns.fourth}%`}">{{ props.item.Action }}</td>
                <td :style="{width: `${columns.fifth}%`}">{{ props.item.description }}</td>
              </tr>
            </template>

            <template v-slot:item.Date="{ item }">{{item.Date}}</template>
            <template
              v-slot:item.Type="{ item }"
            >{{LOG_TYPES.find(Type => Type.idr === item.TypeId).name}}</template>
          </v-data-table>
        </v-card-text>
      </v-card>
    </v-col>
  </v-row>
</template>

<script>
import { mapGetters, mapActions } from "vuex";
import psPaginationPageSet from "@/components/pagination/ps-pagination-page-set";
import filters from "@/helpers/filters";

export default {
  name: "Logs",
  components: {
    psPaginationPageSet,
  },
  filters: {
    ...filters,
  },
  data() {
    return {
      pagination: {
        page: 1,
        rowsPerPage: 25,
        totalItems: 0,
        totalPages: 0,
      },
      windowHeight: window.innerHeight,
      tableWidth: 0,
      tableNotEmpty: false,
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
      headers: [
        {
          text: this.$vuetify.lang.t("$vuetify.date"),
          align: "start",
          value: "Date",
        },
        { text: this.$vuetify.lang.t("$vuetify.author"), value: "Author" },
        { text: this.$vuetify.lang.t("$vuetify.type"), value: "Type" },
        { text: this.$vuetify.lang.t("$vuetify.action"), value: "Action" },
        {
          text: this.$vuetify.lang.t("$vuetify.description"),
          value: 'description',
        },
      ],
    };
  },
  async mounted() {
    await this.FETCH_LOGS();
    this.pagination.totalItems = this.LOGS.items.length;
    this.pagination.totalPages =
      (await Math.floor(this.LOGS.items.length / this.pagination.rowsPerPage)) +
      1;
    this.FETCH_LOG_TYPES();
    setInterval(() => {
      if (window.innerHeight !== this.windowHeight) {
         this.windowHeight = window.innerHeight;
         this.tableWidth = this.$refs.tablecontainer.offsetWidth;
      }
      if (this.$refs.tablecontainer && this.tableWidth !== this.$refs.tablecontainer.offsetWidth) {
        this.tableWidth = this.$refs.tablecontainer.offsetWidth;
      }
      if (!this.$refs.tablecontainer) {
        this.tableNotEmpty = false;
      } else {
        this.tableNotEmpty = true;
      }
    }, 1000);
  },
  computed: {
    ...mapGetters(["LOGS", "LOG_TYPES", "CONFIG"]),
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
    ...mapActions(["FETCH_LOGS", "FETCH_LOG_TYPES"]),
    setPage(page) {
      this.pagination.page = parseInt(page);
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
      if (columns.first < 7.3 || columns.second < 4.4 || columns.third < 2.8 || columns.fourth < 5.8 || columns.fifth < 5.9) {
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
  },
};
</script>
