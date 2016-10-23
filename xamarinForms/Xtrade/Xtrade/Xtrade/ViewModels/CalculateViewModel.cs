namespace Xtrade.Shared.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Domain.Models;
    using Interfaces.Domain.Models;
    using Interfaces.Managers;
    using Interfaces.Repository;
    using Interfaces.ViewModels;
    using MvvmHelpers;
    using Utilities;

    public class CalculateViewModel : BaseViewModel, ICalculateViewModel
    {
        private readonly IXtradeRepository _xtradeRepository;

        private readonly IWebServiceManager _webServiceManager;

        private decimal _dollarValue;

        private string _newValue;

        public ObservableRangeCollection<IConvertedRateViewModel> ConvertedRateViewModels { get; private set; }

        public CalculateViewModel(IXtradeRepository xtradeRepository, IWebServiceManager webServiceManager)
        {
            this._xtradeRepository = xtradeRepository;
            this._webServiceManager = webServiceManager;
            this.ConvertedRateViewModels = new ObservableRangeCollection<IConvertedRateViewModel>();
            this._dollarValue = 0;
        }

        public async void UpdateData()
        {
            if (this.ConvertedRateViewModels.Count == 0)
            {
                IList<Rate> rates = await this._xtradeRepository.GetAllRates();

                foreach (Rate rate in rates)
                {
                    this.ConvertedRateViewModels.Add(new ConvertedRateViewModel() { Code = rate.CurrencyCode, SellRate = rate.SellsNotes });
                }

                this.ConvertedRateViewModels.ReplaceRange(this.ConvertedRateViewModels.OrderedRatesList());
            }
            
            foreach (IConvertedRateViewModel convertedRateViewModel in this.ConvertedRateViewModels)
            {
                decimal convertedValue = convertedRateViewModel.SellRate * this._dollarValue;
                convertedRateViewModel.ConvertedRate = convertedValue.ToString("C");
            }
        }

        public string DollarValue
        {
            get
            {
                return this._dollarValue.ToString("C0"); 
            }
            set
            {
                decimal newValueParsed;
                if (decimal.TryParse(value.Trim('$'), out newValueParsed))
                {
                    this._dollarValue = newValueParsed;
                }
                else
                {
                    this._dollarValue = 0;
                }

                SetProperty(ref _newValue, value);
                this.UpdateData();
            }
        }
    }
}