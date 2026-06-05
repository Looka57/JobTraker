<script setup>
import Dialog from 'primevue/dialog'
import InputText from 'primevue/inputtext'
import InputNumber from 'primevue/inputnumber'
import Button from 'primevue/button'
import { ref, watch } from 'vue'

const props = defineProps({
    visible: Boolean,
    candidature: Object
})

const emit = defineEmits(['update:visible'])
const localCandidature = ref({})

watch(() => props.candidature, (val) => {
    console.log("Données reçuSs :", val);
    if (val) localCandidature.value = { ...val }
}, { immediate: true })

function close() { emit('update:visible', false) }
function save() {
    console.log('SAVE', localCandidature.value)
    emit('update:visible', false)
}
</script>

<template>
    <Dialog :visible="visible" @update:visible="val => emit('update:visible', val)" modal header="Modifier candidature"
        :style="{ width: '32rem' }">
        <div class="form-container">
            <div class="field-row">
                <label for="poste">Poste</label>
                <InputText id="poste" v-model="localCandidature.poste" class="input-field" />
            </div>

            <div class="field-row">
                <label for="typeContrat">Type de contrat</label>
                <InputText id="typeContrat" v-model="localCandidature.typeContrat" class="input-field" />
            </div>

            <div class="field-row">
                <label for="typeContrat">Entreprise</label>
                <InputText id="typeContrat" v-model="localCandidature.name" class="input-field" />
            </div>

            <div class="field-row">
                <label for="salaire">Salaire (€)</label>
                <InputNumber id="salaire" v-model="localCandidature.salaire" class="input-field" />
            </div>

            <div class="field-row">
                <label for="motivation">Motivation /5</label>
                <InputNumber id="motivation" v-model="localCandidature.niveauMotivation" :min="0" :max="5"
                    class="input-field" />
            </div>
            <div class="field-row">
                <label for="urlOffre">Url de l'offre</label>
                <InputText id="urlOffre" v-model="localCandidature.urlOffre" class="input-field" />
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