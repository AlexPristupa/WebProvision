
<template>
  <div>
    <!-- Компонент для выбора конкретной страницы в пагинации -->
    <v-text-field
      @keydown.enter="onSetPage"
      maxlength="2"
      dense
      hide-details
      v-on:blur="onSetPage"
      ref="page"
      :rules="rules"
      v-model="page"
      type="number"
    />
  </div>
</template>

<style lang="scss">
.ps-pagination-page-set {
  width: 40px;
  border: rgba(0, 0, 0, 0.7) solid 1px !important;
  text-align: center;
}
</style>

<script>
import { mapGetters } from "vuex";
export default {
  name: "psPaginationPageSet",
  props: {
    totalPages: {
      // нужно передавать кол-во страниц для валидации
      type: Number,
      required: true,
    },
  },
  computed: {
    ...mapGetters(["CONFIG"]),
  },
  methods: {
    onSetPage() {
      if (this.page < 1) {
        this.page = null;
        return;
      }
      if (this.$refs.page.validate()) {
        this.$emit("onSetPage", this.page);
      } else {
        if (this.page === null || this.page.length <= 0) {
          this.$emit("onValidationError", []);
          this.$refs.page.resetValidation();
        } else {
          this.$emit("onValidationError", this.$refs.page.errorBucket);
        }
      }
      document.activeElement.blur();
    },
  },
  data() {
    return {
      page: null,
      rules: [
        () => this.page > 0 || "Значение должно быть больше нуля",
        () =>
          this.page <= this.totalPages ||
          `Значение должно быть меньше ${this.totalPages + 1}`,
      ],
    };
  },
};
</script>