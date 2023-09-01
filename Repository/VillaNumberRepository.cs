using System.Linq.Expressions;
using MagicVilla_WebAPI.Data;
using MagicVilla_WebAPI.Models;
using MagicVilla_WebAPI.Models.DTOs;
using MagicVilla_WebAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_WebAPI.Repository;

public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
{
    private readonly ApplicationDbContext _dbContext;

    public VillaNumberRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
 
    public async Task<VillaNumber> UpdateAsync(VillaNumber entity)
    {
        entity.UpdatedDate = DateTime.Now;
        _dbContext.VillaNumbers.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }


    
}