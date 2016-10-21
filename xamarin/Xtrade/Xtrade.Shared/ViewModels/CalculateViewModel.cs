namespace Xtrade.Shared.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Domain.Models;
    using Interfaces.Managers;
    using Interfaces.Repository;
    using Interfaces.ViewModels;

    public class CalculateViewModel : ICalculateViewModel
    {
        private readonly IXtradeRepository _xtradeRepository;

        private readonly IWebServiceManager _webServiceManager;

        private decimal _dollarValue;

        public List<ConvertedRateViewModel> ConvertedRateViewModels { get; private set; }

        public CalculateViewModel(IXtradeRepository xtradeRepository, IWebServiceManager webServiceManager)
        {
            this._xtradeRepository = xtradeRepository;
            this._webServiceManager = webServiceManager;
            this.ConvertedRateViewModels = new List<ConvertedRateViewModel>();
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
            }
            
            decimal newValueParsed;
            if (decimal.TryParse(newValue.Trim('$'), out newValueParsed))
            {
                this._dollarValue = newValueParsed;

                foreach (ConvertedRateViewModel convertedRateViewModel in this.ConvertedRateViewModels)
                {
                    decimal convertedValue = convertedRateViewModel.SellRate * this._dollarValue;
                    convertedRateViewModel.ConvertedRate = convertedValue.ToString("C");
                }
            }
            
            this.OnViewModelDataChanged?.Invoke(this, null);
        }

        public string DollarValue { get { return this._dollarValue.ToString("C"); } }
    }
}