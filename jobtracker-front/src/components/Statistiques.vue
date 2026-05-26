<script setup>
</script>


<template>
    <h2>📊 Statistiques</h2>

    <div class="cardsStats">
        <div class="cardStat">
            <h3>Nombre total de candidatures envoyées</h3>
            <p>42</p>
        </div>

        <div class="cardStat">
            <h3>Taux de réponses</h3>
            <p>15</p>
        </div>

        <div class="cardStat">
            <h3>Candidatures du mois</h3>
            <p>20</p>
        </div>
    </div>
</template>

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