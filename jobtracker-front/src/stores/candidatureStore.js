import { defineStore } from 'pinia'
import { candidatureService } from '@/Services/candidatureService' 

// Déclaration du store "candidature"
export const useCandidatureStore = defineStore('candidature', {
    
    // --------------------
    // STATE (données globales)
    // --------------------
    state: () => ({
        candidatures: [],  // Liste des candidatures récupérées depuis l'API
        chargement: false,  // Indique si un chargement est en cours (loader UI)
        erreur: null  // Stocke un message d'erreur si l'API échoue
    }),

    // --------------------
    // GETTERS (données calculées)
    // --------------------
    getters: {
        // Nombre de candidatures refusées
        totalRefuse: (state) =>
            state.candidatures.filter(c => c.statusLibelle === 'Refusé').length,
        // Nombre de candidatures acceptées
        totalAccepte: (state) =>
            state.candidatures.filter(c => c.statusLibelle === 'Accepté').length,
        // Nombre de candidatures en cours de traitement
        totalEnCours: (state) =>
            state.candidatures.filter(c =>
                c.statusLibelle === 'Suivi' ||
                c.statusLibelle === 'Entretien' ||
                c.statusLibelle === 'Envoyée'
            ).length
    },

    // --------------------
    // ACTIONS (logique métier / appels API)
    // --------------------
    actions: {

        // Récupère toutes les candidatures depuis l'API
        async chargerCandidatures() {
            // Optimisation : évite de recharger si déjà en mémoire
            if (this.candidatures.length > 0) return 
            // Activation du loader
            this.chargement = true
            // Reset de l'erreur avant nouvel appel
            this.erreur = null
            try {
                // Appel au service API (ASP.NET backend)
                this.candidatures = await candidatureService.getAll()
            }
            catch (err) {
                // Log pour debug développeur
                console.error(err)

                // Message utilisateur
                this.erreur = "Impossible de joindre l'API .NET."
            }
            finally {
                // Désactivation du loader dans tous les cas
                this.chargement = false
            }
        }
    }
})