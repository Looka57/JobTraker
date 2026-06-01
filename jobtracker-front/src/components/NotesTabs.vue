<script setup>
import { ref, computed, onMounted } from 'vue'
import axios from 'axios'
import Tabs from 'primevue/tabs'
import TabList from 'primevue/tablist'
import Tab from 'primevue/tab'


const activeTab = ref('Toutes')
const notes = ref([])
const chargement = ref(true)
const erreur = ref(null)
const entreprise = ref([])


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

// Les classes CSS dynamiques basées sur tes types
const typeColor = {
    ContactInitial: 'blue',
    Relancement: 'orange',
    AppelRh: 'purple',
    Entretiens: 'yellow',
    EntretienTechnique: 'indigo',
    EntretienFinal: 'teal',
    OffreRecu: 'green',
    Refus: 'red'
}

// Pour afficher un joli texte dans le badge plutôt que le nom de la variable
const typeLabels = {
    ContactInitial: 'Candidature',
    Relancement: 'Relance',
    AppelRh: 'Appel RH',
    Entretiens: 'Entretien',
    EntretienTechnique: 'Entretien Tech',
    EntretienFinal: 'Entretien Final',
    OffreRecu: 'Offre Reçue',
    Refus: 'Refusé'
}

const typeMap = {
    0: 'ContactInitial',
    1: 'Relancement',
    2: 'AppelRh',
    3: 'Entretiens',
    4: 'EntretienTechnique',
    5: 'EntretienFinal',
    6: 'OffreRecu',
    7: 'Refus'
}


const chargerEntreprises = async () => {
    try {
        const response = await axios.get('https://localhost:7265/api/Companies')
        entreprise.value = response.data
    } catch (err) {
        console.error("Erreur chargement entreprises", err)
    }
}

const getCardClass = (type) => {
    return `card-${typeColor[type] || 'default'}`
}

const chargerNotes = async () => {
    try {
        chargement.value = true
        // Remplacement temporaire par une simulation si ton API locale n'est pas lancée
        const response = await axios.get('https://localhost:7265/api/Interactions')
        notes.value = response.data.map(n => ({
            ...n,
            type: typeMap[n.type]
        }))
    } catch (err) {
        console.error(err)
        erreur.value = "Impossible de joindre l'API .NET."

        // SCRIPT DE SECOURS (Mock) pour que tu puisses tester le design immédiatement :
        notes.value = [
            { id: 1, notes: 'Développeur - TechCorp - Culture d\'entreprise axée sur l\'innovation. Stack technologique moderne.', type: 'ContactInitial' },
            { id: 2, notes: 'Culture de compagnie intéressante. Relance prévue d\'ici vendredi si pas de retour.', type: 'Relancement' },
            { id: 3, notes: 'Prep. Entretien Tech. Questions potentielles sur Vue 3, l\'architecture API et l\'optimisation.', type: 'EntretienTechnique' },
            { id: 4, notes: 'Offre reçue par mail ! 45k€ + avantages. À analyser avant lundi prochain.', type: 'OffreRecu' }
        ]
    } finally {
        chargement.value = false
    }
}

onMounted(async () => {
    await Promise.all([
        chargerNotes(),
        chargerEntreprises()
    ])
})

const filteredNotes = computed(() => {
    if (activeTab.value === 'Toutes') return notes.value
    return notes.value.filter(n => n.type === activeTab.value)
})
</script>

<template>
    <pre>{{ notes.entrepriseId }}</pre>
    
    <div class="notes-container">
        <!-- Barre d'onglets (PrimeVue Tabs) -->
        <Tabs v-model:value="activeTab" class="custom-tabs">
            <TabList>
                <Tab v-for="tab in tabs" :key="tab.value" :value="tab.value">
                    {{ tab.label }}
                </Tab>
            </TabList>
        </Tabs>


        <!-- Grille des cartes de notes -->
        <div class="cards">
            <div v-for="note in filteredNotes" :key="note.id" class="card-note" :class="getCardClass(note.type)">
                <!-- Contenu de la note -->
                <div class="titreNote">
                    <h2>{{ typeLabels[note.type] || note.type }} </h2>
                    <div class="action icons">
                        <i class="pi pi-eye"></i>
                        <i class="pi pi-file-edit"></i>
                        <i class="pi pi-times"></i>
                    </div>
                </div>
                   <h4>{{ note.companyName || 'Entreprise inconnue' }}</h4>
                <p v-if="note.notes" class="note-content">{{ note.notes }}</p>


                <!-- Badge (Tag) dynamique -->
                <div class="tag-wrapper">
                    <span class="tag">{{ typeLabels[note.type] || note.type }}</span>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped>
.notes-container {
    padding: 20px;
    background-color: #f8fafc;
    /* Fond de page très légèrement grisé comme sur la maquette */
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

/* Grille des cartes */
.cards {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
    gap: 20px;
    margin-top: 25px;
}

/* Style de base d'une carte (Neutre) */
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
    /* Texte gris foncé / charcoal pour un super contraste */
    font-size: 14px;
    line-height: 1.6;
    margin: 0 0 15px 0;
}

.tag-wrapper {
    margin-top: auto;
}

/* Style de base du Badge tag */
.tag {
    display: inline-block;
    font-size: 11px;
    font-weight: 600;
    padding: 4px 12px;
    border-radius: 8px;
    text-transform: uppercase;
    letter-spacing: 0.5px;
}

/* -----------------------------------------------------------
   PALETTE DE COULEURS ISSU DU DESIGN (Fonds clairs + Badges)
-------------------------------------------------------------- */

/* BLEU : Contact Initial / Candidature */
.card-blue {
    background-color: #eff6ff;
}

.card-blue .tag {
    background-color: #dbeafe;
    color: #1e40af;
}

/* ORANGE : Relancement */
.card-orange {
    background-color: #fffbeb;
}

.card-orange .tag {
    background-color: #fef3c7;
    color: #b45309;
}

/* VIOLET : Appel RH */
.card-purple {
    background-color: #faf5ff;
}

.card-purple .tag {
    background-color: #f3e8ff;
    color: #6b21a8;
}

/* JAUNE : Entretiens */
.card-yellow {
    background-color: #fefce8;
}

.card-yellow .tag {
    background-color: #fef9c3;
    color: #854d0e;
}

/* INDIGO : Entretien Technique */
.card-indigo {
    background-color: #e0e7ff;
}

.card-indigo .tag {
    background-color: #c7d2fe;
    color: #3730a3;
}

/* TEAL : Entretien Final */
.card-teal {
    background-color: #f0fdfa;
}

.card-teal .tag {
    background-color: #ccfbf1;
    color: #115e59;
}

/* VERT : Offre Reçue (Valider) */
.card-green {
    background-color: #f0fdf4;
}

.card-green .tag {
    background-color: #dcfce7;
    color: #166534;
}

/* ROUGE : Refus */
.card-red {
    background-color: #fef2f2;
}

.card-red .tag {
    background-color: #fee2e2;
    color: #991b1b;
}

/* DEFAULT */
.card-default {
    background-color: #f8fafc;
}

.card-default .tag {
    background-color: #e2e8f0;
    color: #475569;
}
</style>