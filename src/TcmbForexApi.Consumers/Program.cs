using Autofac;
using Autofac.Extensions.DependencyInjection;
using Confluent.Kafka;
using Newtonsoft.Json;
using TcmbForexApi.Consumers;
using TcmbForexApi.Consumers.Business.Abstracts;
using TcmbForexApi.Consumers.Business.Dtos;
using TcmbForexApi.Consumers.Core.Configuration;
using TcmbForexApi.Consumers.Core.Enums;
using TcmbForexApi.Consumers.Core.Models;

var environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

var host = Host.CreateDefaultBuilder(args)
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
     {
        containerBuilder.RegisterModule(new TcmbForexConsumersModule(configuration));
     }).Build();

var kafkaSettings = host.Services.GetRequiredService<KafkaSettings>();

var conf = new ConsumerConfig
        {
            GroupId = kafkaSettings.GroupId,
            BootstrapServers = kafkaSettings.Host,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false
        };

using var c = new ConsumerBuilder<Ignore, string>(conf).Build();
c.Subscribe(kafkaSettings.TopicName);

CancellationTokenSource cts = new();

var forexRateService = host.Services.GetRequiredService<IForexRateService>();

while (true)
{
    var cr = c.Consume(cts.Token);

    if (string.IsNullOrWhiteSpace(cr.Message.Value))
    {
        c.Commit(cr);
        continue;
    }

    var queueData = JsonConvert.DeserializeObject<DebeziumPayload<RateMessageModel>>(cr.Message.Value);

    if (queueData is null || queueData.Op == DebeziumOperationType.Read)
    {
        c.Commit(cr);
        continue;
    }

    if (queueData.Op is DebeziumOperationType.Create && queueData.After is not null)
    {
        try
        {
            var tempRateDate = new DateTime(1970, 1, 1).AddDays(queueData.After.RateDate);
            var createDto = new RateCreateDto
            {
                RateId = queueData.After.Id,
                Code = queueData.After.Code,
                BanknoteBuying = queueData.After.BanknoteBuying,
                BanknoteSelling = queueData.After.BanknoteSelling,
                CrossRateUSD = queueData.After.CrossRateUSD,
                Date = DateOnly.FromDateTime(tempRateDate),
                ForexBuying = queueData.After.ForexBuying,
                ForexSelling = queueData.After.ForexSelling,
                Name = queueData.After.Name,
                Unit = queueData.After.Unit
            };

            var result = await forexRateService.CreateRateAsync(createDto);

            if (!result)
            {
                Console.WriteLine($"INSERT - FAIL");
                continue;
            }

            Console.WriteLine($"INSERT - OK");

            c.Commit(cr);
        }
        catch (Exception createException)
        {
            Console.WriteLine($"INSERT FAILED! Exception message is {createException.Message}");
        }
    }
}
