<script setup>

import Button from 'primevue/button'
import NotesTabs from '@/components/NotesTabs.vue';





</script>

<template>
  <div class="header">
    <h3>📝 Mes Notes</h3>

    <router-link to="/FormAjtCandidature">
      <Button label="Ajout note" severity="info" raised />
    </router-link>
  </div>


    <NotesTabs />


</template>

<style scoped>
.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
}
</style>