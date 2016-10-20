namespace Xtrade.AndroidApp.Activities
{
    using System;
    using Adapters;
    using Android.App;
    using Android.Content;
    using Android.OS;
    using Android.Support.Design.Widget;
    using Android.Support.V4.Widget;
    using Android.Support.V7.Widget;
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
        private RecyclerView ratesRecyclerView;
        private RecyclerView.LayoutManager ratesRecylerViewLayoutManager;
        private RatesRecyclerAdapter ratesRecyclerAdapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.activity_all_exchange_rates);

            this.applicationToolbar = this.FindViewById<Toolbar>(Resource.Id.applicationToolbar);
            this.noRatesTextView = this.FindViewById<TextView>(Resource.Id.noRatesTextView);
            this.swipeRefreshLayout = this.FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout);
            this.ratesRecyclerView = this.FindViewById<RecyclerView>(Resource.Id.ratesRecyclerView);
            this.ratesRecylerViewLayoutManager = new LinearLayoutManager(this);

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
                this.ratesRecyclerView.Visibility = ViewStates.Gone;
            }
            else
            {
                this.noRatesTextView.Visibility = ViewStates.Gone;
                this.ratesRecyclerView.Visibility = ViewStates.Visible;

                if (this.ratesRecyclerAdapter == null)
                {
                    this.ratesRecyclerAdapter = new RatesRecyclerAdapter(this, this.ViewModel.AllRates, i =>
                    {
                        Intent detailsIntent = new Intent(this, typeof(ExchangeRateDetailsActivity));
                        detailsIntent.PutExtra(Helpers.AndroidConstants.SelectedRateCode, this.ViewModel.AllRates[i].CurrencyCode);
                        this.StartActivity(detailsIntent);
                    });

                    this.ratesRecyclerView.SetAdapter(this.ratesRecyclerAdapter);
                    this.ratesRecyclerView.SetLayoutManager(this.ratesRecylerViewLayoutManager);
                }
                else
                {
                    this.ratesRecyclerAdapter.UpdateDataSet(this.ViewModel.AllRates);
                }
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