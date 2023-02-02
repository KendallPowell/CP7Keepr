<template v-if="activeKeep">
  <div class="modal fade" id="keepDetails" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl ">
      <div class="modal-content text-dark">
        <div class="d-flex">
          <div class="">
            <img :src="activeKeep.img" class="img-fluid rounded-start" alt="">
          </div>
          <div class="keepModal">
            <i class="mdi mdi-eye">{{ activeKeep?.views }}</i>
            <i class="mdi mdi-alpha-k-circle-outline"> {{ activeKeep?.kept }}</i>
            <h2> {{ activeKeep?.name }} </h2>
            <h5> {{ activeKeep?.description }}</h5>
            <div class="align-self-end justify-content-around d-flex align-items-center">
              <h6>{{ activeKeep.creator?.name }}</h6>
              <img @click="goToProfile" data-bs-dismiss="modal" class="creator-pic" :src="activeKeep.creator?.picture"
                alt="">
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
import { computed } from 'vue';
import { useRouter } from "vue-router";
export default {
  setup() {
    const router = useRouter()
    return {
      activeKeep: computed(() => AppState.activeKeep),

      goToProfile() {

        router.push({ name: "Profile", params: { profileId: this.activeKeep.creatorId } })
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