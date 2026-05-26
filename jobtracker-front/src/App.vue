<template>
  <div class="dashboard-layout">

    <aside class="sidebar">
      <div>
        <div class="brand">
          <i class="pi pi-briefcase text-primary"></i>
          <span>JobTracker</span>
        </div>
        <nav class="nav-links">
          <router-link to="/" class="nav-item active">
            <i class="pi pi-chart-bar"></i>
            <span>Dashboard</span>
          </router-link>
          <router-link to="/candidatures" class="nav-item">
            <i class="pi pi-list"></i>
            <span>Candidatures</span>
          </router-link>
          <router-link to="/entreprises" class="nav-item">
            <i class="pi pi-building"></i>
            <span>Entreprises</span>
          </router-link>
          <router-link to="/notes" class="nav-item">
            <i class="pi pi-clipboard"></i>
            <span>Notes</span>
          </router-link>
          <router-link to="/recherches" class="nav-item">
            <i class="pi pi-search"></i>
            <span>Recherche</span>
          </router-link>
        </nav>
      </div>
      <div class="version">Version 1.0</div>
    </aside>

    <main class="main-content">
      <router-view />
    </main>

  </div>
</template>

<style scoped>
.dashboard-layout {
  display: flex;
  height: 100vh;
  background-color: #f8f9fa;
  font-family: var(--p-font-family, sans-serif);
}

.sidebar {
  width: 280px;
  background-color: #ffffff;
  border-right: 1px solid #e2e8f0;
  display: flex;
  flex-col: column;
  flex-direction: column;
  justify-content: space-between;
  padding: 1.5rem;
}

.brand {
  display: flex;
  align-items: center;
  gap: 0.75rem;
  font-size: 1.5rem;
  font-weight: 700;
  color: #1e293b;
  margin-bottom: 2rem;
}

.brand i {
  color: #3b82f6;
}

.nav-links {
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}

.nav-item {
  display: flex;
  align-items: center;
  gap: 0.75rem;
  padding: 0.75rem 1rem;
  color: #64748b;
  text-decoration: none;
  border-radius: 6px;
  font-weight: 500;
  transition: all 0.2s;
}

.nav-item:hover {
  background-color: #f1f5f9;
  color: #1e293b;
}

.nav-item.active {
  background-color: #eff6ff;
  color: #1d4ed8;
}

.version {
  font-size: 0.85rem;
  color: #94a3b8;
}

.main-content {
  flex-1: 1;
  flex-grow: 1;
  padding: 2rem;
  overflow-y: auto;
}
</style>