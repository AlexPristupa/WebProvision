<template>
  <v-form ref="form" @submit.prevent="onSubmit" :class="{'login-form-block' : pageLogin, 'login-form-block-error' : !pageLogin}">
    <!-- <v-alert :color="CONFIG.colors.red" dense text v-if="errors" transition="slide-x-transition">
      <div v-for="(error, index) in errors" :key="index">{{error.message}}</div>
    </v-alert>-->
    <div class="login-form-title"><span class="login-form-title-bold">Вход</span> <span class="login-form-title-notbold"> MentolProvision</span></div>
    <v-divider class="login-form-divider"></v-divider>
    <div v-if="pageLogin">
    <v-select
      hide-details="auto"
      append-icon="mdi-menu-down"
      class="ps-select-login mb-3 margin-right-64 margin-left-64"
      color='white'
      dense
      outlined
      v-model="select"
      :items="items"
      item-text="state"
      item-value="abbr"
      item-color="blue darken-4"
    ></v-select>
    <v-text-field
      ref="loginField"
      class="login-form-text-size login-field"
      autofocus
      dense
      outlined
      v-model="login"
      name="username"
      :placeholder="$vuetify.lang.t('$vuetify.login')"
    ></v-text-field>
    <v-text-field
      ref="passwordField"
      class="login-form-text-size2 login-field"
      dense
      outlined
      :append-icon="passwordShow ? 'mdi-eye' : 'mdi-eye-off'"
      :type="passwordShow ? 'text' : 'password'"
      @click:append="passwordShow = !passwordShow"
      v-model="password"
      :rules="passwordRules"
      name="password"
      :placeholder="$vuetify.lang.t('$vuetify.password')"
      required
      :error-messages="passwordErrors"
    ></v-text-field>
    <div class="login-btns">
      <v-btn
        height="26px"
        width="111px"
        type="submit"
        class="submit-btn white--text login-form-text-size3"
        :color="CONFIG.colors.primary"
        @click.prevent="onSubmit"
      >{{$vuetify.lang.t('$vuetify.join')}}</v-btn>
    </div>
  </div>
  <div v-else>
    <p class="login-error-top">Ошибка авторизации</p>
    <p class="login-error-bottom">Неверный логин или пароль</p>
    <v-btn
        height="26px"
        width="167px"
        type="submit"
        style="font-size: 12px;margin-left: 93px;margin-top: 51px; margin-bottom: 7px;"
        class="submit-btn white--text"
        :color="CONFIG.colors.primary"
        @click.prevent="onLoginOpen"
      >Попробовать снова</v-btn>
  </div>
  </v-form>
</template>

<script>
import { mapActions, mapGetters } from "vuex";
import { required } from "@/helpers/validators";

export default {
  name: "psLoginForm",
  data() {
    return {
      select: { state: "Встроенная учетная запись", abbr: "DEF" },
      items: [{ state: "Встроенная учетная запись", abbr: "DEF" }, {state: "Доменная авторизация", abbr: "DA"}],
      login: "",
      password: "",
      loginErrors: [],
      passwordErrors: [],
      pageLogin: true,
      loginRules: [
        () => {
          const rule = required(this.login);
          if (rule !== true) {
            console.log("login old", this.loginBlank);
            this.loginBlank = true;
            console.log("login new", this.loginBlank);
          }
          return rule;
        },
      ],
      passwordRules: [
        () => {
          const rule = required(this.password);
          if (rule != true) {
            this.passwordBlank = true;
          }
          return rule;
        },
      ],
      errors: [],
      // для вывода ошибок в alert
      loginBlank: false,
      passwordBlank: false,
      passwordShow: false, // показывать пароль или нет
    };
  },
  methods: {
    ...mapActions(["LOGIN"]),
    onLoginOpen() {
      this.pageLogin= true;
    },
    onSubmit() {
      if (this.$refs.form.validate()) {
        this.LOGIN({
          login: this.login,
          password: this.password,
        })
          .then(() => {
            this.$router.push({ path: "/" });
          })
          .catch(() => {
            this.errors.push({
              message:
                "Не удаётся войти. Пожалуйста, проверьте введенные логин и пароль.",
              type: "notValidPasswordOfLogin",
            });
            this.pageLogin= false;
            this.login = "";
            this.password = "";
            this.loginErrors.push("Неверный логин или пароль");
            this.passwordErrors.push("Неверный логин или пароль");
            this.loginErrors = Array.from(new Set(this.loginErrors)); // очищаем от дубликатов
            this.passwordErrors = Array.from(new Set(this.passwordErrors)); // очищаем от дубликатов
            // очищаем массив this.errors от дубликатов
            this.errors = this.errors.filter(
              (error, index, self) =>
                index === self.findIndex((e) => e.type === error.type)
            );
          });
      } else {
        // если такой ошибки еще не было, то добавляем ее в стек
        this.errors.push({
          message:
            "Не удаётся войти. Пожалуйста, проверьте введенные логин и пароль.",
          type: "notValidPasswordOfLogin",
        });
        console.log(this.errors);
        // очищаем массив this.errors от дубликатов
        this.errors = this.errors.filter(
          (error, index, self) =>
            index === self.findIndex((e) => e.type === error.type)
        );
        console.log(this.errors);
      }
    },
    onCloseAlert(error) {
      this.errors.filter((err) => err.message !== error.message && err);
    },
  },
  computed: {
    ...mapGetters(["CONFIG"]),
  },
};
</script>

<style lang="scss" scoped>
.login-form-text-size {
  font-size: 12px;
  margin-left: 64px;
  margin-right: 64px;
  margin-top: 17px;
}

.login-form-text-size2 {
    font-size: 12px;
    margin-left: 64px;
   margin-right: 64px;
   margin-top: -2px;
}

.login-form-text-size3 {
  font-size: 12px;
  margin-left: 64px;
  margin-right: 64px;
  margin-top: 2px;
}
</style>