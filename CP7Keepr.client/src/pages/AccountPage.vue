<template>
  <div class="about text-center">
    <div class="d-flex justify-content-center">
      <img class="cover-img" :src="account.coverImg" alt="Cover Image">
    </div>
    <img class="rounded" :src="account.picture" alt="User Image" />
    <h1>{{ account.name }}</h1>
  </div>
  <div class="vault-card">
    <VaultCard :vault="v" v-for="v in vaults" />
  </div>
  <div>
    <form @submit.prevent="editAccount()" class="d-flex flex-column">
      <input v-model="editable.name" class="col-3 rounded p-2 m-2" type="text" placeholder="Name">
      <input v-model="editable.picture" class="col-3 rounded p-2 m-2" type="url" placeholder="Picture">
      <input v-model="editable.coverImg" class="col-3 rounded p-2 m-2" type="url" placeholder="Cover Image">
      <div class="">
        <button class="btn btn-primary ms-3">Save</button>
      </div>
    </form>
  </div>
</template>

<script>
import { computed, onMounted, ref } from 'vue'
import { useRoute } from "vue-router"
import { AppState } from '../AppState'
import { accountService } from "../services/AccountService.js"
import { vaultsService } from "../services/VaultsService.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
export default {
  setup() {
    const editable = ref({})
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
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.myVaults),
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
}
</script>

<style scoped>
img {
  max-width: 100px;
}

.cover-img {
  padding: 1rem;
  height: 40vh;
  width: 50%;
  background-size: cover;
  background-position: center;
}

.vault-card {
  padding: 1rem;
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
}

.coverImg {
  position: absolute;
  width: 759px;
  height: 338px;
}
</style>
