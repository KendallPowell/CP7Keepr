<template>
  <RouterLink :to="{ name: 'Vault', params: { id: vault.id } }">
    <div class="vaultCard text-white p-1 elevation-3 selectable">
      <img class="card-img rounded-3" :src="vault.img" alt="">
      <div class="card-img-overlay d-flex justify-content-between align-items-end">
        <h4 class="p-3">{{ vault.name }}</h4>
      </div>
    </div>
  </RouterLink>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
export default {
  props: { vault: { type: Object, required: true } },
  setup(props) {
    return {

    }
  }
};
</script>


<style lang="scss" scoped>
.vaultCard {
  position: relative;
  height: 25vh;
  width: 15vw;
}
</style>