
<template>
  <v-row style="height: 100%" justify="space-between">
    <v-dialog
      v-if="currentVersionDialog"
      no-click-animation
      persistent
      v-model="currentVersionDialog"
      width="600px"
      content-class="border-radius-1"
      @keydown.esc="currentVersionDialog = false"
    >
      <div class="popup-header ps-dialog-header height-40 pr-5 border-radius-1-nobottom">
        <v-card-title class="white--text pb-3 pt-3 pl-1 font-size-16 font-weight-bold">{{$vuetify.lang.t('$vuetify.warning')}}</v-card-title>
        <v-icon
          autofocus
          class="ps-btn-close pr-0 mr-0 warn-close"
          :color="CONFIG.colors.white"
          @click="currentVersionDialog = false"
        >mdi-close</v-icon>
      </div>
      <v-card elevation="0" class="border-radius-1-notop">
        <div style="display:flex; justify-content: space-between; padding-top: 42px;">
          <div style="margin-left: 30px;">
            <svg width="64" height="64" viewBox="0 0 64 64" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M61.7597 46.18L39.9997 8.56001C39.1885 7.15633 38.0222 5.9908 36.6179 5.18049C35.2137 4.37018 33.621 3.9436 31.9997 3.9436C30.3785 3.9436 28.7858 4.37018 27.3815 5.18049C25.9773 5.9908 24.811 7.15633 23.9997 8.56001L2.25974 46.18C1.42677 47.5901 0.98179 49.1954 0.969968 50.833C0.958147 52.4707 1.37991 54.0823 2.19243 55.5042C3.00495 56.9261 4.17928 58.1077 5.59618 58.9289C7.01307 59.7501 8.62205 60.1818 10.2597 60.18H53.6597C55.2987 60.181 56.9091 59.7507 58.3292 58.9322C59.7492 58.1138 60.9289 56.9361 61.7497 55.5174C62.5705 54.0988 63.0035 52.4891 63.0053 50.8501C63.0071 49.2111 62.5775 47.6005 61.7597 46.18V46.18Z" stroke="#FF8419" stroke-linecap="round" stroke-linejoin="round"/>
            <path d="M32 51.76C31.0824 51.76 30.2023 51.3954 29.5534 50.7466C28.9046 50.0977 28.54 49.2176 28.54 48.3C28.54 47.3823 28.9046 46.5023 29.5534 45.8534C30.2023 45.2045 31.0824 44.84 32 44.84C32.4586 44.8422 32.9121 44.9371 33.333 45.1191C33.7539 45.3011 34.1337 45.5663 34.4495 45.8989C34.7653 46.2315 35.0105 46.6245 35.1704 47.0543C35.3303 47.4841 35.4016 47.9419 35.38 48.4C35.3748 49.2929 35.0163 50.1475 34.383 50.7771C33.7497 51.4066 32.893 51.76 32 51.76Z" fill="#FFBF8F" stroke="#FF8419" stroke-linecap="round" stroke-linejoin="round"/>
            <path d="M35.16 29.4L34.68 37.4C34.68 38.26 34.68 39.06 34.68 39.9C34.6749 40.2449 34.6011 40.5853 34.4631 40.9014C34.3251 41.2176 34.1256 41.5031 33.8762 41.7413C33.6268 41.9796 33.3324 42.1658 33.0103 42.2892C32.6882 42.4126 32.3448 42.4706 32 42.46C31.3297 42.4762 30.6801 42.2266 30.1931 41.7658C29.706 41.3049 29.4209 40.6702 29.4 40C29.16 35.82 28.92 31.74 28.7 27.56L28.46 24.26C28.4169 23.4204 28.6579 22.5908 29.1441 21.905C29.6303 21.2191 30.3335 20.7172 31.14 20.48C31.9355 20.2902 32.7723 20.3867 33.5038 20.7524C34.2352 21.1181 34.8145 21.7298 35.14 22.48C35.3684 23.0309 35.4775 23.6239 35.46 24.22C35.4 26 35.22 27.68 35.16 29.4Z" fill="#FFBF8F" stroke="#FF8419" stroke-linecap="round" stroke-linejoin="round"/>
            </svg>
            </div>
          <div style="margin-left: 30px;font-size: 16px; font-weight: 500; margin-top: 18px; margin-right: 30px; line-height: 20px; color: rgba(0, 0, 0, 0.8);">{{$vuetify.lang.t("$vuetify.warningDesc")}}</div>
        </div>
        <v-card-actions class="mr-3 mt-5 pb-5"> 
          <v-spacer></v-spacer>
          <v-btn
            ref="button"
            autofocus
            class="ps-btn-perform white--text button-close-warning border-radius-1"
            :color="CONFIG.colors.primary"
            @click="currentVersionDialog = false"
          >{{$vuetify.lang.t("$vuetify.close")}}</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-dialog
      v-if="deleteConfirmDialog"
      no-click-animation
      persistent
      v-model="deleteConfirmDialog"
      width="750px"
      @keydown.esc="deleteConfirmDialog = false"
    >
      <div class="popup-header ps-dialog-header">
        <v-card-title class="white--text pb-2 pt-2">{{$vuetify.lang.t('$vuetify.deleteRole')}}</v-card-title>
        <v-icon
          class="ps-btn-close"
          :color="CONFIG.colors.white"
          @click="cancelDialog = false"
        >mdi-close</v-icon>
      </div>
      <v-card elevation="0">
        <v-card-title>
          <span>{{$vuetify.lang.t("$vuetify.deleteRole")}}</span>
        </v-card-title>
        <v-card-text>{{$vuetify.lang.t("$vuetify.areYouSureYouWantToDeleteTheRole")}}</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            class="ps-btn-delete white--text"
            :color="CONFIG.colors.primary"
            @click="deleteItem"
          >{{$vuetify.lang.t("$vuetify.delete")}}</v-btn>
          <v-btn
            autofocus
            class="cancel-btn-delete"
            :color="CONFIG.colors.secondary"
            @click="closeDeleteForm"
          >{{$vuetify.lang.t("$vuetify.close")}}</v-btn>
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
          <v-card-title class="pl-0 pb-2">{{editedItem.name}}</v-card-title>
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
                    >{{$vuetify.lang.t('$vuetify.name')}}</v-subheader>
                  </v-col>
                  <v-col :class="CONFIG.form.fieldColClass">
                    <v-text-field
                      dense
                      outlined
                      :rules="nameRules"
                      v-model="editedItem.name"
                      :placeholder="$vuetify.lang.t('$vuetify.name')"
                    ></v-text-field>
                  </v-col>
                </v-row>
                <v-row :class="CONFIG.form.rowClass">
                  <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass">
                    <v-subheader
                      :class="CONFIG.form.labelClass"
                    >{{$vuetify.lang.t('$vuetify.description')}}</v-subheader>
                  </v-col>
                  <v-col :class="CONFIG.form.fieldColClass">
                    <v-text-field
                      dense
                      outlined
                      v-model="editedItem.description"
                      :placeholder="$vuetify.lang.t('$vuetify.description')"
                    ></v-text-field>
                  </v-col>
                </v-row>
              </v-col>
            </v-row>
          </v-form>
        </v-card-text>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn class="white--text" :color="CONFIG.colors.primary" @click="saveForm">{{okBtnText}}</v-btn>
          <v-btn
            autofocus
            class="ps-btn-ok cancel-btn-delete"
            color="secondary"
            @click="closeAddOrEditForm"
          >{{$vuetify.lang.t('$vuetify.close')}}</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <!-- ЛЕВАЯ КОЛОНКА -->
    <v-col cols="6" style="height: 100%" class="ps-col-wrapper">
      <v-card-title class="ps-card-title-top">
        <h1 class="page-header">{{$vuetify.lang.t('$vuetify.roles')}}</h1>
      </v-card-title>
      <v-card elevation="0" height="100%" class="d-flex flex-column">
        <v-card-actions class="ps-card-actions-top ps-card-actions-top__margins">
          <!-- Поле поиска -->
          <div class="search-width">
            <v-text-field
              @keyup="searchTimeOut()"
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
              class="ps-btn-add ps-add-btn font-size-16 add-left"
              max-height="42"
              @click.stop.prevent="showAddForm()"
            >{{$vuetify.lang.t('$vuetify.add')}}</v-btn>
          </div>
        </v-card-actions>
        <v-card-text class="ps-card-text">
          <div :style="{ width: `${tableWidth}px`, position: 'relative'}">
            <div class="table-width-setter" @mousedown="dragMouseDown(0,$event)" :style="{ left: `${columns.first}%`}"></div> 
            <div class="table-width-setter" @mousedown="dragMouseDown(1,$event)" :style="{ left: `${columns.first + columns.second}%`}"></div>
          </div> 
          <v-data-table
            :server-items-length="pagination.totalItems"
            :options.sync="options"
            style="background green"
            :height="windowHeight - 225"
            :hide-default-footer="CONFIG.table.hideDefaultFooter"
            :header-props="CONFIG.table.headerProps"
            :class="CONFIG.table.class"
            :footer-props="CONFIG.table.footerProps"
            :dense="CONFIG.table.dense"
            :fixed-header="CONFIG.table.fixedHeader"
            :headers="headers"
            :items="ROLES.items"
            :page.sync="pagination.page"
            :items-per-page.sync="pagination.rowsPerPage"
            @page-count="pagination.totalPages = $event"
            @click:row="showSettingsDialog"
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
                <v-spacer></v-spacer>
                <div class="pagination-block r-off">
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
            <!-- row handle click -->
            <template v-slot:item="props">
              <tr
                ref="tablecontainer"
                style="cursor: pointer"
                @click="showSettingsDialog(props.item)"
                :class="{ 'v-data-table__selected':selectedRole === props.item }"
              >
                <td :style="{width: `${columns.first}%`}">{{ props.item.name }}</td>
                <td :style="{width: `${columns.second}%`}">{{ props.item.description }}</td>
                <td :style="{width: `${columns.third}%`}">
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
          </v-data-table>
        </v-card-text>
      </v-card>
    </v-col>
    <!-- ПРАВАЯ КОЛОНКА -->
    <v-col cols="6" class="ps-col-wrapper">
      <div style="background-color:#fff; height: 65px; margin-top: 38px; padding-left: 8px; position: relative;">
      <v-tabs
        value="selectedTab"
        class="elevation-0"
        background-color="#f6f7f8"
        style="padding-top: 15px;"
        @change="onChangeTab"
      >
        <div class="dark-tab" @click="opencurrentVersionDialog()"></div>
        <div class="dark-tab-2" @click="opencurrentVersionDialog()"></div>
        <v-tab
          :class="{'ps-tab': true, 'black--text': true,  'mr-2':true, 'ml-2': true, 'pt-6':true, 'pb-6': true}"
          v-for="(page, index) in pages"
          :key="index"
          :disabled="index !== 0"
        >{{ page }}</v-tab>
      </v-tabs>
      </div>
      <v-tabs-items v-model="selectedTab" class="ps-v-tabs">
        <v-tab-item v-for="(page, index) in pages" :key="index">
          <ps-users-table
            v-if="page === $vuetify.lang.t('$vuetify.users')"
            :items="USERS.items"
            :roleName="selectedRole.name"
            class="side-section-wrapper roles-users-section elevation-0"
          />
          <v-card elevation="0" v-else-if="page === $vuetify.lang.t('$vuetify.pages')" style="padding-top: 1px">
            <div class="ps-card-actions-top side ps-card-actions-top__margins mt-1 mb-0">
              <!-- Поле поиска -->
              <div class="width-363">
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
              <div style="width: 300px; font-size: 16px; text-align: center">
                <p>В предварительной версии</p>
                <p>не функционально</p>
              </div>
              <div class="width-210">
                <v-btn
                  disabled
                  max-height="42"
                  class="ps-btn-add ps-add-btn font-size-16 "
                  @click.stop.prevent="saveSelectedPages()"
                >{{$vuetify.lang.t('$vuetify.save')}}</v-btn>
              </div>
            </div>
            <v-card-text style="padding-top: 8px; padding-left: 0; padding-right:0;">
              <!--
                    <v-data-table
        class="side-table"
        calculate-widths
        :hide-default-footer="CONFIG.table.hideDefaultFooter"
        :header-props="CONFIG.table.headerProps"
        :fixed-header="CONFIG.table.fixedHeader"
        :class="CONFIG.table.class"
        :footer-props="CONFIG.table.footerProps"
        :dense="CONFIG.table.dense"
        :height="windowHeight - 285"
        :items="OBJECTS"
        :headers="headers"
        :page.sync="page"
        :items-per-page.sync="pagination.rowsPerPage"
        :search="search"
        @page-count="pagination.totalPages = $event"
      >
      -->
              <v-data-table
                :headers="treeHeaders"
                :height="windowHeight - 279"
                :hide-default-footer="CONFIG.table.hideDefaultFooter"
                :items="pagesItems"
                :expanded.sync="expanded"
                item-key="name"
                show-expand
                class="elevation"
                :single-expand="false"
                calculate-widths
              >
              <template v-slot:header.name="{ header }">
                <span class="left-shift">{{ header.text }}</span>
              </template>
              <template v-slot:expanded-item="{ headers, item }">
                <td :colspan=3>
                  <v-data-table
                    :items="item.children"
                    hide-default-footer
                    :headers="treeHeadersForExpanded"
                    class="elevation"
                  >
                    <template v-slot:item.name="{ item }">
                      <span class="left-shift-72">{{ item.name }}</span>
                    </template>
                    <template v-slot:item.opened="{ item }">

                      <v-simple-checkbox
                        v-model="item.opened"
                      ></v-simple-checkbox>
                    </template>
                  </v-data-table>
                </td>
              </template>

              <template v-slot:item.opened="{ item }">
                <v-simple-checkbox
                  v-model="item.opened"
                ></v-simple-checkbox>
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
                  </v-row>
                </template>
              </v-data-table>     
            
              <!--
              <v-data-table
                :headers="treeHeaders"
                hide-default-footer
                hide-default-header
                :height="windowHeight - 336"
              >
                <template v-slot:body>
                  <v-treeview
                    class="ps-treeview"
                    v-model="selectedPages"
                    selectable
                    :items="pagesItems"
                    :value="idr"
                    item-key="idr"
                    activatable
                  >
                    <template v-slot:append="props">
                      {{props.selected}}
                      <v-checkbox
                        v-model="props.selected"
                        @click="updateSelectedPages(props.selected, props.item.idr)"
                      ></v-checkbox>
                    </template>
                  </v-treeview>
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

                    //
                     <div class="page-set-errors">
                  <p
                    class="red--text pl-10 pt-2"
                    style="width: 100%; font-size: 8px; text-align: center !important"
                    v-for="(err, index) in pageSetErrors"
                    :key="index"
                  >{{err}}</p>
                    </div>
                  </v-row>
                </template>
              </v-data-table>
              ///
-->

            </v-card-text>
          </v-card>
          <ps-permitted-actions-table
            class="side-section-wrapper roles-actions-section"
            v-else-if="page === $vuetify.lang.t('$vuetify.actions')"
          />
        </v-tab-item>
      </v-tabs-items>
    </v-col>
  </v-row>
</template>

<script>
import { mapGetters, mapActions } from "vuex";
import { required } from "@/helpers/validators";
import psUsersTable from "@/components/tables/ps-users-table";
import psPermittedActionsTable from "@/components/tables/ps-permitted-actions-table";
import { pages } from "@/public/data";
// import Axios from "axios";
// import psPaginationPageSet from "@/components/pagination/ps-pagination-page-set";

export default {
  name: "Roles",
  components: {
    psUsersTable,
    psPermittedActionsTable,
    // psPaginationPageSet,
  },
  data() {
    return {
      pagination: {
        page: 1,
        rowsPerPage: 25,
        totalItems: 0,
        totalPages: 0,
      },
      timer: null,
      options: {sortBy: [], sortDesc: []},
      search: "",
      expanded: [],
      headers: [
        { text: this.$vuetify.lang.t("$vuetify.name"), value: "name" },
        {
          text: this.$vuetify.lang.t("$vuetify.description"),
          value: "description",
        },
        {
          text: this.$vuetify.lang.t("$vuetify.actions"),
          value: "actions",
          sortable: false,
        },
      ],
      tableWidth: 0,
      point: 0,
      activeBar: 0,
      columns: {
        first: 33.333,
        second: 33.333,
        third: 33.333,
      },
      treeHeaders: [
        { text: '', value: 'data-table-expand', sortable: false, width: 50},
        { text: this.$vuetify.lang.t("$vuetify.name"), value: "name" },
        { text: this.$vuetify.lang.t("$vuetify.appointed"), value: "opened", sortable: false, width: 205},
      ],
      treeHeadersForExpanded: [
        { text: this.$vuetify.lang.t("$vuetify.name"), value: "name", class:"height-0", sortable: false},
        { text: this.$vuetify.lang.t("$vuetify.appointed"), value: "opened", width: 205, class:"height-0", sortable: false},
      ],
      dialog: false,
      windowHeight: window.innerHeight,
      deleteConfirmDialog: false,
      currentVersionDialog: false,
      editedIndex: -1,
      editedItem: {
        idr: null,
        name: "",
        description: "",
      },
      defaultItem: {
        idr: null,
        name: "",
        description: "",
      },
      settingsDialog: true,
      selectedTab: 0,
      selectedRole: null,
      pages: [
        this.$vuetify.lang.t("$vuetify.users"),
        this.$vuetify.lang.t("$vuetify.pages"),
        this.$vuetify.lang.t("$vuetify.actions"),
      ],
      initialSelectedPages: [], // выбранные страницы при монтировании, для того чтобы сравнивать с тем что выбрано в selectedPages
      selectedPages: [],
      pagesItems: [],
      nameRules: [() => required(this.editedItem.name)],
    };
  },
  async mounted() {
    this.initialSelectedPages = this.selectedPages; // сохраняем выбранные ранее страницы в стейт
    await this.FETCH_ROLES({
      limit: this.pagination.rowsPerPage,
      offset: this.pagination.page - 1,
    });
    this.pagination.totalItems = await this.ROLES.totalCount;
    this.pagination.totalPages =
      (await Math.floor(this.ROLES.totalCount / this.pagination.rowsPerPage)) +
      1;
    const role = this.ROLES.items[0]; // первая роль в массиве будет по умолчанию.
    this.showSettingsDialog(role);
    await this.createPagesTree();
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
  computed: {
    ...mapGetters(["ROLES", "USERS", "PAGES", "CONFIG"]),
    treeStyles() {
      return {
        height: `${this.windowHeight} !important`,
      };
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
    itemsPerPageOptionsCols() {
      return this.settingsDialog ? "2" : "1";
    },
    rawPages() {
      return pages;
    },
    isSelectedNowPages() {
      return (
        JSON.stringify(this.initialSelectedPages) !==
        JSON.stringify(this.selectedPages)
      );
    },
    formTitle() {
      return this.editedIndex === -1
        ? this.$vuetify.lang.t("$vuetify.addRole")
        : this.$vuetify.lang.t("$vuetify.editRole");
    },
    okBtnText() {
      return this.editedIndex === -1
        ? this.$vuetify.lang.t("$vuetify.add")
        : this.$vuetify.lang.t("$vuetify.save");
    },
  },
  methods: {
    ...mapActions(["FETCH_ROLES", "ADD_ROLE", "CHANGE_ROLE", "REMOVE_ROLE"]),
    updateSelectedPages(isSelected, itemIdr) {
      console.log(itemIdr);
      console.log(isSelected);
      if (isSelected) {
        this.selectedPages.push(itemIdr);
      } else {
        console.log("remove");
        const newPages = this.selectedPages.map((item) => {
          if (item !== itemIdr) {
            return item;
          }
        });
        this.selectedPages = newPages;
        console.log(this.selectedPages);
        console.log(newPages);
      }
    },
    opencurrentVersionDialog() {
        this.currentVersionDialog = true;
        //this.selectedTab = 0;
        setTimeout(() => {
          this.$refs.button.$el.focus()
        })
    },
    onChangeTab() {
      return
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
      if (columns.first < 18.5 || columns.second < 11.9 || columns.third < 11.5) {
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
    searchTimeOut() {  
      if (this.timer) {
        clearTimeout(this.timer);
        this.timer = null;
      }
      this.timer = setTimeout(() => {
        console.log(this.search)
        this.FETCH_ROLES({ limit: this.pagination.rowsPerPage, offset: this.pagination.page - 1, search: this.search, orderedColumn: this.options.sortBy[0], sortDesc: this.options.sortDesc[0]});
      }, 1000);
    },
    handlePageSetErrors(errors) {
      this.pageSetErrors = errors;
    },
    setPage(page) {
      this.pagination.page = parseInt(page);
    },
    createPagesTree() {
      let tree = [];
      this.PAGES.map((page) => {
        if (!page.IsLeaf) {
          const childrens = this.rawPages.filter(
            (item) => item.ParentId == page.idr
          );
          if (childrens.length) {
            page.children = [];
            childrens.map((item) => {
              page.children.push(item);
            });
          }
          tree.push(page);
        } else {
          tree.push(page);
        }
      });
      const result = tree.map((item) => {
        return {name: item.name, opened: true, children: item.children.map((child) => {
          return {name: child.name, opened: true}
          }
        )}
      })
      this.pagesItems = result;
      console.log("pages", this.pagesItems);
    },
    showEditForm(item) {
      this.editedIndex = 1;
      this.editedItem = Object.assign({}, item);
      this.dialog = true;
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
      this.REMOVE_ROLE(this.editedItem);
      this.closeDeleteForm();
    },
    showSettingsDialog(role) {
      this.selectedRole = role;
      console.log("ITEM ", role);
      this.settingsDialog = true;
    },
    saveForm() {
      if (this.$refs.form.validate()) {
        if (this.editedIndex > -1) {
          this.CHANGE_ROLE(this.editedItem);
        } else {
          this.ADD_ROLE({ ...(this.editedItem || null) });
        }
        this.closeAddOrEditForm();
      }
    },
    saveSelectedPages(){
      console.log("SAVE_NEW_PAGES", this.selectedPages)
    }
  },
};
</script>
