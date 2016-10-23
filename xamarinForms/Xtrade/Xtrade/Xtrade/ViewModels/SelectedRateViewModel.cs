namespace Xtrade.Shared.ViewModels
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Domain.Converters;
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

        private string _selectedRateCode;

        public SelectedRateViewModel(IXtradeRepository xtradeRepository, IWebServiceManager webServiceManager)
        {
            this._xtradeRepository = xtradeRepository;
            this._webServiceManager = webServiceManager;
            this.SelectedRate = null;
        }

        public event EventHandler<string> OnRefreshError;

        public event EventHandler<string> OnRefreshSuccess;

        public IRate SelectedRate { get; private set; }

        public async void LoadData(string code)
        {
            this._selectedRateCode = code;
            this.SelectedRate = await this._xtradeRepository.GetRateByCode(this._selectedRateCode);
        }

        public ICommand RefreshRatesCommand => new Command(async () => await this.RefreshSelectedRate());

        private async Task RefreshSelectedRate()
        {
            this.IsBusy = true;
            IBaseResponse<IRatesWrapper> newRatesResponse = await this._webServiceManager.GetAndParse<IRatesWrapper>("exchange-rates?currecnyCode=" + this._selectedRateCode, new JsonConverter[] { new RateConverter(), new RatesWrapperConverter() });

            if (newRatesResponse != null && newRatesResponse.IsValid() && newRatesResponse.Result.Value != null && newRatesResponse.Result.Value.Count > 0)
            {
                this.SelectedRate = newRatesResponse.Result.Value.FirstOrDefault();
                await this._xtradeRepository.InsertRateAsync(this.SelectedRate);
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