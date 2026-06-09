import { defineStore } from 'pinia'
import axios from 'axios'

export const useCandidatureStore = defineStore('candidature', {

    // ======================
    // STATE (remplace tes ref)
    // ======================
    state: () => ({
        candidatures: [],   // ancien: const candidatures = ref([])
        chargement: false,  // ancien: const chargement = ref(true)
        erreur: null        // ancien: const erreur = ref(null)
    }),

    // ======================
    // GETTERS (remplace tes computed)
    // ======================
    getters: {

        totalRefuse: (state) =>
            state.candidatures.filter(c => c.statusLibelle === 'Refusé').length,

        totalAccepte: (state) =>
            state.candidatures.filter(c => c.statusLibelle === 'Accepté').length,

        totalEnCours: (state) =>
            state.candidatures.filter(c =>
                c.statusLibelle === 'Suivi' ||
                c.statusLibelle === 'Entretien' ||
                c.statusLibelle === 'Envoyée'
            ).length
    },

    // ======================
    // ACTIONS (remplace axios + onMounted)
    // ======================
    actions: {

        async chargerCandidatures() {
            this.chargement = true
            this.erreur = null

            try {
                const response = await axios.get('https://localhost:7265/api/Candidatures')

                this.candidatures = response.data
            }
            catch (err) {
                console.error(err)
                this.erreur = "Impossible de joindre l'API .NET."
            }
            finally {
                this.chargement = false
            }
        }

    }
})