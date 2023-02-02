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
              <input type="text" required name="name" v-model="editable.name" class="form-control" id="name"
                aria-describedby="name of keep">
              <label for="name" class="form-label">Name</label>
            </div>
            <div class="mb-3">
              <input type="text" required name="img" v-model="editable.img" class="form-control" id="img"
                aria-describedby="image of keep">
              <label for="img" class="form-label">Image</label>
            </div>
            <div class="mb-3">
              <textarea type="text" required name="description" v-model="editable.description" class="form-control"
                id="description"></textarea>
              <label for="description" class="form-label">Description</label>
            </div>
          </form>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
          <button type="" class="btn btn-primary">Save changes</button>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { useRoute } from "vue-router";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { keepsService } from "../services/KeepsService.js";
export default {
  setup() {
    const route = useRoute()
    const editable = ({})
    return {
      editable,
      async createKeep() {
        try {
          editable.value.keepId = route.params.id
          await keepsService.createKeep(editable.value)
          editable.value = {}
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