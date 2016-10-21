namespace Xtrade.Shared.Interfaces.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Shared.ViewModels;

    public interface ICalculateViewModel
    {
        List<ConvertedRateViewModel> ConvertedRateViewModels { get; }

        event EventHandler OnViewModelDataChanged;

        void UpdateData(string newValue);

        string DollarValue { get; }
    }
}