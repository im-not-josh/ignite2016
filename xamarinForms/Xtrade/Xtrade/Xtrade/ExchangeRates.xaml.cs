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

    public partial class ExchangeRates : ContentPage
    {
        //private AllRatesViewModel ViewModel => vm ?? (vm = BindingContext as AllRatesViewModel);
        //private AllRatesViewModel vm;

		public ExchangeRates ()
		{
			InitializeComponent ();
		    //BindingContext = BootStrapper.Resolve<IAllRatesViewModel>();
		}
	}
}
