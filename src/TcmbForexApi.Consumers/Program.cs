using Autofac;
using Autofac.Extensions.DependencyInjection;
using TcmbForexApi.Consumers;
using TcmbForexApi.Consumers.Core.Configuration;
using TcmbForexApi.Consumers.Data.MongoDB;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                     .AddEnvironmentVariables();

builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection("KafkaSettings"));
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {   
        containerBuilder.RegisterModule(new TcmbForexConsumersModule());
    });

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();
