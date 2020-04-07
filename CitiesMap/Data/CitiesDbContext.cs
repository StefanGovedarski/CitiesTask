using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesMap.Data
{
    public class CitiesDbContext : DbContext
    {
        public CitiesDbContext(DbContextOptions<CitiesDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CityEntity>(entity => entity.HasIndex(e => e.Name).IsUnique());
        }

        public DbSet<CityEntity> Cities { get; set; }
        public DbSet<RoadEntity> Roads { get; set; }
        public DbSet<LogisticCenterEntity> LogisticCenter { get; set; }
    }
}
