
<template>
  <v-row style="height: 100%" justify="center">
    <v-dialog
      v-if="infoWindow"
      no-click-animation
      persistent
      v-model="infoWindow"
      width="600px"
      content-class="border-radius-1"
      @keydown.esc="closeDialog"
    >
      <div class="popup-header ps-dialog-header height-40">
        <v-card-title class="white--text pb-2 pt-3 pl-1 font-size-16 font-weight-bold">{{message.title}}</v-card-title>
        <v-icon
          autofocus
          class="ps-btn-close pr-0 mr-1 warn-close"
          :color="CONFIG.colors.white"
          @click="closeDialog"
        >mdi-close</v-icon>
      </div>
      <v-card elevation="0">
        <div style="display:flex; justify-content: flex-start; padding-top: 42px;">
          <div style="margin-left: 30px;">
            <svg width="64" height="64" viewBox="0 0 64 64" fill="none" xmlns="http://www.w3.org/2000/svg" v-if="message.type==='warning'">
              <path d="M61.7597 46.18L39.9997 8.56001C39.1885 7.15633 38.0222 5.9908 36.6179 5.18049C35.2137 4.37018 33.621 3.9436 31.9997 3.9436C30.3785 3.9436 28.7858 4.37018 27.3815 5.18049C25.9773 5.9908 24.811 7.15633 23.9997 8.56001L2.25974 46.18C1.42677 47.5901 0.98179 49.1954 0.969968 50.833C0.958147 52.4707 1.37991 54.0823 2.19243 55.5042C3.00495 56.9261 4.17928 58.1077 5.59618 58.9289C7.01307 59.7501 8.62205 60.1818 10.2597 60.18H53.6597C55.2987 60.181 56.9091 59.7507 58.3292 58.9322C59.7492 58.1138 60.9289 56.9361 61.7497 55.5174C62.5705 54.0988 63.0035 52.4891 63.0053 50.8501C63.0071 49.2111 62.5775 47.6005 61.7597 46.18V46.18Z" stroke="#FF8419" stroke-linecap="round" stroke-linejoin="round"/>
              <path d="M32 51.76C31.0824 51.76 30.2023 51.3954 29.5534 50.7466C28.9046 50.0977 28.54 49.2176 28.54 48.3C28.54 47.3823 28.9046 46.5023 29.5534 45.8534C30.2023 45.2045 31.0824 44.84 32 44.84C32.4586 44.8422 32.9121 44.9371 33.333 45.1191C33.7539 45.3011 34.1337 45.5663 34.4495 45.8989C34.7653 46.2315 35.0105 46.6245 35.1704 47.0543C35.3303 47.4841 35.4016 47.9419 35.38 48.4C35.3748 49.2929 35.0163 50.1475 34.383 50.7771C33.7497 51.4066 32.893 51.76 32 51.76Z" fill="#FFBF8F" stroke="#FF8419" stroke-linecap="round" stroke-linejoin="round"/>
              <path d="M35.16 29.4L34.68 37.4C34.68 38.26 34.68 39.06 34.68 39.9C34.6749 40.2449 34.6011 40.5853 34.4631 40.9014C34.3251 41.2176 34.1256 41.5031 33.8762 41.7413C33.6268 41.9796 33.3324 42.1658 33.0103 42.2892C32.6882 42.4126 32.3448 42.4706 32 42.46C31.3297 42.4762 30.6801 42.2266 30.1931 41.7658C29.706 41.3049 29.4209 40.6702 29.4 40C29.16 35.82 28.92 31.74 28.7 27.56L28.46 24.26C28.4169 23.4204 28.6579 22.5908 29.1441 21.905C29.6303 21.2191 30.3335 20.7172 31.14 20.48C31.9355 20.2902 32.7723 20.3867 33.5038 20.7524C34.2352 21.1181 34.8145 21.7298 35.14 22.48C35.3684 23.0309 35.4775 23.6239 35.46 24.22C35.4 26 35.22 27.68 35.16 29.4Z" fill="#FFBF8F" stroke="#FF8419" stroke-linecap="round" stroke-linejoin="round"/>
            </svg>
            <svg width="64" height="66" viewBox="0 0 64 66" fill="none" xmlns="http://www.w3.org/2000/svg" v-if="message.type==='success'">
              <circle cx="32" cy="34" r="32" fill="#36D725" fill-opacity="0.5"/>
              <circle cx="32" cy="34" r="30.5" stroke="#36D725" stroke-opacity="0.8" stroke-width="3"/>
              <path d="M52 17.9995L25.3637 50.8071L11.5697 30.728" stroke="white" stroke-width="5" stroke-linecap="round" stroke-linejoin="round"/>
              <path d="M52 17.9995L25.3637 50.8071L11.5697 30.728" stroke="url(#paint0_linear)" stroke-width="5" stroke-linecap="round" stroke-linejoin="round"/>
              <path d="M52 17.9995L25.3637 50.8071L11.5697 30.728" stroke="url(#paint1_linear)" stroke-width="5" stroke-linecap="round" stroke-linejoin="round"/>
              <defs>
              <linearGradient id="paint0_linear" x1="18.5437" y1="46.7782" x2="39.8015" y2="10.7933" gradientUnits="userSpaceOnUse">
              <stop stop-color="#68E15B" stop-opacity="0"/>
              <stop offset="1" stop-color="#68E15B"/>
              </linearGradient>
              <linearGradient id="paint1_linear" x1="18.5437" y1="46.7782" x2="39.8015" y2="10.7933" gradientUnits="userSpaceOnUse">
              <stop stop-color="white"/>
              <stop offset="1" stop-color="white" stop-opacity="0"/>
              </linearGradient>
              </defs>
            </svg>

            <svg width="72" height="72" viewBox="0 0 72 72" fill="none" xmlns="http://www.w3.org/2000/svg" v-if="message.type==='error'">
              <circle cx="36" cy="36" r="30.5" fill="#D72525" fill-opacity="0.8" stroke="#D72525" stroke-width="3"/>
              <path d="M53.678 53.6778L18.323 18.3221" stroke="url(#paint0_linear)" stroke-width="5" stroke-linecap="round" stroke-linejoin="round"/>
              <path d="M53.678 53.6778L18.323 18.3221" stroke="url(#paint1_angular)" stroke-width="5" stroke-linecap="round" stroke-linejoin="round"/>
              <path d="M18.3223 53.6777L35.9999 36L53.6776 18.3223" stroke="url(#paint2_linear)" stroke-width="5" stroke-linecap="round" stroke-linejoin="round"/>
              <path d="M18.3223 53.6777L35.9999 36L53.6776 18.3223" stroke="url(#paint3_angular)" stroke-width="5" stroke-linecap="round" stroke-linejoin="round"/>
              <defs>
              <linearGradient id="paint0_linear" x1="35.7484" y1="36.2451" x2="36.2527" y2="35.7548" gradientUnits="userSpaceOnUse">
              <stop stop-color="white"/>
              <stop offset="1" stop-color="white" stop-opacity="0"/>
              </linearGradient>
              <radialGradient id="paint1_angular" cx="0" cy="0" r="1" gradientUnits="userSpaceOnUse" gradientTransform="translate(23.5 16.5) rotate(47.5638) scale(23.7118 18.1235)">
              <stop stop-color="white"/>
              <stop offset="1" stop-color="#D72525"/>
              </radialGradient>
              <linearGradient id="paint2_linear" x1="35.9999" y1="36" x2="36.707" y2="36.7071" gradientUnits="userSpaceOnUse">
              <stop stop-color="white"/>
              <stop offset="1" stop-color="white" stop-opacity="0"/>
              </linearGradient>
              <radialGradient id="paint3_angular" cx="0" cy="0" r="1" gradientUnits="userSpaceOnUse" gradientTransform="translate(57.0584 24.9595) rotate(137.498) scale(23.7255 25.757)">
              <stop stop-color="white"/>
              <stop offset="1" stop-color="#D72525"/>
              </radialGradient>
              </defs>
            </svg>
          </div>
          <div style="margin-left: 30px;font-size: 16px; font-weight: 500; margin-top: 18px; margin-right: 30px; line-height: 20px; color: rgba(0, 0, 0, 0.8);">{{message.text}}</div>
        </div>
        <v-card-actions class="mr-3 mt-5 pb-5"> 
          <v-spacer></v-spacer>
            <v-btn
            v-if="message.continue"
            class="ps-btn-perform white--text button-close-warning secondary border-radius-1 continue"
            :color="CONFIG.colors.secondary"
            @click="continueTask"
          >{{$vuetify.lang.t("$vuetify.continue")}}</v-btn>
          <v-btn
            ref="button"
            autofocus
            class="ps-btn-perform white--text button-close-warning border-radius-1 cancel"
            :color="CONFIG.colors.primary"
            @click="closeDialog"
          >{{$vuetify.lang.t("$vuetify.close")}}</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-slide-x-transition>
      <ps-switch-phone-owner-dialog
        :selectedPhone="selectedPhone"
        :isShow="switchPhoneOwnerDialog"
        :task="TASKS_LIST.find((t) => t.idr === this.selectedPhoneTask)"
        :uuid="uuid"
        @click.stop.prevent
        @error="saveError"
        @success="saveSuccess"
        v-on:close="closeSwitchPhoneOwnerPhone()"
      />
    </v-slide-x-transition>
    <v-col cols="12" style="height: 100%;" class="ps-col-wrapper">
      <v-card-title class="ps-card-title-top">
        <h1 class="page-header">{{$vuetify.lang.t('$vuetify.phones')}}</h1>
      </v-card-title>
      <v-card elevation="0" height="100%" class="d-flex flex-column">
        <v-card-actions class="ps-card-actions-top ps-card-actions-top__margins">
          <div class="search-width">
            <v-text-field
              @keyup="searchTimeOut()"
              hide-details="auto"
              class="ps-search-input font-size-16"
              v-model="search"
              :placeholder="$vuetify.lang.t('$vuetify.searchText')"
              append-icon="mdi-magnify"
              :background-color="CONFIG.colors.white"
              single-line
            ></v-text-field>
          </div>
          <v-spacer></v-spacer>
          <div class="width-210" style="display: none;">
            <v-select
              hide-details="auto"
              :placeholder="$vuetify.lang.t('$vuetify.selectTask')"
              append-icon="mdi-chevron-down"
              class="ps-select top-padding font-weight-bold font-size-16"
              :menu-props="{ bottom: true, offsetY: true }"
              ref="taskSelect"
              @change="handleSelectTask"
              :rules="[(val) => selectedPhone || $vuetify.lang.t('$vuetify.selectAtLeastOnePhone')]"
              dense
              outlined
              item-text="name"
              item-value="idr"
              v-model="selectedPhoneTask"
              :items="TASKS_LIST"
            >            
            </v-select>
          </div>
          <div class="width-210" style="height:42px; margin-left: 15px; position:relative; ">
            <div style="height:42px; background-color: #1976d3; cursor: pointer; display: flex; Justify-content: flex-start;" @click="openTaskSelection">
              <p style="color: #fff; font-size: 16px; padding-top: 12px; margin: 0; margin-left: 15px; margin-right: 0; font-weight: bold; line-height: 20px; width: 156px;">Выбрать задание</p>
              <i class="mdi mdi-chevron-down" style="width: 24px; height: 24px; color: #fff; font-size: 24px; margin-top: 3px;"></i>
            </div>
            <div v-if="selectionWindow" style="position:absolute; width: 210px; z-index: 6; background-color: #fff; top: 42px; box-shadow: 0px 0px 8px rgba(0, 0, 0, 0.25) !important;">
              <div v-for="task of TASKS_LIST" :key="`taskid-${task.idr}`" class="taskitem" @click="selectTask(task.idr)">
              {{task.name}}
              </div>
            </div>
            <div v-if="selectionError" style="position:absolute; z-index: 5; top: 46px;">
              <p style="font-size: 8px; color: red; margin: 0;">Выберите хотя бы один телефон</p>
            </div>
          </div>
        </v-card-actions>
        <v-card-text class="ps-card-text">
        <div :style="{ width: `${tableWidth}px`, position: 'relative'}" v-if="tableNotEmpty">
         <div class="table-width-setter" ref="firstBar" @mousedown="dragMouseDown(0,$event)" :style="{ left: `${columns.first}%`}"></div> 
         <div class="table-width-setter" ref="secondBar" @mousedown="dragMouseDown(1,$event)" :style="{ left: `${columns.first + columns.second}%`}"></div>
         <div class="table-width-setter" ref="thirdBar" @mousedown="dragMouseDown(2, $event)" :style="{ left: `${columns.first + columns.second + columns.third}%`}"></div> 
        </div>
          <v-data-table
            :options.sync="options"
            :server-items-length="PHONES.totalCount"
            :hide-default-footer="CONFIG.table.hideDefaultFooter"
            :header-props="CONFIG.table.headerProps"
            :class="CONFIG.table.class"
            :footer-props="CONFIG.table.footerProps"
            :fixed-header="CONFIG.table.fixedHeader"
            :dense="CONFIG.table.dense"
            ref="table"
            :height="windowHeight - 225"
            :headers="headers"
            :items="PHONES.items"
            :page.sync="pagination.page"
            :items-per-page.sync="pagination.rowsPerPage"
            :search="search"
            @page-count="pagination.totalPages"
          >
            <template v-slot:footer>
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
                    >{{(pagination.rowsPerPage * (pagination.page - 1))+1}}-{{pagination.rowsPerPage * (pagination.page ) > pagination.totalItems ? pagination.totalItems : pagination.rowsPerPage * (pagination.page )}}</div>
                    из {{pagination.totalItems}}
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
            <!-- row handle click -->
            <template v-slot:item="props">
              <tr ref="tablecontainer"
                style="cursor: pointer"
                @click="handleRowClick(props)"
                :class="{ 'v-data-table__selected':selectedPhone === props.item }"
              >
                <td :style="{width: `${columns.first}%`}">{{ props.item.phoneName }}</td>
                <td :style="{width: `${columns.second}%`}">{{ props.item.phoneDescription }}</td>
                <td :style="{width: `${columns.third}%`}">{{ props.item.phoneIpAddress }}</td>
                <td :style="{width: `${columns.fourth}%`}">{{ props.item.linePhoneNumber }}</td>
              </tr>
            </template>
            
          </v-data-table>
        </v-card-text>
      </v-card>
    </v-col>
  </v-row>
</template>

<script>
import psSwitchPhoneOwnerDialog from "@/components/dialogs/ps-switch-phone-owner-dialog"; /* eslint-disable */
import { mapGetters, mapActions } from "vuex";
import uniqid from "uniqid";
import { required } from "@/helpers/validators";
import psPaginationPageSet from "@/components/pagination/ps-pagination-page-set";
 import { Draggable } from 'draggable-vue-directive'

export default {
  name: "Phones",
  components: {
    psSwitchPhoneOwnerDialog,
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
      options: {},
      selectionWindow: false,
      selectionError: false,
      tableNotEmpty: false,
      tableWidth: 0,
      point: 0,
      activeBar: 0,
      columns: {
        first: 34.097,
        second: 18.657,
        third: 16.203,
        fourth: 31.042,
      },
      search: "",
      timer: null,
      headers: [
        {
          text: this.$vuetify.lang.t("$vuetify.name"),
          value: "phoneName",
        },
        {
          text: this.$vuetify.lang.t("$vuetify.description"),
          value: "phoneDescription",
        },
        {
          text: this.$vuetify.lang.t("$vuetify.ipAddress"),
          value: "phoneIpAddress",
        },
        {
          text: this.$vuetify.lang.t("$vuetify.phoneNumber"),
          value: "linePhoneNumber",
        },
      ],
      switchPhoneOwnerDialog: false,
      windowHeight: window.innerHeight,
      selectedPhone: null,
      uuid: null,
      infoWindow: false,
      message: {
        title: '',
        type: 'warning',
        text: '',
        continue: false,
      },
      statuses: [
        this.$vuetify.lang.t("$vuetify.isActive"),
        this.$vuetify.lang.t("$vuetify.isBlocked"),
        this.$vuetify.lang.t("$vuetify.isHidden"),
      ],
      phoneTasks: [
        { id: 1, name: this.$vuetify.lang.t("$vuetify.changeOfPhoneOwner") },
      ],
      selectedPhoneTask: null,
      nameRules: [() => required(this.editedItem.name)],
      pageSetErrors: [],
    };
  },
  watch: {
    "pagination.page": {
      async handler() {
        this.selectedPhone = null;
        const rowsPerPage = this.pagination.rowsPerPage;
        const page = this.pagination.page;
        await this.FETCH_PHONES({ 
          limit: rowsPerPage, 
          offset: (page - 1) * rowsPerPage, 
          search: this.search, 
          orderedColumn: this.options.sortBy[0], 
          sortDesc: this.options.sortDesc[0]
        });
      },
    },
    "pagination.rowsPerPage": {
      async handler() {
        this.selectedPhone = null;
        const rowsPerPage = this.pagination.rowsPerPage;
        const page = this.pagination.page;
        const res = await this.FETCH_PHONES({ 
          limit: rowsPerPage, 
          offset: 0, 
          search: this.search, 
          orderedColumn: this.options.sortBy[0], 
          sortDesc: this.options.sortDesc[0]
        });
        console.log(res)
        this.pagination.totalItems = res.totalCount.meta.count;
        this.pagination.totalPages =
          (Math.floor(
            res.totalCount.meta.count / this.pagination.rowsPerPage
        )) + 1;
      },
    },
    options: {
        async handler ({sortBy, sortDesc}) {
          const res = await this.FETCH_PHONES({ 
            limit: this.pagination.rowsPerPage, 
            offset: (this.pagination.page - 1) * this.pagination.rowsPerPage, 
            orderedColumn: sortBy[0], 
            sortDesc: sortDesc[0], 
            search: this.search}
          );
          console.log(res)
          this.pagination.totalItems = res.totalCount.meta.count;
          this.pagination.totalPages =
            (Math.floor(
              res.totalCount.meta.count / this.pagination.rowsPerPage
          )) + 1;
        },
      },
    selectionWindow(t){
      console.log(t)
    }
  },

  async mounted() {
    const res = await this.FETCH_PHONES({ limit: this.pagination.rowsPerPage, offset: 0 });
    console.log(res)
    this.pagination.totalItems = res.totalCount.meta.count;
    this.pagination.totalPages =
      (Math.floor(
        res.totalCount.meta.count / this.pagination.rowsPerPage
      )) + 1;
    console.log(this.pagination.totalPages)
    console.log(this.pagination)
    await this.FETCH_TASKS_LISTS();
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
    ...mapGetters(["PHONES", "CONFIG", "TASKS_LIST"]),
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
    ...mapActions(["FETCH_PHONES", "FETCH_TASKS_LISTS", "DEVICE_ACTIONS"]),
    searchTimeOut() {  
      if (this.timer) {
        clearTimeout(this.timer);
        this.timer = null;
      }
      console.log(this.options)
      this.timer = setTimeout(() => {
        this.FETCH_PHONES({ limit: this.pagination.rowsPerPage, offset: this.pagination.page - 1, search: this.search, orderedColumn: this.options.sortBy[0], sortDesc: this.options.sortDesc[0]});
      }, 1000);
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
      console.log(width)
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
      if (columns.first < 10 || columns.second < 7 || columns.third < 6 || columns.fourth < 10) {
        this.point = e.clientX;
        return;
      }
      console.log(this.TASKS_LIST)
      this.columns = columns;
      this.point = e.clientX;
    },
    closeDragElement() {
      document.onmouseup = null;
      document.onmousemove = null;
    },
    makeWider(type, e) {
      event.stopPropagation();
      const columns = {...this.columns};
  
      const keys = Object.keys(columns)
      columns.first -= 1;
      columns.second -= 1;
      columns.third -= 1;
      columns.fourth -= 1;
      columns[type] += 4;
      if (columns.first < 15) return;
      if (columns.third < 10) return;
      if (columns.fourth < 15) return;
      this.columns = columns;
    },
    makeNarrow(type, e) {
      event.stopPropagation();
      const columns = {...this.columns};
      const keys = Object.keys(columns)
      columns.first += 1;
      columns.second += 1;
      columns.third += 1;
      columns.fourth += 1;
      columns[type] -= 4;
      if (columns.first < 15) return;
      if (columns.third < 10) return;
      if (columns.fourth < 15) return;
      this.columns = columns;
    },
    setPage(page) {
      this.pagination.page = parseInt(page);
      this.pageSetErrors = [];
    },
    handlePageSetErrors(errors) {
      this.pageSetErrors = errors;
    },
    async handleSelectTask(task) {

      try {
      if (this.$refs.taskSelect.validate()) {
        const taksObj = this.TASKS_LIST.find((t) => t.idr === task)
          // Cмена владельца телефона
          console.dir(this.selectedPhoneTask)
          console.dir(this.TASKS_LIST)
          console.log(this.selectedPhone)
          console.log(taksObj)
          const uuid = uniqid('', `-${uniqid.time()}-${taksObj.idr}-preview`);
          const data = {
            meta: {
              UUID: uuid,
              action: 'preview',
              },
            task: {
              taskTypeId: taksObj.idr,
              deviceId: this.selectedPhone.phoneId,
            }
          }
          const response = await this.DEVICE_ACTIONS(data)
          console.log(response)
          if (response.status === 202) {
             console.log(response.data.status.code)
            if (response.data.status.code === 1122 || response.data.status.code === 1132) {
              this.switchPhoneOwnerDialog = true;
            } else if (response.data.status.code === 2132) {
              this.message.title = `Предупреждение для номера ${this.selectedPhone.linePhoneNumber || ''}`;
              this.message.type = 'warning';
              this.message.text = response.data.status.message;
              this.message.continue = true;
              this.infoWindow = true;
            }
          }
          this.uuid= uuid;
      } else {
        this.switchPhoneOwnerDialog = await false;
        this.selectedPhoneTask = await null;
      }
      document.activeElement.blur();
      } catch(e) {
        console.dir(e)
        if (e.response.status === 400) {
          this.message.title = `Задание для номера ${this.selectedPhone.linePhoneNumber || ''}`;
          this.message.type = 'error';
          this.message.text = 'В данный момент добавление заданий недоступно';
          this.message.continue = false;
          this.infoWindow = true;
        } else if (e.response.status === 403) {
          this.message.title = `Предупреждение для номера ${this.selectedPhone.linePhoneNumber || ''}`;
          this.message.type = 'warning';
          this.message.text = e.response.data.status.message;
          this.message.continue = false;
          this.infoWindow = true;
        } else if (e.response.status === 401) {
          this.message.title = `Задание для номера ${this.selectedPhone.linePhoneNumber || ''}`;
          this.message.type = 'error';
          this.message.text = 'Ошибка авторизации, пожалуйста, повторите процесс авторизации и попробуйте снова';
          this.message.continue = false;
          this.infoWindow = true;
        }
        
        this.uuid = null;
        this.switchPhoneOwnerDialog = false;
        this.selectedPhoneTask = null;
      }
    },
    async continueTask() {
      try {
        const taksObj = this.TASKS_LIST.find((t) => t.idr === this.selectedPhoneTask)
        const data = {
          meta: {
            UUID: this.uuid,
            action: 'continue',
            },
          task: {
            taskTypeId: taksObj.idr,
            deviceId: this.selectedPhone.phoneId,
          }
        }
        console.log(data)
        const response = await this.DEVICE_ACTIONS(data);
        console.log(response)
        if (response.status === 202) {
          console.log(response.status)
          if (response.data.status.code === 1132) {
            this.infoWindow = false;
            this.message.continue = false;
            this.switchPhoneOwnerDialog = true;
          } 
        }
      } catch(e) {
        if (e.response.status === 400) {
          this.message.title = `Задание для номера ${this.selectedPhone.linePhoneNumber || ''}`;
          this.message.type = 'error';
          this.message.text = 'В данный момент добавление заданий недоступно';
          this.message.continue = false;
          this.infoWindow = true;
          this.uuid = null;
        } 
        this.switchPhoneOwnerDialog = false;
        this.selectedPhoneTask = null;
      }
    },
    async cancelTask() {
      try {
        const taksObj = this.TASKS_LIST.find((t) => t.idr === this.selectedPhoneTask)
        const data = {
          meta: {
            UUID: this.uuid,
            action: 'cancel',
            },
          task: {
            taskTypeId: taksObj.idr,
            deviceId: this.selectedPhone.phoneId,
          }
        }
        const response = await this.DEVICE_ACTIONS(data);
        console.log(response)
      } catch(e) {
        if (e.response.status === 400) {
          this.message.title = `Задание для номера ${this.selectedPhone.linePhoneNumber || ''}`;
          this.message.type = 'error';
          this.message.text = 'В данный момент добавление заданий недоступно';
          this.message.continue = false;
          this.infoWindow = true;
          this.uuid = null;
        } else if (e.response.status === 410) {
          this.uuid = null;
        }
        this.switchPhoneOwnerDialog = false;
        this.selectedPhoneTask = null;
      }
    },
    closeDialog() {
      if (this.message.continue) {
        this.infoWindow = false;
        this.cancelTask();
      } else {
        this.infoWindow = false;
      }
    },
    saveError() {
      this.message.title = `Задание для номера ${this.selectedPhone.linePhoneNumber || ''}`;
      this.message.type = 'error';
      this.message.text = 'В данный момент добавление заданий недоступно';
      this.message.continue = false;
      this.switchPhoneOwnerDialog = false;
      this.selectedPhoneTask = null;
      this.infoWindow = true;
      this.uuid = null;
    },
    saveSuccess(message) {
      this.message.title = `Задание для номера ${this.selectedPhone.linePhoneNumber || ''}`;
      this.message.type = 'success';
      this.message.text = message;
      this.message.continue = false;
      this.switchPhoneOwnerDialog = false;
      this.selectedPhoneTask = null;
      this.infoWindow = true;
      this.uuid = null;      
    },
    closeSwitchPhoneOwnerPhone() {
      this.cancelTask();
    },
    handleRowClick(props) {
      this.selectedPhone = props.item;
    },
    openTaskSelection() {
      if (this.selectionWindow) {
        this.selectionWindow = false;
      } else {
        this.selectionWindow = true;
      }

    },
    selectTask(id) {
      if (this.selectedPhone === null) {
        this.selectionError = true;
        this.selectionWindow = false;
      } else {
        this.selectionError = false;
        this.selectionWindow = false;
        this.selectedPhoneTask = id;
        this.handleSelectTask(id);
      }
    }
  },
};
</script>


<style lang="scss">
@import "src/scss/self/main.scss";
.v-text-field__details {
  background: red !important;
}
</style>


