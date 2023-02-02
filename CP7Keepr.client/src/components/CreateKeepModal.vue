<template>
  <div class="modal fade" id="CreateKeep" tabindex="-1" aria-labelledby="createkeepLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="exampleModalLabel">Create a New Keep</h1>
        </div>
        <div class="modal-body">
          <form @submit.prevent="createKeep()">
            <div class="mb-3">
              <label for="name" class="form-label">Name</label>
              <input type="text" required name="name" v-model="editable.name" class="form-control" id="name"
                aria-describedby="name of keep">
            </div>
            <div class="mb-3">
              <label for="img" class="form-label">Image</label>
              <input type="text" required name="img" v-model="editable.img" class="form-control" id="img"
                aria-describedby="image of keep">
            </div>
            <div class="mb-3">
              <label for="description" class="form-label">Description</label>
              <textarea type="text" required name="description" v-model="editable.description" class="form-control"
                id="description"></textarea>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
              <button type="submit" class="btn btn-primary">Create New Keep</button>
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
import { keepsService } from "../services/KeepsService.js";
export default {
  setup() {
    const route = useRoute()
    const editable = ref({})
    return {
      editable,
      async createKeep() {
        try {
          await keepsService.createKeep(editable.value)
          editable.value = {}
          editable.value.keepId = route.params.keepId
          Pop.toast('Keep Created', 'success')
        } catch (error) {
          Pop.error(error)
          logger.error(error)
        }
      }
    }
  }
}

</script>


<style lang="scss" scoped>

</style>