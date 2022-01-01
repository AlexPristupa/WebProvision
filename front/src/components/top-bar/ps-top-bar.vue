<template>
  <v-card height="auto">
    <v-toolbar>
      <v-toolbar-title>Mentol Provision</v-toolbar-title>
    </v-toolbar>
  </v-card>
</template>

<script>
import { mapGetters, mapActions } from "vuex";
export default {
  name: "psTopBar",
  data() {
    return {
      dialogs: {
        aboutProduct: false
      }
    };
  },
  computed: { ...mapGetters(["IS_AUTH", "USER"]) },
  methods: {
    ...mapActions(["SIGN_OUT"]),
    logoutHandler() {
      this.SIGN_OUT();
      this.$router.push({ name: "sign-in" }); // редирект на форму авторизации
    }
  }
};
</script>

<style lang="scss">
</style>
