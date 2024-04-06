using BikeRent.Domain;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace BikeRent.Infra.Database
{
    public class BikeRentDbContext: DbContext
    {
        public DbSet<Bike> Bikes { get; init; }

        public BikeRentDbContext() {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Bike>().ToCollection("bikes");
        }
    }
}
