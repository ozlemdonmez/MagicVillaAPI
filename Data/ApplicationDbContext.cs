using MagicVilla_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_WebAPI.Data;

public class ApplicationDbContext : DbContext
{
   public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
   {
      
   }
   public DbSet<Villa> Villas { get; set; }
   public DbSet<VillaNumber> VillaNumbers { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      modelBuilder.Entity<Villa>().HasData(
         new Villa()
         {
            Id = 1,
            Name = "Palm Spring",
            Occupancy = 5,
            Rate = 9,
            Details = "Lorem ipsum ipsum ipsum...",
            Sqft = 450,
            Amenity = "bla bla",
            ImageUrl = "denemeimage",
            CreatedDate = DateTime.Now
            
            
         });
   }
}