<template>
  {{ keep }}

</template>


<script setup>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';

let props = defineProps({
  keep: {
    type: Object,
    required: true
  }
})
</script>


<style lang="scss" scoped>

</style>