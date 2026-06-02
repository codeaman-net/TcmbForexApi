using Autofac;
using TcmbForexApi.WriteAPI.Business.Abstract;
using TcmbForexApi.WriteAPI.Business.Concrete;
using TcmbForexApi.WriteAPI.Core.Repositories;
using TcmbForexApi.WriteAPI.Data.PostgreSQL;

namespace TcmbForexApi
{
    public class TcmbForexWriteAPIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ForexService>().As<IForexService>().InstancePerLifetimeScope();
            builder.RegisterType<ForexRepository>().As<IForexRepository>().InstancePerLifetimeScope();
        }
    }
}