<template>
  <div class="about text-center">
    <img class="coverImg" :src="account.CoverImg" alt="not working">
    <img class="rounded" :src="account.picture" alt="" />
    <h1>{{ account.name }}</h1>
  </div>
  <div class="row">
    <form @submit.prevent="editAccount()" class="d-flex flex-column">
      <input v-model="editable.name" class="col-3 rounded p-2 m-2" type="text">
      <input v-model="editable.picture" class="col-3 rounded p-2 m-2" type="url">
      <input v-model="editable.coverImg" class="col-3 rounded p-2 m-2" type="url">
      <div>
        <button class="btn btn-success">Save</button>
      </div>
    </form>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { useRoute } from "vue-router"
import { AppState } from '../AppState'
import EditAccountModal from "../components/EditAccountModal.vue.js"
import { accountService } from "../services/AccountService.js"
import { keepsService } from "../services/KeepsService.js"
import { vaultsService } from "../services/VaultsService.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
export default {
  setup() {
    const route = useRoute();
    async function getMyVaults() {
      try {
        await vaultsService.getMyVaults();
      }
      catch (error) {
        Pop.error(error.message);
        logger.log(error);
      }
    }
    onMounted(() => {
      getMyVaults();
    });
    return {
      editable,
      myKeeps: computed(() => AppState.myKeeps),
      myVaults: computed(() => AppState.myVaults),
      account: computed(() => AppState.account),
      async editAccount() {
        try {
          await accountService.editAccount(editable.value)
        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
      }
    };
  },
  components: { EditAccountModal }
}
</script>

<style scoped>
img {
  max-width: 100px;
}

.coverImg {
  position: absolute;
  width: 759px;
  height: 338px;
}
</style>
