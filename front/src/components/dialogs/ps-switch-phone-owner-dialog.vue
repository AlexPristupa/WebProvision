<template>
  <v-dialog
    no-click-animation
    persistent
    width="651px"
    v-model="isShow"
    @keydown.esc="closeHandler()"
  >
    <v-dialog
      no-click-animation
      persistent
      v-model="taskStatusDialog"
      width="500"
      @keydown.esc="closeHandler()"
    >
      <v-card elevation="0" height="100%" class="d-flex flex-column">
        <div class="popup-header">
          <v-card-title class="pl-3">Заголовок</v-card-title>
          <v-icon class="white--text" @click="closeHandler()" :color="CONFIG.colors.white">mdi-close</v-icon>
        </div>
        <v-row no-gutters class="pl-7 pr-7">
          <v-card-title class="pl-0 pb-2">{{editedItem.name}}</v-card-title>
          <v-spacer></v-spacer>
        </v-row>
        <v-divider class="ml-7 mr-7"></v-divider>

        <v-card-text>
          <v-checkbox
            dense
            hide-details="auto"
            class="ma-0 pa-0"
            v-model="sendEmail"
            :label="$vuetify.lang.t('$vuetify.sendNotificationsAboutTheStatusOfTheTaskByEmail')"
          ></v-checkbox>
        </v-card-text>

        <v-divider></v-divider>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            class="ps-btn-close"
            :color="CONFIG.colors.light"
            text
            @click="closeHandler()"
          >{{$vuetify.lang.t('$vuetify.close')}}</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-card elevation="0" height="100%" class="d-flex flex-column">
      <div class="popup-header">
        <v-card-title
          class="pl-3 font-size-16 font-weight-bold"
        >{{$vuetify.lang.t('$vuetify.addTaskForNumber')}} {{selectedPhone.linePhoneNumber || ""}}</v-card-title>
        <v-icon class="white--text" @click="closeHandler()" :color="CONFIG.colors.white">mdi-close</v-icon>
      </div>
      <v-row no-gutters class="pl-7 pr-7">
        <v-card-title class="pl-0 pb-2 font-size-16">{{editedItem.name}}</v-card-title>
        <v-spacer></v-spacer>
      </v-row>
      <v-divider class="ml-7 mr-7"></v-divider>
      <v-card-text>
        <v-form ref="form" lazy-validation class="pr-1 pl-1">
          <v-row>
            <v-col class="pr-5 pl-3 pt-6 pb-0">
              <v-row align="top" :class="CONFIG.form.rowClass">
                <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass2">
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
                    v-model="editedItem.description"
                    :placeholder="$vuetify.lang.t('$vuetify.description')"
                  ></v-text-field>
                </v-col>
              </v-row>
              <v-row align="top" :class="CONFIG.form.rowClass">
                <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass2">
                  <v-subheader :class="CONFIG.form.labelClass">Тестовый сервер</v-subheader>
                </v-col>
                <v-col :class="CONFIG.form.fieldColClassNopadding">
                  <v-select
                    dense
                    outlined
                    :rules="[testServerIsRequired]"
                    :items="serverList"
                    :placeholder="$vuetify.lang.t('$vuetify.notSelected')"
                    item-value="idr"
                    item-text="name"
                    v-model="editedItem.serverTestId"
                    :menu-props="{ bottom: true, offsetY: true }"
                  ></v-select>
                </v-col>
              </v-row>
              <v-row align="top" :class="CONFIG.form.rowClass">
                <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass2">
                  <v-subheader :class="CONFIG.form.labelClass">Дата, время запуска</v-subheader>
                </v-col>
                <v-col cols="8" :class="CONFIG.form.fieldColClassNopadding">
                  <div style="display: flex; justify-content: space-between; width: 421px">
                    <v-checkbox
                      @change="onChangeIsRunNow"
                      :value="editedItem.isRunNow"
                      hide-details
                      class="ma-0 mr-2 pa-0 width-124"
                      dense
                      label="по расписанию"
                    ></v-checkbox>
                    <div class="datetime-wrapper width-283">
                      <div class="date-picker-wrapper single pr-5 width-163">
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
                              :disabled="editedItem.isRunNow"
                              ref="runTimeDate"
                              dense
                              outlined
                               :rules="[requiredDate ]"
                              full-width
                              v-model="computedRunTimeDateFormatted"
                              :placeholder="$vuetify.lang.t('$vuetify.datePlaceholder')"
                              readonly
                              v-bind="attrs"
                              v-on="on"
                            ></v-text-field>
                          </template>
                          <v-date-picker
                            color="primary"
                            :disabled="editedItem.isRunNow"
                            hide-details
                            no-title
                            show-current
                            :rules="[runDateIsValidCheck, requiredTime]"
                            @input="menu2 = false"
                            v-model="editedItem.runTimeDate"
                          ></v-date-picker>
                        </v-menu>
                      </div>
                      <div class="time-input-wrapper single">
                        <v-text-field
                          clearable
                          clear-icon="mdi-close-circle"
                          :disabled="editedItem.isRunNow"
                          @change="validateDate()"
                          dense
                          outlined
                          v-model="editedItem.runTimeHoursMinutes"
                          placeholder="00:00"
                          ref="timeField"
                          type="text"
                          return-masked-value
                           v-mask="'##:##'"
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
              <v-row align="top" class="mt-3" :class="CONFIG.form.rowClass">
                <v-card-subtitle
                  :class="CONFIG.form.labelClass"
                  class="font-weight-bold"
                >{{$vuetify.lang.t('$vuetify.telephoneSet')}}</v-card-subtitle>
              </v-row>
              <v-row align="top" :class="CONFIG.form.rowClass">
                <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass2">
                  <v-subheader
                    :class="CONFIG.form.labelClass"
                  >Description</v-subheader>
                </v-col>
                <v-col :class="CONFIG.form.fieldColClassNopadding">
                  <v-text-field
                    dense
                    outlined
                    :rules="[required, maxLength50, baseRule ]"
                    v-model="editedItem.telephone.description"
                    :placeholder="$vuetify.lang.t('$vuetify.description')"
                  ></v-text-field>
                </v-col>
              </v-row>
              <v-row align="top" :class="CONFIG.form.rowClass">
                <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass2">
                  <v-subheader :class="CONFIG.form.labelClass">Owner User ID</v-subheader>
                </v-col>
                <v-col :class="CONFIG.form.fieldColClassNopadding">
                  <v-text-field
                    dense
                    outlined
                    :rules="[required, maxLength50, baseRule ]"
                    v-model="editedItem.telephone.login"
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
                <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass2">
                  <v-subheader
                    :class="CONFIG.form.labelClass"
                  >Description</v-subheader>
                </v-col>
                <v-col :class="CONFIG.form.fieldColClassNopadding">
                  <v-text-field
                    dense
                    outlined
                    :rules="[required, maxLength50, baseRule ]"
                    v-model="editedItem.line.description"
                    :placeholder="$vuetify.lang.t('$vuetify.description')"
                  ></v-text-field>
                </v-col>
              </v-row>
              <v-row align="top" :class="CONFIG.form.rowClass">
                <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass2">
                  <v-subheader :class="CONFIG.form.labelClass">Alerting Name</v-subheader>
                </v-col>
                <v-col :class="CONFIG.form.fieldColClassNopadding">
                  <v-text-field
                    dense
                    outlined
                    :rules="[required, maxLength50, baseRule ]"
                    v-model="editedItem.line.alertingName"
                    :placeholder="$vuetify.lang.t('$vuetify.description')"
                  ></v-text-field>
                </v-col>
              </v-row>
              <v-row align="top" :class="CONFIG.form.rowClass">
                <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass2">
                  <v-subheader :class="CONFIG.form.labelClass">ASCII Alerting Name</v-subheader>
                </v-col>
                <v-col :class="CONFIG.form.fieldColClassNopadding">
                  <v-text-field
                    dense
                    outlined
                    :rules="[required, maxLength50, baseRule ]"
                    v-model="editedItem.line.asciiAlertingName"
                    :placeholder="$vuetify.lang.t('$vuetify.lastNameFirstNameInEnglish')"
                  ></v-text-field>
                </v-col>
              </v-row>
              <v-row align="top" :class="CONFIG.form.rowClass">
                <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass2">
                  <v-subheader :class="CONFIG.form.labelClass">Display (Caller ID)</v-subheader>
                </v-col>
                <v-col :class="CONFIG.form.fieldColClassNopadding">
                  <v-text-field
                    dense
                    outlined
                    :rules="[required, maxLength50, baseRule ]"
                    v-model="editedItem.line.display"
                    :placeholder="$vuetify.lang.t('$vuetify.description')"
                  ></v-text-field>
                </v-col>
              </v-row>
              <v-row align="top" :class="CONFIG.form.rowClass">
                <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass2">
                  <v-subheader :class="CONFIG.form.labelClass">ASCII Display (Caller ID)</v-subheader>
                </v-col>
                <v-col :class="CONFIG.form.fieldColClassNopadding">
                  <v-text-field
                    dense
                    outlined
                    :rules="[required, maxLength50, baseRule ]"
                    v-model="editedItem.line.asciiDisplay"
                    :placeholder="$vuetify.lang.t('$vuetify.description')"
                  ></v-text-field>
                </v-col>
              </v-row>
              <v-row align="top" :class="CONFIG.form.rowClass">
                <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass2">
                  <v-subheader :class="CONFIG.form.labelClass2">User Associated with Line</v-subheader>
                </v-col>

                <v-col :class="CONFIG.form.fieldColClassNopadding">
                  <v-text-field
                    dense
                    outlined
                    :rules="[required, maxLength50, baseRule ]"
                    v-model="editedItem.line.userAssociated"
                    :placeholder="$vuetify.lang.t('$vuetify.userLogin')"
                  ></v-text-field>
                </v-col>
              </v-row>
            </v-col>
          </v-row>
        </v-form>
      </v-card-text>
      <v-card-actions class="mr-5 ml-5 pt-0 pb-7">
        <v-spacer></v-spacer>
        <v-btn
          type="submit"
          class="white--text ps-btn-without-padding button-add"
          :color="CONFIG.colors.primary"
          @click.prevent="submitHandler(true)"
        >{{$vuetify.lang.t('$vuetify.addWithTesting')}}</v-btn>
        <v-btn
          type="submit"
          class="ps-btn-perform white--text button-close"
          :color="CONFIG.colors.primary"
          @click.prevent="submitHandler(false)"
        >{{$vuetify.lang.t('$vuetify.add')}}</v-btn>
        <v-btn
          color="secondary"
          class="button-close"
          @click.prevent="closeHandler()"
        >{{$vuetify.lang.t('$vuetify.close')}}</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import { mapGetters, mapActions } from "vuex";
import { required, requiredDate,  requiredTime, maxLength50, maxLength128 } from "@/helpers/validators";
import filters from "@/helpers/filters";


export default {
  name: "psSwitchPhoneOwnerDialog",
  props: {
    selectedPhone: {
      type: Object,
      required: true,
    },
    isShow: {
      type: Boolean,
      required: true,
    },
    task: {
      type: Object,
      required: true,
    },
    uuid: {
      type: String,
      required: true,
    }
  },
  data() {
    return {
      runWithTests: null,
      descriptionError: null,
      editedItem: {
        idr: null,
        name: null,
        description: null,
        isTestServer: false,
        serverTestId: null,
        isRunNow: false,
        runTimeDate: null,
        runTimeHoursMinutes: null,
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
        idr: null,
        name: null,
        description: null,
        isTestServer: false,
        serverTestId: null,
        isRunNow: false,
        runTimeDate: null,
        runTimeHoursMinutes: null,
        telephone: {
          description: null,
          login: null,
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
      requiredDate: (text) => {
        return (
          requiredDate(text) && requiredTime(this.editedItem.runTimeHoursMinutes)||
          this.$vuetify.lang.t("$vuetify.theFieldыMustNotBeEmpty")
        );
      },
      requiredTime: (text) => {
        return (
          requiredTime(text) || "");
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
        if (this.runWithTests == true) {
          console.log(this.editedItem.serverTestId != null ||
            this.$vuetify.lang.t("$vuetify.testServerNotSelected"))
          return (
            this.editedItem.serverTestId != null ||
            this.$vuetify.lang.t("$vuetify.testServerNotSelected")
          );
        } else if (this.runWithTests == false) {
          return (
            this.editedItem.serverTestId == null ||
            "Значение должно быть пустым"
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
      taskStatusDialog: false,
      sendEmail: false,
    };
  },
  watch: {
    "editedItem.testServerId": function () {
      this.runWithTests = null;
    },
  },
  computed: {
    ...mapGetters(["SERVERS", "CONFIG"]),
    computedRunTimeDateFormatted() {
      console.log(this.editedItem.runTimeDate)
      return this.formatDate(this.editedItem.runTimeDate) || null;
    },
    serverList() {
      if (this.SERVERS.length > 0) {
        return [{idr: null, name: 'Не выбран'}, ...this.SERVERS.items]
      }
      return []
    },
    timeFieldType() {
      if (this.editedItem.runTimeHoursMinutes) {
        return "time";
      } else {
        return "text";
      }
    },
  },
  mounted() {
    try {
      this.FETCH_SERVERS({ limit: 1000, offset: 0 });
    } catch(e) {
      console.log(e)
    }
  },
  methods: {
    ...mapActions(["FETCH_SERVERS", "ADD_TASK_TO_POOL", "DEVICE_ACTIONS"]),
    ...filters,
    setTimeField(){
      this.editedItem.runTimeHoursMinutes = "00:00"
    },
    async submitHandler(test) {
      try{
        //const runWithTests = isTesting ? true : false;
        /*
        if (this.$refs.form.validate()) {
          this.taskStatusDialog = true;
          if (this.editedItem.isRunNow) {
            // this.taskStatusDialog = true;
          } else {
            alert(
              `Задача будет запущена в ${new Date(
                `${this.editedItem.runTimeDate} ${this.editedItem.runTimeHoursMinutes}`
              )}`
            );
          }
        }
        */
       this.runWithTests = test;
       console.log(this.$refs.form.validate())
       console.dir(this.$refs.timeField)
       if (this.$refs.form.validate()) {

          const taskJsonNew = {
            Device: {
              Name: this.selectedPhone.name,
              Description: this.editedItem.telephone.description,
              "Owner User ID": this.editedItem.telephone.login,
              Line: {
                Description: this.editedItem.line.description,
                "Alerting Name": this.editedItem.line.alertingName,
                "ASCII Alerting Name": this.editedItem.line.asciiAlertingName,
                "Display (Caller ID)": this.editedItem.line.display,
                "ASCII Display (Caller ID)": this.editedItem.line.asciiAlertingName,
                "User Associated with Line": this.editedItem.telephone.login 
              }
            }
          }
          console.log(taskJsonNew)
          const serverTestId = {};
          if (test) {
            serverTestId.serverTestId = this.editedItem.serverTestId
          }
          const date = this.computedRunTimeDateFormatted.split('.').reverse().join('-')
          const data = {
            meta: {
              UUID: this.uuid,
              action: 'create',
            },
            task: {
              taskTypeId: this.task.idr,
              taskDescription: this.editedItem.description,
              taskDateRun: `${date} ${this.editedItem.runTimeHoursMinutes}`,
              deviceId: this.selectedPhone.phoneId,
              ...serverTestId,
              taskJsonNew: JSON.stringify(taskJsonNew),
            }
          }
          console.log(data)
          const response = await this.DEVICE_ACTIONS(data)
          console.log(response)
          if (response.status === 201) {
            this.$emit("success", response.data.status.message);
            this.editedItem= {
              idr: null,
              name: null,
              description: null,
              isTestServer: false,
              serverTestId: null,
              isRunNow: false,
              runTimeDate: null,
              runTimeHoursMinutes: null,
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
            };
          }
       }
      } catch (e) {
        console.dir(e)
        this.$emit("error");
      }
    },
    closeHandler() {
      console.log('close')
      if (this.$refs.form.validate()) {
        if (this.editedItem.isRunNow) {
          this.ADD_TASK_TO_POOL(this.editedItem);
          alert("Задача добавлена в очередь");
        } else {
          this.ADD_TASK_TO_POOL(this.editedItem);
          alert("Задача добавлена в очередь и отложена до указанного времени");
        }
        this.editedItem = this.defaultItem;
        this.$refs.form.resetValidation();
        this.$emit("close");
      } else {
        this.editedItem = this.defaultItem;
        this.$refs.form.resetValidation();
        this.$emit("close");
      }
    },
    validateDate() {
      this.$refs.runTimeDate.validate(); // валидирует поле даты
    },
    onChangeIsRunNow(e) {
      console.log(e)
      this.editedItem.isRunNow = e;
      const now = new Date(Date.now())
      this.editedItem.runTimeDate = new Date(now)
      const time = `${now.getHours().toString().length < 2 ? '0'+now.getHours().toString() : now.getHours()}:${now.getMinutes().toString().length < 2 ? '0'+now.getMinutes().toString() : now.getMinutes()}`;
      console.log(time)
      this.editedItem.runTimeHoursMinutes = time
    }
  },
};
</script>
