

<template>
  <div>
    <v-slide-y-transition>
    <v-dialog
      v-if="infoWindow"
      no-click-animation
      persistent
      v-model="infoWindow"
      width="600px"
      @keydown.esc="infoWindow = false"
    >
      <div class="popup-header ps-dialog-header height-40">
        <v-card-title class="white--text pb-2 pt-3 pl-1 font-size-16 font-weight-bold">Ошибка получения данных продукта</v-card-title>
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
          <div style="margin-left: 27px;font-size: 16px; font-weight: 500; margin-top: 7px; margin-right: 30px; line-height: 20px; color: rgba(0, 0, 0, 0.8);"><p style="margin-bottom: 0;">
            Произошла ошибка получения данных продукта.</p>
            <p style="margin-bottom: 0;">Возможно, информационная система не настроена.</p></div>
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
      <v-dialog
        content-class="border-radius-1"
        no-click-animation
        persistent
        v-model="aboutProductDialog"
        width="651px"
        @keydown.esc="aboutProductDialog = false"
      >
        <v-card elevation="0" height="100%" class="d-flex flex-column">
          <div class="popup-header" style="height: 40px;">
            <v-card-title class="pl-1 font-size-16 pt-4 pb-4">{{$vuetify.lang.t('$vuetify.informationOfProduct')}}</v-card-title>
            <v-icon
              class="white--text ps-btn-close"
              @click="aboutProductDialog = false"
              :color="CONFIG.colors.white"
            >mdi-close</v-icon>
          </div>
          <v-card-text>
            <div class="info-title"> 
              <p class="info-title__left">Mentol<span class="font-weight-bold">Provision</span></p>
              <p class="info-title__right font-weight-bold">Номер версии: <span class="font-weight-normal">{{ABOUT_PRODUCT.productVersion}}</span></p>
            </div>
            <v-divider class="ml-2 mr-2 mt-1"></v-divider>
            <v-form ref="form" lazy-validation class="pr-1 pl-1 pb-1">
              <v-row>
                <v-col class="pr-5 pt-5" style="padding-bottom: 5px;">
                  <v-row align="top" :class="CONFIG.form.rowClass2">
                    <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass2">
                      <v-subheader
                        :class="CONFIG.form.labelClass"
                      >{{$vuetify.lang.t('$vuetify.name')}}</v-subheader>
                    </v-col>
                    <v-col :class="CONFIG.form.fieldColClass2">
                      <v-text-field
                        hide-details
                        readonly
                        dense
                        outlined
                        v-model="ABOUT_PRODUCT.productName"
                        :placeholder="$vuetify.lang.t('$vuetify.name')"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-row align="top" :class="CONFIG.form.rowClass2">
                    <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass2">
                      <v-subheader
                        :class="CONFIG.form.labelClass"
                      >{{$vuetify.lang.t('$vuetify.decimalNumber')}}</v-subheader>
                    </v-col>
                    <v-col :class="CONFIG.form.fieldColClass2">
                      <v-text-field
                        hide-details
                        readonly
                        dense
                        outlined
                        v-model="ABOUT_PRODUCT.productDNumber"
                        :placeholder="$vuetify.lang.t('$vuetify.decimalNumber')"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-row align="top" :class="CONFIG.form.rowClass2">
                    <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass2">
                      <v-subheader
                        :class="CONFIG.form.labelClass"
                      >{{$vuetify.lang.t('$vuetify.serialNumber')}}</v-subheader>
                    </v-col>
                    <v-col :class="CONFIG.form.fieldColClass2">
                      <v-text-field
                        hide-details
                        readonly
                        dense
                        outlined
                        v-model="ABOUT_PRODUCT.serialNumber"
                        :placeholder="$vuetify.lang.t('$vuetify.serialNumber')"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-row align="top" :class="CONFIG.form.rowClass2">
                    <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass2">
                      <v-subheader
                        :class="CONFIG.form.labelClass"
                      >{{$vuetify.lang.t('$vuetify.contacts')}}</v-subheader>
                    </v-col>
                    <v-col :class="CONFIG.form.fieldColClass2">
                      <v-text-field
                        hide-details
                        readonly
                        dense
                        outlined
                        v-model="ABOUT_PRODUCT.productContact"
                        :placeholder="$vuetify.lang.t('$vuetify.contacts')"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-row align="top" :class="CONFIG.form.rowClass2">
                    <v-col :cols="CONFIG.vuetify.cols.labelCols" :class="CONFIG.form.labelColClass2">
                      <v-subheader
                        :class="CONFIG.form.labelClass3"
                      >{{$vuetify.lang.t('$vuetify.additionally')}}</v-subheader>
                    </v-col>
                    <v-col :class="CONFIG.form.fieldColClass2">
                      <v-textarea
                        hide-details
                        no-resize
                        readonly
                        dense
                        outlined
                        v-model="ABOUT_PRODUCT.productAddInfo"
                        :placeholder="$vuetify.lang.t('$vuetify.additionally')"
                        height="50"
                        rows="2"
                      ></v-textarea>
                    </v-col>
                  </v-row>
                </v-col>
              </v-row>
            </v-form>
          </v-card-text>
          <v-card-actions style="padding-bottom: 19px; padding-top: 0;">
            <v-spacer></v-spacer>
            <v-btn
              class="white--text ps-btn-close button-close mr-5 border-radius-1"
              :color="CONFIG.colors.primary"
              @click="aboutProductDialog = false"
            >{{$vuetify.lang.t('$vuetify.close')}}</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-slide-y-transition>
    <v-menu offset-y transition="slide-y-transition" bottom right>
      <template v-slot:activator="{ on, attrs }">
        <a style="padding: 15px 0;" class="user-block" v-bind="attrs" v-on="on">
          <div class="user-block">
            <div class="user-block__icon">
              <svg-icon name="user" />
            </div>
            <p class="user-block__username text-md-body-1">{{USER.username}}</p>
            <v-icon class="user-block__more">mdi-chevron-down</v-icon>
          </div>
        </a>
      </template>

      <v-list>
        <v-list-item @click="logoutHandler()">{{$vuetify.lang.t('$vuetify.leave')}}</v-list-item>
        <v-list-item @click="openInfo">{{$vuetify.lang.t('$vuetify.aboutProduct')}}</v-list-item>
      </v-list>
    </v-menu>
  </div>
</template>

<script>
import { mapActions, mapGetters } from "vuex";
import svgIcon from "@/components/svg-icons/svg-icon";

export default {
  name: "psAuthBlock",
  components: {
    svgIcon,
  },
  data() {
    return {
      aboutProductDialog: false,
      infoWindow: false,
      errorBool: false,
    };
  },
  async mounted() {
    try {
      await this.FETCH_ABOUT_PRODUCT();
      await this.FETCH_LICENCE();
    }catch (e) {
      if (e.response && e.response.status !== 404) {
        this.errorBool= true;
      }
      this.LOGOUT()
      this.$router.push({ name: "login" });
      console.log(e)
    }
  },
  computed: {
    ...mapGetters(["USER", "IS_AUTH", "ABOUT_PRODUCT", "CONFIG"]),
  },
  methods: {
    ...mapActions(["LOGOUT", "FETCH_ABOUT_PRODUCT", "FETCH_LICENCE", "LOGOUT"]),
    logoutHandler() {
      this.LOGOUT();
      this.$router.push({ name: "login" }); // редирект на форму авторизации
    },
    openInfo() {
      if (this.errorBool === false) {
        this.aboutProductDialog = true;
        this.infoWindow = false;
      } else {
        this.aboutProductDialog = false;
        this.infoWindow = true;
      }
    }
  },
};
</script>

