<script setup>
import Dialog from 'primevue/dialog'
import Button from 'primevue/button'


const props = defineProps({
    visible: Boolean,
    candidature: Object
})

const emit = defineEmits(['update:visible'])

</script>


<template>
    <Dialog :visible="visible" @update:visible="val => emit('update:visible', val)" modal
        :header="candidature ? `Détails : ${candidature.poste}` : 'Suppression de la candidature'"
        :style="{ width: '32rem' }">

        <div v-if="candidature">

            <div class="chosen">
                <p>Êtes-vous sûr de vouloir supprimer cette candidature ?</p>
                <Button class="" type="submit" severity="danger" label="Supprimer" />
            </div>
        </div>

    </Dialog>
</template>
