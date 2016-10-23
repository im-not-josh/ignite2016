using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Xtrade
{
    using Managers;
    using Shared;
    using Shared.Domain.Models;
    using Shared.Interfaces.ViewModels;
    using Shared.ViewModels;

    public partial class RateDetails : ContentPage
	{
        private SelectedRateViewModel ViewModel => vm ?? (vm = BindingContext as SelectedRateViewModel);
        private SelectedRateViewModel vm;

        public RateDetails (Rate selectedRate)
		{
			InitializeComponent ();
            BootStrapper.Resolve<ISelectedRateViewModel>().LoadData(selectedRate);
            BindingContext = BootStrapper.Resolve<ISelectedRateViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnRefreshFinish += ViewModel_OnRefreshSuccess;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel.OnRefreshFinish -= ViewModel_OnRefreshSuccess;
        }

        private void ViewModel_OnRefreshSuccess(object sender, string e)
        {
            if (Device.OS == TargetPlatform.Android)
            {
                DependencyService.Get<ISnacker>().ShowSnack(e);
            }
        }
    }
}
