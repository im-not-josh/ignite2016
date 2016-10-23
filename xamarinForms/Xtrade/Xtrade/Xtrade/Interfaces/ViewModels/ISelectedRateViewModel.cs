namespace Xtrade.Shared.Interfaces.ViewModels
{
    using System;
    using System.Windows.Input;
    using Domain.Models;
    using Shared.Domain.Models;

    public interface ISelectedRateViewModel
    {
        event EventHandler<string> OnRefreshFinish;

        bool IsBusy { get; set; }

        Rate SelectedRate { get; set; }

        void LoadData(Rate selectedRate);

        ICommand RefreshRateCommand { get; }
    }
}