using Xamarin.Forms;

namespace Xtrade
{
    using Interfaces.Managers;
    using Shared;
    using Shared.Domain.Models;
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

            RatesListView.ItemTapped += RatesListView_ItemTapped;

		    RatesListView.ItemSelected += async (sender, args) =>
		    {
		        var rate = RatesListView.SelectedItem as Rate;
		        await BootStrapper.Resolve<INavigationManager>().PushAsync(Navigation, new RateDetails(rate.CurrencyCode));
		        RatesListView.SelectedItem = null;
		    };
		}

        private void RatesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var list = sender as ListView;
            if (list != null)
            {
                list.SelectedItem = null;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.LoadData();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            RatesListView.ItemTapped -= RatesListView_ItemTapped;
        }
    }
}
