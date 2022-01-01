<template>
  <div>
    <div v-if="item.serverTestId" class="icon-with-text-wrapper">
      <div class="table-icon-wrapper">
        <svg-icon name="done" />
      </div>
      {{$vuetify.lang.t('$vuetify.yes')}}
    </div>
    <div v-else class="icon-with-text-wrapper">
      <div class="table-icon-wrapper">
        <div class="table-icon-wrapper">
          <svg-icon name="fail" />
        </div>
      </div>
      {{$vuetify.lang.t('$vuetify.no')}}
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import svgIcon from "@/components/svg-icons/svg-icon";
export default {
  name: "psIsTestingValue",
  props: {
    item: {
      type: Object,
      required: true,
    },
  },
  components: {
    svgIcon,
  },
  computed: {
    ...mapGetters(["SERVERS", "CURRENT_TASKS"]),
    isTestingValue() {
      if (this.SERVERS.length && this.CURRENT_TASKS.length) {
        const task = this.CURRENT_TASKS.find(
          (task) => task.id === this.item.taskId
        );
        if (task) {
          // если такой таск не undefined
          const server = this.SERVERS.find(
            (server) => server.idr === task.serverTestId
          );
          if (server) {
            // если server не undefined
            console.log("server0", server);
            return server.testBench;
          } else {
            return false;
          }
        } else {
          return false;
        }
      } else {
        return false;
      }
    },
  },
};
</script>

