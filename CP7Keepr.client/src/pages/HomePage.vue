<template>
  <div class="container-fluid">
    <div class="mason">
      <div v-for="k in keeps" title="Go to Keep's Details." class="">
        <KeepCard :keep="k" />
      </div>
    </div>
  </div>
  <KeepModal />
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { logger } from "../utils/Logger.js";
import { keepsService } from "../services/KeepsService.js";
import Pop from "../utils/Pop.js";
import KeepCard from "../components/KeepCard.vue";

export default {
  setup() {

    async function getKeeps() {
      try {
        await keepsService.getKeeps();
      } catch (error) {
        Pop.error(error);
        logger.error(error.message);
      }
    }

    onMounted(() => {
      getKeeps();
    });

    return {
      keeps: computed(() => AppState.keeps),
    }
  }
};
</script>


<style lang="scss" scoped>
.homePage {
  padding: 2rem;
  display: flex;
  justify-content: space-between;
}

.mason {
  columns: 4;
}

@media (max-width: 550px) {
  .mason {
    columns: 2;
  }
}
</style>
