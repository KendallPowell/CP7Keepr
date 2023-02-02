<template>
  <div class="keepCard text-white p-1 elevation-3 m-4 selectable" :title="keep.name" @click="activeKeep(keep.id)"
    data-bs-toggle="modal" data-bs-target="#keepDetails">
    <img :src="keep.img" alt="can not display image" class="card-img rounded-3">
    <div class="card-img-overlay d-flex justify-content-between align-items-end">
      <button v-if="keep.creatorId == account.id" @click="deleteKeep"><i
          class="mdi mdi-delete text-white fs-4"></i></button>
      <h3 class="p-3">{{ keep.name }}</h3>
      <img :src="keep.creator.picture" class="creator-pic" alt="">

    </div>
  </div>
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
      },
      async deleteKeep() {
        try {
          if (await Pop.confirm())
            await keepsService.deleteKeep(props.keep.id)
        } catch (error) {
          Pop.error(error)
          logger.error(error.message)
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