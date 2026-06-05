<script setup>
import { ref, watch } from 'vue'
import Dialog from 'primevue/dialog'
import Button from 'primevue/button'
import Select from 'primevue/select' // 1. IMPORTATION DU COMPOSANT SELECT DE PRIMEVUE

const props = defineProps({
    visible: Boolean,
    candidature: Object,     // La candidature sélectionnée par défaut (si modification/ajout ciblé)
    candidatures: Array      // 2. ON REÇOIT LA LISTE DE TOUTES LES CANDIDATURES DEPUIS LE PARENT
})

const emit = defineEmits(['update:visible', 'save'])

// On ajoute 'candidature' dans notre objet local pour stocker celle choisie dans le Select
const localNote = ref({
    title: '',
    candidature: null,
    notes: ''
})

// On réinitialise ou pré-remplit les champs à chaque ouverture du Dialog
watch(() => props.visible, (isOpen) => {
    if (isOpen) {
        // Si le parent passe une candidature spécifique, on essaie de la caler par défaut
        // Sinon, on met null pour forcer l'utilisateur à choisir dans la liste
        localNote.value = {
            title: props.candidature?.title || '',
            candidature: props.candidature || null,
            notes: props.candidature?.notes || ''
        }
    }
})

const closeDialog = () => {
    emit('update:visible', false)
}

const saveNote = () => {
    // 1. On prépare l'objet à envoyer
    const noteToSave = {
        title: localNote.value.title,
        notes: localNote.value.notes,
        candidatureId: localNote.value.candidature?.id || null,
        candidature: localNote.value.candidature
    }

    // 2. On met le console.log ICI pour voir les données au moment du clic
    console.log("Données envoyées au parent :", noteToSave)

    // 3. On envoie et on ferme
    emit('save', noteToSave)
    closeDialog()
}

</script>

<template>
    <Dialog :visible="visible" @update:visible="val => emit('update:visible', val)" modal :header="'Ajout d\'une note'"
        :style="{ width: '32rem' }">

        <div class="chosen">
            <form @submit.prevent="saveNote" class="form-container">

                <div class="field">
                    <input type="text" v-model="localNote.title" placeholder="Titre de la note"
                        class="p-inputtext p-component" />
                </div>

                <div class="field">
                    <Select v-model="localNote.candidature" :options="candidatures" optionLabel="name"
                        placeholder="Choisir une candidature" class="w-full" />
                </div>

                <div class="field">
                    <textarea v-model="localNote.notes" rows="5" placeholder="Entrez votre note ici..."
                        class="p-inputtextarea p-component" />
                </div>

                <div class="actions">
                    <Button type="button" severity="secondary" label="Annuler" @click="closeDialog" />
                    <Button type="submit" severity="info" label="Enregistrer" @click="saveNote" />
                </div>
            </form>
        </div>

    </Dialog>
</template>

<style scoped>
.form-container {
    display: flex;
    flex-direction: column;
    gap: 15px;
    margin-top: 15px;
}

.field {
    display: flex;
    flex-direction: column;
}

.p-inputtext,
.p-inputtextarea {
    width: 100%;
    padding: 10px;
    border: 1px solid #ccc;
    border-radius: 6px;
    font-family: inherit;
}


:deep(.p-select) {
    width: 100%;
}

.p-inputtextarea {
    resize: vertical;
}

.actions {
    display: flex;
    gap: 10px;
    justify-content: flex-end;
    margin-top: 10px;
}
</style>