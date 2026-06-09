<script setup>
// =========================
// IMPORT VUE
import { onMounted } from 'vue'
import { useCandidatureStore } from '@/stores/candidatureStore'

// =========================
// Composants PrimeVue
import Card from 'primevue/card'
import Button from 'primevue/button'

// =========================
// Composants internes
import ListeCandidatures from '../components/ListeCandidatures.vue'
import Statistiques from '@/components/Statistiques.vue'

// =========================
// STORE PINIA
// =========================
const store = useCandidatureStore()

// =========================
// CHARGEMENT API VIA STORE
// =========================
onMounted(() => {
    store.chargerCandidatures()
})
</script>

<template>
    <div class="dashboard-container">
        <!-- ================= HEADER ================= -->
        <div class="header-zone">
            <h1>📊 Mon Tableau de bord</h1>
            <div class="date-badge">
                <i class="pi pi-calendar"></i>
                <span>{{ new Date().toLocaleDateString() }}</span>
            </div>
        </div>

        <!-- ================= ETATS API ================= -->
        <!-- Chargement -->
        <div v-if="store.chargement" class="status-msg">
            ⏳ Connexion à l'API .NET...
        </div>

        <!-- Erreur API -->
        <div v-else-if="store.erreur" class="status-msg text-error">
            ❌ {{ store.erreur }}
        </div>

        <div v-else>
            <!-- ================= STATS ================= -->
            <div class="stats-grid">
                <Card class="stat-card refuse">
                    <template #title>
                        <span class="card-title">Refusé</span>
                    </template>
                    <template #content>
                        <span class="card-value">{{ store.totalRefuse }}</span>
                    </template>
                </Card>

                <Card class="stat-card accepte">
                    <template #title>
                        <span class="card-title">Accepté</span>
                    </template>
                    <template #content>
                        <span class="card-value">{{ store.totalAccepte }}</span>
                    </template>
                </Card>

                <Card class="stat-card encours">
                    <template #title>
                        <span class="card-title">En cours</span>
                    </template>
                    <template #content>
                        <span class="card-value">{{ store.totalEnCours }}</span>
                    </template>
                </Card>

            </div>

            <!-- ================= LISTE ================= -->
            <Card class="table-card">
                <template #title>
                    <div class="table-header">
                        <h3>📋 Les 5 dernières candidatures</h3>

                        <router-link to="/FormAjtCandidature">
                            <Button label="Ajout candidature" severity="secondary" raised />
                        </router-link>
                    </div>
                </template>

                <template #content>
                    <ListeCandidatures
                        :donnees="store.candidatures"
                        :limite="5"
                    />
                </template>
            </Card>

            <!-- ================= STATS COMPONENT ================= -->
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

/* ===== DATE BADGE ===== */
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

/* ===== GRID STATS ===== */
.stats-grid {
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    gap: 1.5rem;
    margin-bottom: 2rem;
}

/* ===== CARDS STATS ===== */
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

/* ===== REFUSÉ ===== */
.refuse {
    background-color: #fef2f2;
    border-color: #fee2e2;
}

.refuse .card-value,
.refuse .card-title {
    color: #dc2626;
}

/* ===== ACCEPTÉ ===== */
.accepte {
    background-color: #f0fdf4;
    border-color: #dcfce7;
}

.accepte .card-value,
.accepte .card-title {
    color: #16a34a;
}

/* ===== EN COURS ===== */
.encours {
    background-color: #fffbeb;
    border-color: #fef3c7;
}

.encours .card-value,
.encours .card-title {
    color: #d97706;
}

/* ===== TABLE ===== */
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

/* ===== STATUS MESSAGES ===== */
.status-msg {
    padding: 1rem;
    border-radius: 6px;
    background: white;
    border: 1px solid #e2e8f0;
}

/* ===== STATS SECTION ===== */
.table-card-stats {
    margin-top: 2rem;
}

/* (pas utilisé actuellement) */
.btn-candidature {
    margin-top: 1rem;
    text-align: center;
}

</style>