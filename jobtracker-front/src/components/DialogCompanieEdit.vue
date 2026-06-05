<script setup>
import { ref, watch } from 'vue'
import Dialog from 'primevue/dialog'
import InputText from 'primevue/inputtext'
import Button from 'primevue/button'

const props = defineProps({ //Donnée entrante du composant
    visible: Boolean,
    companie: Object
})

const emit = defineEmits(['update:visible', 'save']) //Donnée sortante du composant
const localCompanie = ref({}) //Donnée locale du composant

watch(() => props.companie, (val) => {
    console.log("Données reçues :", val);
    if (val) {
        localCompanie.value = { ...val } // Copie locale pour éviter de modifier le parent en direct
    } else {
        localCompanie.value = {}
    }
}, { immediate: true })

function close() { 
    emit('update:visible', false) 
}

function save() {
    console.log('SAVE', localCompanie.value)
    emit('update:visible', false)
}
</script>

<template>
    <Dialog :visible="visible" @update:visible="val => emit('update:visible', val)" modal header="Modifier l'entreprise"
        :style="{ width: '32rem' }">
        <div class="form-container">
            <div class="field-row">
                <label for="nomEntreprise">Nom de l'entreprise</label>
                <InputText id="nomEntreprise" v-model="localCompanie.name" class="input-field" />
            </div>

            <div class="field-row">
                <label for="lieu">Lieu de l'entreprise</label>
                <InputText id="lieu" v-model="localCompanie.lieu" class="input-field" />
            </div>

            <div class="field-row">
                <label for="site">Site de l'entreprise</label>
                <InputText id="site" v-model="localCompanie.site" class="input-field" /> 
            </div>
        </div>

        <template #footer>
            <Button label="Annuler" text severity="secondary" @click="close" />
            <Button label="Enregistrer" severity="success" icon="pi pi-check" @click="save" />

            <!-- FIXME: L'enregistrement dans la BDD ne se fait pas  -->
            <!-- FIXME: La suppression dans la BDD ne se fait pas  -->
        </template>
    </Dialog>

</template>

<style scoped>
.form-container {
    display: flex;
    flex-direction: column;
    gap: 1.2rem;
    padding: 0.5rem 0;
}

.field-row {
    display: flex;
    align-items: center;
    gap: 1rem;
}

.field-row label {
    width: 130px;
    min-width: 130px;
    font-weight: 600;
    font-size: 0.875rem;
}

.input-field {
    flex: 1;
    width: 100%;
}
</style>