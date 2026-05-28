<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Button from 'primevue/button'


const entreprises = ref([])

onMounted(async () => {
    // On appelle la nouvelle route qui renvoie une liste propre
    const response = await axios.get('https://localhost:7265/api/Companies')
    entreprises.value = response.data
    console.log(response.data)
})
</script>

<template>
    <!-- BUG: gerer le "sortable" pour le tri des colonnes, actuellement ca ne fonctionne pas car les données ne sont pas dans le bon format pour le tri -->
    <DataTable :value="entreprises" stripedRows>
        <Column header="Nom de l'entreprise" sortable>
            <template #body="slotProps">
                {{ slotProps.data.name }}
            </template>
        </Column>
        <Column header="Lieu de l'entreprise" sortable>
            <template #body="slotProps">
                {{ slotProps.data.lieu }}
            </template>
        </Column>
        <Column header="Site de l'entreprise" sortable>
            <template #body="slotProps">
                {{ slotProps.data.site }}
            </template>
        </Column>
        <Column header="Actions" >
            <template #body>
                <Button icon="pi pi-file-edit" severity="success" text rounded />
                <Button icon="pi pi-times" severity="danger" text rounded />
            </template>
        </Column>
    </DataTable>

</template>