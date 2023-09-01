using System.Linq.Expressions;
using MagicVilla_WebAPI.Data;
using MagicVilla_WebAPI.Models;
using MagicVilla_WebAPI.Models.DTOs;
using MagicVilla_WebAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_WebAPI.Repository;

public class VillaRepository : Repository<Villa>, IVillaRepository
{
    private readonly ApplicationDbContext _dbContext;

    public VillaRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
 
    public async Task<Villa> UpdateAsync(Villa entity)
    {
        entity.UpdatedDate = DateTime.Now;
        _dbContext.Villas.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }


    
}