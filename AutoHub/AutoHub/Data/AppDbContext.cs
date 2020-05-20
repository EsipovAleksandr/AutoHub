using AutoHub.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoHub.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<Color> Colors { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options) { }

        public AppDbContext() { }
 
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Brand>().HasIndex(u => u.Name).IsUnique();
        }
    }
}
