<script setup>
import Dialog from 'primevue/dialog'

const props = defineProps({
    visible: Boolean,
    candidature: Object // Reçoit la note sélectionnée
})

const emit = defineEmits(['update:visible'])
</script>

<template>
    <Dialog :visible="visible" @update:visible="val => emit('update:visible', val)" modal
        :header="candidature ? `Détails : ${candidature.companyName}` : 'Détails de la note'"
        :style="{ width: '32rem' }" >

        <div v-if="candidature">
            <h5 class="font-bold mb-2 text-lg text-slate-700">Note enregistrée :</h5>
            <p
                class="text-sm text-gray-700 bg-slate-50 p-3 rounded-lg border border-slate-200 whitespace-pre-line mb-4">
                {{ candidature.notes || "Aucun commentaire rédigé pour le moment." }}
            </p>
        </div>
        <p class="text-xs text-gray-400 italic">
            Ici vous pouvez consulter les réflexions personnelles et les informations importantes liées à cette étape du
            recrutement.
        </p>
    </Dialog>
</template>