using System.Linq.Expressions;
using MagicVilla_WebAPI.Models;
using MagicVilla_WebAPI.Models.DTOs;

namespace MagicVilla_WebAPI.Repository.IRepository;

public interface IVillaNumberRepository : IRepository<VillaNumber>
{
   
    Task<VillaNumber> UpdateAsync(VillaNumber entity);
   
}