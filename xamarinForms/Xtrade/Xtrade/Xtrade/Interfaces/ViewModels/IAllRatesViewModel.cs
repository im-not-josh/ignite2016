namespace Xtrade.Shared.Interfaces.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Domain.Models;
    using MvvmHelpers;

    public interface IAllRatesViewModel
    {
        event EventHandler<string> OnRefreshError;

        event EventHandler<string> OnRefreshSuccess;

        ObservableRangeCollection<IRate> AllRates { get; }

        bool IsBusy { get; set; }

        void LoadData();

        ICommand RefreshRatesCommand { get; }
    }
}