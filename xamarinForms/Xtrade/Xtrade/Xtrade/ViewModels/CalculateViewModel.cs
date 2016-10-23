namespace Xtrade.Shared.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Domain.Models;
    using Interfaces.Domain.Models;
    using Interfaces.Managers;
    using Interfaces.Repository;
    using Interfaces.ViewModels;
    using Utilities;

    public class CalculateViewModel : ICalculateViewModel
    {
        private readonly IXtradeRepository _xtradeRepository;

        private readonly IWebServiceManager _webServiceManager;

        private decimal _dollarValue;

        public IList<IConvertedRateViewModel> ConvertedRateViewModels { get; private set; }

        public CalculateViewModel(IXtradeRepository xtradeRepository, IWebServiceManager webServiceManager)
        {
            this._xtradeRepository = xtradeRepository;
            this._webServiceManager = webServiceManager;
            this.ConvertedRateViewModels = new List<IConvertedRateViewModel>();
            this._dollarValue = 0;
        }

        public event EventHandler OnViewModelDataChanged;

        public async void UpdateData(string newValue)
        {
            if (this.ConvertedRateViewModels.Count == 0)
            {
                IList<Rate> rates = await this._xtradeRepository.GetAllRates();

                foreach (Rate rate in rates)
                {
                    this.ConvertedRateViewModels.Add(new ConvertedRateViewModel() { Code = rate.CurrencyCode, SellRate = rate.SellsNotes });
                }

                this.ConvertedRateViewModels = this.ConvertedRateViewModels.OrderedRatesList();
            }
            
            decimal newValueParsed;
            if (decimal.TryParse(newValue.Trim('$'), out newValueParsed))
            {
                this._dollarValue = newValueParsed;
            }
            else
            {
                this._dollarValue = 0;
            }

            foreach (IConvertedRateViewModel convertedRateViewModel in this.ConvertedRateViewModels)
            {
                decimal convertedValue = convertedRateViewModel.SellRate * this._dollarValue;
                convertedRateViewModel.ConvertedRate = convertedValue.ToString("C");
            }

            this.OnViewModelDataChanged?.Invoke(this, null);
        }

        public string DollarValue { get { return this._dollarValue.ToString("C0"); } }
    }
}