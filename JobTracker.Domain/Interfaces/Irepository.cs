using JobTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobTracker.Domain.Interfaces
{

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

