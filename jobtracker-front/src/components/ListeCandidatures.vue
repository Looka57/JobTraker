<script setup>
import { computed, ref } from 'vue'
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Button from 'primevue/button'
import DialogActionTable from './DialogActionTable.vue'

// props
const props = defineProps({
    donnees: Array,
    limite: Number
})

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
                {{ slotProps.data.statusLibelle }}
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

</style>