<template>
  <div class="text-center p-3">
    <img :src="vault?.img" class="rounded" alt="">
    <h1>{{ vault?.name }}</h1>
    <h4>by {{ vault.creator?.name }}</h4>
    <button class="border-0 bg-transparent" v-if="vault?.creatorId == account?.id" @click.stop="deleteVault()"
      title="Delete Vault"><i class="mdi mdi-delete text-button fs-4"></i></button>
  </div>
  <div>
    <h5 class="text-center">{{ keeps?.length }} Keeps</h5>
  </div>
  <div class="mason">
    <div v-for="k in keeps" class="p-1">
      <KeepCard :keep="k" />
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { useRoute, useRouter } from "vue-router";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { router } from '../router.js'
import { vaultsService } from "../services/VaultsService.js";
import { keepsService } from "../services/KeepsService.js";
import KeepCard from "../components/KeepCard.vue";
export default {
  props: { vault: { type: Object, required: true } },
  setup(props) {
    const route = useRoute();
    onMounted(() => {
      getVaultById();
      getVaultKeeps();
    });
    async function getVaultById() {
      try {
        await vaultsService.getVaultById(route.params.id);

      }
      catch (error) {
        logger.error(error);
        Pop.error(error.message);
        Pop.error('you are not supposed to be here')
        router.push({ name: 'Home' })
      }
    }
    async function getVaultKeeps() {
      try {
        await keepsService.getVaultKeeps(route.params.id);
      }
      catch (error) {
        logger.error(error);
        Pop.error(error.message);
      }
    }
    return {
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.activeVaultKeeps),
      account: computed(() => AppState.account),
      async removeKeep(vaultKeepId) {
        try {
          await keepsService.removeKeep(vaultKeepId);
        }
        catch (error) {
          Pop.toast(error.data, "error", "center");
        }
      },
      async deleteVault() {
        try {
          if (await Pop.confirm())
            await vaultsService.deleteVault(AppState.activeVault?.id)
          Pop.toast('Vault Deleted', 'success')
        } catch (error) {

        }
      }
    };
  },
  components: { KeepCard }
};
</script>


<style lang="scss" scoped>
.mason {
  columns: 4;
}
</style>