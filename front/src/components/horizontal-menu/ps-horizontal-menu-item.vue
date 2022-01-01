<template>
  <v-menu offset-y transition="slide-y-transition" bottom right>
    <template v-slot:activator="{ on, attrs }">
      <v-tab class="ps-tab pt-0 pb-0" v-if="page.IsLeaf" link :to="page.Action">{{page.PrivateName}}</v-tab>
      <v-tab
        :class="parentRoute == page.name && 'active' "
        class="ps-tab"
        v-else
        v-bind="attrs"
        v-on="on"
      >
        <div class="tab-icon-wrapper" v-if="page.PrivateName === 'Устройства'">
          <svg-icon name="devices" :active="parentRoute == page.name" />
        </div>
        <div class="tab-icon-wrapper" v-else-if="page.PrivateName === 'Задания'">
          <svg-icon name="tasks" :active="parentRoute == page.name" />
        </div>
        <div class="tab-icon-wrapper" v-else-if="page.PrivateName === 'Конфигурация'">
          <svg-icon name="configuration" :active="parentRoute == page.name" />
        </div>
        <div class="tab-icon-wrapper" v-else-if="page.PrivateName === 'Безопасность'">
          <svg-icon name="security" :active="parentRoute == page.name" />
        </div>
        {{page.PrivateName}}
      </v-tab>
    </template>

    <v-list dense v-for="(item, i) in childPages" :key="i">
      <v-list-item
        class="pt-0 pb-0"
        dense
        v-if="item.IsLeaf"
        link
        :to="item.Action"
      >{{ item.PrivateName }}</v-list-item>
      <ps-horizontal-menu-item v-else :page="item" />
    </v-list>
  </v-menu>
</template>

<script>
import { mapActions, mapGetters } from "vuex";
import svgIcon from "@/components/svg-icons/svg-icon";
// import axios from "axios";
import { pages } from "@/public/data"; // временное хранилище данных

export default {
  name: "psHorizontalMenuItem",
  components: {
    svgIcon,
  },
  data() {
    return {
      childPages: [],
    };
  },
  props: {
    page: {
      type: Object,
    },
  },
  computed: {
    ...mapGetters(["PAGES"]),
    parentRoute() {
      const route = this.$route.name.split(".");
      return route[0];
    },
  },
  mounted() {
    if (!this.page.IsLeaf) {
      this.fetchChildsPages();
    }
  },
  methods: {
    ...mapActions(["FETCH_PAGES"]),
    fetchChildsPages() {
      // await axios
      //   .get(`http://localhost:3000/pages?ParentId=${this.page.idr}`)
      //   .then((res) => {
      //     this.childPages = res.data;
      //   })
      //   .catch((err) => console.error(err));
      if (this.PAGES.length) {
        this.childPages = pages.filter(
          (item) => item.ParentId === this.page.idr
        );
      }
    },
  },
};
</script>
