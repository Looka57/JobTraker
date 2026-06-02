<script setup>
import Dialog from 'primevue/dialog'
import Button from 'primevue/button'
import Textarea from 'primevue/textarea';


const props = defineProps({
    visible: Boolean,
    candidature: Object // Reçoit la note sélectionnée
})

const emit = defineEmits(['update:visible'])
</script>

<template>
    <Dialog :visible="visible" @update:visible="val => emit('update:visible', val)" modal
        :header="candidature ? `Détails : ${candidature.companyName}` : 'Modification de la note'"
        :style="{ width: '32rem' }">

        <div v-if="candidature">
            <div class="textarea-container">
                <Textarea v-model="candidature.notes" rows="5" cols="53" />
            </div>

            <Button class="save-button" type="submit" severity="secondary" label="Enregistrer" />
        </div>

    </Dialog>
</template>

<!-- NOTE: a mettre en place la modification -->
 
<style scoped>
.textarea-container {
    display: flex;
    justify-content: center;
}

.save-button {
    margin-top: 1rem;
    width: 100%;
}
</style>