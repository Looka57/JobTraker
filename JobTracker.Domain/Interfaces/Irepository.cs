using JobTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobTracker.Domain.Interfaces
{
    //l'interface de base CRUD pour les entités du domaine. Elle est générique, ce qui signifie que tu peux l'utiliser pour n'importe quelle entité
    //qui hérite de BaseEntity (comme Candidature, Company, etc.).
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        Task DeleteAsync(T entity);
        Task SaveChangesAsync();
    }

}

