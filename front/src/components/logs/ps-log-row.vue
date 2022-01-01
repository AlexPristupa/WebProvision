
<template>
  <div class="log-row">
    <div class="log-row__text">{{text | ''}}</div>
    <div class="log-row__status">
      <div class="status__icon">
          <!-- values: done, fail, wait -->
        <svg-icon :name="status" /> 
      </div>
      <div v-if="status === 'done'" class="status__text">
          Выполнено
      </div>
      <div v-else-if="status === 'fail'" class="status__text">
          Ошибка
      </div>
      <div v-else-if="status === 'wait'" class="status__text">
          Ожидание
      </div>
    </div>
  </div>
</template>

<script>
import svgIcon from "@/components/svg-icons/svg-icon";

export default {
  name: "psLogRow",
  components: {
      svgIcon,
  },
  props: {
    text: {
        type: String,
    },
    status: {
        type: Boolean,
        required: true
    }
  },
};
</script>