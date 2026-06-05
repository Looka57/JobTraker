<script setup>
import { computed, ref } from 'vue'
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Button from 'primevue/button'
import Select from 'primevue/select' // 1. On importe le composant Select
import DialogCandidatureEdit from './DialogCandidatureEdit.vue'
import DialogCandidatureDelete from './DialogCandidatureDelete.vue'

// props
const props = defineProps({
    donnees: Array,
    limite: Number
})

// La liste de tes statuts disponibles pour le Select
const listeStatuts = ref([
    { label: 'Brouillon' },
    { label: 'Envoyée' },
    { label: 'Suivi' },
    { label: 'Entretien' },
    { label: 'Accepté' },
    { label: 'Refusé' }
])

// Badge couleur basé sur le libellé du statut
const getStatusClass = (status) => {
    switch (status) {
        case 'Brouillon': return 'status-brouillon';
        case 'Envoyée': return 'status-envoyee';
        case 'Suivi': return 'status-suivi';
        case 'Entretien': return 'status-entretien';
        case 'Accepté': return 'status-accepte';
        case 'Refusé': return 'status-refuse';
        default: return 'status-default';
    }
}

// Fonction appelée quand l'utilisateur change le statut dans le tableau
const updateStatus = (newValue, candidature) => {
    console.log(`Changement de statut pour la candidature ${candidature.id || ''} :`, newValue)
    // C'est ici que tu appelleras ton API axios pour sauvegarder le changement en base de données :
    // axios.put(`https://localhost:7265/api/Candidatures/${candidature.id}`, { ...candidature, statusLibelle: newValue })
}


// STATE DIALOG
const visible = ref(false)
const selectedCandidature = ref(null)
const visibleDelete = ref(false)

// OPEN DIALOG
function editDialog(candidature) {
    selectedCandidature.value = { ...candidature }
    visible.value = true
}

// DELETE
function deleteCandidature(candidature) {
    selectedCandidature.value = { ...candidature }
    visibleDelete.value = true

    console.log('DELETE', candidature)
}

// FILTER
const candidaturesFiltrees = computed(() => {
    if (props.limite && props.donnees.length > props.limite) {
        return props.donnees.slice(-props.limite).reverse()
    }
    return [...props.donnees].reverse()
})
</script>

<template>
    <DataTable :value="candidaturesFiltrees">
        <Column field="poste" header="Poste" />
        <Column header="Entreprise">
            <template #body="slotProps">
                {{ slotProps.data.name || 'Inconnue' }}
            </template>
        </Column>

        <Column header="Status">
            <template #body="slotProps">
                <Select 
                    v-model="slotProps.data.statusLibelle" 
                    :options="listeStatuts" 
                    optionLabel="label" 
                    optionValue="label"
                    @change="(e) => updateStatus(e.value, slotProps.data)"
                    class="status-dropdown"
                >
                    <template #value="valProps">
                        <span v-if="valProps.value" class="custom-badge" :class="getStatusClass(valProps.value)">
                            {{ valProps.value }}
                        </span>
                    </template>

                    <template #option="optProps">
                        <span class="custom-badge" :class="getStatusClass(optProps.option.label)">
                            {{ optProps.option.label }}
                        </span>
                    </template>
                </Select>
            </template>
        </Column>

        <Column header="Motivation">
            <template #body="slotProps">
                ⭐ {{ slotProps.data.niveauMotivation }}/5
            </template>
        </Column>
        <Column header="Url de l'offre">
            <template #body="slotProps">
                <a :href="slotProps.data.urlOffre" target="_blank" rel="noopener noreferrer"> Voir l'offre </a>
            </template>
        </Column>
        <Column header="Actions">
            <template #body="slotProps">
                <Button icon="pi pi-file-edit" severity="success" text rounded @click="editDialog(slotProps.data)" />
                <Button icon="pi pi-times" severity="danger" text rounded @click="deleteCandidature(slotProps.data)" />
            </template>
        </Column>
    </DataTable>
    
    <DialogCandidatureEdit v-model:visible="visible" :candidature="selectedCandidature" />
    <DialogCandidatureDelete v-model:visible="visibleDelete" :candidature="selectedCandidature" />
</template>

<style scoped>
a {
    color: #3b82f6;
    text-decoration: none;
}

/* On nettoie le Select de PrimeVue pour enlever son fond blanc et ses bordures par défaut */
:deep(.p-select) {
    background: transparent;
    border: none;
    box-shadow: none;
}

:deep(.p-select-label) {
    padding: 0;
    display: flex;
    align-items: center;
}

/* Style de base commun pour émuler l'ancien composant Badge */
.custom-badge {
    padding: 4px 12px;
    border-radius: 20px;
    font-weight: 600;
    font-size: 0.85rem;
    display: inline-block;
    text-align: center;
}

/* Tes classes de couleurs CSS inchangées */
.status-brouillon {
    background: #e5e7eb !important;
    color: #374151 !important;
}

.status-envoyee {
    background: #dbeafe !important;
    color: #1e40af !important;
}

.status-suivi {
    background: #fef3c7 !important;
    color: #92400e !important;
}

.status-entretien {
    background: #ddd6fe !important;
    color: #5b21b6 !important;
}

.status-accepte {
    background: #dcfce7 !important;
    color: #166534 !important;
}

.status-refuse {
    background: #fee2e2 !important;
    color: #991b1b !important;
}
</style>