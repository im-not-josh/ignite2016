namespace Xtrade.Shared.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Domain.Converters;
    using Domain.Models;
    using Interfaces.Domain.Models;
    using Interfaces.Domain.ResponseModels;
    using Interfaces.Managers;
    using Interfaces.Repository;
    using Interfaces.ViewModels;
    using Newtonsoft.Json;
    using Utilities;

    public class AllRatesViewModel : IAllRatesViewModel
    {
        private readonly IXtradeRepository _xtradeRepository;

        private readonly IWebServiceManager _webServiceManager;

        public AllRatesViewModel(IXtradeRepository xtradeRepository, IWebServiceManager webServiceManager)
        {
            this._xtradeRepository = xtradeRepository;
            this._webServiceManager = webServiceManager;
            this.AllRates = new List<IRate>();
        }

        public event EventHandler OnViewModelDataChanged;

        public event EventHandler<string> OnRefreshError;

        public event EventHandler<string> OnRefreshSuccess;

        public IList<IRate> AllRates { get; private set; }

        public bool IsDataRefreshing { get; private set; }

        public async void LoadData()
        {
            this.IsDataRefreshing = true;

            IList<Rate> allRates = await this._xtradeRepository.GetAllRates();

            if (allRates == null || allRates.Count == 0)
            {
                this.RefreshRates();
            }
            else
            {
                this.AllRates = allRates.OrderedRatesList();
                this.IsDataRefreshing = false;
                this.OnViewModelDataChanged?.Invoke(this, null);
            }
        }

        public async void RefreshRates()
        {
            this.IsDataRefreshing = true;
            IBaseResponse<IRatesWrapper> newRatesResponse = await this._webServiceManager.GetAndParse<IRatesWrapper>("exchange-rates", new JsonConverter[] { new RateConverter(), new RatesWrapperConverter() });

            if (newRatesResponse != null && newRatesResponse.IsValid() && newRatesResponse.Result.Value != null && newRatesResponse.Result.Value.Count > 0)
            {
                this.AllRates = newRatesResponse.Result.Value.OrderedRatesList();
                await this._xtradeRepository.InsertRatesAsync(this.AllRates);
                this.OnViewModelDataChanged?.Invoke(this, null);
                this.IsDataRefreshing = false;
                this.OnRefreshSuccess?.Invoke(this, "Rates updated");
            }
            else
            {
                this.IsDataRefreshing = false;
                this.OnRefreshError?.Invoke(this, "Could not refresh exchang rates");
            }
           
        }
    }
}