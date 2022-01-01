<template>
  <v-select
    :menu-props="{ bottom: true, offsetY: true, contentClass: 'font-size-16'}"
    class="table-select"
    hide-details
    @change="updatePermissions"
    dense
    outlined
    v-model="selectedPermission"
    :items="PERMISSIONS"
  >

  </v-select>
</template>

<script>
import { mapGetters } from "vuex";
export default {
  name: "ps-page-permissions-item",
  props: {
    pageIdr: {
      type: Number,
      required: true,
    },
  },
  data() {
    return {
      initialSelectedPermissions: [],
      selectedPermission: null,
    };
  },
  mounted() {
    console.log(this.PERMISSIONS)
    this.initialSelectedPermission = this.selectedPermission; // нужно для сравнивания
  },
  computed: {
    ...mapGetters(["PERMISSIONS"]),
  },
  methods: {
    updatePermissions() {
      this.$emit("updatePermissions", {
        pageIdr: this.pageIdr,
        oldValue: JSON.stringify(this.initialSelectedPermission),
        newValue: JSON.stringify(this.selectedPermissions),
      });
    },
  },
};
</script>
<style>

</style>

