using System.Linq.Expressions;
using MagicVilla_WebAPI.Models;
using MagicVilla_WebAPI.Models.DTOs;

namespace MagicVilla_WebAPI.Repository.IRepository;

public interface IVillaRepository : IRepository<Villa>
{
   
    Task<Villa> UpdateAsync(Villa entity);
   
}