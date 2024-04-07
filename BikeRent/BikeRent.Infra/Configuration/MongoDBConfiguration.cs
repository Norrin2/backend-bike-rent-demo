using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

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

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(dataBaseName);

            services.AddSingleton(database);

            return services;
        }
    }
}
