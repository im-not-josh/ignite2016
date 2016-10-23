namespace Xtrade.Shared
{
    using System.Collections.Generic;
    using Autofac;
    using Domain.ResponseModels;
    using Interfaces.Domain.ResponseModels;
    using Interfaces.Repository;
    using Interfaces.ViewModels;
    using Repository;
    using Domain.Models;
    using Interfaces.Domain.Models;
    using Interfaces.Managers;
    using Managers;
    using ViewModels;
    using Xtrade.Interfaces.Managers;
    using Xtrade.Managers;

    public class BootStrapper
    {
        static BootStrapper()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();

            containerBuilder.Register(n => new Rate()).As<IRate>();
            containerBuilder.Register(n => new List<IRate>()).As<IList<IRate>>();
            containerBuilder.Register(n => new RatesWrapper()).As<IRatesWrapper>();
            containerBuilder.Register(n => new BaseResponse<IRatesWrapper>()).As<IBaseResponse<IRatesWrapper>>();

            containerBuilder.RegisterType<XtradeRepository>().As<IXtradeRepository>().SingleInstance();
            containerBuilder.RegisterType<WebServiceManager>().As<IWebServiceManager>().SingleInstance();
            containerBuilder.RegisterType<NavigationManager>().As<INavigationManager>().SingleInstance();

            containerBuilder.RegisterType<AllRatesViewModel>().As<IAllRatesViewModel>().SingleInstance();
            containerBuilder.RegisterType<SelectedRateViewModel>().As<ISelectedRateViewModel>().SingleInstance();
            containerBuilder.RegisterType<CalculateViewModel>().As<ICalculateViewModel>().SingleInstance();

            Container = containerBuilder.Build();
        }

        private static IContainer Container { get; set; }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}