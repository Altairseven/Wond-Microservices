<template>
  <div>Products</div>

  <h1 v-if="authStore.user != null">
    {{ authStore.user?.userId }} | {{ authStore.user?.userName }}
  </h1>
</template>

<script setup lang="ts">
import { useAuthStore } from "../../store/Index";

const authStore = useAuthStore();
</script>
