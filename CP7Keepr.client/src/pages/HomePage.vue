<template>
  <div class="container">
    <div class="row">
      <div>

      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { keepsService } from "../services/KeepsService.js";
export default {
  setup() {
    async function getKeeps() {
      try {
        await keepsService.getKeeps()
      } catch (error) {
        Pop.error(error);
        logger.error(error.message);
      }
    }
    return {

    }
  }
};
</script>


<style lang="scss" scoped>

</style>
