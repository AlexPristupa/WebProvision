<template>
  <v-app>
    <!-- <ps-top-bar v-if="IS_AUTH" /> -->
    <ps-horizontal-menu v-if="IS_AUTH" />
    <v-container class="wrapper" fluid style="padding: 60px 35px; height: 100%">
      <router-view />
    </v-container>
  </v-app>
</template>

<script>
import { mapActions, mapGetters } from "vuex";
import psHorizontalMenu from "@/components/horizontal-menu/ps-horizontal-menu";
// import psTopBar from "@/components/top-bar/ps-top-bar";

export default {
  components: {
    psHorizontalMenu,
    // psTopBar,
  },
  data() {
    return {};
  },
  mounted() {
    //this.CHECK_IS_VALID_TOKEN();
    console.log(location.protocol)
    console.log(location.hostname)
    // Делает окна перетаскиваемыми
    const d = {};
    document.addEventListener("mousedown", (e) => {
      
      const closestDialog = e.target.closest(".v-dialog.v-dialog--active");

      if (
        e.button === 0 &&
        closestDialog != null &&
        e.target.classList.contains("popup-header")
      ) {
        // element which can be used to move element
        d.el = closestDialog; // element which should be moved
        d.mouseStartX = e.clientX;
        d.mouseStartY = e.clientY;
        d.elStartX = d.el.getBoundingClientRect().left;
        d.elStartY = d.el.getBoundingClientRect().top;
        d.el.style.position = "fixed";
        d.el.style.margin = 0;
        d.oldTransition = d.el.style.transition;
        d.el.style.transition = "none";
      }
    });
    document.addEventListener("mousemove", (e) => {
      if (d.el === undefined) return;
      d.el.style.left =
        Math.min(
          Math.max(d.elStartX + e.clientX - d.mouseStartX, 0),
          window.innerWidth - d.el.getBoundingClientRect().width
        ) + "px";
      d.el.style.top =
        Math.min(
          Math.max(d.elStartY + e.clientY - d.mouseStartY, 0),
          window.innerHeight - d.el.getBoundingClientRect().height
        ) + "px";
    });
    document.addEventListener("mouseup", () => {
      if (d.el === undefined) return;
      d.el.style.transition = d.oldTransition;
      d.el = undefined;
    });
    setInterval(() => {
      // prevent out of bounds
      const dialog = document.querySelector(".v-dialog.v-dialog--active");
      if (dialog === null) return;
      dialog.style.left =
        Math.min(
          parseInt(dialog.style.left),
          window.innerWidth - dialog.getBoundingClientRect().width
        ) + "px";
      dialog.style.top =
        Math.min(
          parseInt(dialog.style.top),
          window.innerHeight - dialog.getBoundingClientRect().height
        ) + "px";
    }, 100);
    this.changeLocale("ru"); // меняем язык на русский
  },
  computed: {
    ...mapGetters(["IS_AUTH"]),
  },
  methods: {
    ...mapActions(["SIGN_OUT", "CHECK_IS_VALID_TOKEN"]),
    changeLocale(locale) {
      this.$vuetify.lang.current = locale;
    },
  },
};
</script>

<style lang="scss">
@import "src/scss/self/main.scss";
</style>
