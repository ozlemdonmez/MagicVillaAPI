using System.Linq.Expressions;
using MagicVilla_WebAPI.Data;
using MagicVilla_WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using MagicVilla_WebAPI.Repository.IRepository;

namespace MagicVilla_WebAPI.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _dbContext;
    internal DbSet<T> dbSet;

    public Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        this.dbSet = _dbContext.Set<T>();
    }
    
    
    public async Task CreateAsync(T entity)
    {
        await dbSet.AddAsync(entity);
        await SaveAsync();
    }
    


    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
    {
        IQueryable<T> query = dbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.ToListAsync();
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true)
    {
        IQueryable<T> query = dbSet;

        if (tracked != null)
        {
            query = query.AsNoTracking();
        }
        if (filter != null)
        {
            query = query.Where(filter);
        } 

        return await query.FirstOrDefaultAsync();
    }

    

    public async Task RemoveAsync(T entity)
    {
       dbSet.Remove(entity);
       await SaveAsync();
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}