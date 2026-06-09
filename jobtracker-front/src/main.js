import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import { createPinia } from 'pinia'

// 1. Importation de PrimeVue et de son thème
import PrimeVue from 'primevue/config'
import Aura from '@primevue/themes/aura'
import 'primeicons/primeicons.css' // Les icônes officielles

const app = createApp(App)
const pinia = createPinia()

app.use(router)

// 2. On configure PrimeVue pour utiliser le thème Aura
app.use(PrimeVue, {
    theme: {
        preset: Aura,
        options: {
            darkModeSelector: '.my-app-dark-selector' // Évite le mode sombre forcé au démarrage
        }
    }
})

app.use(pinia)
app.mount('#app')