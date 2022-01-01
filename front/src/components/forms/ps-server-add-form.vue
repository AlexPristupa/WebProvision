<template>
  <v-card>
    <v-form v-if="form" @submit="submitHandler()">
      <v-card-title center>{{form.Title}}</v-card-title>
      <v-card-text>
        <div v-for="field in form.Fields" :key="field.idr">
          <v-text-field
            v-if="field.Type === 'textfield' "
            v-model="field.value"
            :label="field.Label"
            outlined
          />
          <v-text-field
            v-else-if="field.Type === 'textarea' "
            v-model="field.value"
            :label="field.Label"
            outlined
          />
          <v-select
            v-else-if="field.Type === 'select' "
            v-model="field.value"
            :items="field.options"
            :label="field.Label"
            dense
            outlined
          />
          <v-checkbox v-else-if="field.Type === 'checkbox'" :label="field.Label" />
        </div>
      </v-card-text>
      <v-card-actions>
        <v-btn
          class="ps-btn-add"
          type="submit"
          :color="CONFIG.colors.primary"
          block
          @click.prevent="submitHandler()"
        >{{$vuetify.lang.t('$vuetify.add')}}</v-btn>
      </v-card-actions>
    </v-form>
  </v-card>
</template>

<script>
import axios from "axios";
import { mapActions, mapGetters } from "vuex";

export default {
  name: "psSwitchPhoneOwnerForm",
  data() {
    return {
      form: null,
    };
  },
  mounted() {
    this.fetchFields();
  },
  methods: {
    ...mapActions(["ADD_SERVER"]),
    async fetchFields() {
      // Получает с сервера поля для формы.
      await axios
        .get("http://localhost:3000/forms?Tag=server_add_form")
        .then((res) => {
          if (res.data[0]) {
            const form = res.data[0]; // объект нужной формы
            this.form = form;
            this.form.Fields.map((field) => {
              field.value = field.default || "";
            });
          }
        })
        .catch((err) => console.error(err));
    },
    submitHandler() {
      let formData = { idr: Date.now() };
      this.form.Fields.map((field) => {
        if (field.value) {
          formData = { ...formData, [field.name]: field.value };
          console.log(formData);
        }
      });
      this.ADD_SERVER(formData);
    },
  },
  computed: {
    ...mapGetters(["CONFIG"]),
  },
};
</script>
