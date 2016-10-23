using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Xtrade
{
    using Shared;
    using Shared.Interfaces.ViewModels;
    using Shared.ViewModels;

    public partial class RateDetails : ContentPage
	{
        private SelectedRateViewModel ViewModel => vm ?? (vm = BindingContext as SelectedRateViewModel);
        private SelectedRateViewModel vm;
        private string selectedCode;

        public RateDetails (string selectedCode)
		{
			InitializeComponent ();
            BindingContext = BootStrapper.Resolve<ISelectedRateViewModel>();
            this.selectedCode = selectedCode;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.LoadData(selectedCode);
        }
    }
}
