using Autofac;
using TcmbForexApi.Consumers.Business.Abstracts;
using TcmbForexApi.Consumers.Business.Concretes;
using TcmbForexApi.Consumers.Core.Repositories;
using TcmbForexApi.Consumers.Data.MongoDB;

namespace TcmbForexApi.Consumers
{
    public class TcmbForexConsumersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ForexRateRepository>().As<IForexRateRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ForexRateService>().As<IForexRateService>().InstancePerLifetimeScope();
        }
    }
}