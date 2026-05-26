<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'

// 1. On crée une variable réactive pour stocker nos candidatures
const candidatures = ref([])
const chargement = ref(true)
const erreur = ref(null)

// 2. Fonction qui va appeler ton API .NET
const chargerCandidatures = async () => {
    try {
        chargement.value = true
        // ⚠️ Remplace le port 7265 par le vrai port de TON API .NET (regarde ton URL Swagger)
        const response = await axios.get('https://localhost:7265/api/Candidatures')

        candidatures.value = response.data
    } catch (err) {
        console.error("Erreur lors du fetch :", err)
        erreur.value = "Impossible de charger les candidatures. Vérifie que le Back-End tourne et que le CORS est activé !"
    } finally {
        chargement.value = false
    }
}

// 3. On dit à Vue d'exécuter la fonction dès que la page s'affiche à l'écran
onMounted(() => {
    chargerCandidatures()
})
</script>

<template>
    <div class="container">
        <h1>📋 Suivi de mes Candidatures</h1>

        <div v-if="chargement" class="info-box loading">
            ⏳ Récupération des données depuis l'API C#...
        </div>

        <div v-else-if="erreur" class="info-box error">
            ❌ {{ erreur }}
        </div>

        <div v-else class="table-container">
            <table v-if="candidatures.length > 0">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Poste</th>
                        <th>Entreprise</th>
                        <th>Type de Contrat</th>
                        <th>Motivation</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="candidature in candidatures" :key="candidature.id">
                        <td>{{ candidature.id }}</td>
                        <td class="bold">{{ candidature.poste }}</td>
                        <td>{{ candidature.company?.name || 'Non spécifiée' }}</td>
                        <td><span class="badge">{{ candidature.typeContrat }}</span></td>
                        <td>⭐ {{ candidature.niveauMotivation }}/5</td>
                    </tr>
                </tbody>
            </table>

            <p v-else class="empty-message">Aucune candidature trouvée. Ajoutes-en une depuis Swagger pour tester !</p>
        </div>
    </div>
</template>

<style scoped>
.container {
    max-width: 1000px;
    margin: 0 auto;
}

h1 {
    color: #2c3e50;
    margin-bottom: 20px;
}

.info-box {
    padding: 15px;
    border-radius: 6px;
    margin-bottom: 20px;
}

.loading {
    background-color: #e2e8f0;
    color: #4a5568;
}

.error {
    background-color: #fed7d7;
    color: #9b2c2c;
    border: 1px solid #feb2b2;
}

.table-container {
    background: white;
    border-radius: 8px;
    box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.1);
    overflow: hidden;
}

table {
    width: 100%;
    border-collapse: collapse;
    text-align: left;
}

th,
td {
    padding: 12px 15px;
    border-bottom: 1px solid #e2e8f0;
}

th {
    background-color: #edf2f7;
    color: #4a5568;
    font-weight: bold;
}

tr:hover {
    background-color: #f7fafc;
}

.bold {
    font-weight: bold;
    color: #2d3748;
}

.badge {
    background-color: #ebf8ff;
    color: #2b6cb0;
    padding: 4px 8px;
    border-radius: 4px;
    font-size: 0.85em;
    font-weight: bold;
}

.empty-message {
    padding: 20px;
    text-align: center;
    color: #718096;
}
</style>