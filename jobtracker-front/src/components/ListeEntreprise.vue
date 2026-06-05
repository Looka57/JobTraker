<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Button from 'primevue/button'
import DialogCompanieEdit from './DialogCompanieEdit.vue'
import DialogCompanieDelete from './DialogCompanieDelete.vue'

const entreprises = ref([])
const visible = ref(false)
const visibleDelete = ref(false)
const selectedCompanie = ref(null)


// openDialog
const editDialog = (companie) =>{
    selectedCompanie.value = {...companie}
    visible.value = true
}

// modifier la compagnie dans le tableau après la modification
const updateEntreprise = (entrepriseModifiee) => {
    // 1. Trouver l'entreprise dans le tableau grâce à son ID
    const index = entreprises.value.findIndex(e => e.id === entrepriseModifiee.id)
    
    if (index !== -1) {
        // 2. Mettre à jour le tableau localement
        entreprises.value[index] = entrepriseModifiee
    }
    console.log('Entreprise mise à jour dans le tableau :', entrepriseModifiee)
}

// DeletedDialog
const deletedCompanie = (companie) => {
    selectedCompanie.value = { ...companie }
    visibleDelete.value = true
}


onMounted(async () => {
    // On appelle la nouvelle route qui renvoie une liste propre
    const response = await axios.get('https://localhost:7265/api/Companies')
    entreprises.value = response.data
    console.log(response.data)
})
</script>

<template>
    <DataTable :value="entreprises" stripedRows>
        <Column header="Nom de l'entreprise" >
            <template #body="slotProps">
                {{ slotProps.data.name }}
            </template>
        </Column>
        <Column header="Lieu de l'entreprise" >
            <template #body="slotProps">
                {{ slotProps.data.lieu }}
            </template>
        </Column>
        <Column header="Site de l'entreprise" >
            <template #body="slotProps">
                {{ slotProps.data.site }}
            </template>
        </Column>
        <Column header="Actions" >
            <template #body ="slotProps">
                <Button label="Modifier" icon="pi pi-file-edit" severity="success" text rounded @click="editDialog(slotProps.data)" />  
                <Button label="Supprimer" icon="pi pi-times" severity="danger" text rounded @click="deletedCompanie(slotProps.data)" />
            </template>
        </Column>
    </DataTable>
<DialogCompanieEdit 
    v-model:visible="visible" 
    :companie="selectedCompanie" 
    @save="updateEntreprise" 
/>

<DialogCompanieDelete 
    v-model:visible="visibleDelete" 
    :companie="selectedCompanie" 
    @delete="deletedCompanie"
    />

</template>