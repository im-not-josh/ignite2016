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

    public partial class Calculate : ContentPage
	{
        private CalculateViewModel ViewModel => vm ?? (vm = BindingContext as CalculateViewModel);
        private CalculateViewModel vm;

        public Calculate ()
		{
			InitializeComponent ();
            BindingContext = BootStrapper.Resolve<ICalculateViewModel>();
            ViewModel.UpdateData();
            CalculatedRatesListView.ItemTapped += CalculatedRatesListView_ItemTapped;

            CalculatedRatesListView.ItemSelected += (sender, args) =>
            {
                if (CalculatedRatesListView != null)
                {
                    CalculatedRatesListView.SelectedItem = null;
                }
            };
        }

        private void CalculatedRatesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var list = sender as ListView;
            if (list != null)
            {
                list.SelectedItem = null;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (CalculatedRatesListView != null)
            {
                CalculatedRatesListView.ItemTapped -= CalculatedRatesListView_ItemTapped;
            }
        }
    }
}
