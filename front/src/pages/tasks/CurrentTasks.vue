
<template>
  <v-row style="height: 100%" justify="center">
    <v-dialog
      v-if="infoWindow"
      no-click-animation
      persistent
      v-model="infoWindow"
      width="600px"
      @keydown.esc="infoWindow = false"
    >
      <div class="popup-header ps-dialog-header height-40">
        <v-card-title class="white--text pb-2 pt-3 pl-1 font-size-16 font-weight-bold">{{message.title}}</v-card-title>
        <v-icon
          autofocus
          class="ps-btn-close pr-0 mr-1 warn-close"
          :color="CONFIG.colors.white"
          @click="infoWindow = false"
        >mdi-close</v-icon>
      </div>
      <v-card elevation="0">
        <div style="display:flex; justify-content: space-between; padding-top: 47px;">
          <div style="margin-left: 26px;">
            <img src="../../public/fail.png" width=72 height=72>
          </div>
          <div style="margin-left: 27px;font-size: 16px; font-weight: 500; margin-top: 7px; margin-right: 30px; line-height: 20px; color: rgba(0, 0, 0, 0.8);"><p style="margin-bottom: 0;">{{message.text1}}</p><p style="margin-bottom: 0;">{{message.text2}}</p></div>
        </div>
        <v-card-actions class="mr-3 mt-2 pb-5"> 
          <v-spacer></v-spacer>
          <v-btn
            ref="button"
            autofocus
            class="ps-btn-perform white--text button-close-warning"
            :color="CONFIG.colors.primary"
            @click="infoWindow = false"
          >{{$vuetify.lang.t("$vuetify.close")}}</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-col cols="12" style="height: 100%" class="ps-col-wrapper">
      <v-slide-x-transition>
        <v-dialog
          v-if="settingsDialog"
          no-click-animation
          persistent
          width="651px"
          v-model="settingsDialog"
          @keydown.esc="settingsDialog = false"
        >
          <v-card elevation="0" height="100%" class="d-flex flex-column">
            <div class="popup-header">
              <v-card-title
                class="pl-3 font-size-16 font-weight-bold"
              >Просмотр текущего задания для номера {{editedItem.devicePhoneNumber || ""}}</v-card-title>
              <v-icon
                class="white--text"
                @click="settingsDialog = false"
                :color="CONFIG.colors.white"
              >mdi-close</v-icon>
            </div>
            <v-row no-gutters class="pl-7 pr-7">
              <v-card-title class="pl-0 pb-2 font-size-16">{{editedItem.taskType}}</v-card-title>
              <v-spacer></v-spacer>
              <v-card-subtitle class="pl-0 pr-2 pb-2 pt-6 font-weight-medium">Номер задания: {{editedItem.taskId}}</v-card-subtitle>
              <v-card-subtitle class="pl-0 pr-0 pb-2 pt-6 font-weight-medium">Автор: {{editedItem.userLogin}}</v-card-subtitle>
            </v-row>
            <v-divider class="ml-7 mr-7"></v-divider>
            <v-card-text style="height: auto">
              <v-form :disabled="settingsDialog" ref="form" lazy-validation class="pr-1 pl-1">
                <v-row>
                  <v-col class="pr-5 pl-3 pt-6 pb-0">
                    <v-row align="top" :class="CONFIG.form.rowClass">
                      <v-col
                        :cols="CONFIG.vuetify.cols.labelCols"
                        :class="CONFIG.form.labelColClass2"
                      >
                        <v-subheader
                          :class="CONFIG.form.labelClass"
                        >{{$vuetify.lang.t('$vuetify.description')}}</v-subheader>
                      </v-col>
                      <v-col :class="CONFIG.form.fieldColClassNopadding">
                        <v-text-field
                          autofocus
                          dense
                          outlined
                          :rules="[required, baseRule, maxLength128 ]"
                          v-model="editedItem.taskDescription"
                          :placeholder="$vuetify.lang.t('$vuetify.description')"
                        ></v-text-field>
                      </v-col>
                    </v-row>
                    <v-row align="top" :class="CONFIG.form.rowClass">
                      <v-col
                        :cols="CONFIG.vuetify.cols.labelCols"
                        :class="CONFIG.form.labelColClass2"
                      >
                        <v-subheader :class="CONFIG.form.labelClass">Тестовый сервер</v-subheader>
                      </v-col>
                      <v-col :class="CONFIG.form.fieldColClassNopadding">
                        <v-select
                          class="disable-select"
                          dense
                          outlined
                          :rules="[testServerIsRequired]"
                          :items="SERVERS.items"
                          :placeholder="$vuetify.lang.t('$vuetify.unchoosen')"
                          item-value="idr"
                          item-text="name"
                          :menu-props="{ bottom: true, offsetY: true }"
                          :disabled="!editedItem.isTestServer"
                          v-model="editedItem.serverTestId"
                        ></v-select>
                        <div class="select-reset" @click="editedItem.testServerId = null" v-if="editedItem.isTestServer">
                          <svg width="10" height="10" viewBox="0 0 12 12" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <circle cx="6" cy="6" r="5" fill="#C4C4C4"/>
                            <path d="M3 9L9 3M3 3L9 9" stroke="white"/>
                          </svg>
                        </div>
                      </v-col>
                    </v-row>
                    <v-row align="top" :class="CONFIG.form.rowClass" justify="center">
                      <v-col
                        :cols="CONFIG.vuetify.cols.labelCols"
                        :class="CONFIG.form.labelColClass2"
                      >
                        <v-subheader :class="CONFIG.form.labelClass">Дата, время запуска</v-subheader>
                      </v-col>
                      <v-col cols="8" :class="CONFIG.form.fieldColClassNopadding2">
                        <div style="display: flex; justify-content: space-between; width: 421px">
                          <div class="datetime-wrapper-current width-421">
                            <div class="pr-3 date-picker-wrapper-width">
                              <v-menu
                                content-class="ps-datepicker"
                                :close-on-content-click="false"
                                transition="scale-transition"
                                offset-y
                              >
                                <template v-slot:activator="{ on, attrs }">
                                  <v-text-field
                                    clearable
                                    clear-icon="mdi-close-circle"
                                    hide-details
                                    ref="runTimeDate"
                                    dense
                                    outlined
                                    :rules="[runDateIsValidCheck]"
                                    full-width
                                    v-model="editedItem.taskDateRunDate"
                                    :placeholder="$vuetify.lang.t('$vuetify.datePlaceholder')"
                                    readonly
                                    v-bind="attrs"
                                    v-on="on"
                                  ></v-text-field>
                                </template>
                                <v-date-picker
                                  hide-details
                                  no-title
                                  show-current
                                  :rules="[runDateIsValidCheck]"
                                  @input="menu2 = false"
                                  v-model="editedItem.runTimeDate"
                                ></v-date-picker>
                              </v-menu>
                            </div>
                            <div class="time-input-wrapper single width-100 pr-1">
                              <v-text-field
                                class="width-100"
                                clearable
                                clear-icon="mdi-close-circle"
                                hide-details
                                @change="validateDate()"
                                dense
                                outlined
                                :rules="[runDateIsValidCheck]"
                                v-model="editedItem.taskDateRunTime"
                                placeholder="00"
                                ref="timeField"

                                @focus="setTimeField()"
                              ></v-text-field>
                            </div>
                          </div>
                        </div>
                        <!-- <v-slide-x-transition v-if="editedItem.runTimeDate">
                          <div style="color: red; font-size: 8px">Ошибка неверно указана дата</div>
                        </v-slide-x-transition>-->
                      </v-col>
                    </v-row>
                    <v-row align="top" class="mt-7" :class="CONFIG.form.rowClass">
                      <v-card-subtitle
                        :class="CONFIG.form.labelClass"
                        class="font-weight-bold"
                      >{{$vuetify.lang.t('$vuetify.telephoneSet')}}</v-card-subtitle>
                    </v-row>
                    <v-row align="top" :class="CONFIG.form.rowClass">
                      <v-col
                        :cols="CONFIG.vuetify.cols.labelCols"
                        :class="CONFIG.form.labelColClass2"
                      >
                        <v-subheader
                          :class="CONFIG.form.labelClass"
                        >Description</v-subheader>
                      </v-col>
                      <v-col :class="CONFIG.form.fieldColClassNopadding">
                        <v-text-field
                          dense
                          outlined
                          :rules="[required, maxLength50, baseRule ]"
                          v-model="editedItem.device.Description"
                          :placeholder="$vuetify.lang.t('$vuetify.description')"
                        ></v-text-field>
                      </v-col>
                    </v-row>
                    <v-row align="top" :class="CONFIG.form.rowClass">
                      <v-col
                        :cols="CONFIG.vuetify.cols.labelCols"
                        :class="CONFIG.form.labelColClass2"
                      >
                        <v-subheader :class="CONFIG.form.labelClass">Owner User ID</v-subheader>
                      </v-col>
                      <v-col :class="CONFIG.form.fieldColClassNopadding">
                        <v-text-field
                          dense
                          outlined
                          :rules="[required, maxLength50, baseRule ]"
                          v-model="editedItem.device['Owner User ID']"
                          :placeholder="$vuetify.lang.t('$vuetify.userLogin')"
                        ></v-text-field>
                      </v-col>
                    </v-row>
                    <v-row align="top" class="mt-2" :class="CONFIG.form.rowClass">
                      <v-card-subtitle
                        :class="CONFIG.form.labelClass"
                        class="font-weight-bold"
                      >{{$vuetify.lang.t('$vuetify.line')}}</v-card-subtitle>
                    </v-row>
                    <v-row align="top" :class="CONFIG.form.rowClass">
                      <v-col
                        :cols="CONFIG.vuetify.cols.labelCols"
                        :class="CONFIG.form.labelColClass2"
                      >
                        <v-subheader
                          :class="CONFIG.form.labelClass"
                        >Description</v-subheader>
                      </v-col>
                      <v-col :class="CONFIG.form.fieldColClassNopadding">
                        <v-text-field
                          dense
                          outlined
                          :rules="[required, maxLength50, baseRule ]"
                          v-model="editedItem.device['Line']['Description']"
                          :placeholder="$vuetify.lang.t('$vuetify.description')"
                        ></v-text-field>
                      </v-col>
                    </v-row>
                    <v-row align="top" :class="CONFIG.form.rowClass">
                      <v-col
                        :cols="CONFIG.vuetify.cols.labelCols"
                        :class="CONFIG.form.labelColClass2"
                      >
                        <v-subheader :class="CONFIG.form.labelClass">Alerting Name</v-subheader>
                      </v-col>
                      <v-col :class="CONFIG.form.fieldColClassNopadding">
                        <v-text-field
                          dense
                          outlined
                          :rules="[required, maxLength50, baseRule ]"
                          v-model="editedItem.device['Line']['Alerting Name']"
                          :placeholder="$vuetify.lang.t('$vuetify.description')"
                        ></v-text-field>
                      </v-col>
                    </v-row>
                    <v-row align="top" :class="CONFIG.form.rowClass">
                      <v-col
                        :cols="CONFIG.vuetify.cols.labelCols"
                        :class="CONFIG.form.labelColClass2"
                      >
                        <v-subheader :class="CONFIG.form.labelClass">ASCII Alerting Name</v-subheader>
                      </v-col>
                      <v-col :class="CONFIG.form.fieldColClassNopadding">
                        <v-text-field
                          dense
                          outlined
                          :rules="[required, maxLength50, baseRule ]"
                          v-model="editedItem.device['Line']['ASCII Alerting Name']"
                          :placeholder="$vuetify.lang.t('$vuetify.lastNameFirstNameInEnglish')"
                        ></v-text-field>
                      </v-col>
                    </v-row>
                    <v-row align="top" :class="CONFIG.form.rowClass">
                      <v-col
                        :cols="CONFIG.vuetify.cols.labelCols"
                        :class="CONFIG.form.labelColClass2"
                      >
                        <v-subheader :class="CONFIG.form.labelClass">Display (Caller ID)</v-subheader>
                      </v-col>
                      <v-col :class="CONFIG.form.fieldColClassNopadding">
                        <v-text-field
                          dense
                          outlined
                          :rules="[required, maxLength50, baseRule ]"
                          v-model="editedItem.device['Line']['Display (Caller ID)']"
                          :placeholder="$vuetify.lang.t('$vuetify.description')"
                        ></v-text-field>
                      </v-col>
                    </v-row>
                    <v-row align="top" :class="CONFIG.form.rowClass">
                      <v-col
                        :cols="CONFIG.vuetify.cols.labelCols"
                        :class="CONFIG.form.labelColClass2"
                      >
                        <v-subheader :class="CONFIG.form.labelClass">ASCII Display (Caller ID)</v-subheader>
                      </v-col>
                      <v-col :class="CONFIG.form.fieldColClassNopadding">
                        <v-text-field
                          dense
                          outlined
                          :rules="[required, maxLength50, baseRule ]"
                          v-model="editedItem.device['Line']['ASCII Display (Caller ID)']"
                          :placeholder="$vuetify.lang.t('$vuetify.description')"
                        ></v-text-field>
                      </v-col>
                    </v-row>
                    <v-row align="top" :class="CONFIG.form.rowClass">
                      <v-col
                        :cols="CONFIG.vuetify.cols.labelCols"
                        :class="CONFIG.form.labelColClass2"
                      >
                        <v-subheader :class="CONFIG.form.labelClass2">User Associated with Line</v-subheader>
                      </v-col>

                      <v-col :class="CONFIG.form.fieldColClassNopadding">
                        <v-text-field
                          dense
                          outlined
                          :rules="[required, maxLength50, baseRule ]"
                          v-model="editedItem.device['Line']['User Associated with Line']"
                          :placeholder="$vuetify.lang.t('$vuetify.userLogin')"
                        ></v-text-field>
                      </v-col>
                    </v-row>
                  </v-col>
                </v-row>
              </v-form>
            </v-card-text>
            <v-card-actions class="pt-0 pb-7 mr-5 ml-5">
              <v-spacer></v-spacer>
              <v-btn autofocus color="secondary" class="button-close" style="color: #fff;" @click="settingsDialog = false">Закрыть</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-slide-x-transition>
      <!-- Журнал -->
      <v-card-title class="ps-card-title-top">
        <h1 class="page-header">{{$vuetify.lang.t('$vuetify.current')}}</h1>
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
        </v-card-actions>
        <v-card-text class="ps-card-text">
          <div :style="{ width: `${tableWidth}px`, position: 'relative'}" v-if="tableNotEmpty">
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
            :dense="CONFIG.table.dense"
            :height="windowHeight - 225"
            ref="table"
            :fixed-header="CONFIG.table.fixedHeader"
            :headers="headers"
            :server-items-length="CURRENT_TASKS.totalCount"
            :items="CURRENT_TASKS.items"
            :page.sync="pagination.page"
            :items-per-page.sync="pagination.rowsPerPage"
            :search="search"
            @page-count="pagination.totalPages = $event"
            single-select
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
                        append-icon
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
            <!-- row handle click -->
            <template v-slot:item="props">
              <tr
                ref="tablecontainer"
                style="cursor: pointer"
                @click="showSettingsDialog(props.item)"
                :class="{ 'v-data-table__selected':selectedTask === props.item }"
              >
                <td :style="{width: `${columns.first}%`}">{{ props.item.taskId }}</td>
                <td :style="{width: `${columns.second}%`}">{{ props.item.taskType }}</td>
                <td :style="{width: `${columns.third}%`}">{{ props.item.taskDescription }}</td>
                <td :style="{width: `${columns.fourth}%`}">{{ props.item.taskDateRun | formatDateTime }}</td>
                <td :style="{width: `${columns.fifth}%`}">
                  <div v-if="props.item.serverTestId && props.item.serverFQDN" class="icon-with-text-wrapper">
                    <div class="table-icon-wrapper">
                      <div class="table-icon-wrapper">
                        <svg-icon name="done" />
                      </div>
                    </div>
                    {{props.item.serverTestId}}
                  </div>
                  <div v-else class="icon-with-text-wrapper">
                    <div class="table-icon-wrapper">
                      <div class="table-icon-wrapper">
                        <svg-icon name="fail" />
                      </div>
                    </div>
                    {{$vuetify.lang.t('$vuetify.no')}}
                  </div>
                </td>
              </tr>
            </template>
          </v-data-table>
        </v-card-text>
      </v-card>
    </v-col>
  </v-row>
</template>

<script>
import { required, maxLength50, maxLength128 } from "@/helpers/validators";
import { mapGetters, mapActions } from "vuex";
import psPaginationPageSet from "@/components/pagination/ps-pagination-page-set";
import svgIcon from "@/components/svg-icons/svg-icon";
import filters from "@/helpers/filters";

export default {
  name: "CurrentTasks",
  components: {
    svgIcon,
    psPaginationPageSet,
  },
  filters: {
    ...filters,
  },
  data() {
    return {
      search: "",
      tableNotEmpty: false,
      infoWindow: false,
      message: {
        title: '',
        text: ''
      },
      timer: null,
      options: {},
      tableWidth: 0,
      point: 0,
      activeBar: 0,
      columns: {
        first: 15,
        second: 20,
        third: 20,
        fourth: 20,
        fifth: 25,
      },
      headers: [
        {
          text: "№",
          value: "taskId",
        },
        { text: this.$vuetify.lang.t("$vuetify.task"), value: "taskType" },
        {
          text: this.$vuetify.lang.t("$vuetify.description"),
          value: "taskDescription",
        },
        { text: this.$vuetify.lang.t("$vuetify.dateRun"), value: "dateRun" },
        {
          text: `${this.$vuetify.lang.t("$vuetify.isTestServer")}`,
          value: "isTesting",
        },
      ],
      pagination: {
        page: 1,
        rowsPerPage: 25,
        totalItems: 0,
        totalPages: 0,
      },
      windowHeight: window.innerHeight,
      settingsDialog: false,
      editedIndex: -1,
      editedItem: {
        idr: Date.now(),
        name: null,
        description: null,
        isTestServer: false,
        TestServerIdr: null,
        isRunNow: false,
        runTimeDate: null,
        runTimeHoursMinutes: "00:00",
        device: {
          description: null,
          'Owner User ID': null,
          line: {
            'ASCII Alerting Name': null,
            'ASCII Display (Caller ID)': null,
            'Alerting Name': null,
            'Description': null,
            'Display (Caller ID)': null,
            'User Associated with Line': null,
          }
        },
        line: {
          description: null,
          alertingName: null, // link: line.description
          asciiAlertingName: null,
          display: null, // link: line.description
          asciiDisplay: null, // link: line.asciiAlertingName
          userAssociated: null, // link: telephone.login
        },
      },
      defaultItem: {
        idr: Date.now(),
        devicePhoneNumber: '',
        taskType: "Смена владельца телефона",
        taskDescription: null,
        serverName: null,
        ServerTestBench: null,
        serverId: null,
        taskDateRunDate: null,
        taskDateRunTime: null,
        device: {
          description: null,
          'Owner User ID': null,
          Line: {
            'ASCII Alerting Name': null,
            'ASCII Display (Caller ID)': null,
            'Alerting Name': null,
            'Description': null,
            'Display (Caller ID)': null,
            'User Associated with Line': null,
          }
        },
      },
      selectedTask: null,
      // rules
      baseRule: (text) => {
        const e =
          new RegExp("^[a-zA-Z0-9 !#$'()*+,./:;=?@^_`}~-]{0,1000}$").test(
            text
          ) || this.$vuetify.lang.t("$vuetify.invalidCharacters"); //eslint-disable-line
        console.log("valid", e);
        return e;
      },
      required: (text) => {
        return (
          required(text) ||
          this.$vuetify.lang.t("$vuetify.theFieldMustNotBeEmpty")
        );
      },
      maxLength50: (text) => {
        return (
          maxLength50(text) ||
          this.$vuetify.lang.t("$vuetify.allowedNumberOfCharacters50")
        );
      },
      maxLength128: (text) => {
        return (
          maxLength128(text) ||
          this.$vuetify.lang.t("$vuetify.allowedNumberOfCharacters128")
        );
      },
      testServerIsRequired: () => {
        if (this.editedItem.isTestServer) {
          return (
            this.editedItem.testServerid != null ||
            this.$vuetify.lang.t("$vuetify.testServerNotSelected")
          );
        } else {
          return true;
        }
      },
      runDateIsValidCheck: () => {
        if (!this.editedItem.isRunNow) {
          if (
            this.editedItem.runTimeDate != null &&
            this.editedItem.runTimeHoursMinutes != null
          ) {
            const datetime = new Date(
              this.editedItem.runTimeDate +
                " " +
                this.editedItem.runTimeHoursMinutes
            );
            console.log("date", datetime);
            return (
              datetime > new Date() ||
              this.$vuetify.lang.t("$vuetify.theTaskStartTimeIsInThePast")
            );
          } else {
            return this.$vuetify.lang.t("$vuetify.dateAndTimeAreRequired");
          }
        } else {
          return true;
        }
      },
    };
  },
  watch: {
    "pagination.page": {
      handler() {
        this.FETCH_CURRENT_TASKS_SELECTION({
          limit: this.pagination.rowsPerPage,
          offset: this.pagination.page - 1,
        });
      },
    },
    "pagination.rowsPerPage": {
      handler() {
        this.FETCH_CURRENT_TASKS_SELECTION({
          limit: this.pagination.rowsPerPage,
          offset: this.pagination.page - 1,
        });
      },
    },
    options: {
        handler ({sortBy, sortDesc}) {
          console.log(this.options)
          this.FETCH_CURRENT_TASKS_SELECTION({ limit: this.pagination.rowsPerPage, offset: this.pagination.page - 1, orderedColumn: sortBy[0], orderDesc: sortDesc[0], search: this.search})
        },
      },
  },
  async mounted() {
    await this.FETCH_CURRENT_TASKS_SELECTION({ 
      limit: this.pagination.rowsPerPage, 
      offset: this.pagination.page - 1, 
    });
    this.pagination.totalItems = await this.CURRENT_TASKS.totalCount;
    this.pagination.totalPages =
      (await Math.floor(
        this.CURRENT_TASKS.totalCount / this.pagination.rowsPerPage
      )) + 1;
    this.FETCH_SERVERS({ limit: 100, offset: 0 });
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
    ...mapGetters(["CURRENT_TASKS", "SERVERS", "CONFIG", "TASK_CURRENT"]),
    timeFieldType() {
      if (this.editedItem.runTimeHoursMinutes) {
        return "time";
      } else {
        return "text";
      }
    },
    computedRunTimeDateFormatted() {
      return this.formatDate(this.editedItem.runTimeDate);
    },
    computedEndTimeDateFormatted() {
      return this.formatDate(this.editedItem.runTimeDate);
    },
    itemsPerPageOptionsCols() {
      return this.settingsDialog ? "2" : "1";
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
    ...mapActions(["FETCH_CURRENT_TASKS", "FETCH_CURRENT_TASKS_SELECTION", "FETCH_SERVERS", "FETCH_TASK"]),
    ...filters,
    searchTimeOut() {  
      if (this.timer) {
        clearTimeout(this.timer);
        this.timer = null;
      }
      this.timer = setTimeout(() => {
        this.FETCH_CURRENT_TASKS_SELECTION({ limit: this.pagination.rowsPerPage, offset: this.pagination.page - 1, search: this.search, orderedColumn: this.options.sortBy[0], orderDesc: this.options.sortDesc[0]});
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
      this.tableWidth = width;
      const pos = this.point - 35;
      const position = +(((pos/width)*100).toFixed(3));
      const keys = Object.keys(columns);
      let diffTotal = position;
      for (let i = 0; i <= activeBar; i += 1) {
        diffTotal = diffTotal - columns[keys[i]];
      }
      console.log(diffTotal)
      console.log(activeBar)
      let result = position;
      for (let i = activeBar -1; i >= 0; i -= 1) {
        result = result - columns[keys[i]]
      }
      columns[keys[activeBar]] = result;
      columns[keys[activeBar + 1]] = columns[keys[activeBar + 1]] - diffTotal;
      if (columns.first < 3 || columns.second < 5 || columns.third < 6 || columns.fourth < 10 || columns.fifth < 10) {
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
    setTimeField(){
      this.editedItem.runTimeHoursMinutes = "00:00"
    },
    setPage(page) {
      this.pagination.page = parseInt(page);
      this.pageSetErrors = [];
    },
    handlePageSetErrors(errors) {
      this.pageSetErrors = errors;
    },
    async showSettingsDialog(item) {
      try {
        const data = {taskNumber: item.taskId, type: 'current'}
        const res = await this.FETCH_TASK(data);
        console.log(res)
        if (res.deviceJson.length > 0) {
          const itemData = JSON.parse(res.deviceJson);
          res.device = itemData.Device
        }

        res.taskDateRunDate = res.taskDateRun.split(' ')[0]
        res.taskDateRunTime = res.taskDateRun.split(' ')[1]
        this.editedItem = Object.assign(this.defaultItem, {
          ...res,
        });
        console.log(this.editedItem)
        this.selectedTask = item;
        this.settingsDialog = true;
      } catch(e) {
        console.log(e)
        if (e.response && e.response.status === 401) {
          this.infoWindow = true;
          this.message.title = `Ошибка при получении задания ${item.number}`;
          this.message.text1 = 'Ошибка авторизации, пожалуйста, повторите';
          this.message.text2 = 'процесс авторизации и попробуйте снова';
        } else {
          this.infoWindow = true;
          this.message.title = `Ошибка при получении задания ${item.number}`;
          this.message.text1 = 'В данный момент, детальная информация по заданию недоступна';
          this.message.text2 = 'Попробуйте повторить запрос позже';
        }
      }
    },
    validateDate() {
      this.$refs.runTimeDate.validate(); // валидирует поле даты
    },
  },
};
</script>
