using Microsoft.EntityFrameworkCore;
using JobTracker.Domain.Entities;
using JobTracker.Domain.Interfaces;
using JobTracker.Infrastructure.Data;

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

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

    // Correspondance avec ton interface : void Update
    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    // Correspondance avec ton interface : Task DeleteAsync(T entity)
    public async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await Task.CompletedTask;
    }

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}