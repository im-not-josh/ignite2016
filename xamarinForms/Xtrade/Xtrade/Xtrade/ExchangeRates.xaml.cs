using Xamarin.Forms;

namespace Xtrade
{
    using Shared;
    using Shared.Interfaces.ViewModels;
    using Shared.ViewModels;

    public partial class ExchangeRates : ContentPage
    {
        private AllRatesViewModel ViewModel => vm ?? (vm = BindingContext as AllRatesViewModel);
        private AllRatesViewModel vm;

		public ExchangeRates ()
		{
			InitializeComponent ();
		    BindingContext = BootStrapper.Resolve<IAllRatesViewModel>();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.LoadData();
        } 
    }
}
