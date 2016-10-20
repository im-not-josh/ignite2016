namespace Xtrade.Shared.Interfaces.ViewModels
{
    using System;
    using Domain.Models;

    public interface ISelectedRateViewModel
    {
        event EventHandler OnViewModelDataChanged;

        event EventHandler<string> OnRefreshError;

        event EventHandler<string> OnRefreshSuccess;

        IRate SelectedRate { get; }

        bool IsDataRefreshing { get; }

        void LoadData(string code);

        void RefreshSelectedRate();
    }
}