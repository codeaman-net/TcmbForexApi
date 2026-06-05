using Autofac;
using Autofac.Extensions.DependencyInjection;
using FastEndpoints;
using TcmbForexApi.WriteAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                     .AddEnvironmentVariables();

builder.Services.AddFastEndpoints();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {   
        containerBuilder.RegisterModule(new TcmbForexWriteAPIModule());
    });

var app = builder.Build();

app.UseHttpsRedirection();
app.UseFastEndpoints();

app.Run();