<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'
import Card from 'primevue/card'
import ListeCandidatures from '../components/ListeCandidatures.vue'

const candidatures = ref([])
const chargement = ref(true)
const erreur = ref(null)

const chargerCandidatures = async () => {
    try {
        chargement.value = true
        const response = await axios.get('https://localhost:7265/api/Candidatures')
        candidatures.value = response.data
    } catch (err) {
        console.error(err)
        erreur.value = "Impossible de charger les candidatures."
    } finally {
        chargement.value = false
    }
}

onMounted(() => {
    chargerCandidatures()
})
</script>

<template>
    <div>
        <h2 class="fw-bold mb-4">📋 Historique complet de mes candidatures</h2>

        <div v-if="chargement">⏳ Chargement...</div>
        <div v-else-if="erreur" class="text-error">❌ {{ erreur }}</div>

        <Card v-else style="border: 1px solid #e2e8f0;">
            <template #content>
                <ListeCandidatures :donnees="candidatures" />
            </template>
        </Card>
    </div>
</template>

<style scoped>
h2 {
    color: #1e293b;
    font-weight: 700;
}
</style>