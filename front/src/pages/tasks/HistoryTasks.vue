
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
        <div style="display:flex; justify-content: flex-start; padding-top: 47px; align-items: center;">
          <div style="margin-left: 26px;">

            <img src="../../public/fail.png" v-if="message.type==='error'" width=72 height=72>
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
          <div style="margin-left: 27px;font-size: 16px; font-weight: 500; margin-right: 30px; line-height: 20px; color: rgba(0, 0, 0, 0.8);"><p style="margin-bottom: 0;">{{message.text1}}</p><p style="margin-bottom: 0;">{{message.text2}}</p></div>
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
    <!-- Журнал -->
    <v-col cols="12" style="height: 100%" class="ps-col-wrapper">
      <v-slide-y-transition>
        <!-- Диалог отмены задачи -->
        <v-dialog
          v-if="cancelDialog"
          no-click-animation
          persistent
          width="651px"
          v-model="cancelDialog"
          @keydown.esc="cancelDialog = false"
        >
          <v-card elevation="0" height="100%" class="d-flex flex-column">
            <div class="popup-header">
              <v-card-title class="pl-3 font-size-16 pt-5">Отмена задания для номера</v-card-title>
              <v-icon
                class="white--text"
                @click="closeCancelDialog"
                :color="CONFIG.colors.white"
              >mdi-close</v-icon>
            </div>
            <v-row no-gutters class="pl-7 pr-7">
              <v-card-title class="pl-0 pb-2 font-size-16">Отмена задания:{{editedItem.taskId}}</v-card-title>
              <v-spacer></v-spacer>
              <v-card-subtitle class="pl-0 pr-0 pb-2 pt-6 font-weight-medium">Автор: {{editedItem.userLogin}}</v-card-subtitle>
            </v-row>
            <v-divider class="ml-7 mr-7"></v-divider>

            <v-card-text style="height: auto">
              <v-form :disabled="cancelDialog" ref="form" lazy-validation class="pr-1 pl-1">
                <v-row>
                  <v-col class="pr-5 pl-3 pt-6 pb-0">
                    <v-row align="top" :class="CONFIG.form.rowClass3">
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
                          dense
                          outlined
                          :rules="[required, baseRule, maxLength128 ]"
                          rows="2"
                          auto-grow
                          v-model="editedItem.taskDescription"
                          :placeholder="$vuetify.lang.t('$vuetify.description')"
                        ></v-text-field>
                      </v-col>
                    </v-row>
                    <v-row align="top" :class="CONFIG.form.rowClass3">
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
                          item-value="taskId"
                          item-text="name"
                          :disabled="!editedItem.isTestServer"
                          :menu-props="{ top: true, offsetY: true }"
                          v-model="editedItem.serverTestId"
                        ></v-select>
                      </v-col>
                    </v-row>
                    <v-row :class="CONFIG.form.rowClass3" align="top" justify="center">
                      <v-col
                        :cols="CONFIG.vuetify.cols.labelCols"
                        :class="CONFIG.form.labelColClass2"
                      >
                        <v-subheader :class="CONFIG.form.labelClass">Дата, время запуска</v-subheader>
                      </v-col>
                      <v-col cols="8" :class="CONFIG.form.fieldColClassNopadding2">
                        <div style="display: flex; justify-content: space-between; width: 421px; padding-right: 2px">
                          <div class="datetime-wrapper width-149">
                            <div class="date-picker-wrapper2 width-83">
                              <v-menu
                                content-class="ps-datepicker"
                                :close-on-content-click="false"
                                transition="scale-transition"
                                offset-y
                              >
                                <template v-slot:activator="{ on, attrs }">
                                  <v-text-field
                                    hide-details
                                    ref="runTimeDate"
                                    dense
                                    outlined
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
                                  v-model="editedItem.taskDateRunDate"
                                ></v-date-picker>
                              </v-menu>
                            </div>
                            <div class="time-input-wrapper width-51">
                              <v-text-field
                                hide-details
                                @change="validateDate()"
                                dense
                                outlined
                                v-model="editedItem.taskDateRunTime"
                                placeholder="00:00"
                                
                              ></v-text-field>
                            </div>
                          </div>
                          <div style="padding-top: 3px; width: 124px; text-align: center;">и завершения</div>
                          <div class="datetime-wrapper width-149">
                            <div class="date-picker-wrapper2 width-83">
                              <v-menu
                                content-class="ps-datepicker"
                                :close-on-content-click="false"
                                transition="scale-transition"
                                offset-y
                              >
                                <template v-slot:activator="{ on, attrs }">
                                  <v-text-field
                                    hide-details
                                    ref="runTimeDate"
                                    dense
                                    outlined
                                    :rules="[endDateIsValidCheck]"
                                    full-width
                                    v-model="editedItem.dateEndDate"
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
                                  :rules="[endDateIsValidCheck]"
                                  @input="menu2 = false"
                                  v-model="editedItem.taskDateEndDate"
                                ></v-date-picker>
                              </v-menu>
                            </div>
                            <div class="time-input-wrapper width-51">
                              <v-text-field
                                hide-details
                                @change="validateDate()"
                                dense
                                outlined
                                :rules="[runDateIsValidCheck]"
                                :disabled="editedItem.isRunNow"
                                v-model="editedItem.taskDateEndTime"
                                placeholder="00:00"
                                
                              ></v-text-field>
                            </div>
                          </div>
                        </div>
                        <!-- <v-slide-x-transition v-if="editedItem.runTimeDate">
                          <div style="color: red; font-size: 8px">Ошибка неверно указана дата</div>
                        </v-slide-x-transition>-->
                      </v-col>
                    </v-row>
                    <v-row class="mt-7" :class="CONFIG.form.rowClass3">
                      <v-card-subtitle
                        :class="CONFIG.form.labelClass"
                        class="font-weight-bold"
                      >{{$vuetify.lang.t('$vuetify.telephoneSet')}}</v-card-subtitle>
                    </v-row>
                    <v-row align="top" :class="CONFIG.form.rowClass3">
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
                          v-model="editedItem.device['Description']"
                          :placeholder="$vuetify.lang.t('$vuetify.description')"
                        ></v-text-field>
                      </v-col>
                    </v-row>
                    <v-row align="top" :class="CONFIG.form.rowClass3">
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
                    <v-row align="top" class="mt-2" :class="CONFIG.form.rowClass3">
                      <v-card-subtitle
                        :class="CONFIG.form.labelClass"
                        class="font-weight-bold"
                      >{{$vuetify.lang.t('$vuetify.line')}}</v-card-subtitle>
                    </v-row>
                    <v-row align="top" :class="CONFIG.form.rowClass3">
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
                    <v-row align="top" :class="CONFIG.form.rowClass3">
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
                    <v-row align="top" :class="CONFIG.form.rowClass3">
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
                    <v-row align="top" :class="CONFIG.form.rowClass3">
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
                    <v-row align="top" :class="CONFIG.form.rowClass3">
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
                    <v-row align="top" :class="CONFIG.form.rowClass3">
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



            <v-card-actions class="mb-5 mt-5 ml-4 mr-4">
              <v-spacer></v-spacer>
              <v-btn
                class="white--text ps-btn-ok mr-3"
                style="width: 198px; height: 26px;"
                :color="CONFIG.colors.primary"
              >Добавить с тестированием</v-btn>
              <v-btn
                class="white--text ps-btn-ok mr-3"
                style="width: 126px; height: 26px;"
                @click="cancelTask"
                :color="CONFIG.colors.primary"
              >Добавить</v-btn>
              <v-btn
                autofocus
                @click="closeCancelDialog"
                color="secondary"
                class="button-close" style="margin-right: 4px; height: 26px;"
              >{{$vuetify.lang.t('$vuetify.close')}}</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-slide-y-transition>
      <v-slide-y-transition>
        <!-- Диалог для журнала -->
        <v-dialog
          v-if="logDialog"
          no-click-animation
          persistent
          v-model="logDialog"
          width="651px"
          @keydown.esc="logDialog = false"
        >
          <v-card elevation="0" height="100%" class="d-flex flex-column">
            <div class="popup-header">
              <v-card-title class="journal-title">Просмотр журнала завершенного задания</v-card-title>
              <v-icon
                class="white--text"
                @click="logDialog = false"
                :color="CONFIG.colors.white"
              >mdi-close</v-icon>
            </div>
            <v-row no-gutters class="pl-7 pr-7">
              <v-card-title class="pl-0 pb-3 pt-3 font-size-16">
                Номер телефона: 
                <div class="card-title__phone-number ml-1"> 123456</div>
              </v-card-title>
              <v-spacer></v-spacer>
              <v-card-subtitle class="pl-0 pr-2 pb-2 pt-5 font-weight-medium">Номер задания: 89</v-card-subtitle>
              <v-card-subtitle class="pl-0 pr-0 pb-2 pt-5 font-weight-medium">Автор: Admin</v-card-subtitle>
            </v-row>
            <v-divider class="ml-7 mr-7"></v-divider>
            <v-card-text>
              <ps-log-row text="Процедура 1" status="done" />
              <ps-log-row text="Процедура 2" status="done" />
              <ps-log-row text="Процедура 3" status="done" />
              <ps-log-row text="Процедура 4" status="done" />
              <ps-log-row text="Процедура 5" status="done" />
              <ps-log-row text="Процедура 6" status="fail" />
              <ps-log-row text="Процедура 7" status="wait" />
              <ps-log-row text="Процедура 8" status="wait" />
              <ps-log-row text="Процедура 9" status="wait" />
              <ps-log-row text="Процедура 10" status="wait" />
              <ps-log-row text="Процедура 11" status="wait" />
              <ps-log-row text="Процедура 12" status="wait" />
              <ps-log-row text="Процедура 13" status="wait" />
            </v-card-text>
            <v-card-actions class="pb-7 pt-5">
              <v-spacer></v-spacer>
              <v-btn autofocus color="secondary" @click="logDialog = false" class="button-close" style="margin-right: 19px; height: 26px;">Закрыть</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-slide-y-transition>
      <v-slide-y-transition>
        <!-- Диалог изменения задачи -->
        <v-dialog
          v-if="settingsDialog"
          no-click-animation
          persistent
          v-model="settingsDialog"
          width="651px"
          @keydown.esc="settingsDialog = false"
        >
          <v-card elevation="0" height="100%" class="d-flex flex-column">
            <div class="popup-header">
              <v-card-title
                class="pl-3 font-size-16 font-weight-bold"
              >Просмотр завершенного задания для номера {{editedItem.devicePhoneNumber}}</v-card-title>
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
                    <v-row align="top" :class="CONFIG.form.rowClass3">
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
                          dense
                          outlined
                          :rules="[required, baseRule, maxLength128 ]"
                          rows="2"
                          auto-grow
                          v-model="editedItem.taskDescription"
                          :placeholder="$vuetify.lang.t('$vuetify.description')"
                        ></v-text-field>
                      </v-col>
                    </v-row>
                    <v-row align="top" :class="CONFIG.form.rowClass3">
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
                          item-value="taskId"
                          item-text="name"
                          :disabled="!editedItem.isTestServer"
                          :menu-props="{ top: true, offsetY: true }"
                          v-model="editedItem.serverTestId"
                        ></v-select>
                      </v-col>
                    </v-row>
                    <v-row :class="CONFIG.form.rowClass3" align="top" justify="center">
                      <v-col
                        :cols="CONFIG.vuetify.cols.labelCols"
                        :class="CONFIG.form.labelColClass2"
                      >
                        <v-subheader :class="CONFIG.form.labelClass">Дата, время запуска</v-subheader>
                      </v-col>
                      <v-col cols="8" :class="CONFIG.form.fieldColClassNopadding2">
                        <div style="display: flex; justify-content: space-between; width: 421px; padding-right: 2px">
                          <div class="datetime-wrapper width-149">
                            <div class="date-picker-wrapper2 width-83">
                              <v-menu
                                content-class="ps-datepicker"
                                :close-on-content-click="false"
                                transition="scale-transition"
                                offset-y
                              >
                                <template v-slot:activator="{ on, attrs }">
                                  <v-text-field
                                    hide-details
                                    ref="runTimeDate"
                                    dense
                                    outlined
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
                                  v-model="editedItem.taskDateRunDate"
                                ></v-date-picker>
                              </v-menu>
                            </div>
                            <div class="time-input-wrapper width-51">
                              <v-text-field
                                hide-details
                                @change="validateDate()"
                                dense
                                outlined
                                v-model="editedItem.taskDateRunTime"
                                placeholder="00:00"
                                
                              ></v-text-field>
                            </div>
                          </div>
                          <div style="padding-top: 3px; width: 124px; text-align: center;">и завершения</div>
                          <div class="datetime-wrapper width-149">
                            <div class="date-picker-wrapper2 width-83">
                              <v-menu
                                content-class="ps-datepicker"
                                :close-on-content-click="false"
                                transition="scale-transition"
                                offset-y
                              >
                                <template v-slot:activator="{ on, attrs }">
                                  <v-text-field
                                    hide-details
                                    ref="runTimeDate"
                                    dense
                                    outlined
                                    :rules="[endDateIsValidCheck]"
                                    full-width
                                    v-model="editedItem.dateEndDate"
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
                                  :rules="[endDateIsValidCheck]"
                                  @input="menu2 = false"
                                  v-model="editedItem.taskDateEndDate"
                                ></v-date-picker>
                              </v-menu>
                            </div>
                            <div class="time-input-wrapper width-51">
                              <v-text-field
                                hide-details
                                @change="validateDate()"
                                dense
                                outlined
                                :rules="[runDateIsValidCheck]"
                                :disabled="editedItem.isRunNow"
                                v-model="editedItem.taskDateEndTime"
                                placeholder="00:00"
                                
                              ></v-text-field>
                            </div>
                          </div>
                        </div>
                        <!-- <v-slide-x-transition v-if="editedItem.runTimeDate">
                          <div style="color: red; font-size: 8px">Ошибка неверно указана дата</div>
                        </v-slide-x-transition>-->
                      </v-col>
                    </v-row>
                    <v-row class="mt-7" :class="CONFIG.form.rowClass3">
                      <v-card-subtitle
                        :class="CONFIG.form.labelClass"
                        class="font-weight-bold"
                      >{{$vuetify.lang.t('$vuetify.telephoneSet')}}</v-card-subtitle>
                    </v-row>
                    <v-row align="top" :class="CONFIG.form.rowClass3">
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
                          v-model="editedItem.device['Description']"
                          :placeholder="$vuetify.lang.t('$vuetify.description')"
                        ></v-text-field>
                      </v-col>
                    </v-row>
                    <v-row align="top" :class="CONFIG.form.rowClass3">
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
                    <v-row align="top" class="mt-2" :class="CONFIG.form.rowClass3">
                      <v-card-subtitle
                        :class="CONFIG.form.labelClass"
                        class="font-weight-bold"
                      >{{$vuetify.lang.t('$vuetify.line')}}</v-card-subtitle>
                    </v-row>
                    <v-row align="top" :class="CONFIG.form.rowClass3">
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
                    <v-row align="top" :class="CONFIG.form.rowClass3">
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
                    <v-row align="top" :class="CONFIG.form.rowClass3">
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
                    <v-row align="top" :class="CONFIG.form.rowClass3">
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
                    <v-row align="top" :class="CONFIG.form.rowClass3">
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
                    <v-row align="top" :class="CONFIG.form.rowClass3">
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
            <v-card-actions class="pt-0 pb-4 mr-5 ml-5">
              <v-spacer></v-spacer>
              <v-btn color="secondary" class="button-close" @click="settingsDialog = false" autofocus>Закрыть</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-slide-y-transition>
      <v-card-title class="ps-card-title-top">
        <h1 class="page-header">{{$vuetify.lang.t('$vuetify.completed')}}</h1>
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
          <div class="top-btn-width-period" style="margin-right: 15px">
            <v-menu
              content-class="ps-datepicker"
              ref="periodMenu"
              v-model="periodMenu"
              :close-on-content-click="false"
              transition="scroll-y-transition"
              offset-y
            >
              <template v-slot:activator="{ on, attrs }">
                <v-text-field
                  append-icon="mdi-chevron-down"
                  class="top-padding ps-select grey font-size-14"
                  dense
                  outlined
                  hide-details
                  v-model="dateRangeText"
                  :placeholder="$vuetify.lang.t('$vuetify.period')"
                  readonly
                  v-bind="attrs"
                  v-on="on"
                  @click:append="periodMenu = true"
                ></v-text-field>
              </template>
              <v-date-picker color="primary" v-model="periodDates" no-title range scrollable>
                <v-spacer></v-spacer>
                <v-btn
                  class="ps-btn-ok"
                  text
                  :color="CONFIG.colors.primary"
                  @click="$refs.periodMenu.save(date)"
                >OK</v-btn>
                <v-spacer></v-spacer>
              </v-date-picker>
            </v-menu>
          </div>
          <v-btn
            tile
            class="white--text refresh-btn ml"
            :color="CONFIG.colors.primary"
            small
            @click.prevent="refreshCompletedTasks"
          >
            <v-icon>mdi-chevron-right</v-icon>
          </v-btn>
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
            <div class="table-width-setter" @mousedown="dragMouseDown(9, $event)" :style="{ left: `${columns.first + columns.second + columns.third + columns.fourth + columns.fifth + columns.six + columns.seven + columns.eight + columns.nine + columns.ten}%`}"></div>
            <div class="table-width-setter" @mousedown="dragMouseDown(10, $event)" :style="{ left: `${columns.first + columns.second + columns.third + columns.fourth + columns.fifth + columns.six + columns.seven + columns.eight + columns.nine + columns.ten + columns.eleven}%`}"></div>
          </div> 
          <v-data-table
            :options.sync="options"
            :hide-default-footer="CONFIG.table.hideDefaultFooter"
            :header-props="CONFIG.table.headerProps"
            :class="CONFIG.table.class"
            :footer-props="CONFIG.table.footerProps"
            :dense="CONFIG.table.dense"
            :height="windowHeight - 225"
            :fixed-header="CONFIG.table.fixedHeader"
            :headers="headers"
            :server-items-length="COMPLETED_TASKS.totalCount"
            :items="COMPLETED_TASKS.items"
            :page.sync="pagination.page"
            :items-per-page.sync="pagination.rowsPerPage"
            :search="search"
            @page-count="pagination.totalPages = $event"
            single-select
          >
            <template v-slot:footer="">
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
              <tr
                ref="tablecontainer"
                style="cursor: pointer"
                @click="showEditForm(props.item)"
                :class="{ 'v-data-table__selected':editedItem.taskId === props.item.taskId }"
              >
                <td :style="{width: `${columns.first}%`}">{{ props.item.taskId }}</td>
                <td :style="{width: `${columns.second}%`}">{{ props.item.devicePhoneNumber }}</td>
                <td :style="{width: `${columns.third}%`}">{{ props.item.taskType }}</td>
                <td :style="{width: `${columns.fourth}%`}">{{ props.item.userName }}</td>
                <td :style="{width: `${columns.fifth}%`}">{{ props.item.taskDescription }}</td>
                <td :style="{width: `${columns.six}%`}">{{ props.item.taskDateRun | formatDateTime}}</td>
                <td :style="{width: `${columns.seven}%`}">{{ props.item.taskDateEnd | formatDateTime}}</td>
                <td :style="{width: `${columns.eight}%`}">
                  <ps-is-testing-value :item="props.item" />
                </td>
                <td :style="{width: `${columns.nine}%`}">
                  <div
                    v-if="props.item.taskStatus.toUpperCase() == 'OK'"
                    class="icon-with-text-wrapper"
                  >
                    <div class="table-icon-wrapper">
                      <svg-icon name="done" />
                    </div>
                    {{$vuetify.lang.t('$vuetify.yes')}}
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
                <td :style="{width: `${columns.ten}%`}">
                  <div class="icon-with-text-wrapper" v-if="props.item.taskCancelId">
                    <div class="table-icon-wrapper">
                      <svg-icon name="done" />
                    </div>
                    {{$vuetify.lang.t('$vuetify.yes')}}
                  </div>
                  <div class="icon-with-text-wrapper" v-else>
                    <div class="table-icon-wrapper">
                      <svg-icon name="fail" />
                    </div>
                    {{$vuetify.lang.t('$vuetify.no')}}
                  </div>
                </td>
                <td :style="{width: `${columns.eleven}%`}">
                  <div>
                    <v-icon
                      @click.stop.prevent="showLogDialog(props.item)"
                      class="cursor: pointer"
                    >mdi-file-outline</v-icon>
                  </div>
                </td>
                <td :style="{width: `${columns.twelve}%`}">
                  <div v-if="!props.item.taskCancelId" @click="showCancelForm(props.item, $event)">
                    <div class="table-icon-wrapper dark-grey--color">
                      <svg-icon name="return-arrow-left" />
                    </div>
                    <!-- <v-btn
                      class="white--text ps-btn-cancel"
                      @click.stop.prevent="showCancelForm(props.item)"
                      :color="CONFIG.colors.primary"
                      rounded
                    >{{$vuetify.lang.t('$vuetify.cancel')}}</v-btn>-->
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
import uniqid from "uniqid";
import psIsTestingValue from "@/components/tables/computed-rows/history/ps-is-testing-value/";
import psPaginationPageSet from "@/components/pagination/ps-pagination-page-set";
import psLogRow from "@/components/logs/ps-log-row";
import svgIcon from "@/components/svg-icons/svg-icon";
import filters from "@/helpers/filters";

export default {
  name: "HistoryTasks",
  components: {
    svgIcon,
    psIsTestingValue,
    psPaginationPageSet,
    psLogRow,
  },
  filters: {
    ...filters,
  },
  data() {
    return {
      uuid: null,
      search: "",
      tableNotEmpty: false,
      infoWindow: false,
      message: {
        title: '',
        type: 'warning',
        text1: '',
        text2: '',
        continue: false,
      },
      options: {},
      timer: null,
      tableWidth: 0,
      point: 0,
      activeBar: 0,
      columns: {
        first: 3,
        second: 10,
        third: 6,
        fourth: 5,
        fifth: 8,
        six: 10,
        seven: 11,
        eight: 9,
        nine: 9,
        ten: 9,
        eleven: 10,
        twelve: 10,
      },
      headers: [
        {
          text: "№",
          value: "taskId",
        },
        {
          text: this.$vuetify.lang.t("$vuetify.phoneNumber"),
          value: "devicePhoneNumber",
        },
        {
          text: this.$vuetify.lang.t("$vuetify.task"),
          value: "taskId",
        },
        {
          text: this.$vuetify.lang.t("$vuetify.author"),
          value: "userName",
        },
        {
          text: this.$vuetify.lang.t("$vuetify.description"),
          value: "taskDescription",
        },
        {
          text: this.$vuetify.lang.t("$vuetify.dateRun"),
          value: "taskDateRun",
        },
        {
          text: this.$vuetify.lang.t("$vuetify.dateEnd"),
          value: "taskDateEnd",
        },
        {
          text: this.$vuetify.lang.t("$vuetify.isTesting"),
          value: "isTesting",
        },
        {
          text: this.$vuetify.lang.t("$vuetify.status"),
          value: "taskStatus",
        },
        {
          text: this.$vuetify.lang.t("$vuetify.cancel"),
          value: "cancel",
        },
        {
          text: this.$vuetify.lang.t("$vuetify.log"),
          value: "log",
        },
        {
          text: this.$vuetify.lang.t("$vuetify.actions"),
          value: "actions",
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
        taskId: null,
        taskType: null,
        taskDescription: null,
        userName: null,
        isTestServer: false,
        TestServeridr: null,
        isRunNow: false,
        runTimeDate: new Date().toISOString().substr(0, 10),
        endTimeDate: null,
        runTimeHoursMinutes: "00:00",
        endTimeHoursMinutes: "00:00",
        telephone: {
          description: null,
          login: null,
        },
        line: {
          description: null,
          alertingName: null, // link: line.description
          asciiAlertingName: null,
          display: null, // link: line.description
          asciiDisplay: null,
          userAssociated: null, // link: telephone.login
        },
      },
      defaultItem: {
        taskId: null,
        devicePhoneNumber: '',
        taskType: "Смена владельца телефона",
        userName: null,
        taskDescription: null,
        serverName: null,
        ServerTestBench: null,
        serverId: null,
        taskDateRunDate: null,
        taskDateRunTime: null,
        taskDateEndDate: null,
        taskDateEndTime: null,
        device: {
          'Description': null,
          'Owner User ID': null,
          'Line': {
            'ASCII Alerting Name': null, 
            'ASCII Display (Caller ID)': null,
            'Alerting Name': null,
            'Description': null,
            'Display (Caller ID)': null,
            'User Associated with Line': null,
          }
        }
      },
      periodDates: [],
      periodMenu: false,
      selectedTask: null,
      logDialog: false,
      cancelDialog: false,
      // rules
      baseRule: (text) => {
        const e =
          new RegExp("^[a-zA-Z0-9 !#$'()*+,./:;=?@^_`}~-]{0,1000}$").test(
            text
          ) || this.$vuetify.lang.t("$vuetify.invalidCharacters"); //eslint-disable-line
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
            this.editedItem.TestServerId != null ||
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
      endDateIsValidCheck: () => {
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
        if (this.periodDates.length > 0) {
          const periodDates = [...this.periodDates]
          if (periodDates.length === 1) periodDates.push(periodDates[0])
          const date1 = new Date(periodDates[0])
          const date2 = new Date(periodDates[1])
          const dateFrom = date1 <= date2 ? `${periodDates[0]}T00:00:00.000Z` : `${periodDates[1]}T00:00:00.000Z`;
          const dateTo = date1 <= date2 ? `${periodDates[1]}T23:59:59.000Z` : `${periodDates[0]}T23:59:59.000Z`;
          this.FETCH_COMPLETED_TASKS_DATE({
            dateFrom: dateFrom,
            dateTo: dateTo,
            orderDesc: this.options.sortDesc[0],
            orderedColumn: this.options.sortBy[0] || 'taskDateEnd', 
            limit: this.pagination.rowsPerPage,
            offset: (this.pagination.page - 1)*this.pagination.rowsPerPage,
          })
        } else {
          this.FETCH_COMPLETED_TASKS({
            limit: this.pagination.rowsPerPage,
            offset: (this.pagination.page - 1)*this.pagination.rowsPerPage,
            search: this.search, 
            orderedColumn: this.options.sortBy[0], 
            orderDesc: this.options.sortDesc[0]
          });
        }
      },
    },
    "pagination.rowsPerPage": {
      async handler() {
        if (this.periodDates.length > 0) {
          const periodDates = [...this.periodDates]
          if (periodDates.length === 1) periodDates.push(periodDates[0])
          const date1 = new Date(periodDates[0])
          const date2 = new Date(periodDates[1])
          const dateFrom = date1 <= date2 ? `${periodDates[0]}T00:00:00.000Z` : `${periodDates[1]}T00:00:00.000Z`;
          const dateTo = date1 <= date2 ? `${periodDates[1]}T23:59:59.000Z` : `${periodDates[0]}T23:59:59.000Z`;
          const res = await this.FETCH_COMPLETED_TASKS_DATE({
            dateFrom,
            dateTo,
            orderDesc: this.options.sortDesc[0],
            orderedColumn: this.options.sortBy[0] || 'taskDateEnd', 
            limit: this.pagination.rowsPerPage,
            offset: 0,
          })
          this.pagination.totalItems = res.totalCount;
          this.pagination.totalPages =
            (await Math.floor(
              res.totalCount / this.pagination.rowsPerPage
            )) + 1;
          this.pagination.page = 1;
        } else {
          const res = await this.FETCH_COMPLETED_TASKS({
            limit: this.pagination.rowsPerPage,
            offset: 0,
            search: this.search, 
            orderedColumn: this.options.sortBy[0], 
            orderDesc: this.options.sortDesc[0]
          });
          this.pagination.totalItems = res.totalCount;
          this.pagination.totalPages =
            (await Math.floor(
              res.totalCount / this.pagination.rowsPerPage
            )) + 1;
          this.pagination.page = 1;
        }
      },
    },
    options: {
        async handler ({sortBy, sortDesc}) {
          console.log({sortBy, sortDesc})
          if (this.periodDates.length > 0) {
            const periodDates = [...this.periodDates]
            if (periodDates.length === 1) periodDates.push(periodDates[0])
            const date1 = new Date(periodDates[0])
            const date2 = new Date(periodDates[1])
            const dateFrom = date1 <= date2 ? `${periodDates[0]}T00:00:00.000Z` : `${periodDates[1]}T00:00:00.000Z`;
            const dateTo = date1 <= date2 ? `${periodDates[1]}T23:59:59.000Z` : `${periodDates[0]}T23:59:59.000Z`;
            const res = await this.FETCH_COMPLETED_TASKS_DATE({
              dateFrom,
              dateTo,
              orderDesc: sortDesc[0], 
              orderedColumn: sortBy[0] || 'taskDateEnd', 
              limit: this.pagination.rowsPerPage,
              offset: (this.pagination.page - 1)*this.pagination.rowsPerPage,
            })
            this.pagination.totalItems = res.totalCount;
            this.pagination.totalPages =
              (await Math.floor(
                res.totalCount / this.pagination.rowsPerPage
              )) + 1;
          } else {
            const res = await this.FETCH_COMPLETED_TASKS({ 
              limit: this.pagination.rowsPerPage, 
              offset: (this.pagination.page - 1)*this.pagination.rowsPerPage, 
              orderedColumn: sortBy[0], 
              orderDesc: sortDesc[0], 
              search: this.search
            })
            this.pagination.totalItems = res.totalCount;
            this.pagination.totalPages =
              (await Math.floor(
                res.totalCount / this.pagination.rowsPerPage
              )) + 1;
            }
        },
      },
  },
  async mounted() {
    this.fetchCompletedTasks();
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
    ...mapGetters([
      "CURRENT_TASKS",
      "COMPLETED_TASKS",
      "LOGS",
      "SERVERS",
      "CONFIG",
    ]),
    timeFieldType() {
      if (this.editedItem.runTimeHoursMinutes) {
        return "time";
      } else {
        return "text";
      }
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
    computedRunTimeDateFormatted() {
      return this.formatDate(this.editedItem.runTimeDate);
    },
    dateRangeText() {
      if (this.periodDates.length === 2) {
        const date1 = new Date(this.periodDates[0])
        const date2 = new Date(this.periodDates[1])
        if (date2 > date1) {
          return [this.periodDates[0], this.periodDates[1]].join(" — ");
        } else {
          return [this.periodDates[1], this.periodDates[0]].join(" — ");
        }
      } else if (this.periodDates.length === 1) {
        return [this.periodDates[0], this.periodDates[0]].join(" — ");
      } else {
        return ''
      }
    },
  },
  methods: {
    ...mapActions([
      "FETCH_COMPLETED_TASKS",
      "FETCH_COMPLETED_TASKS_DATE",
      "FETCH_CURRENT_TASKS",
      "FETCH_LOGS",
      "FETCH_SERVERS",
      "FETCH_TASK",
      "CANCEL_COMPLETED_ACTION",
    ]),
    ...filters,
    setTimeField() {
      this.editedItem.runTimeHoursMinutes = "00:00";
    },
    async fetchCompletedTasks() {
      const res = await this.FETCH_COMPLETED_TASKS({
        limit: this.pagination.rowsPerPage,
        offset: this.pagination.page - 1,
      });
      this.pagination.totalItems = res.totalCount;
      this.pagination.totalPages =
        (await Math.floor(
          res.totalCount / this.pagination.rowsPerPage
        )) + 1;
      await this.FETCH_LOGS();
      await this.FETCH_CURRENT_TASKS({ limit: 100, offset: 0 });
      await this.FETCH_SERVERS({ limit: 100, offset: 0 });
    },
    async refreshCompletedTasks() {
      console.log(this.periodDates)
      if (this.periodDates.length < 1) return
      const periodDates = [...this.periodDates]
      if (periodDates.length === 1) periodDates.push(periodDates[0])
      const date1 = new Date(periodDates[0])
      const date2 = new Date(periodDates[1])
      const dateFrom = date1 <= date2 ? `${periodDates[0]}T00:00:00.000Z` : `${periodDates[1]}T00:00:00.000Z`;
      const dateTo = date1 <= date2 ? `${periodDates[1]}T23:59:59.000Z` : `${periodDates[0]}T23:59:59.000Z`;
      const res = await this.FETCH_COMPLETED_TASKS_DATE({
        dateFrom: dateFrom,
        dateTo: dateTo,
        orderDesc: this.options.sortDesc[0],
        orderedColumn: this.options.sortBy[0] || 'taskDateEnd', 
        limit: this.pagination.rowsPerPage,
        offset: this.pagination.page - 1,
      })
      this.pagination.totalItems = res.totalCount;
      this.pagination.totalPages =
        (await Math.floor(
          res.totalCount / this.pagination.rowsPerPage
        )) + 1;
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
      if (columns.first < 3 || columns.second < 9.1 || columns.third < 5 || columns.fourth < 4 || columns.fifth < 6 || columns.six < 8.2
      || columns.seven < 10.4 || columns.eight < 7.8 || columns.nine < 4.4 || columns.ten < 4.7 || columns.eleven < 4.9 || columns.twelve < 5.9) {
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
        this.FETCH_COMPLETED_TASKS({ limit: this.pagination.rowsPerPage, offset: this.pagination.page - 1, search: this.search, orderedColumn: this.options.sortBy[0], orderDesc: this.options.sortDesc[0] }).then((result) => {
          console.log(result)
          this.pagination.totalItems = result.totalCount;
          this.pagination.totalPages =(Math.floor(result.totalCount / this.pagination.rowsPerPage)) + 1;
        }).catch((e)=>{
          console.log(e)
          this.pagination.totalItems = e.totalCount;
          this.pagination.totalPages =(Math.floor(e.totalCount / this.pagination.rowsPerPage)) + 1;
        })
      }, 1000);
    },
    setPage(page) {
      this.pagination.page = parseInt(page);
      this.pageSetErrors = [];
    },
    handlePageSetErrors(errors) {
      this.pageSetErrors = errors;
    },
    async showEditForm(item) {
      try {
        if (!this.cancelDialog && !this.settingsDialog) {
          const data = {taskNumber: item.taskId, type: 'done'}
          const res = await this.FETCH_TASK(data);
          const itemData = JSON.parse(res.deviceJson);
          res.device = itemData.Device
          if (res.taskDateRun) {
            res.taskDateRunDate = res.taskDateRun.split(' ')[0]
            res.taskDateRunTime = res.taskDateRun.split(' ')[1]
          }
          if (res.taskDateEnd) {
            res.taskDateEndDate = res.taskDateEnd.split(' ')[0]
            res.taskDateEndTime = res.taskDateEnd.split(' ')[1]
          }
          console.log(res)
          this.editedItem = Object.assign(this.defaultItem, {
            ...res,
          });
          this.settingsDialog = true;
        }
      } catch(e) {
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
    async showCancelForm(item, e) {
      /*
      if (!this.cancelDialog && !this.settingsDialog) {
        this.editedIndex = 1;
        this.editedItem = Object.assign(this.defaultItem, {
          ...item,
          isTestServer: item.TestServerId !== null,
        });
        console.log(this.editedItem)
        this.cancelDialog = true;
      }
    */
      e.stopPropagation()
      try {
        console.log(item)
        const uuid = uniqid('', `-${uniqid.time()}-${item.idr}-preview`);
        const data = {
          meta: {
            UUID: uuid,
            action: 'preview',
          },
          task: {
            taskId: item.taskId,   
            taskDescription: item.taskDescription,
            taskDateRun: item.taskDateRun, 
            serverTestId: item.serverTestId 
          }
        }
        const response = await this.CANCEL_COMPLETED_ACTION(data)
        console.log(response)
          if (response.status === 202) {
            const itemData = JSON.parse(response.data.task.taskJsonNew);
            item.device = itemData.Device
            this.editedIndex = 1;
            this.editedItem = Object.assign(this.defaultItem, {
              ...item,
              isTestServer: item.TestServerId !== null,
            });
            console.log(this.editedItem)
            this.cancelDialog = true;
            this.uuid= uuid;
          }
      } catch(e) {
        console.dir(e)
        if (e.response.status === 400) {
          console.log(e)
          this.message.title = 'Предупреждение';
          this.message.type = 'error';
          this.message.text1 = 'В данный момент отмена заданий недоступна,';
          this.message.text2 = 'Повторите попытку позднее';
          this.message.continue = false;
          this.infoWindow = true;
        } else if (e.response.status === 403) {
          console.log(e)
          this.message.title = 'Предупреждение';
          this.message.type = 'warning';
          this.message.text1 = e.response.data.status.message;
          this.message.continue = false;
          this.infoWindow = true;
        } else if (e.response.status === 401) {
          console.log(e)
          this.message.title = 'Предупреждение';
          this.message.type = 'error';
          this.message.text1 = 'Ошибка авторизации, пожалуйста, повторите';
          this.message.text2 = 'процесс авторизации и попробуйте снова';
          this.message.continue = false;
          this.infoWindow = true;
        }
        
        this.uuid = null;
        this.cancelDialog = false;
        this.selectedPhoneTask = null;
      }


    },
    async closeCancelDialog() {
      this.cancelDialog = false;
      try {
        const data = {
            meta: {
              UUID: this.uuid,
              action: 'cancel',
            },
            task: {
              taskId: this.editedItem.taskId,   
              taskDescription: this.editedItem.taskDescription,
              taskDateRun: this.editedItem.taskDateRun, 
              serverTestId: this.editedItem.serverTestId 
            }
          }
          const response = await this.CANCEL_COMPLETED_ACTION(data)
          console.log(response)
      } catch(e) {
        console.dir(e)  
      }
    },
    showLogDialog(item) {
      if (!this.cancelDialog && !this.settingsDialog) {
        this.editedItem = Object.assign(this.defaultItem, item);
        this.logDialog = true;
      }
    },
    pushCancelTask() {
      const form = this.$refs.cancelForm;
      if (form.validate()) {
        // form.resetValidation();
        alert("Тут должна быть отправка задачи");
        this.cancelDialog = false;
      }
    },
    async cancelTask() {
      try {
        this.cancelDialog = false;
        const data = {
            meta: {
              UUID: this.uuid,
              action: 'create',
            },
            task: {
              taskId: this.editedItem.taskId,   
              taskDescription: this.editedItem.taskDescription,
              taskDateRun: this.editedItem.taskDateRun, 
              serverTestId: this.editedItem.serverTestId 
            }
          }
          const response = await this.CANCEL_COMPLETED_ACTION(data)
          console.log(response)
          if (response.status === 201) {
            this.cancelDialog = false;
            this.uuid= null;
            this.message.title = 'Задание создано';
            this.message.type = 'success';
            this.message.text1 = `Задание ${this.editedItem.taskId} для отмены успешно`;
            this.message.text2 = 'создано';
            this.message.continue = false;
            this.infoWindow = true;
            if (this.periodDates.length > 0) {
              const periodDates = [...this.periodDates]
              if (periodDates.length === 1) periodDates.push(periodDates[0])
              const date1 = new Date(periodDates[0])
              const date2 = new Date(periodDates[1])
              const dateFrom = date1 <= date2 ? `${periodDates[0]}T00:00:00.000Z` : `${periodDates[1]}T00:00:00.000Z`;
              const dateTo = date1 <= date2 ? `${periodDates[1]}T23:59:59.000Z` : `${periodDates[0]}T23:59:59.000Z`;
              this.FETCH_COMPLETED_TASKS_DATE({
                dateFrom: dateFrom,
                dateTo: dateTo,
                orderDesc: this.options.sortDesc[0],
                orderedColumn: this.options.sortBy[0] || 'taskDateEnd', 
                limit: this.pagination.rowsPerPage,
                offset: (this.pagination.page - 1)*this.pagination.rowsPerPage,
              })
            } else {
              this.FETCH_COMPLETED_TASKS({
                limit: this.pagination.rowsPerPage,
                offset: (this.pagination.page - 1)*this.pagination.rowsPerPage,
                search: this.search, 
                orderedColumn: this.options.sortBy[0], 
                orderDesc: this.options.sortDesc[0]
              });
            }
          }
      } catch(e) {
        console.dir(e)  
      }
    },
    validateDate() {
      this.$refs.runTimeDate.validate(); // валидирует поле даты
    },
  },
};
</script>
