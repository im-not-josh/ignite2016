using Xamarin.Forms;

namespace Xtrade
{
    using Interfaces.Managers;
    using Managers;
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
		        if (RatesListView != null)
		        {
		            var rate = RatesListView.SelectedItem as Rate;

		            if (rate != null)
		            {
		                await BootStrapper.Resolve<INavigationManager>().PushAsync(Navigation, new RateDetails(rate));
		            }

		            RatesListView.SelectedItem = null;
		        }
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
            ViewModel.OnRefreshFinish += ViewModel_OnRefreshSuccess;
        }

        private void ViewModel_OnRefreshSuccess(object sender, string e)
        {
            if (Device.OS == TargetPlatform.Android)
            {
                DependencyService.Get<ISnacker>().ShowSnack(e);
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel.OnRefreshFinish -= ViewModel_OnRefreshSuccess;

            if (RatesListView != null)
            {
                RatesListView.ItemTapped -= RatesListView_ItemTapped;
            }
        }
    }
}
