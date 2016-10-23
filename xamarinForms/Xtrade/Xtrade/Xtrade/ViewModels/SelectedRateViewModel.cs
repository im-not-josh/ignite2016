namespace Xtrade.Shared.ViewModels
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Domain.Converters;
    using Domain.Models;
    using Interfaces.Domain.Models;
    using Interfaces.Domain.ResponseModels;
    using Interfaces.Managers;
    using Interfaces.Repository;
    using Interfaces.ViewModels;
    using MvvmHelpers;
    using Newtonsoft.Json;
    using Xamarin.Forms;

    public class SelectedRateViewModel : BaseViewModel, ISelectedRateViewModel
    {
        private readonly IXtradeRepository _xtradeRepository;

        private readonly IWebServiceManager _webServiceManager;

        public SelectedRateViewModel(IXtradeRepository xtradeRepository, IWebServiceManager webServiceManager)
        {
            this._xtradeRepository = xtradeRepository;
            this._webServiceManager = webServiceManager;
            this._selectedRate = null;
        }

        public event EventHandler<string> OnRefreshError;

        public event EventHandler<string> OnRefreshSuccess;

        private Rate _selectedRate;

        public Rate SelectedRate
        {
            get { return _selectedRate; }
            set { SetProperty(ref _selectedRate, value); }
        }

        public void LoadData(Rate selectedRate)
        {
            this._selectedRate = selectedRate;
        }

        public ICommand RefreshRateCommand => new Command(async () => await this.RefreshSelectedRate());

        private async Task RefreshSelectedRate()
        {
            IsBusy = true;
            IBaseResponse<IRatesWrapper> newRatesResponse = await this._webServiceManager.GetAndParse<IRatesWrapper>("exchange-rates?currencyCode=" + this._selectedRate.CurrencyCode, new JsonConverter[] { new RateConverter(), new RatesWrapperConverter() });

            if (newRatesResponse != null && newRatesResponse.IsValid() && newRatesResponse.Result.Value != null && newRatesResponse.Result.Value.Count > 0)
            {
                this.SelectedRate = newRatesResponse.Result.Value.FirstOrDefault();
                await this._xtradeRepository.InsertRateAsync(this._selectedRate);
                this.IsBusy = false;
                this.OnRefreshSuccess?.Invoke(this, "Rate updated");
            }
            else
            {
                this.IsBusy = false;
                this.OnRefreshError?.Invoke(this, "Could not refresh exchange rate");
            }
        }
    }
}