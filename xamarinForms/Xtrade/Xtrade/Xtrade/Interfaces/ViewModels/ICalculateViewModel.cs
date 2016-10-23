namespace Xtrade.Shared.Interfaces.ViewModels
{
    using System;
    using System.Collections.Generic;
    using MvvmHelpers;
    using Shared.ViewModels;

    public interface ICalculateViewModel
    {
        ObservableRangeCollection<IConvertedRateViewModel> ConvertedRateViewModels { get; }

        void UpdateData();

        string DollarValue { get; set; }
    }
}