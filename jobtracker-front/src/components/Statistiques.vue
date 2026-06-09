<script setup>

import { ref, onMounted, computed } from 'vue'
import axios from 'axios'
import Button from 'primevue/button';


const candidatures = ref([])
const chargement = ref(true)
const erreur = ref(null)


const chargerCandidatures = async () => {
    try {
        chargement.value = true
        const response = await axios.get('https://localhost:7265/api/Candidatures')
        candidatures.value = response.data
        
        // 🔍 ICI : Ouvre la console de ton navigateur (F12) pour regarder la structure !
        if (response.data && response.data.length > 0) {
            console.log("Structure d'une candidature :", response.data[0])
        }
    } catch (err) {
        console.error(err)
        erreur.value = "Impossible de joindre l'API .NET."
    } finally {
        chargement.value = false
    }
}

// Ensemble des cards statistiques afficher sur le dashboard
const totalEnvoyees = computed(() => candidatures.value.length)

const tauxReponses = computed(() => {
    const total = candidatures.value.length
    if (total === 0) return 0

    const reponses = candidatures.value.filter(c => {
        const statut = c.statusLibelle ? c.statusLibelle.toLowerCase().trim() : ''
        return statut === 'accepté' || statut === 'refusé' || statut === 'entretien'
    })

    return Math.round((reponses.length / total) * 100)
})

const candidatureMois = computed (() => {
    const now = new Date()
    const currentMonth = now.getMonth()
    const currentYear = now.getFullYear()

    return candidatures.value.filter(c => {
        const dateCandidature = new Date(c.dateCandidature) 
        return dateCandidature.getMonth() === currentMonth && dateCandidature.getFullYear() === currentYear
    }).length
}

)


onMounted(() => {
    chargerCandidatures()
})

</script>


<template>
    <h2>📊 Statistiques</h2>
    <!-- TODO: Ajouter l'ancre pour faire atterir le bon graphique selon le bouton cliqué-->

    <div class="cardsStats">
        <div class="cardStat">
            <h3>Total de candidatures envoyées</h3>
            <p>{{totalEnvoyees}}</p>
            <router-link to="/candidatures">
                <Button label="En savoir plus" severity="info" variant="text" raised />
            </router-link>
        </div>

        <div class="cardStat">
            <h3>Taux de réponses</h3>
            <!-- <small> Envoyées, Refusées et Entretiens</small> -->
            <p>{{ tauxReponses }} % </p>
            <router-link to="/candidatures">
                <Button label="En savoir plus" severity="info" variant="text" raised />
            </router-link>
        </div>

        <div class="cardStat">
            <h3>Candidatures du mois</h3>
            <p>{{candidatureMois}}</p>
            <router-link to="/candidatures">
                <Button label="En savoir plus" severity="info" variant="text" raised />
            </router-link>
        </div>
    </div>
</template>

    <!-- TODO: Récupérer les données réelles depuis l'API .NET et les afficher dans les cartes statistiques.
    FIXME: Implémenter la récupération des données depuis l'API
    NOTE: Les valeurs affichées sont temporaires
    BUG: Activer les logs pour le débogage
    HACK: Ajouter des graphiques pour une meilleure visualisation des statistiques
    OPTIMIZE: Organiser le code pour une meilleure maintenabilité -->

<style scoped>
.cardsStats {
    display: flex;
    gap: 1rem;
    margin-top: 2rem;
}

.cardStat {
    flex: 1;
    background-color: #f8fafc;
    border: 1px solid #dbeafe;
    border-radius: 10px;
    padding: 1.5rem;
    text-align: center;
    box-shadow: 0 2px 6px rgba(0, 0, 0, 0.05);
}

.cardStat h3 {
    font-size: 1rem;
    color: #1e293b;
    margin-bottom: 1rem;
}

.cardStat p {
    font-size: 2rem;
    font-weight: bold;
    color: #2563eb;
}
</style>