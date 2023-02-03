<template>
  <div class="keepCard text-white p-1 elevation-3 m-4 selectable" :title="keep.name" @click="activeKeep(keep.id)"
    data-bs-toggle="modal" data-bs-target="#keepDetails">
    <img :src="keep.img" alt="can not display image" class="img-fluid rounded-3">
    <div class="card-img-overlay d-flex justify-content-between align-items-end">
      <h3 class="p-3">{{ keep.name }}</h3>
      <img :src="keep.creator.picture" class="creator-pic" :title="keep.creator.name">
    </div>
  </div>
  <KeepModal :keep="keep" />
</template>


<script>
import { computed } from "vue";
import { AppState } from "../AppState.js";
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";



export default {
  props: { keep: { type: Object, required: true } },
  setup(props) {
    return {
      account: computed(() => AppState.account),
      async activeKeep(keepId) {
        try {
          await keepsService.getActiveKeep(keepId)
        } catch (error) {
          Pop.error(error.message)
          logger.error(error)
        }
      }
    }
  }
};
</script>


<style lang="scss" scoped>
.keepCard {
  position: relative;
}

.creator-pic {
  height: 50px;
  width: 50px;
  border-radius: 50%;
  object-fit: cover;
  margin-right: 10px;
  margin-bottom: 15px;
}
</style>