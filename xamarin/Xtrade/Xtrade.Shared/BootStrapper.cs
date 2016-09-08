namespace Xtrade.Shared
{
    using System.Collections.Generic;
    using Autofac;
    using Domain.ResponseModels;
    using Interfaces.Domain.ResponseModels;
    using Interfaces.Repository;
    using Interfaces.ViewModels;
    using Repository;
    using Shared.Domain.Models;
    using Shared.Interfaces.Domain.Models;
    using Shared.Interfaces.Managers;
    using Shared.Managers;
    using ViewModels;

    public class BootStrapper
    {
        static BootStrapper()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();

            containerBuilder.Register(n => new Rate()).As<IRate>();
            containerBuilder.Register(n => new List<IRate>()).As<IList<IRate>>();
            containerBuilder.Register(n => new BaseResponse<List<IRate>>()).As<IBaseResponse<List<IRate>>>();

            containerBuilder.RegisterType<XtradeRepository>().As<IXtradeRepository>().SingleInstance();
            containerBuilder.RegisterType<WebServiceManager>().As<IWebServiceManager>().SingleInstance();

            containerBuilder.RegisterType<AllRatesViewModel>().As<IAllRatesViewModel>().SingleInstance();

            Container = containerBuilder.Build();
        }

        private static IContainer Container { get; set; }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}