<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-dark px-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <a class="d-flex flex-column align-items-center selectable fw-bold">
        Home
      </a>
    </router-link>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse dropdown p-3" id="navbarText">
      <a class="d-flex flex-column align-items-center selectable fs-5 fw-bold dropdown-toggle"
        data-bs-toggle="dropdown">
        Create
      </a>
      <ul class="dropdown-menu">
        <li><button class="dropdown-item" data-bs-toggle="modal" data-bs-target="#CreateKeep">Create
            Keep</button></li>
        <li><button class="dropdown-item" data-bs-toggle="modal" data-bs-target="#CreateVault">Create
            Vault</button></li>
      </ul>
      <!-- LOGIN COMPONENT HERE -->
    </div>
    <Login />
  </nav>
  <CreateKeepModal />
  <CreateVaultModal />
</template>

<script>
import CreateKeepModal from "./CreateKeepModal.vue";
import CreateVaultModal from "./CreateVaultModal.vue";
import Login from './Login.vue'
export default {
  setup() {
    return {}
  },
  components: { Login, CreateKeepModal, CreateVaultModal }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
