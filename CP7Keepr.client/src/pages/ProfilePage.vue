<template>
  Profile Page
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { profilesService } from "../services/ProfilesService.js"
import { useRoute } from "vue-router";
export default {
  setup() {
    const route = useRoute()
    onMounted(() => {
      getProfile();
      getProfileKeeps();
      getProfileVaults();
    });
    async function getProfile() {
      try {
        await profilesService.getProfile(route.params.profileId)
      } catch (error) {
        Pop.error(error)
        logger.error(error)
      }
    }
    async function getProfileKeeps() {
      try {
        await profilesService.getProfileKeeps(route.params.profileId)
      } catch (error) {
        Pop.error(error)
        logger.error(error)
      }
    }
    async function getProfileVaults() {
      try {
        await profilesService.getProfileVaults(route.profileId)
      } catch (error) {
        Pop.error(error)
        logger.error(error)
      }
    }
    return {
      profile: computed(() => AppState.activeProfile),
      profileKeeps: computed(() => AppState.profileKeeps),
      profileVaults: computed(() => AppState.profileVaults),
      route,
    }
  }
};
</script>


<style lang="scss" scoped>

</style>