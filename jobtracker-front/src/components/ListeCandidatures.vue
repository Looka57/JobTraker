<script setup>
import { computed, ref } from 'vue'
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Button from 'primevue/button'
import DialogActionTable from './DialogActionTable.vue'
import Badge from 'primevue/badge'

// props
const props = defineProps({
    donnees: Array,
    limite: Number
})

// //Badge couleur
const getStatusClass = (status) => {
    switch (status) {
        case 'Brouillon':
            return 'status-brouillon';

        case 'Envoyée':
            return 'status-envoyee';

        case 'Suivi':
            return 'status-suivi';

        case 'Entretien':
            return 'status-entretien';

        case 'Accepté':
            return 'status-accepte';

        case 'Refusé':
            return 'status-refuse';

        default:
            return 'status-default';
    }
}

// STATE DIALOG
const visible = ref(false)
const selectedCandidature = ref(null)

// OPEN DIALOG
function editDialog(candidature) {
    selectedCandidature.value = { ...candidature }
    visible.value = true
}

// DELETE
function deleteCandidature(candidature) {
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
        <Badge
            :value="slotProps.data.statusLibelle"
            :class="getStatusClass(slotProps.data.statusLibelle)"
        />
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
        <!-- ACTIONS -->
        <Column header="Actions">
            <template #body="slotProps">
                <Button icon="pi pi-file-edit" severity="success" text rounded @click="editDialog(slotProps.data)" />
                <Button icon="pi pi-times" severity="danger" text rounded @click="deleteCandidature(slotProps.data)" />
            </template>
        </Column>
    </DataTable>
    <DialogActionTable v-model:visible="visible" :candidature="selectedCandidature" />
</template>


<style scoped>
a {
    color: #3b82f6;
    text-decoration: none;
}

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