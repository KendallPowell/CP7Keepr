<template>
  <div class="modal fade" id="CreateVault" tabindex="-1" aria-labelledby="createvaultLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="exampleModalLabel">Create a New Vault</h1>
        </div>
        <div class="modal-body">
          <form @submit.prevent="makeVault()">
            <div class="mb-3">
              <label for="name" class="form-label">Name</label>
              <input type="text" required name="name" v-model="editable.name" class="form-control" id="name"
                aria-describedby="name of vault">
            </div>
            <div class="mb-3">
              <label for="image" class="form-label">Image</label>
              <input type="text" required name="img" v-model="editable.img" class="form-control" id="img"
                aria-describedby="image">
            </div>
            <div class="mb-3">
              <label for="description" class="form-label">Description</label>
              <textarea type="text" required name="description" v-model="editable.description" class="form-control"
                id="description"></textarea>
            </div>
            <div class="mb-3 form-check">
              <input type="checkbox" class="form-check-input" id="exampleCheck1">
              <label class="form-check-label" for="exampleCheck1">Make Private?</label>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
              <button type="submit" class="btn btn-primary">Save changes</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { useRoute } from "vue-router";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { vaultsService } from "../services/VaultsService.js";
export default {
  setup() {
    const route = useRoute()
    const editable = ref({})
    return {
      editable,
      async makeVault() {
        try {
          editable.value.vaultId = route.params.id
          editable.value.IsPrivate = false
          await vaultsService.makeVault(editable.value)
          editable.value = {}
          Pop.toast('Vault Created', 'success')
        } catch (error) {
          Pop.error(error)
          logger.error(error)
        }
      }
    }
  }
};
</script>


<style lang="scss" scoped>

</style>