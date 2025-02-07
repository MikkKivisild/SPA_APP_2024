<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
</script>

<template>
  <nav>
    <RouterLink to="/home" style="background-color: crimson; color:mediumpurple">Home</RouterLink>
    <RouterLink to="/about" style="color: crimson">About</RouterLink>
  </nav>
  <RouterView />  
</template>

<style scoped>
</style>
