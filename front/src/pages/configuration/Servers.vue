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
            <img src="../../public/fail.png" width=72 height=72 v-if="message.type==='error'">
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
          </div>
          <div style="margin-left: 27px;font-size: 16px; font-weight: 500; margin-top: 7px; margin-right: 30px; line-height: 20px; color: rgba(0, 0, 0, 0.8);"><p style="margin-bottom: 0;">{{message.text1}}</p><p style="margin-bottom: 0;">{{message.text2}}</p></div>
        </div>
        <v-card-actions class="mr-3 mt-2 pb-5"> 
          <v-spacer></v-spacer>
          <v-btn
            v-if="message.type==='warning'"
            ref="button"
            autofocus
            class="ps-btn-perform white--text button-close-warning secondary border-radius-1 continue"
            :color="CONFIG.colors.primary"
            @click="deleteServer"
          >Продолжить</v-btn>
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
    <v-slide-y-transition>
      <v-dialog
        v-if="deleteConfirmDialog"
        no-click-animation
        persistent
        width="650"
        v-model="deleteConfirmDialog"
        @keydown.esc="deleteConfirmDialog = false"
      >
        <v-card elevation="0" height="100%" class="d-flex flex-column">
          <div class="popup-header">
            <v-card-title>Удалить сервер</v-card-title>
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
          >{{$vuetify.lang.t("$vuetify.areYouSureYouWantToDeleteTheServer")}}?</v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn
              class="white--text ps-btn-delete"
              :color="CONFIG.colors.primary"
              @click="deleteItem"
            >{{$vuetify.lang.t('$vuetify.delete')}}</v-btn>
            <v-btn
              autofocus
              class="cancel-btn-delete"
              color="secondary"
              @click="closeDeleteForm"
            >{{$vuetify.lang.t('$vuetify.close')}}</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-slide-y-transition>
    <v-slide-y-transition>
      <v-dialog
        v-if="dialog"
        no-click-animation
        persistent
        width="650"
        v-model="dialog"
        @keydown.esc="dialog = false"
      >
        <v-card elevation="0" height="100%" class="d-flex flex-column">
          <div class="popup-header">
            <v-card-title>{{formTitle}}</v-card-title>
            <v-icon
              class="white--text"
              @click="dialog = false"
              :color="CONFIG.colors.white"
            >mdi-close</v-icon>
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
                  <v-row align="top" :class="CONFIG.form.rowClass">
                    <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass">
                      <v-subheader
                        :class="CONFIG.form.labelClass"
                      >{{$vuetify.lang.t('$vuetify.name')}}</v-subheader>
                    </v-col>
                    <v-col cols="8" :class="CONFIG.form.fieldColClass">
                      <v-text-field
                        dense
                        outlined
                        :rules="nameRules"
                        v-model="editedItem.name"
                        :placeholder="$vuetify.lang.t('$vuetify.manufacturer')"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-row align="top" :class="CONFIG.form.rowClass">
                    <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass">
                      <v-subheader
                        :class="CONFIG.form.labelClass"
                      >{{$vuetify.lang.t("$vuetify.description")}}</v-subheader>
                    </v-col>
                    <v-col cols="8" :class="CONFIG.form.fieldColClass">
                      <v-text-field
                        dense
                        outlined
                        v-model="editedItem.description"
                        :placeholder="$vuetify.lang.t('$vuetify.description')"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-row align="top" :class="CONFIG.form.rowClass">
                    <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass">
                      <v-subheader
                        :class="CONFIG.form.labelClass"
                      >{{$vuetify.lang.t("$vuetify.manufacturer")}}</v-subheader>
                    </v-col>
                    <v-col cols="8" :class="CONFIG.form.fieldColClass">
                      <!-- <v-text-field outlined v-model="editedItem.remoteIpAddress" placeholder="IP-адрес"></v-text-field> -->
                      <v-autocomplete
                        dense
                        outlined
                        :items="VENDORS"
                        item-value="idr"
                        item-text="name"
                        :placeholder="$vuetify.lang.t('$vuetify.manufacturer')"
                        v-model="editedItem.vendorId"
                      ></v-autocomplete>
                    </v-col>
                  </v-row>
                  <v-row align="top" :class="CONFIG.form.rowClass">
                    <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass">
                      <v-subheader
                        :class="CONFIG.form.labelClass"
                      >{{$vuetify.lang.t("$vuetify.model")}}</v-subheader>
                    </v-col>
                    <v-col cols="8" :class="CONFIG.form.fieldColClass">
                      <v-autocomplete
                        dense
                        outlined
                        :items="MODELS"
                        item-value="idr"
                        item-text="name"
                        :placeholder="$vuetify.lang.t('$vuetify.model')"
                        v-model="editedItem.modelId"
                      ></v-autocomplete>
                    </v-col>
                  </v-row>
                  <!-- Таблица для установки приоритета для нодов сервера, видна только при редактировании -->
                  <!-- Когда будет готов API изменить editedIndex на 1 вместо 5 -->
                  <v-row align="top" :class="CONFIG.form.rowClass" v-if="editedIndex === 5">
                    <ps-nodes-table :serverId="editedItem.idr" />
                  </v-row>
                  <v-row align="top" :class="CONFIG.form.rowClass" v-if="editedIndex === 1">
                    <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass">
                      <v-subheader
                        :class="CONFIG.form.labelClass"
                      >{{$vuetify.lang.t("$vuetify.timezone")}}</v-subheader>
                    </v-col>
                    <v-col cols="8" :class="CONFIG.form.fieldColClass">
                      <v-autocomplete
                        dense
                        outlined
                        :placeholder="$vuetify.lang.t('$vuetify.timezone')"
                        :items="timezones"
                        item-value="idr"
                        item-text="offset"
                        v-model="editedItem.timezone"
                      />
                    </v-col>
                  </v-row>
                  <v-row align="top" :class="CONFIG.form.rowClass">
                    <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass">
                      <v-subheader
                        :class="CONFIG.form.labelClass"
                      >{{$vuetify.lang.t("$vuetify.taskExecutionTime")}}</v-subheader>
                    </v-col>
                    <v-col cols="8" :class="CONFIG.form.fieldColClass">
                      <v-text-field
                        dense
                        outlined
                        type="time"
                        :placeholder="$vuetify.lang.t('$vuetify.taskExecutionTimeDefault')"
                        v-model="editedItem.defStartTime"
                      />
                    </v-col>
                  </v-row>

                  <v-row align="top" :class="CONFIG.form.rowClass">
                    <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass">
                      <v-subheader
                        :class="CONFIG.form.labelClass"
                      >{{$vuetify.lang.t('$vuetify.login')}}</v-subheader>
                    </v-col>
                    <v-col cols="8" :class="CONFIG.form.fieldColClass">
                      <v-text-field
                        dense
                        outlined
                        v-model="editedItem.login"
                        :placeholder="$vuetify.lang.t('$vuetify.login')"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-row align="top" :class="CONFIG.form.rowClass">
                    <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass">
                      <v-subheader
                        :class="CONFIG.form.labelClass"
                      >{{$vuetify.lang.t('$vuetify.password')}}</v-subheader>
                    </v-col>
                    <v-col cols="8" :class="CONFIG.form.fieldColClass">
                      <v-text-field
                        :append-icon="passwordShow ? 'mdi-eye' : 'mdi-eye-off'"
                        :type="passwordShow ? 'text' : 'password'"
                        @click:append="passwordShow = !passwordShow"
                        dense
                        outlined
                        v-model="editedItem.password"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-row align="top" :class="CONFIG.form.rowClass">
                    <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass">
                      <v-subheader
                        :class="CONFIG.form.labelClass"
                      >{{$vuetify.lang.t('$vuetify.port')}}</v-subheader>
                    </v-col>
                    <v-col cols="8" :class="CONFIG.form.fieldColClass">
                      <v-text-field
                        dense
                        outlined
                        v-model="editedItem.port"
                        :placeholder="$vuetify.lang.t('$vuetify.port')"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-row align="top" :class="CONFIG.form.rowClass">
                    <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass">
                      <v-subheader
                        :class="CONFIG.form.labelClass"
                      >{{$vuetify.lang.t('$vuetify.mode')}}</v-subheader>
                    </v-col>
                    <v-col cols="8" :class="CONFIG.form.fieldColClass">
                      <v-autocomplete
                        dense
                        outlined
                        :items="modesNames"
                        :placeholder="$vuetify.lang.t('$vuetify.mode')"
                        v-model="editedItem.mode"
                      ></v-autocomplete>
                    </v-col>
                  </v-row>
                </v-col>
              </v-row>
              <v-row justify="start" :class="CONFIG.form.rowClass">
                <v-checkbox
                  hide-details="auto"
                  class="ma-0"
                  dense
                  :label="$vuetify.lang.t('$vuetify.isTestServer')"
                  v-model="editedItem.testBench"
                />
                <v-checkbox
                  hide-details="auto"
                  class="ma-0"
                  dense
                  :label="$vuetify.lang.t('$vuetify.turnOn')"
                  v-model="editedItem.enable"
                />
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
              class="ps-btn-close cancel-btn-delete"
              color="secondary"
              @click="closeAddOrEditForm"
            >{{$vuetify.lang.t('$vuetify.close')}}</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-slide-y-transition>

        <v-slide-y-transition>
      <v-dialog
        v-if="addDialog"
        no-click-animation
        persistent
        width="650"
        v-model="addDialog"
        @keydown.esc="addDialog = false"
      >
        <v-card elevation="0" height="100%" class="d-flex flex-column">
          <div class="popup-header" style="height: 40px;">
            <v-card-title class="ml-0 pl-2 pt-5 font-size-16">Добавление сервера в систему</v-card-title>
            <v-icon
              class="white--text"
              @click="addDialog = false"
              :color="CONFIG.colors.white"
            >mdi-close</v-icon>
          </div>


          <v-card-text>
            <v-form ref="form" lazy-validation class="pr-1 pl-1 pt-0">
              <v-row no-gutters class="pl-0 pr-0">
                <v-card-title class="pl-0 font-size-12 font-weight-bold" style="padding-top: 22px; padding-bottom: 5px;">Наименование и описание</v-card-title>
                <v-spacer></v-spacer>
              </v-row>
              <v-divider class="ml-0 mr-0"></v-divider>
              <v-row>
                <v-col class="pr-5 pl-3">
                    <v-row align="top" :class="CONFIG.form.rowClass" style="margin-top: 9px;">
                    <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass2">
                      <v-subheader
                        :class="CONFIG.form.labelClass" style="font-size: 12px; padding-right: 0;"
                      >FQDN и порт</v-subheader>
                    </v-col>
                    <v-col :class="CONFIG.form.fieldColClass2">
                        <div style="display: flex; justify-content: space-between; width: 416px">
                          <div>
                              <v-text-field
                                class="time-zone"
                                dense
                                outlined
                                :rules="nameRules"
                                v-model="createItem.serverFQDN"
                                placeholder="Sample.example.com (обязательно)"
                              />
                          </div>
                          <div>
                            <v-text-field
                                class="width-53 port-input"
                                dense
                                type="number"
                                outlined
                                placeholder="00000"
                                v-model="createItem.serverPort"
                              />
                          </div>

                        </div>
                    </v-col>
                  </v-row>
                  <v-row align="top" :class="CONFIG.form.rowClass">
                    <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass2">
                      <v-subheader
                        :class="CONFIG.form.labelClass" style="font-size: 12px;"
                      >{{$vuetify.lang.t("$vuetify.description")}}</v-subheader>
                    </v-col>
                    <v-col :class="CONFIG.form.fieldColClass2">
                      <v-text-field
                        dense
                        outlined
                        v-model="createItem.serverDescription"
                        :placeholder="$vuetify.lang.t('$vuetify.description')"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-row align="top" :class="CONFIG.form.rowClass">
                    <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass2">
                      <v-subheader
                        :class="CONFIG.form.labelClass" style="font-size: 12px; padding-right: 0;"
                      >Производитель и модель</v-subheader>
                    </v-col>
                    <v-col :class="CONFIG.form.fieldColClass2">
                      <div style="display:flex; justify-content: flex-start;">
                      <!--
                        <v-autocomplete
                          class="manufacturer-model"
                          dense
                          outlined
                          :items="VENDORS"
                          item-value="idr"
                          item-text="name"
                          :placeholder="$vuetify.lang.t('$vuetify.manufacturer')"
                          full-width="198"
                          style="margin-right: 18px;"
                          v-model="createItem.serverVendorName"
                        ></v-autocomplete>
                      -->
                        <v-autocomplete
                          id="manufacturer"
                          class="manufacturer-model"
                          dense
                          outlined
                          :items="MODELS"
                          item-value="idr"
                          item-text="name"
                          :placeholder="$vuetify.lang.t('$vuetify.model')"
                          v-model="createItem.serverVendorModelId"
                        ></v-autocomplete>
                      </div>
                    </v-col>
                  </v-row>
                  <!--
                  <v-row align="top" :class="CONFIG.form.rowClass">
                    <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass2">
                      <v-subheader
                        :class="CONFIG.form.labelClass" style="font-size: 12px; padding-right: 0;"
                      >Версия и режим</v-subheader>
                    </v-col>
                    <v-col :class="CONFIG.form.fieldColClass2">
                      <div style="display:flex; justify-content: flex-start;">
                        <v-autocomplete
                          class="manufacturer-model"
                          dense
                          outlined
                          :items="VENDORS"
                          item-value="idr"
                          item-text="name"
                          placeholder="Не выбрана"
                          full-width="198"
                          style="margin-right: 18px;"

                        ></v-autocomplete>
                        <v-autocomplete
                          id="manufacturer"
                          class="manufacturer-model"
                          dense
                          outlined
                          :items="MODELS"
                          item-value="idr"
                          item-text="name"
                          placeholder="Не выбрана"

                        ></v-autocomplete>
                      </div>
                    </v-col>
                  </v-row>
                  -->
                  <v-row no-gutters class="pl-0 pr-0">
                    <v-card-title class="pl-0 font-size-12 font-weight-bold" style="padding-top: 0px; padding-bottom: 5px;">Данные авторизации</v-card-title>
                    <v-spacer></v-spacer>
                  </v-row>
                  <v-divider class="ml-0 mr-0 mb-0"></v-divider>
                  <v-row align="top" :class="CONFIG.form.rowClass"  style="margin-top: 9px;">
                    <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass2">
                      <v-subheader
                        :class="CONFIG.form.labelClass" style="font-size: 12px; padding-right: 0;"
                      >Логин и пароль</v-subheader>
                    </v-col>
                    <v-col :class="CONFIG.form.fieldColClass2">
                      <div style="display:flex; justify-content: flex-start;">
                        <v-text-field
                          class="manufacturer-model"
                          dense
                          outlined
                          :placeholder="$vuetify.lang.t('$vuetify.login')"
                          v-model="createItem.serverLogin"
                          full-width="198"
                          style="margin-right: 18px;"
                        ></v-text-field>
                        <v-text-field
                          :append-icon="passwordShow ? 'mdi-eye' : 'mdi-eye-off'"
                          :type="passwordShow ? 'text' : 'password'"
                          @click:append="passwordShow = !passwordShow"
                          class="manufacturer-model"
                          dense
                          outlined
                          :placeholder="$vuetify.lang.t('$vuetify.password')"
                          v-model="createItem.serverPassword"
                        ></v-text-field>
                      </div>
                    </v-col>
                  </v-row>
                    <v-row no-gutters class="pl-0 pr-0">
                    <v-card-title class="pl-0 pr-0 font-size-12 font-weight-bold" style="padding-top: 0px; padding-bottom: 5px;display:flex; justify-content: space-between; width: 100%;">
                    <div style="display:flex; justify-content: flex-start; align-items: center;">
                      <p style="margin-bottom: 0;">Ноды сервера</p>
                      <button class="server-table__add-node" @click.prevent="addNode('create')">+</button>
                    </div>
                    
                    <div style="display:flex; justify-content: flex-start; align-items: center;">
                      <p style="margin-bottom: 0; font-weight: 500;">Версия кластера: 00.97</p>
                      <button class="server-table__edit-cluster" @click.prevent>
                        <svg width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <circle cx="8" cy="8" r="7.5" fill="#D9D9D9" stroke="#C4C4C4"/>
                        <path d="M12.3482 6.3671C12.5794 6.1353 12.7072 5.82526 12.7072 5.4983C12.7072 5.4982 12.7072 5.4981 12.7072 5.498C12.7072 5.49789 12.7072 5.49778 12.7072 5.49767C12.707 5.1708 12.5789 4.86062 12.3482 4.62908C12.3482 4.62907 12.3482 4.62905 12.3482 4.62904V6.3671Z" stroke="black" stroke-opacity="0.6"/>
                        <mask id="path-3-inside-1" fill="white">
                        <path d="M9.09125 6.36052L6.60303 8.85742L7.16377 9.42012L9.65199 6.92322L9.09125 6.36052Z"/>
                        </mask>
                        <path d="M6.60303 8.85742L5.89716 8.14909L5.19129 8.85742L5.89716 9.56575L6.60303 8.85742ZM9.09125 6.36052L9.79711 5.65219L9.09125 4.94386L8.38538 5.65219L9.09125 6.36052ZM9.65199 6.92322L10.3579 7.63155L11.0637 6.92322L10.3579 6.21489L9.65199 6.92322ZM7.16377 9.42012L6.4579 10.1285L7.16377 10.8368L7.86964 10.1285L7.16377 9.42012ZM7.30889 9.56575L9.79711 7.06885L8.38538 5.65219L5.89716 8.14909L7.30889 9.56575ZM8.38538 7.06885L8.94612 7.63155L10.3579 6.21489L9.79711 5.65219L8.38538 7.06885ZM8.94612 6.21489L6.4579 8.71179L7.86964 10.1285L10.3579 7.63155L8.94612 6.21489ZM7.86964 8.71179L7.30889 8.14909L5.89716 9.56575L6.4579 10.1285L7.86964 8.71179Z" fill="black" fill-opacity="0.6" mask="url(#path-3-inside-1)"/>
                        </svg>
                      </button>
                    </div>
                    </v-card-title>
                    <v-spacer></v-spacer>
                  </v-row>
                  <v-divider class="ml-0 mr-0 mb-0"></v-divider>
                  <v-row align="top" :class="CONFIG.form.rowClass"  style="margin-top: 9px;">
                    <table class="server-table">
                      <tr class="server-table__title-row">
                        <th class="server-table__title-row__1">Приоритет</th>
                        <th class="server-table__title-row__2">Ip Адрес</th>
                        <th class="server-table__title-row__3">FQDN</th>
                        <th class="server-table__title-row__4">Действия</th>
                      </tr>
                      <tr v-for="(node, index) of createItem.nodes" :key="`${node.nodePriority}-${node.index}`">
                        <td class="server-table__row">{{node.nodePriority}}</td>
                        <td class="server-table__row"><input type="text" maxlength="14" v-model="node.nodeIpAddress"></td>
                        <td class="server-table__row"><input type="text" v-model="node.nodeFQDN"></td>
                        <td class="server-table__row"><button class="server-table__delete_node" @click.prevent="deleteNode(index)">
                          <svg width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
                          <path d="M14.7924 8.00921V6.23888C14.7924 6.03251 14.6249 5.86504 14.4188 5.86504H11.9434V4.65097C11.9434 4.44482 11.7762 4.27734 11.57 4.27734H8.7678C8.56143 4.27734 8.39396 4.44482 8.39396 4.65097V5.86504H5.91879C5.71242 5.86504 5.54517 6.03251 5.54517 6.23888V8.00921C5.54517 8.21537 5.71242 8.38262 5.91879 8.38262H6.02604V15.8598C6.02604 16.0659 6.1933 16.2334 6.39945 16.2334H12.8788C13.6689 16.2334 14.3118 15.5908 14.3118 14.8009V8.38284H14.4186C14.6249 8.38284 14.7924 8.21559 14.7924 8.00921ZM9.14121 5.0246H11.1962V5.86504H9.14121V5.0246ZM13.5647 14.8011C13.5647 15.1789 13.257 15.4861 12.879 15.4861H6.77352V8.38284L13.5647 8.38328V14.8011ZM14.0449 7.63559H6.2922V6.61251H8.7678H11.57H14.0452V7.63559H14.0449Z" fill="black" fill-opacity="0.8"/>
                          <path d="M8.67401 9.88184H7.92676V14.2719H8.67401V9.88184Z" fill="black" fill-opacity="0.8"/>
                          <path d="M10.5422 9.88184H9.79492V14.2719H10.5422V9.88184Z" fill="black" fill-opacity="0.8"/>
                          <path d="M12.4103 9.88184H11.6631V14.2719H12.4103V9.88184Z" fill="black" fill-opacity="0.8"/>
                          </svg></button></td>
                      </tr>
                    </table>
                  </v-row>

                  <v-row no-gutters class="pl-0 pr-0">
                    <v-card-title class="pl-0 font-size-12 font-weight-bold" style="padding-top: 0px; padding-bottom: 5px;">Состояние сервера, время выполения заданий</v-card-title>
                    <v-spacer></v-spacer>
                  </v-row>
                  <v-divider class="ml-0 mr-0 mb-0"></v-divider>
                  <v-row align="top" :class="CONFIG.form.rowClass" style="margin-top: 9px;">
                    <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass2">
                      <v-subheader
                        :class="CONFIG.form.labelClass" style="font-size: 12px; padding-right: 0;"
                      >Состояние и статус</v-subheader>
                    </v-col>
                    <v-col :class="CONFIG.form.fieldColClass2">
                      <div style="display:flex; justify-content: flex-start; max-width: 416px;">
                        <p class="manufacturer-model-checknox_label" style="margin-left: 75px;">Состояние:</p>
                        <v-checkbox
                          v-model="createItem.serverIsEnabled"
                          color="primary"
                          hide-details
                          label="включен"
                          class="manufacturer-model-checknox"
                        ></v-checkbox>
                        <p class="manufacturer-model-checknox_label" style="margin-left: 0px;">Статус:</p>
                        <v-checkbox
                          v-model="createItem.serverIsTest"
                          color="primary"
                          hide-details
                          label="тестовый"
                          class="manufacturer-model-checknox"
                        ></v-checkbox>
                      </div>
                    </v-col>
                  </v-row>
                  <!--
                  <v-row align="top" :class="CONFIG.form.rowClass" style="margin-top: 9px;">
                    <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass2">
                      <v-subheader
                        :class="CONFIG.form.labelClass" style="font-size: 12px; padding-right: 0;"
                      >Время и часовой пояс</v-subheader>
                    </v-col>
                    <v-col :class="CONFIG.form.fieldColClass2">
                        <div style="display: flex; justify-content: space-between; width: 416px">
                          <div>
                            <v-text-field
                                class="width-53"
                                dense
                                outlined
                                type="time"
                                placeholder="00:00"
                              />
                          </div>
                          <div>
                              <v-autocomplete
                                id="time-zone"
                                class="time-zone"
                                dense
                                outlined
                                :items="timeZones"
                                item-value="id"
                                item-text="name"
                                placeholder="Часовой пояс"
                              ></v-autocomplete>
                          </div>

                        </div>
                    </v-col>
                  </v-row>
                -->


                </v-col>
              </v-row>
            </v-form>
          </v-card-text>
          <v-card-actions class="mr-3 mt-5 pb-5">
            <v-spacer></v-spacer>
              <v-btn
              class="white--text ps-btn-ok border-radius-1 button-close-warning"
              :color="CONFIG.colors.primary"
              @click="editForm"
              v-if="isEdit"
            >Редактировать</v-btn>
            <v-btn
              class="white--text ps-btn-ok border-radius-1 button-close-warning"
              :color="CONFIG.colors.primary"
              @click="saveForm"
              v-else
            >Добавить</v-btn>
            <v-btn
              autofocus
              class="ps-btn-perform white--text button-close-warning border-radius-1"
              color="secondary"
              @click="addDialog = false"
            >Отменить</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-slide-y-transition>

    <v-col cols="12" style="height: 100%" class="ps-col-wrapper">
      <v-card-title class="ps-card-title-top">
        <h1 class="page-header">{{$vuetify.lang.t('$vuetify.servers')}}</h1>
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
              class="white--text ps-btn-add ps-add-btn font-size-16"
              max-height="42"
              @click="showAddForm"
            >{{$vuetify.lang.t('$vuetify.add')}}</v-btn>
          </div>
        </v-card-actions>
        <v-card-text class="ps-card-text">
          <div :style="{ width: `${tableWidth}px`, position: 'relative'}" v-if="tableNotEmpty">
            <div class="table-width-setter" @mousedown="dragMouseDown(0, $event)" :style="{ left: `${columns.first}%`}"></div> 
            <div class="table-width-setter" @mousedown="dragMouseDown(1, $event)" :style="{ left: `${columns.first + columns.second}%`}"></div>
            <div class="table-width-setter" @mousedown="dragMouseDown(2, $event)" :style="{ left: `${columns.first + columns.second + columns.third}%`}"></div> 
            <div class="table-width-setter" @mousedown="dragMouseDown(3, $event)" :style="{ left: `${columns.first + columns.second + columns.third + columns.fourth}%`}"></div> 
            <div class="table-width-setter" @mousedown="dragMouseDown(4, $event)" :style="{ left: `${columns.first + columns.second + columns.third + columns.fourth + columns.fifth}%`}"></div> 
            <div class="table-width-setter" @mousedown="dragMouseDown(5, $event)" :style="{ left: `${columns.first + columns.second + columns.third + columns.fourth + columns.fifth + columns.six}%`}"></div> 
            <div class="table-width-setter" @mousedown="dragMouseDown(6, $event)" :style="{ left: `${columns.first + columns.second + columns.third + columns.fourth + columns.fifth + columns.six + columns.seven}%`}"></div>
            <div class="table-width-setter" @mousedown="dragMouseDown(7, $event)" :style="{ left: `${columns.first + columns.second + columns.third + columns.fourth + columns.fifth + columns.six + columns.seven + columns.eight}%`}"></div>
            <div class="table-width-setter" @mousedown="dragMouseDown(8, $event)" :style="{ left: `${columns.first + columns.second + columns.third + columns.fourth + columns.fifth + columns.six + columns.seven + columns.eight + columns.nine}%`}"></div>
          </div> 
          <v-data-table
            :options.sync="options"
            :server-items-length="SERVERS2.totalCount"
            :hide-default-footer="CONFIG.table.hideDefaultFooter"
            :header-props="CONFIG.table.headerProps"
            :class="CONFIG.table.class"
            :footer-props="CONFIG.table.footerProps"
            :fixed-header="CONFIG.table.fixedHeader"
            :dense="CONFIG.table.dense"
            ref="table"
            :height="windowHeight - 225"
            :headers="headers"
            :items="SERVERS2.items.servers"
            :page.sync="pagination.page"
            :items-per-page.sync="pagination.rowsPerPage"
            :search="search"
            item-key="idr"
            @page-count="pagination.totalPages = $event"
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
              <tr style="cursor: pointer" class="item" ref="tablecontainer">
                <td :style="{width: `${columns.first}%`}">{{ props.item.serverFQDN }}</td>
                <td :style="{width: `${columns.second}%`}">{{ props.item.serverIpAddress }}</td>
                <td :style="{width: `${columns.third}%`}">{{ props.item.serverPort }}</td>
                <td :style="{width: `${columns.fourth}%`}">{{ props.item.serverLogin }}</td>
                <td :style="{width: `${columns.fifth}%`}">{{ props.item.mode }}</td>
                <!-- Имя производителя по ID -->
                <td :style="{width: `${columns.six}%`}">{{props.item.serverVendorName}}</td>
                <!-- Название модели по ID -->
                <td :style="{width: `${columns.seven}%`}">{{MODELS.find(model => model.idr === props.item.serverVendorModelId) ? MODELS.find(model => model.idr === props.item.serverVendorModelId).name : null}}</td>
                <!-- Включен? - Текст -->
                <td :style="{width: `${columns.eight}%`}">
                  <div
                    class="status-elipse status-elipse-on"
                    v-if="props.item.serverIsEnabled"
                  >{{$vuetify.lang.t('$vuetify.yes')}}</div>
                  <div
                    class="status-elipse status-elipse-off"
                    v-else
                  >{{$vuetify.lang.t('$vuetify.no')}}</div>
                </td>
                <!-- Тестовый? - Текст -->
                <td :style="{width: `${columns.nine}%`}">
                  <div
                    class="status-elipse status-elipse-on"
                    v-if="props.item.serverIsTest"
                  >{{$vuetify.lang.t('$vuetify.yes')}}</div>
                  <div
                    class="status-elipse status-elipse-off"
                    v-else
                  >{{$vuetify.lang.t('$vuetify.no')}}</div>
                </td>
                <td :style="{width: `${columns.ten}%`}">
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
  </v-row>
</template>

<script>
import { mapActions, mapGetters } from "vuex";
import { required } from "@/helpers/validators";
import psNodesTable from "@/components/tables/ps-nodes-table";
import psPaginationPageSet from "@/components/pagination/ps-pagination-page-set";

export default {
  name: "Servers",
  components: {
    psNodesTable,
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
      point: 0,
      tableNotEmpty: true,
      infoWindow: false,
      message: {
        type: '',
        title: '',
        text1: '',
        text2: '',
      },
      tableWidth: 0,
      activeBar: 0,
      columns: {
        first: 14,
        second: 10,
        third: 6,
        fourth: 5,
        fifth: 10,
        six: 10,
        seven: 10,
        eight: 10,
        nine: 15,
        ten: 10,
      },
      options: {},
      timer: null,
      search: "",
      timeZones: [
        {id: 1, name: '(UTC +02:00) Калининград'},
        {id: 2, name: '(UTC +03:00) Москва, Санкт-Петербург'},
        {id: 3, name: '(UTC +04:00) Самара'},
        {id: 4, name: '(UTC +05:00) Екатеринбург'},
        {id: 5, name: '(UTC +06:00) Омск'},
        {id: 6, name: '(UTC +07:00) Красноярск'},
        {id: 7, name: '(UTC +08:00) Иркутск'},
        {id: 8, name: '(UTC +09:00) Якутия'},
        {id: 9, name: '(UTC +10:00) Владивосток'},
        {id: 10, name: '(UTC +11:00) Сахалин'},
        {id: 11, name: '(UTC +12:00) Камчатка'},
      ],
      regimes: [
        {id: 1, name: 'обычный'},
        {id: 2, name: 'необычный'},
      ],
      headers: [
        {
          text: this.$vuetify.lang.t("$vuetify.name"),
          value: "serverFQDN",
        },
        {
          text: this.$vuetify.lang.t("$vuetify.ipAddress"),
          value: "serverIpAddress",
        },
        {
          text: this.$vuetify.lang.t("$vuetify.port"),
          value: "serverPort",
        },
        {
          text: this.$vuetify.lang.t("$vuetify.login"),
          value: "serverLogin",
        },
        {
          text: this.$vuetify.lang.t("$vuetify.mode"),
          value: "mode",
        },
        {
          text: this.$vuetify.lang.t("$vuetify.manufacturer"),
          value: "serverVendorName",
        },
        {
          text: this.$vuetify.lang.t("$vuetify.model"),
          value: "serverVendorModelId",
        },
        {
          text: this.$vuetify.lang.t("$vuetify.isEnable"),
          value: "serverIsEnabled",
        },
        {
          text: this.$vuetify.lang.t("$vuetify.isTestServer"),
          value: "serverIsTes",
        },
        {
          text: this.$vuetify.lang.t("$vuetify.actions"),
          value: "actions",
          sortable: false,
        },
      ],
      dialog: false,
      deleteConfirmDialog: false,
      addDialog: false,
      editedIndex: -1,
      windowHeight: window.innerHeight,
      editedItem: {
        serverDescription: '',
        serverFQDN: '',
        serverVendorModelId: null,
        serverIsEnabled: false,
        serverIsTest: false,
        serverPassword: '',
        serverLogin: '',
        serverPort: null,
        nodes: []
      },
      createItem: {
        serverDescription: '',
        serverFQDN: '',
        serverVendorModelId: null,
        serverIsEnabled: false,
        serverIsTest: false,
        serverPassword: '',
        serverLogin: '',
        serverPort: null,
        nodes: []
      },
      defaultItem: {
        serverDescription: '',
        serverFQDN: '',
        serverIsEnabled: false,
        serverIsTest: false,
        serverPassword: '',
        serverLogin: '',
        serverPort: null,
        serverVendorModelId: null,
        nodes: []
      },
      deletedServer: null,
      isEdit: false,
      oldNodes: [],
      timezones: [
        {
          offset: "GMT-12:00",
          idr: 1,
        },
        {
          offset: "GMT-11:00",
          idr: 2,
        },
        {
          offset: "GMT-11:00",
          idr: 3,
        },
        {
          offset: "GMT-10:00",
          idr: 4,
        },
        {
          offset: "GMT-09:00",
          idr: 5,
        },
      ],
      nameRules: [() => required(this.createItem.serverFQDN)],
      modesNames: ["Обычный", "Реактивный", "Подводный"],
      passwordShow: false, // показывать пароль или нет
    };
  },
  watch: {
    options: {
      async handler ({sortBy, sortDesc}) {
        try {
          await this.FETCH_SERVERS2({ 
            limit: this.pagination.rowsPerPage, 
            offset: (this.pagination.page - 1) * this.pagination.rowsPerPage, 
            orderedColumn: sortBy[0], 
            sortDesc: sortDesc[0], 
            search: this.search
          })
        } catch(e) {
          if (e.response.status === 401) {
            this.LOGOUT()
            this.$router.push({ name: "login" });
          }
        }
      },
    },
    "pagination.page": {
      async handler() {
        try {
          await this.FETCH_SERVERS2({ 
            limit: this.pagination.rowsPerPage, 
            offset: (this.pagination.page - 1) * this.pagination.rowsPerPage, 
            orderedColumn: this.options.sortBy[0], 
            sortDesc: this.options.sortDesc[0], 
            search: this.search
          });
        } catch(e) {
          if (e.response.status === 401) {
            this.LOGOUT()
            this.$router.push({ name: "login" });
          }
        }
      },
    },
    "pagination.rowsPerPage": {
      async handler() {
        try {
          const servers = await this.FETCH_SERVERS2({ 
            limit: this.pagination.rowsPerPage, 
            offset: 0, 
            orderedColumn: this.options.sortBy[0], 
            sortDesc: this.options.sortDesc[0], 
            search: this.search
          });
          this.pagination.totalPages =(Math.floor(servers.totalCount / this.pagination.rowsPerPage)) + 1;
        } catch(e) {
          if (e.response.status === 401) {
            this.LOGOUT()
            this.$router.push({ name: "login" });
          }
        }
      },
    },
  },
  async mounted() {
    console.log(this.$refs)
    try {
      console.log(this.$refs)
    const servers = await this.FETCH_SERVERS2({ limit: 25, offset: 0 });
    console.log(this.$refs)
    this.pagination.totalItems = servers.totalCount;
    console.log(this.$refs)
    this.pagination.totalPages =(Math.floor(servers.totalCount / this.pagination.rowsPerPage)) + 1;
    console.log(this.$refs)
    await this.FETCH_VENDORS();
    console.log(this.$refs)
    await this.FETCH_MODELS();
    console.log(this.$refs)
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
    }, 1000)
    } catch(e) {
      if (e.response.status === 401) {
        this.LOGOUT()
        this.$router.push({ name: "login" });
      }
    }
  },
  computed: {
    ...mapGetters(["SERVERS2", "VENDORS", "MODELS", "CONFIG"]),
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
        ? this.$vuetify.lang.t("$vuetify.connectServer")
        : this.$vuetify.lang.t("$vuetify.editServer");
    },
    okBtnText() {
      return this.editedIndex === -1
        ? this.$vuetify.lang.t("$vuetify.connect")
        : this.$vuetify.lang.t("$vuetify.edit");
    },
  },
  methods: {
    ...mapActions([
      "LOGOUT",
      "FETCH_SERVERS2",
      "FETCH_NODES",
      "FETCH_VENDORS",
      "FETCH_MODELS",
      "ADD_SERVER",
      "CHANGE_SERVER",
      "REMOVE_SERVER",
      "FETCH_SERVER",
      "REMOVE_SERVER_NODE",
      "ADD_SERVER_NODE",
    ]),
    searchTimeOut() {  
      if (this.timer) {
        clearTimeout(this.timer);
        this.timer = null;
      }
      this.timer = setTimeout(() => {
        this.FETCH_SERVERS2({ 
          limit: this.pagination.rowsPerPage, 
          offset: this.pagination.page - 1, 
          search: this.search, 
          orderedColumn: this.options.sortBy[0], 
          sortDesc: this.options.sortDesc[0]
        }).then((result) => {
          this.pagination.totalItems = result.totalCount;
          this.pagination.totalPages =(Math.floor(result.totalCount / this.pagination.rowsPerPage)) + 1;
        });
      }, 1000);
    },
    addNode(type) {
      if (type === 'create') {
        const nodes = [...this.createItem.nodes]
        nodes.push({"nodeFQDN": "", "nodeIpAddress": "", "nodePriority": nodes.length + 1})
        this.createItem.nodes = nodes;
      }
    },
    showAddForm(){
      console.log(this.$refs)
      this.isEdit = false;
      this.editedId = null;
      this.createItem = {...this.defaultItem}
      this.addDialog = true;
    },
    deleteNode(index){
        console.log(index)
        if (this.createItem.nodes.length <= 1) {
          this.createItem.nodes = []
        } else {
          const nodes = this.createItem.nodes.filter((node, i) => i !== index)
          let priority = 1
          this.createItem.nodes = nodes.map((el) => {
            el.nodePriority = priority
            priority += 1
            return el
          });
        }
    },
    async deleteServer() {
      try {
        this.infoWindow = false;
        await this.REMOVE_SERVER(this.deletedServer)
        await this.FETCH_SERVERS2({ limit: this.pagination.rowsPerPage, offset: this.pagination.page - 1, orderedColumn: this.options.sortBy[0], sortDesc: this.options.sortDesc[0], search: this.search});
        this.message.type = 'success';
        this.message.title= 'Успешное удаление сервера',
        this.message.text1= `Сервер ${this.createItem.serverFQDN} успешно удален из системы`,
        this.message.text2= '',
        this.infoWindow = true;
      } catch(e) {
        this.message.type = 'error';
        this.message.title= 'Ошибка удаление сервера',
        this.message.text1= e.response.data.message || e.response.data.title,
        this.message.text2= '',
        this.infoWindow = true;
      }
    },
    async showEditForm(item) {
      const server = await this.FETCH_SERVER(item.serverId)
      console.log(server)
      this.isEdit = true;
      this.editedId = item.serverId
      this.createItem = server
      this.oldNodes = server.nodes
      this.addDialog = true;
    },
    async editForm() {
      try {
        if (this.$refs.form.validate()) {
          const data = {...this.createItem};
          console.log(data)
          for(let key in data) {
            if (typeof data[key] === "string" && data[key].trim() === '') {
              delete data[key]
            }
            if (key === "serverPort") {
              data[key] = parseInt(data[key], 10)
            }
            if (key === "nodes") {
              data.nodes = data.nodes.map((node) => {
                return {...node, nodePriority: parseInt(node.nodePriority, 10)}
              })
            }
          }
          console.log(data)
          const nodesIds = data.nodes.map((el) => el.nodeId)
          console.log(nodesIds)
          console.log(this.oldNodes)
          for (let i=0; i < this.oldNodes.length; i+=1) {
            if (!nodesIds.includes(this.oldNodes[i].nodeId)) {
              console.log(this.oldNodes[i])
              await this.REMOVE_SERVER_NODE(this.oldNodes[i].nodeId)
            }
          }
          if (data.nodes.filter((el)=> !el.nodeId).length > 0) {
            const serverData = {
              serverId: data.serverId,
              nodes: data.nodes.filter((el)=> !el.nodeId)
            }
            await this.ADD_SERVER_NODE(serverData)
          }
          console.log(data)

          await this.CHANGE_SERVER(data);
          const servers = await this.FETCH_SERVERS2({ limit: this.pagination.rowsPerPage, offset: this.pagination.page - 1, orderedColumn: this.options.sortBy[0], sortDesc: this.options.sortDesc[0], search: this.search});
          this.pagination.totalItems = servers.totalCount;
          this.pagination.totalPages =(Math.floor(servers.totalCount / this.pagination.rowsPerPage)) + 1;
          this.addDialog = false;
          this.message.type = 'success';
          this.message.title= 'Успешное обновление данных сервера',
          this.message.text1= `Данные сервера ${this.createItem.serverFQDN} в информационной системе успешно обновлены`,
          this.message.text2= '',
          this.infoWindow = true;
          this.isEdit = false;
          this.editedId = null;
          this.createItem = {...this.defaultItem}
        }
      } catch(e) {
        console.log(e)
        this.addDialog = false;
        this.message.type = 'error';
        this.message.title= 'Ошибка обновления данных сервера',
        this.message.text1= e.response.data.message || e.response.data.title || e.response.data,
        this.message.text2= '',
        this.infoWindow = true;
        this.isEdit = false;
        this.editedId = null;
        this.createItem = {...this.defaultItem}
      }
    },
    setPage(page) {
      this.pagination.page = parseInt(page);
      this.pageSetErrors = [];
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
      if (columns.first < 9.1 || columns.second < 5.1 || columns.third < 3.5 || columns.fourth < 4.1 || columns.fifth < 4.3 || columns.six < 8.5 || columns.seven < 5.4
      || columns.eight < 9.2 || columns.nine < 5.5) {
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
    handlePageSetErrors(errors) {
      this.pageSetErrors = errors;
    },
    showDeleteForm(item) {
          this.deletedServer = item.serverId;
          this.message.type = 'warning';
          this.message.title= `Предупреждение об удалении сервера ${item.serverFQDN}`,
          this.message.text1= `Сервер ${item.serverFQDN} будет удален из системы.Вы уверены?`,
          this.message.text2= '',
          this.infoWindow = true;
    },
    closeAddOrEditForm() {
      this.dialog = false;
    },
    closeDeleteForm() {
      this.deleteConfirmDialog = false;
    },
    deleteItem() {
      this.REMOVE_SERVER(this.editedItem);
      this.closeDeleteForm();
    },
    async saveForm() {
      try {
        if (this.$refs.form.validate()) {
          const data = {...this.createItem};
          console.log(data)
          for(let key in data) {
            if (typeof data[key] === "string" && data[key].trim() === '') {
              delete data[key]
            }
            if (key === "serverPort") {
              data[key] = parseInt(data[key], 10)
            }
            if (key === "nodes") {
              data.nodes = data.nodes.map((node) => {
                return {...node, nodePriority: parseInt(node.nodePriority, 10)}
                })
            }
          }
          console.log(data)
          await this.ADD_SERVER(data);
          const servers = await this.FETCH_SERVERS2({ limit: this.pagination.rowsPerPage, offset: this.pagination.page - 1, orderedColumn: this.options.sortBy[0], sortDesc: this.options.sortDesc[0], search: this.search});
          this.pagination.totalItems = servers.totalCount;
          this.pagination.totalPages =(Math.floor(servers.totalCount / this.pagination.rowsPerPage)) + 1;
          this.addDialog = false;
          this.message.type = 'success';
          this.message.title= 'Успешное добавление сервера',
          this.message.text1= `Сервер ${this.createItem.serverFQDN} успешно добавлен в систему`,
          this.message.text2= '',
          this.infoWindow = true;
        }
      } catch(e) {
        console.log(e.response)
        this.addDialog = false;
        this.message.type = 'error';
        this.message.title= 'Ошибка при создании сервера',
        this.message.text1= typeof e.response.data === 'string' ? e.response.data : e.response.data.message || e.response.data.title,
        this.message.text2= '',
        this.infoWindow = true;
      }
    },
  },
};
</script>
<style>
.server-table {
  width: 100%;
  max-height: 139px;
  overflow-y: scroll;
  font-weight: 500;
  font-size: 12px;
  line-height: 15px;
  border-collapse: collapse;
  background-color: #fff;
  margin-bottom: 21px;
}

.server-table__title-row {
  height: 19px;
}

.server-table__title-row th{
  border: 1px solid rgba(0, 0, 0, 0.15);
  font-weight: 500;
  font-size: 12px;
  line-height: 15px;
}

.server-table__title-row__1 {
  width: 13.41%;
}

.server-table__title-row__2 {
  width: 18.5%;
}

.server-table__title-row__3 {
  width: 44.65%;
}

.server-table__title-row__4 {
  width: 23.44%;
}

.server-table__row {
  height: 20px;
  border: 1px solid rgba(0, 0, 0, 0.15);
  font-weight: 500;
  font-size: 12px;
  line-height: 15px;
  padding-left: 5px;
}

.server-table__add-node {
  width: 16px;
  height: 16px;
  border: 1px solid #C4C4C4;
  border-radius: 50%;
  font-size: 12px;
  color: rgba(0, 0, 0, 0.6);
  margin-left: 5px;
  background: #D9D9D9;
}

.server-table__edit-cluster {
  margin-left: 5px;
}

.server-table__delete_node {
  width: 20px;
  height: 20px;
  margin: 0 auto;
}
</style>


