<template>
  <div class="container-fluid">
    <div class="row align-items-center justify-content-center">
      <div class="d-flex justify-content-center">
        <img class="cover-img" :src="profile.coverImg" alt="">
      </div>
      <img class="creator-pic" :src="profile.picture" alt="">
      <h1 class="text-center">{{ profile.name }}</h1>
      <p class="text-center"> Keeps: {{ profileKeeps.length }} Vaults: {{ profileVaults.length }}</p>
      <h3>Vaults:</h3>
      <div v-if="profileVaults.isPrivate != true" class="vault-card">
        <VaultCard :vault="v" v-for="v in profileVaults" />
      </div>
      <h3>Keeps:</h3>
      <div class="keep-card">
        <KeepCard :keep="k" v-for="k in profileKeeps" />
      </div>
    </div>
  </div>
  <KeepModal />
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { profilesService } from "../services/ProfilesService.js"
import { useRoute } from "vue-router";
import VaultCard from "../components/VaultCard.vue";
import KeepCard from "../components/KeepCard.vue";
import KeepModal from "../components/KeepModal.vue";
export default {
  setup() {
    const route = useRoute();
    onMounted(() => {
      getProfile();
      getProfileKeeps();
      getProfileVaults();
    });
    async function getProfile() {
      try {
        await profilesService.getProfile(route.params.profileId);
      }
      catch (error) {
        Pop.error(error);
        logger.error(error);
      }
    }
    async function getProfileKeeps() {
      try {
        await profilesService.getProfileKeeps(route.params.profileId);
      }
      catch (error) {
        Pop.error(error);
        logger.error(error);
      }
    }
    async function getProfileVaults() {
      try {
        await profilesService.getProfileVaults(route.params.profileId);
      }
      catch (error) {
        Pop.error(error);
        logger.error(error);
      }
    }
    return {
      profile: computed(() => AppState.activeProfile),
      profileKeeps: computed(() => AppState.profileKeeps),
      profileVaults: computed(() => AppState.profileVaults),
      route,
    };
  },
  components: { VaultCard, KeepCard, KeepModal }
};
</script>


<style lang="scss" scoped>
.creator-pic {
  height: 100px;
  width: 130px;
  border-radius: 50%;
  object-fit: cover;
  margin-right: 10px;
  margin-bottom: 15px;
}

.cover-img {
  padding: 1rem;
  height: 40vh;
  width: 50vw;
  background-size: cover;
  background-position: center;
}

.vault-card {
  padding: 1rem;
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
}

.keep-card {
  columns: 4;
}
</style>