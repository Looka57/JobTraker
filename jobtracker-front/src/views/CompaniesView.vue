<script setup>
import { ref, onMounted } from 'vue';
import axios from 'axios';
import Card from 'primevue/card';
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import ListeEntreprise from '@/components/ListeEntreprise.vue';

// État de la vue
const entreprises = ref([]);
const chargement = ref(true);
const erreur = ref(null);

onMounted(async () => {
    try {
        const response = await axios.get('https://localhost:7265/api/Companies');
        entreprises.value = response.data;
        console.log("Données reçues :", response.data);
    } catch (err) {
        erreur.value = "Erreur de chargement des entreprises";
    } finally {
        chargement.value = false;
    }
});
</script>

<template>
    <div class="page-container">
        <h1 class="fw-bold mb-4">🏢 Liste de mes entreprises</h1>

        <div v-if="chargement">⏳ Chargement...</div>
        <div v-else-if="erreur" class="text-error">❌ {{ erreur }}</div>

        <Card v-else>
            <template #content>
                <ListeEntreprise />
            </template>
        </Card>
    </div>
</template>