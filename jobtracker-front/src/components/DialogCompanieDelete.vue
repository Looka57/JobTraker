<script setup>
import Dialog from 'primevue/dialog'
import Button from 'primevue/button'
const props = defineProps({
    visible: Boolean,
    companie: Object
})

const emit = defineEmits(['update:visible'])

</script>


<template>
    <Dialog :visible="visible" @update:visible="val => emit('update:visible', val)" modal
        :header="companie ? `Détails : ${companie.name}` : 'Suppression de l\'entreprise'"
        :style="{ width: '32rem' }">

        <div v-if="companie">

            <div class="chosen">
                <p>Êtes-vous sûr de vouloir supprimer cette entreprise ?</p>
                <Button class="" type="submit" severity="danger" label="Supprimer" />
            </div>
        </div>

    </Dialog>
</template>
