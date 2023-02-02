<template>
  <div class="modal fade" id="CreateKeep" tabindex="-1" aria-labelledby="createkeepLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="exampleModalLabel">Create a New Keep</h1>
        </div>
        <div class="modal-body">
          <form>
            <div class="mb-3">
              <label for="name" class="form-label">Name</label>
              <input type="name" class="form-control" id="name" aria-describedby="name of keep">
            </div>
            <div class="mb-3">
              <label for="img" class="form-label">Image</label>
              <input type="img" class="form-control" id="img" aria-describedby="image of keep">
            </div>
            <div class="mb-3">
              <label for="description" class="form-label">Description</label>
              <textarea type="text" class="form-control" id="description"></textarea>
            </div>
          </form>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
          <button type="button" class="btn btn-primary">Save changes</button>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
export default {
  setup() {
    return {}
  }
};
</script>


<style lang="scss" scoped>

</style>