using BikeRent.Consumer;
using BikeRent.Infra.Configuration;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMongoDBConfig(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddRabbitMQ();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
