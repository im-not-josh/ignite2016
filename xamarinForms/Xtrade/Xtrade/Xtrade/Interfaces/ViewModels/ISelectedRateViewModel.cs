namespace Xtrade.Shared.Interfaces.ViewModels
{
    using System;
    using System.Windows.Input;
    using Domain.Models;

    public interface ISelectedRateViewModel
    {
        event EventHandler<string> OnRefreshError;

        event EventHandler<string> OnRefreshSuccess;

        bool IsBusy { get; set; }

        IRate SelectedRate { get; }

        void LoadData(string code);

        ICommand RefreshRatesCommand { get; }
    }
}