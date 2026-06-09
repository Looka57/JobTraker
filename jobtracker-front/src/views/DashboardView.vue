<script setup>
import { ref, onMounted, computed } from 'vue'
import axios from 'axios'
import Card from 'primevue/card'
import Button from 'primevue/button'
import ListeCandidatures from '../components/ListeCandidatures.vue'
import Statistiques from '@/components/Statistiques.vue'


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
        erreur.value = "Impossible de joindre l'API .NET."
    } finally {
        chargement.value = false
    }
}
// On cible "statusLibelle" qui contient le texte renvoyé par l'API .NET
const totalRefuse = computed(() => {
    return candidatures.value.filter(c => c.statusLibelle === 'Refusé').length
})

const totalAccepte = computed(() => {
    return candidatures.value.filter(c => c.statusLibelle === 'Accepté').length
})

// On regroupe sous "En cours" les états intermédiaires visibles dans tes données
const totalEnCours = computed(() => {
    return candidatures.value.filter(c =>
        c.statusLibelle === 'Suivi' ||
        c.statusLibelle === 'Entretien' ||
        c.statusLibelle === 'Envoyée'
    ).length
})

onMounted(() => {
    chargerCandidatures()
})
</script>

<template>
    <div class="dashboard-container">
        <div class="header-zone">
            <h1>📊 Mon Tableau de bord</h1>
            <div class="date-badge">
                <i class="pi pi-calendar"></i>
                <span>{{ new Date().toLocaleDateString() }}</span>
            </div>
        </div>

        <div v-if="chargement" class="status-msg">⏳ Connexion à l'API .NET...</div>
        <div v-else-if="erreur" class="status-msg text-error">❌ {{ erreur }}</div>

        <div v-else>
            <div class="stats-grid">
                <Card class="stat-card refuse">
                    <template #title><span class="card-title">Refusé</span></template>
                    <template #content><span class="card-value">{{ totalRefuse }}</span></template>
                </Card>
                <Card class="stat-card accepte">
                    <template #title><span class="card-title">Accepté</span></template>
                    <template #content><span class="card-value">{{ totalAccepte }}</span></template>
                </Card>
                <Card class="stat-card encours">
                    <template #title><span class="card-title">En cours</span></template>
                    <template #content><span class="card-value">{{ totalEnCours }}</span></template>
                </Card>
            </div>

            <!-- LEs 5 dernières candidatures -->
            <Card class="table-card">
                <template #title>
                    <div class="table-header ">
                        <h3>📋 Les 5 dernières candidatures</h3>
                        <router-link to="/FormAjtCandidature">
                            <Button label="Ajout candidature" severity="secondary" raised />
                        </router-link>
                    </div>
                </template>
                <template #content>
                    <ListeCandidatures :donnees="candidatures" :limite="5" />
                </template>
            </Card>
            <Card class="table-card-stats">
                <template #content>
                    <Statistiques />
                </template>
            </Card>
        </div>
    </div>
</template>

<style scoped>
.header-zone {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 2rem;
}

.header-zone h1 {
    color: #1e293b;
    font-weight: 700;
    margin: 0;
}

.date-badge {
    display: flex;
    align-items: center;
    gap: 0.5rem;
    background-color: #ffffff;
    padding: 0.5rem 1rem;
    border-radius: 8px;
    border: 1px solid #e2e8f0;
    color: #64748b;
    font-size: 0.9rem;
}

.stats-grid {
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    gap: 1.5rem;
    margin-bottom: 2rem;
}

.stat-card {
    text-align: center;
    border: 1px solid #e2e8f0;
}

.card-title {
    font-size: 1.1rem;
    font-weight: 600;
    color: #64748b;
}

.card-value {
    font-size: 2.5rem;
    font-weight: 700;
}

.refuse {
    background-color: #fef2f2;
    border-color: #fee2e2;
}

.refuse .card-value,
.refuse .card-title {
    color: #dc2626;
}

.accepte {
    background-color: #f0fdf4;
    border-color: #dcfce7;
}

.accepte .card-value,
.accepte .card-title {
    color: #16a34a;
}

.encours {
    background-color: #fffbeb;
    border-color: #fef3c7;
}

.encours .card-value,
.encours .card-title {
    color: #d97706;
}

.table-card {
    border: 1px solid #e2e8f0;
}

.table-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    font-size: 1.4rem;
    font-weight: 600;
    color: #1e293b;
    padding-bottom: 20px;
}

.status-msg {
    padding: 1rem;
    border-radius: 6px;
    background: white;
    border: 1px solid #e2e8f0;
}

.table-card-stats {
    margin-top: 2rem;
}

.btn-candidature {
    margin-top: 1rem;
    text-align: center;
}
</style>