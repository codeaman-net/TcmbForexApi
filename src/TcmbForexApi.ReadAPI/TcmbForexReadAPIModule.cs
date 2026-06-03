using Autofac;
using TcmbForexApi.ReadAPI.Business.Abstracts;
using TcmbForexApi.ReadAPI.Business.Concretes;
using TcmbForexApi.ReadAPI.Core.Repositories;
using TcmbForexApi.ReadAPI.Data.MongoDB;

namespace TcmbForexApi.ReadAPI
{
    public class TcmbForexReadAPIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ForexRateRepository>().As<IForexRateRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ForexRateService>().As<IForexRateService>().InstancePerLifetimeScope();
        }
    }
}