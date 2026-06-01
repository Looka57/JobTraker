using Microsoft.EntityFrameworkCore;
using JobTracker.Domain.Entities;
using JobTracker.Domain.Interfaces;
using JobTracker.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobTracker.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly JobTrackDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(JobTrackDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    // 🌟 AJOUT DE "virtual" ICI pour permettre la surcharge dans InteractionRepository
    public virtual async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await Task.CompletedTask;
    }

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}