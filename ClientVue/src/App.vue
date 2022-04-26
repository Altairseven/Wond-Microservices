

<template>
 <router-view></router-view>


  <!-- <img alt="Vue logo" src="./assets/logo.png" />
  <button v-if="!authStore.user"
    type="button"
    class="
      inline-flex
     
    "
    @click="testLogin()"
    :disabled="authStore.loading" 
  >
    <span v-if="!authStore.loading">Login</span>
    <span v-else> loading...</span>
  </button>
  <button v-else
    type="button"
    class="
      inline-flex
     
    "
    @click="testLogout()"
  >
    logout
  </button>
  <h1 v-if="authStore.user != null">{{ authStore.user?.userId }} | {{ authStore.user?.userName }} </h1> -->
</template>


<script setup lang="ts">
import { useAuthStore } from "./store/Index";


const authStore = useAuthStore();

function testLogin() {
  authStore.doLogin({ username: "altairseven", password: "Test1234" });
}

function testLogout() {
  authStore.doLogout();
}



</script>
<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;

  /* margin-top: 60px; */
}
</style>
