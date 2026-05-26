import DashboardView from '@/views/DashboardView.vue';
import CandidaturesView from '../views/CandidaturesView.vue'
import CompaniesView from '../views/CompaniesView.vue'
import { createRouter, createWebHistory } from "vue-router";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "dashboard",
      component:  DashboardView,
    },
    {
      path: "/candidatures",
      name: "candidatures",
      component:  CandidaturesView,
    },
    {
      path: "/entreprises",
      name: "entreprises",
      component:  CompaniesView,

    },
  ],
});

export default router;
