<script setup>
import { ref, onMounted, computed } from 'vue'
import axios from 'axios'

// Importation des composants PrimeVue dont on a besoin
import Card from 'primevue/card'
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Button from 'primevue/button'

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

// Comptes pour tes 3 boîtes (comme sur ton croquis)
const totalRefuse = computed(() => candidatures.value.filter(c => c.statut === 'Refusé').length)
const totalAccepte = computed(() => candidatures.value.filter(c => c.statut === 'Accepté').length)
const totalEnCours = computed(() => candidatures.value.filter(c => c.statut === 'En cours').length)

onMounted(() => {
    chargerCandidatures()
})
</script>

<template>
    <div class="dashboard-container">
        <div class="header-zone">
            <h2>Tableau de bord</h2>
            <div class="date-badge">
                <i class="pi pi-calendar"></i>
                <span>{{ new Date().toLocaleDateString() }}</span>
            </div>
        </div>

        <div v-if="chargement" class="status-msg text-info">⏳ Connexion à l'API .NET...</div>
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

            <Card class="table-card">
                <template #title>
                    <div class="table-header">Liste des candidatures</div>
                </template>
                <template #content>
                    <DataTable :value="candidatures" responsiveLayout="scroll" class="p-datatable-sm">
                        <Column field="poste" header="Poste" font-weight="bold"></Column>
                        <Column field="company.name" header="Entreprise">
                            <template #body="slotProps">
                                {{ slotProps.data.company?.name || 'Non spécifiée' }}
                            </template>
                        </Column>
                        <Column field="statut" header="Statut">
                            <template #body="slotProps">
                                <span class="status-badge"
                                    :class="slotProps.data.statut?.toLowerCase().replace(' ', '')">
                                    {{ slotProps.data.statut }}
                                </span>
                            </template>
                        </Column>
                        <Column field="niveauMotivation" header="Motivation">
                            <template #body="slotProps">
                                ⭐ {{ slotProps.data.niveauMotivation }}/5
                            </template>
                        </Column>
                        <Column header="Actions" headerStyle="text-align: right" bodyStyle="text-align: right">
                            <template #body>
                                <Button icon="pi pi-check" severity="success" text rounded class="mr-2" />
                                <Button icon="pi pi-times" severity="danger" text rounded />
                            </template>
                        </Column>
                    </DataTable>
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

.header-zone h2 {
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

/* Couleurs personnalisées pour coller à ton dessin */
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
    font-size: 1.2rem;
    font-weight: 600;
    color: #1e293b;
}

.status-badge {
    padding: 0.25rem 0.5rem;
    border-radius: 4px;
    font-size: 0.85rem;
    font-weight: 600;
}

.status-badge.refusé {
    background-color: #fee2e2;
    color: #991b1b;
}

.status-badge.accepté {
    background-color: #dcfce7;
    color: #166534;
}

.status-badge.encours {
    background-color: #fef3c7;
    color: #92400e;
}

.status-msg {
    padding: 1rem;
    border-radius: 6px;
    background: white;
    border: 1px solid #e2e8f0;
}
</style>