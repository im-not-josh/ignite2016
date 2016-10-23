using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Xtrade
{
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
    }
}
