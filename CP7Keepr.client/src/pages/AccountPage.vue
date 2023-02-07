<template>
  <div class="about text-center">
    <div class="d-flex justify-content-center cover-img" :style="`background-image: url(${account.coverImg})`">
      <!-- <img class="cover-img" :src="account.coverImg" alt="Cover Image"> -->
    </div>
    <img class="rounded" :src="account.picture" alt="User Image" />
    <h1>{{ account.name }}</h1>
    <p class="text-center"> Keeps: {{ profileKeeps.length }} Vaults: {{ vaults.length }}</p>
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
  <div class="vault-card">
    <VaultCard :vault="v" v-for="v in vaults" />
  </div>
  <div class="keep-card">
    <KeepCard :keep="k" v-for="k in profileKeeps" />
  </div>
  <EditAccountModal />
</template>

<script>
import { computed, onMounted, ref } from 'vue'
import { useRoute } from "vue-router"
import { AppState } from '../AppState'
import EditAccountModal from "../components/EditAccountModal.vue"
import KeepCard from "../components/KeepCard.vue"
import { accountService } from "../services/AccountService.js"
import { profilesService } from "../services/ProfilesService.js"
import { vaultsService } from "../services/VaultsService.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
export default {
  setup() {
    const editable = ref({});
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
    async function getProfileKeeps() {
      try {
        await profilesService.getProfileKeeps(AppState.account.id)
      } catch (error) {
        Pop.error(error)
        logger.log(error)
      }
    }
    onMounted(() => {
      getMyVaults();
      getProfileKeeps();
    });
    return {
      editable,
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.myVaults),
      profileKeeps: computed(() => AppState.profileKeeps),
      async editAccount() {
        try {
          await accountService.editAccount(editable.value);
        }
        catch (error) {
          logger.error(error);
          Pop.error(error.message);
        }
      }
    };
  },
  components: { KeepCard, EditAccountModal }
}
</script>

<style scoped>
img {
  max-width: 100px;
}

.cover-img {
  height: 50vh;
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
