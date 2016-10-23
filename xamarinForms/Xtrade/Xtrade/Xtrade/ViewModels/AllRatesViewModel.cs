namespace Xtrade.Shared.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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
    using Utilities;
    using Xamarin.Forms;

    public class AllRatesViewModel : BaseViewModel, IAllRatesViewModel
    {
        private readonly IXtradeRepository _xtradeRepository;

        private readonly IWebServiceManager _webServiceManager;

        public AllRatesViewModel(IXtradeRepository xtradeRepository, IWebServiceManager webServiceManager)
        {
            this._xtradeRepository = xtradeRepository;
            this._webServiceManager = webServiceManager;
            this.AllRates = new ObservableRangeCollection<IRate>();
        }

        public event EventHandler<string> OnRefreshFinish;

        public ObservableRangeCollection<IRate> AllRates { get; }

        public async void LoadData()
        {
            IList<Rate> allRates = await this._xtradeRepository.GetAllRates();

            this.AllRates.ReplaceRange(allRates.OrderedRatesList());
            await this.RefreshRates();
        }

        public ICommand RefreshRatesCommand => new Command(async () => await this.RefreshRates());

        private async Task RefreshRates()
        {
            this.IsBusy = true;
            IBaseResponse<IRatesWrapper> newRatesResponse = await this._webServiceManager.GetAndParse<IRatesWrapper>("exchange-rates", new JsonConverter[] { new RateConverter(), new RatesWrapperConverter() });

            if (newRatesResponse != null && newRatesResponse.IsValid() && newRatesResponse.Result.Value != null && newRatesResponse.Result.Value.Count > 0)
            {
                this.AllRates.ReplaceRange(newRatesResponse.Result.Value.OrderedRatesList());
                await this._xtradeRepository.InsertRatesAsync(this.AllRates);
                this.IsBusy = false;
                this.OnRefreshFinish?.Invoke(this, "Rates updated");
            }
            else
            {
                this.IsBusy = false;
                this.OnRefreshFinish?.Invoke(this, "Could not refresh exchange rates");
            }
        }
    }
}