using BikeRent.Domain.Entities;
using BikeRent.Domain.ValueObject;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace BikeRent.Infra.Database
{
    public class BikeRentDbContext: DbContext
    {
        public DbSet<Bike> Bikes { get; init; }
        public DbSet<Deliveryman> Deliverymen { get; init; }

        public BikeRentDbContext(DbContextOptions<BikeRentDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Bike>().ToCollection("bikes");

            modelBuilder.Entity<Deliveryman>()
                .ToCollection("deliveryman");
        }
    }
}
