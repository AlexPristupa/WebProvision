
<template>
  <v-row justify="start">
    <v-dialog v-model="deleteConfirmDialog" max-width="65%">
      <v-card>
        <v-card-title>
          <span class="headline">{{$vuetify.lang.t('$vuetify.deleteServer')}}</span>
        </v-card-title>
        <v-card-text>{{$vuetify.lang.t('$vuetify.areYouSureYouWantToDeleteTheUser')}}</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <!-- <v-btn color="secondary" @click="closeDeleteForm">Закрыть</v-btn>
          <v-btn color="primary" @click="deleteItem">Удалить</v-btn>-->
        </v-card-actions>
      </v-card>
    </v-dialog>

    <v-col cols="12">
      <v-row align="center" justify="space-between">
        <v-col>
          <v-row align="center">
            <v-card-subtitle>{{$vuetify.lang.t('$vuetify.serverNodes')}}</v-card-subtitle>
            <v-btn
              class="ps-btn-add"
              :color="CONFIG.colors.primary"
              @click="addNode"
            >{{$vuetify.lang.t('$vuetify.add')}}</v-btn>
          </v-row>
        </v-col>
        <v-col cols="3">
          <v-card-subtitle>{{$vuetify.lang.t('$vuetify.clusterVersion')}}: 1.11</v-card-subtitle>
        </v-col>
      </v-row>

      <v-data-table
        :hide-default-footer="CONFIG.table.hideDefaultFooter"
        :header-props="CONFIG.table.headerProps"
        :class="CONFIG.table.class"
        :footer-props="CONFIG.table.footerProps"
        :dense="CONFIG.table.dense"
        height="auto"
        disable-pagination
        item-key="idr"
        :headers="headers"
        :items="NODES"
      >
        <template v-slot:item.actions="props">
          <div class="text-truncate">
            <!-- <v-icon class="mr-2" @click="showEditForm(item)" color="primary">mdi-pencil</v-icon> -->
            <v-icon
              class="ps-btn-delete"
              @click="deleteItem(props.item)"
              :color="CONFIG.colors.pink"
            >mdi-delete</v-icon>
          </div>
          <!-- <v-btn color="primary" @click="initialize">Перезагрузить</v-btn> -->
        </template>
      </v-data-table>
    </v-col>
  </v-row>
</template>

<script>
import { mapGetters, mapActions } from "vuex";

export default {
  name: "ps-nodes-table",
  props: ["serverId"],
  data() {
    return {
      deleteConfirmDialog: false,
      headers: [
        {
          text: "",
          sortable: true,
        },
        {
          text: this.$vuetify.lang.t("$vuetify.priority"),
          value: "priority",
        },
        {
          text: this.$vuetify.lang.t("$vuetify.ipAddress"),
          value: "nodAddress",
        },
        {
          text: this.$vuetify.lang.t("$vuetify.name"),
          value: "name",
        },
        {
          text: this.$vuetify.lang.t("$vuetify.actions"),
          value: "actions",
        },
      ],
    };
  },
  mounted() {},
  computed: {
    ...mapGetters(["NODES", "CONFIG"]),
  },
  methods: {
    ...mapActions(["REMOVE_NODE", "ADD_NODE", "CHANGE_NODE"]),
    deleteItem(node) {
      const self = this;
      if (
        confirm(
          this.$vuetify.lang.t("$vuetify.areYouSureYouWantToDeleteTheNode")
        )
      ) {
        this.REMOVE_NODE(node);
        self.$forceUpdate();
      }
    },
    addNode() {
      this.ADD_NODE({
        priority: Date.now(),
        nodAddress: "192.108.80.230",
        name: `Node # ${Date.now()}`,
        serverId: this.serverId,
      });
    },
  },
};
</script>

<style lang="scss" scoped>
.flip-list-move {
  transition: transform 0.5s;
}
.no-move {
  transition: transform 0s;
}
.ghost {
  opacity: 0.5;
  background: #c8ebfb;
}
.list-group {
  min-height: 20px;
}
.list-group-item {
  cursor: move;
}
.list-group-item i {
  cursor: pointer;
}
</style>
