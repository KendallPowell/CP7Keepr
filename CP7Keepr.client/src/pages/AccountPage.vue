<template>
  <div class="about text-center">
    <h1>{{ account.name }}</h1>
    <img :src="account.CoverImg" alt="">
    <img class="rounded" :src="account.picture" alt="" />
    <p>{{ account.email }}</p>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { useRoute } from "vue-router"
import { AppState } from '../AppState'
import { keepsService } from "../services/KeepsService.js"
import { vaultsService } from "../services/VaultsService.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
export default {
  setup() {
    const route = useRoute()
    async function getMyVaults() {
      try {
        await vaultsService.getMyVaults()
      } catch (error) {
        Pop.error(error.message)
        logger.log(error)
      }
    }

    onMounted(() => {
      getMyVaults()
    })
    return {
      myKeeps: computed(() => AppState.myKeeps),
      myVaults: computed(() => AppState.myVaults),
      account: computed(() => AppState.account)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
