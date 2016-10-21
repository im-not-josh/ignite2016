namespace Xtrade.Shared.Interfaces.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Shared.ViewModels;

    public interface ICalculateViewModel
    {
        IList<IConvertedRateViewModel> ConvertedRateViewModels { get; }

        event EventHandler OnViewModelDataChanged;

        void UpdateData(string newValue);

        string DollarValue { get; }
    }
}