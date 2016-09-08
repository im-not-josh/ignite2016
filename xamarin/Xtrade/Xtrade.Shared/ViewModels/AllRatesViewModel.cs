namespace Xtrade.Shared.ViewModels
{
    using Interfaces.Managers;
    using Interfaces.Repository;
    using Interfaces.ViewModels;

    public class AllRatesViewModel : IAllRatesViewModel
    {
        private readonly IXtradeRepository _xtradeRepository;

        private readonly IWebServiceManager _webServiceManager;

        public AllRatesViewModel(IXtradeRepository xtradeRepository, IWebServiceManager webServiceManager)
        {
            this._xtradeRepository = xtradeRepository;
            this._webServiceManager = webServiceManager;
        }
    }
}