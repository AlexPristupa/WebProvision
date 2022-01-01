<template>
  <v-container fluid class="ps-navbar pr-9 pl-9">
    <div class="ps-top-menu">
      <ps-horizontal-menu-item v-for="page in PAGES" :page="page" :key="page.idr" />
      <v-spacer />
      <ps-auth-block />
    </div>
  </v-container>
</template>

<script>
import { mapActions, mapGetters } from "vuex";
import psHorizontalMenuItem from "./ps-horizontal-menu-item";
import psAuthBlock from "@/components/horizontal-menu/ps-auth-block";

export default {
  name: "psHorizontalMenu",
  computed: {
    ...mapGetters(["PAGES", "CONFIG"]),
  },
  components: {
    psHorizontalMenuItem,
    psAuthBlock,
  },
  methods: {
    ...mapActions(["FETCH_PAGES"]),
  },
  mounted() {
    this.FETCH_PAGES(0);
  },
};
</script>

<style lang="scss">
</style>
