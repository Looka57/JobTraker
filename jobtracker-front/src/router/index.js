import CandidaturesView from '../views/CandidaturesView.vue'
import { createRouter, createWebHistory } from "vue-router";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "candidatures",
      component:  CandidaturesView,
    },
  ],
});

export default router;
