<script setup>
import { ref } from 'vue'; // 1. Il faut importer ref

// 2. Il faut déclarer la variable ici pour qu'elle soit connue
const candidature = ref({
    poste: '',
    contrat: '',
    salaire: '',
    entreprise: '',
    motivation: '',
    urlAnnonce: ''
});

// TODO: Implémenter la logique pour envoyer les données de candidature à l'API .NET et recuperer les propriéte des tables pour les inputs des formulaires
const soumettreCandidature = () => {
    // Maintenant, Vue sait ce qu'est "candidature"
    console.log("Données récupérées :", candidature.value);
    alert("Candidature envoyée ! Vérifiez votre console.");
}
</script>

<template>
    <div class="page-wrapper">
        <h1 class="title">Ajouter une candidature</h1>

        <div class="main-layout">
            <!-- Zone gauche : Citation avec image de fond -->
            <div class="containerForm">
                <form @submit.prevent="soumettreCandidature">
                    <div class="form-group"
                        v-for="field in ['Poste', 'Contrat', 'Salaire', 'Entreprise', 'Motivation', 'URL Annonce']"
                        :key="field">
                        <label :for="field.toLowerCase()">{{ field }}</label>
                        <input type="text" :id="field.toLowerCase()" :name="field.toLowerCase()"
                            :placeholder="'Ex: ' + field" v-model="candidature[field.toLowerCase().replace(' ', '')]" />
                    </div>
                    <button type="submit" class="btn-submit">Envoyer</button>
                </form>
            </div>

            <!-- Zone droite : Formulaire -->
            <div class="containerIllustration">
                <div class="citation-overlay">
                    <h2>💡 Inspiration</h2>
                    <p>"Le succès est la somme de petits efforts répétitivement répétés."</p>
                    <small>- Robert Collier</small>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped>
.main-layout {
    display: flex;
    gap: 2rem;
    height: 100%;

}

/* Zone Illustration */
.containerIllustration {
    flex: 1;
    background: linear-gradient(rgba(255, 255, 255, 0.8), rgba(255, 255, 255, 0.8)),
        url('https://images.unsplash.com/photo-1522202176988-66273c2fd55f?q=80&w=1000');
    background-size: cover;
    border-radius: 15px;
    display: flex;
    align-items: center;
    justify-content: center;
    padding: 2rem;
    text-align: center;
}

/* Zone Formulaire */
.containerForm {
    flex: 1;
    padding: 2rem;
    display: flex;
    flex-direction: column;
}

.form-group {
    margin-bottom: 1.2rem;
}

label {
    display: block;
    margin-bottom: 0.5rem;
    font-weight: 500;
    color: #334155;
}

input {
    width: 100%;
    padding: 0.8rem;
    border: 1px solid #cbd5e1;
    border-radius: 8px;
}

.btn-submit {
    margin-top: 1rem;
    padding: 0.8rem 2rem;
    background-color: #334155;
    color: white;
    border: none;
    border-radius: 8px;
    cursor: pointer;
}
</style>