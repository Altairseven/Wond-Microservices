<template>
  <div>Products</div>

  <h1 v-if="authStore.user != null">
    {{ authStore.user?.userId }} | {{ authStore.user?.userName }}
  </h1>

  <button class="rounded bg-emerald-500  hover:bg-emerald-400 duration-500 p-2 shadow-lg text-white hover:scale-110" @click="doTheThing">Mayling</button>
  <h2>{{resp}}</h2>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { http } from "../../http";
import { useAuthStore } from "../../store/Index";

const authStore = useAuthStore();

const resp = ref("");

async function doTheThing() {
  try {

      const rr = await http.get("testauth");
  
    resp.value = rr.data;
  } catch (error) {
    resp.value = JSON.stringify(error);
  }

}
</script>
