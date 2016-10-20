namespace Xtrade.Shared.ViewModels
{
    using System;
    using Domain.Converters;
    using Interfaces.Domain.Models;
    using Interfaces.Domain.ResponseModels;
    using Interfaces.Managers;
    using Interfaces.Repository;
    using Interfaces.ViewModels;
    using Newtonsoft.Json;

    public class SelectedRateViewModel : ISelectedRateViewModel
    {
        private readonly IXtradeRepository _xtradeRepository;

        private readonly IWebServiceManager _webServiceManager;

        private string _selectedRateCode;

        public SelectedRateViewModel(IXtradeRepository xtradeRepository, IWebServiceManager webServiceManager)
        {
            this._xtradeRepository = xtradeRepository;
            this._webServiceManager = webServiceManager;
            this.SelectedRate = null;
        }

        public event EventHandler OnViewModelDataChanged;

        public event EventHandler<string> OnRefreshError;

        public event EventHandler<string> OnRefreshSuccess;

        public IRate SelectedRate { get; private set; }

        public bool IsDataRefreshing { get; private set; }

        public async void LoadData(string code)
        {
            this._selectedRateCode = code;
            this.SelectedRate = await this._xtradeRepository.GetRateByCode(this._selectedRateCode);
            this.OnViewModelDataChanged?.Invoke(this, null);
        }

        public async void RefreshSelectedRate()
        {
            this.IsDataRefreshing = true;
            IBaseResponse<IRateWrapper> newRatesResponse = await this._webServiceManager.GetAndParse<IRateWrapper>("exchange-rates?currecnyCode=" + this._selectedRateCode, new JsonConverter[] { new RateConverter(), new RateWrapperConverter() });

            if (newRatesResponse != null && newRatesResponse.IsValid() && newRatesResponse.Result.Value != null)
            {
                this.SelectedRate = newRatesResponse.Result.Value;
                await this._xtradeRepository.InsertRateAsync(this.SelectedRate);
                this.OnViewModelDataChanged?.Invoke(this, null);
                this.IsDataRefreshing = false;
                this.OnRefreshSuccess?.Invoke(this, "Rate updated");
            }
            else
            {
                this.IsDataRefreshing = false;
                this.OnRefreshError?.Invoke(this, "Could not refresh exchange rate");
            }
        }
    }
}