using Autofac;
using Autofac.Extensions.DependencyInjection;
using FastEndpoints;
using TcmbForexApi.ReadAPI;
using TcmbForexApi.ReadAPI.Data.MongoDB;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                     .AddEnvironmentVariables();

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddFastEndpoints();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new TcmbForexReadAPIModule());
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseFastEndpoints();

app.Run();