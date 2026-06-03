using Autofac;
using TcmbForexApi.WriteAPI.Business.Abstract;
using TcmbForexApi.WriteAPI.Business.Concrete;
using TcmbForexApi.WriteAPI.Core.Repositories;
using TcmbForexApi.WriteAPI.Data.PostgreSQL;
using TcmbForexApi.WriteAPI.Infrastructure.Abstracts;
using TcmbForexApi.WriteAPI.Infrastructure.Concretes;

namespace TcmbForexApi.WriteAPI
{
    public class TcmbForexWriteAPIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ForexService>().As<IForexService>().InstancePerLifetimeScope();
            builder.RegisterType<ForexRepository>().As<IForexRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TcmbService>().As<ITcmbService>().InstancePerLifetimeScope();
            builder.RegisterType<DatabaseInitializer>().AsSelf().SingleInstance();
        }
    }
}