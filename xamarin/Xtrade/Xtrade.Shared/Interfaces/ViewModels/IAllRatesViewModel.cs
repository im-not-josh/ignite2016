namespace Xtrade.Shared.Interfaces.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Domain.Models;

    public interface IAllRatesViewModel
    {
        event EventHandler OnViewModelDataChanged;

        event EventHandler<string> OnRefreshError;

        event EventHandler<string> OnRefreshSuccess;

        IList<IRate> AllRates { get; }

        bool IsDataRefreshing { get; }

        void LoadData();

        void RefreshRates();
    }
}