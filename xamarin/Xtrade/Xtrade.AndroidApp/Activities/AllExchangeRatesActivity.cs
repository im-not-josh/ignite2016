namespace Xtrade.AndroidApp.Activities
{
    using System;
    using Android.App;
    using Android.OS;
    using Android.Support.Design.Widget;
    using Android.Support.V4.Widget;
    using Android.Views;
    using Android.Widget;
    using Shared.Interfaces.ViewModels;
    using Toolbar = Android.Support.V7.Widget.Toolbar;

    [Activity(Label = "@string/allRatesLabel", Theme = "@style/Xtrade", MainLauncher = true, Icon = "@drawable/icon")]
    public class AllExchangeRatesActivity : BaseActivity<IAllRatesViewModel>
    {
        private Toolbar applicationToolbar;
        private TextView noRatesTextView;
        private SwipeRefreshLayout swipeRefreshLayout;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.activityAllExchangeRates);

            this.applicationToolbar = this.FindViewById<Toolbar>(Resource.Id.applicationToolbar);
            this.noRatesTextView = this.FindViewById<TextView>(Resource.Id.noRatesTextView);
            this.swipeRefreshLayout = this.FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout);

            this.SetSupportActionBar(this.applicationToolbar);
        }

        protected override void OnResume()
        {
            base.OnResume();

            this.ViewModel.OnViewModelDataChanged += this.ViewModelDataChanged;
            this.ViewModel.OnRefreshError += this.ViewModelRefreshError;
            this.ViewModel.OnRefreshSuccess += this.ViewModelRefreshSuccess;

            this.swipeRefreshLayout.Refresh += this.SwipeRefreshLayoutOnRefresh;

            this.swipeRefreshLayout.Refreshing = true;
            this.ViewModel.LoadData();
        }
        
        protected override void OnPause()
        {
            base.OnPause();

            this.ViewModel.OnViewModelDataChanged -= this.ViewModelDataChanged;
            this.ViewModel.OnRefreshError -= this.ViewModelRefreshError;
            this.ViewModel.OnRefreshSuccess -= this.ViewModelRefreshSuccess;

            this.swipeRefreshLayout.Refresh -= this.SwipeRefreshLayoutOnRefresh;
        }

        private void UpdateViews()
        {
            this.swipeRefreshLayout.Refreshing = this.ViewModel.IsDataRefreshing;

            if (this.ViewModel.AllRates == null || this.ViewModel.AllRates.Count == 0)
            {
                this.noRatesTextView.Visibility = ViewStates.Visible;
            }
            else
            {
                this.noRatesTextView.Visibility = ViewStates.Gone;
            }
        }

        private void SwipeRefreshLayoutOnRefresh(object sender, EventArgs eventArgs)
        {
            this.swipeRefreshLayout.Refreshing = true;
            this.ViewModel.RefreshRates();
        }

        private void ViewModelDataChanged(object sender, EventArgs eventArgs)
        {
            this.UpdateViews();
        }

        private void ViewModelRefreshError(object sender, string s)
        {
            this.swipeRefreshLayout.Refreshing = this.ViewModel.IsDataRefreshing;
            Snackbar.Make(this.swipeRefreshLayout, s, Snackbar.LengthLong).Show();
        }

        private void ViewModelRefreshSuccess(object sender, string s)
        {
            this.swipeRefreshLayout.Refreshing = this.ViewModel.IsDataRefreshing;
            Snackbar.Make(this.swipeRefreshLayout, s, Snackbar.LengthLong).Show();
        }
    }
}