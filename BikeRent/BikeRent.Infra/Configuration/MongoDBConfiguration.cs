using BikeRent.Infra.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BikeRent.Infra.Configuration
{
    public static class MongoDBConfiguration
    {
        public static IServiceCollection AddMongoDBConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("MongoDB").GetSection("Uri").Value;
            var dataBaseName = configuration.GetSection("MongoDB").GetSection("Database").Value;
            if (connectionString == null)
            {
                Environment.Exit(0);
            }

            services.AddDbContext<BikeRentDbContext>(options => options.UseMongoDB(connectionString, dataBaseName));

            return services;
        }
    }
}
