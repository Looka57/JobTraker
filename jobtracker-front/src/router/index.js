import { createRouter, createWebHistory } from "vue-router";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "dashboard",
      component: () => import("@/views/DashboardView.vue"),
    },
    {
      path: "/candidatures",
      name: "candidatures",
      component: () => import("../views/CandidaturesView.vue"),
    },
    {
      path: "/FormAjtCandidature",
      name: "form-ajt-candidature",
      component: () => import("../views/FormAjtCandidatureView.vue"),
    },
    {
      path: "/entreprises",
      name: "entreprises",
      component: () => import("../views/CompaniesView.vue"),
    },
    {
      path: "/notes",
      name: "notes",
      component: () => import("../views/NotesView.vue"),
    },
    {
      path: "/recherches",
      name: "recherches",
      component: () => import("../views/RecherchesView.vue"),
    },
  ],
});

export default router;
