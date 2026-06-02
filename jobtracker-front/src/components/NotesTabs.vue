<script setup>
import { ref, computed, onMounted } from 'vue'
import axios from 'axios'
import Tabs from 'primevue/tabs'
import TabList from 'primevue/tablist'
import Tab from 'primevue/tab'

// Import de tes futurs composants (ajuste les chemins si nécessaire)
import DialogNotesEyes from './DialogNotesEyes.vue'
import DialogNotesEdit from './DialogNotesEdit.vue'     
import DialogNotesDelete from './DialogNotesDelete.vue' 

const activeTab = ref('Toutes')
const notes = ref([])
const chargement = ref(true)
const erreur = ref(null)
const entreprise = ref([])

// --- Gestion des Dialogs ---
// Un état "visible" distinct pour chaque boîte de dialogue
const isViewVisible = ref(false)
const isEditVisible = ref(false)
const isDeleteVisible = ref(false)

// Une seule variable suffit pour savoir sur quelle note l'utilisateur a cliqué
const selectedNote = ref(null)

const openViewDialog = (note) => {
    selectedNote.value = note
    isViewVisible.value = true
}

const openEditDialog = (note) => {
    selectedNote.value = note
    isEditVisible.value = true
}

const openDeleteDialog = (note) => {
    selectedNote.value = note
    isDeleteVisible.value = true
}
// ----------------------------

const tabs = ref([
    { label: 'Toutes', value: 'Toutes' },
    { label: 'Candidatures', value: 'ContactInitial' },
    { label: 'Relancement', value: 'Relancement' },
    { label: 'Appel', value: 'AppelRh' },
    { label: 'Entretiens', value: 'Entretiens' },
    { label: 'Technique', value: 'EntretienTechnique' },
    { label: 'Final', value: 'EntretienFinal' },
    { label: 'Valider', value: 'OffreRecu' },
    { label: 'Refus', value: 'Refus' }
])

const typeColor = {
    ContactInitial: 'blue', Relancement: 'orange', AppelRh: 'purple', Entretiens: 'yellow',
    EntretienTechnique: 'indigo', EntretienFinal: 'teal', OffreRecu: 'green', Refus: 'red'
}

const typeLabels = {
    ContactInitial: 'Candidature', Relancement: 'Relance', AppelRh: 'Appel RH', Entretiens: 'Entretien',
    EntretienTechnique: 'Entretien Tech', EntretienFinal: 'Entretien Final', OffreRecu: 'Offre Reçue', Refus: 'Refusé'
}

const typeMap = {
    0: 'ContactInitial', 1: 'Relancement', 2: 'AppelRh', 3: 'Entretiens',
    4: 'EntretienTechnique', 5: 'EntretienFinal', 6: 'OffreRecu', 7: 'Refus'
}

const chargerEntreprises = async () => {
    try {
        const response = await axios.get('https://localhost:7265/api/Companies')
        entreprise.value = response.data
    } catch (err) {
        console.error("Erreur chargement entreprises", err)
        entreprise.value = [{ id: 1, name: 'TechCorp' }, { id: 2, name: 'WebAgency' }]
    }
}

const getCardClass = (type) => `card-${typeColor[type] || 'default'}`

const chargerNotes = async () => {
    try {
        chargement.value = true
        const response = await axios.get('https://localhost:7265/api/Interactions')
        notes.value = response.data.map(n => ({ ...n, type: typeMap[n.type] }))
    } catch (err) {
        console.error(err)
        erreur.value = "Impossible de joindre l'API .NET."
        notes.value = [
            { id: 1, notes: 'Développeur - Culture d\'entreprise axée sur l\'innovation.', type: 'ContactInitial', companyId: 1 },
            { id: 2, notes: 'Culture de compagnie intéressante. Relance prévue d\'ici vendredi.', type: 'Relancement', companyId: 2 },
            { id: 3, notes: 'Prep. Entretien Tech. Questions potentielles sur Vue 3.', type: 'EntretienTechnique', companyId: 1 },
            { id: 4, notes: 'Offre reçue par mail ! 45k€ + avantages.', type: 'OffreRecu', companyId: 2 }
        ]
    } finally {
        chargement.value = false
    }
}

onMounted(async () => {
    await Promise.all([chargerNotes(), chargerEntreprises()])
})

const filteredNotes = computed(() => {
    const notesAvecEntreprises = notes.value.map(note => {
        const ent = entreprise.value.find(e => e.id === note.companyId || e.id === note.entrepriseId)
        return { ...note, companyName: ent ? ent.name : 'Entreprise inconnue' }
    })
    if (activeTab.value === 'Toutes') return notesAvecEntreprises
    return notesAvecEntreprises.filter(n => n.type === activeTab.value)
})
</script>

<template>
    <div class="notes-container">
        <!-- Barre d'onglets -->
        <Tabs v-model:value="activeTab" class="custom-tabs">
            <TabList>
                <Tab v-for="tab in tabs" :key="tab.value" :value="tab.value">{{ tab.label }}</Tab>
            </TabList>
        </Tabs>

        <!-- Grille des cartes -->
        <div class="cards">
            <div v-for="note in filteredNotes" :key="note.id" class="card-note" :class="getCardClass(note.type)">
                <div class="titreNote">
                    <h2>{{ typeLabels[note.type] || note.type }} </h2>
                    <div class="action icons">
                        <!-- Les icônes appellent chacune leur fonction dédiée -->
                        <i class="pi pi-eye" @click="openViewDialog(note)"></i>
                        <i class="pi pi-file-edit" @click="openEditDialog(note)"></i>
                        <i class="pi pi-times" @click="openDeleteDialog(note)"></i>
                    </div>
                </div>
                <h4>{{ note.companyName }}</h4>
                <p v-if="note.notes" class="note-content">{{ note.notes }}</p>

                <div class="tag-wrapper">
                    <span class="tag">{{ typeLabels[note.type] || note.type }}</span>
                </div>
            </div>
        </div>

        <!-- 
          DIALOGS UNIQUES ET SORTIS DE LA BOUCLE V-FOR 
          Ils écoutent tous le même objet "selectedNote" mais s'ouvrent indépendamment.
        -->
        <!-- 1. Fenêtre Voir -->
        <DialogNotesEyes v-model:visible="isViewVisible" :candidature="selectedNote" />

        <!-- 2. Fenêtre Modifier (À créer, reçoit l'action refresh pour recharger la liste) -->
        <DialogNotesEdit v-model:visible="isEditVisible" :candidature="selectedNote" @refresh="chargerNotes" />

        <!-- 3. Fenêtre Supprimer (À créer, reçoit aussi l'action refresh) -->
        <DialogNotesDelete v-model:visible="isDeleteVisible" :candidature="selectedNote" @refresh="chargerNotes" />
    </div>
</template>

<style scoped>
/* Ton CSS reste inchangé... */
.notes-container {
    padding: 20px;
    background-color: #f8fafc;
    min-height: 100vh;
}

.titreNote {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 10px;
}

.titreNote .icons {
    display: flex;
    gap: 10px;
    opacity: 0.8;
    cursor: pointer;
}

.cards {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
    gap: 20px;
    margin-top: 25px;
}

.card-note {
    border-radius: 14px;
    padding: 20px;
    box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.05), 0 2px 4px -1px rgba(0, 0, 0, 0.03);
    transition: all 0.2s ease-in-out;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    border: 1px solid rgba(0, 0, 0, 0.03);
    min-height: 160px;
}

.card-note:hover {
    transform: translateY(-4px);
    box-shadow: 0 10px 15px -3px rgba(0, 0, 0, 0.08);
}

.note-content {
    color: #334155;
    font-size: 14px;
    line-height: 1.6;
    margin: 0 0 15px 0;
}

.tag-wrapper {
    margin-top: auto;
}

.tag {
    display: inline-block;
    font-size: 11px;
    font-weight: 600;
    padding: 4px 12px;
    border-radius: 8px;
    text-transform: uppercase;
    letter-spacing: 0.5px;
}

.card-blue {
    background-color: #eff6ff;
}

.card-blue .tag {
    background-color: #dbeafe;
    color: #1e40af;
}

.card-orange {
    background-color: #fffbeb;
}

.card-orange .tag {
    background-color: #fef3c7;
    color: #b45309;
}

.card-purple {
    background-color: #faf5ff;
}

.card-purple .tag {
    background-color: #f3e8ff;
    color: #6b21a8;
}

.card-yellow {
    background-color: #fefce8;
}

.card-yellow .tag {
    background-color: #fef9c3;
    color: #854d0e;
}

.card-indigo {
    background-color: #e0e7ff;
}

.card-indigo .tag {
    background-color: #c7d2fe;
    color: #3730a3;
}

.card-teal {
    background-color: #f0fdfa;
}

.card-teal .tag {
    background-color: #ccfbf1;
    color: #115e59;
}

.card-green {
    background-color: #f0fdf4;
}

.card-green .tag {
    background-color: #dcfce7;
    color: #166534;
}

.card-red {
    background-color: #fef2f2;
}

.card-red .tag {
    background-color: #fee2e2;
    color: #991b1b;
}

.card-default {
    background-color: #f8fafc;
}

.card-default .tag {
    background-color: #e2e8f0;
    color: #475569;
}
</style>