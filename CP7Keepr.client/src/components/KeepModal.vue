<template v-if="activeKeep">
  <div class="modal fade" id="keepDetails" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl ">
      <div class="modal-content text-dark">
        <div class="d-flex">
          <img :src="activeKeep.img" class="w-50 rounded-start" alt="">
          <div class="keepModal d-flex flex-column justify-content-between w-50">
            <div class="d-flex gap-4 justify-content-center w-100">
              <i class="mdi mdi-eye">{{ activeKeep?.views }}</i>
              <i class="mdi mdi-alpha-k-circle-outline"> {{ activeKeep?.kept }}</i>
            </div>
            <div>
              <h2 class="text-center"> {{ activeKeep?.name }} </h2>
              <h5 class=""> {{ activeKeep?.description }}</h5>
            </div>
            <div class="align-self-end justify-content-around d-flex align-items-center gap-3">
              <div v-if="!keep?.vaultKeepId && account?.id">
                <select v-model="editable.vaultId" name="vaultId">
                  <option :value="v.id" v-for="v in vaults">{{ v.name }}</option>
                </select>
                <button @click="addKeepToVault()">Add To Vault</button>
              </div>
              <div v-if="keep?.vaultKeepId && account?.id">
                <button @click="removeKeepFromVault()">Remove Keep from Vault</button>
              </div>
              <button class="border-0 bg-transparent" v-if="keep?.creatorId == account?.id"
                @click.stop="deleteKeep()"><i class="mdi mdi-delete text-button fs-4"></i></button>
              <img @click="goToProfile" data-bs-dismiss="modal" class="creator-pic" :src="activeKeep.creator?.picture"
                :title="activeKeep.creator.name">
              <h6>{{ activeKeep.creator?.name }}</h6>
            </div>
          </div>
          <slot></slot>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref } from 'vue';
import { useRouter } from "vue-router";
import { vaultKeepsService } from '../services/VaultKeepsService.js'
import { keepsService } from '../services/keepsService.js'
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
export default {
  props: { keep: { type: Object } },
  setup(props) {
    const router = useRouter()
    return {
      account: computed(() => AppState.account),
      activeKeep: computed(() => AppState.activeKeep),
      vaults: computed(() => AppState.myVaults),
      editable: ref({}),
      goToProfile() {
        router.push({ name: "Profile", params: { profileId: this.activeKeep.creatorId } })
      },
      addKeepToVault() {
        try {
          this.editable.keepId = this.activeKeep.id
          console.log(this.editable)
          vaultKeepsService.addKeepToVault(this.editable)
        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
      },
      async removeKeepFromVault() {
        try {
          await vaultKeepsService.removeKeepFromVault(props.keep?.vaultKeepId)
        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
      },
      async deleteKeep() {
        try {
          if (await Pop.confirm())
            await keepsService.deleteKeep(AppState.activeKeep?.id)
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
.keepModal {
  padding: 2rem;
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