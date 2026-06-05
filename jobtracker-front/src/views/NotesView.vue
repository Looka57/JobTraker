<script setup>
import { ref, onMounted } from 'vue'
import Button from 'primevue/button'
import NotesTabs from '@/components/NotesTabs.vue';
import DialogNoteAdd from '@/components/DialogNoteAdd.vue';
import axios from 'axios'

// 1. On crée une variable réactive pour contrôler l'affichage du Dialog
const isDialogVisible = ref(false)

// 1. On crée une vraie variable réactive pour stocker la LISTE des candidatures
const candidaturesData = ref([])

// 2. Fonction appelée au clic du bouton pour passer la variable à true
const showAddNoteDialog = () => {
  isDialogVisible.value = true
}
// 2. Ta fonction récupère les données et les stocke dans notre ref
const fetchCandidatures = async () => {
    try {
        const response = await axios.get('https://localhost:7265/api/Candidatures')
        candidaturesData.value = response.data // On remplit le tableau avec les vraies données
    } catch (err) {
        console.error("Erreur chargement candidatures", err)
    }
}

onMounted(() => {
    fetchCandidatures() // On charge les candidatures au montage du composant
})
</script>

<template>
  <div class="header">
    <h3>📝 Mes Notes </h3>
    <Button label="Ajout note" severity="info" raised @click="showAddNoteDialog" />
    <DialogNoteAdd v-model:visible="isDialogVisible" :candidatures="candidaturesData" />
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