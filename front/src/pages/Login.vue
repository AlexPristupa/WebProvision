<template>
  <v-row justify="center" style="margin: 0 auto; margin-top: 167px">
    <div>
      <ps-login/>
    </div>
  </v-row>
</template>

<script>
import psLogin from "@/components/forms/ps-login-form.vue";

export default {
  name: "login",
  components: {
    psLogin
  }
};
</script>
