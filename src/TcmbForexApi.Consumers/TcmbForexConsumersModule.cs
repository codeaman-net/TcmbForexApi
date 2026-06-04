using Autofac;
using TcmbForexApi.Consumers.Business.Abstracts;
using TcmbForexApi.Consumers.Business.Concretes;
using TcmbForexApi.Consumers.Core.Configuration;
using TcmbForexApi.Consumers.Core.Repositories;
using TcmbForexApi.Consumers.Data.MongoDB;

namespace TcmbForexApi.Consumers
{
    public class TcmbForexConsumersModule(IConfiguration configuration) : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ForexRateRepository>().As<IForexRateRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ForexRateService>().As<IForexRateService>().InstancePerLifetimeScope();

            KafkaSettings kafkaSettings = new();
            MongoDbSettings mongoDbSettings = new();

            configuration.GetSection("KafkaSettings").Bind(kafkaSettings);
            configuration.GetSection("MongoDbSettings").Bind(mongoDbSettings);

            builder.RegisterInstance(kafkaSettings).AsSelf();
            builder.RegisterInstance(mongoDbSettings).AsSelf();
        }
    }
}