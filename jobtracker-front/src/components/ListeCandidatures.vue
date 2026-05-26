<script setup>
import { computed } from 'vue'
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Button from 'primevue/button'

// On définit les "props" pour recevoir les données depuis la page parente
const props = defineProps({
    donnees: {
        type: Array,
        required: true
    },
    limite: {
        type: Number,
        default: null // Si pas de limite, on affiche tout
    }
})

// On filtre le tableau si une limite est demandée (ex: les 5 dernières)
const candidaturesFiltrees = computed(() => {
    if (props.limite && props.donnees.length > props.limite) {
        // On prend les X derniers éléments
        return props.donnees.slice(-props.limite).reverse()
    }
    return [...props.donnees].reverse() // Par défaut, on affiche du plus récent au plus ancien
})
</script>

<template>
    <DataTable :value="candidaturesFiltrees" responsiveLayout="scroll" class="p-datatable-sm">
        <Column field="poste" header="Poste"></Column>
        <Column field="company.name" header="Entreprise">
            <template #body="slotProps">
                {{ slotProps.data.company?.name || 'Non spécifiée' }}
            </template>
        </Column>
        <Column field="statut" header="Statut">
            <template #body="slotProps">
                <span class="status-badge" :class="slotProps.data.statut?.toLowerCase().replace(' ', '')">
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
                <Button icon="pi pi-check" severity="success" text rounded />
                <Button icon="pi pi-times" severity="danger" text rounded />
            </template>
        </Column>
    </DataTable>
</template>

<style scoped>
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
</style>