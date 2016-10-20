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

    [Activity(Label = "@string/applicationName", Theme = "@style/Xtrade", MainLauncher = true, Icon = "@mipmap/ic_launcher")]
    public class AllExchangeRatesActivity : BaseActivity<IAllRatesViewModel>
    {
        private Toolbar _applicationToolbar;
        private TextView _noRatesTextView;
        private SwipeRefreshLayout _swipeRefreshLayout;
        private RecyclerView _ratesRecyclerView;
        private RecyclerView.LayoutManager _ratesRecylerViewLayoutManager;
        private RatesRecyclerAdapter _ratesRecyclerAdapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.activity_all_exchange_rates);

            this._applicationToolbar = this.FindViewById<Toolbar>(Resource.Id.applicationToolbar);
            this._noRatesTextView = this.FindViewById<TextView>(Resource.Id.noRatesTextView);
            this._swipeRefreshLayout = this.FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout);
            this._ratesRecyclerView = this.FindViewById<RecyclerView>(Resource.Id.ratesRecyclerView);
            this._ratesRecylerViewLayoutManager = new LinearLayoutManager(this);

            this.SetSupportActionBar(this._applicationToolbar);
            this._applicationToolbar.SetTitle(Resource.String.allRatesLabel);
        }

        protected override void OnResume()
        {
            base.OnResume();

            this.ViewModel.OnViewModelDataChanged += this.ViewModelDataChanged;
            this.ViewModel.OnRefreshError += this.ViewModelRefreshError;
            this.ViewModel.OnRefreshSuccess += this.ViewModelRefreshSuccess;

            this._swipeRefreshLayout.Refresh += this.SwipeRefreshLayoutOnRefresh;

            this._swipeRefreshLayout.Refreshing = true;
            this.ViewModel.LoadData();
        }
        
        protected override void OnPause()
        {
            base.OnPause();

            this.ViewModel.OnViewModelDataChanged -= this.ViewModelDataChanged;
            this.ViewModel.OnRefreshError -= this.ViewModelRefreshError;
            this.ViewModel.OnRefreshSuccess -= this.ViewModelRefreshSuccess;

            this._swipeRefreshLayout.Refresh -= this.SwipeRefreshLayoutOnRefresh;
        }

        private void UpdateViews()
        {

            this._swipeRefreshLayout.Refreshing = this.ViewModel.IsDataRefreshing;

            if (this.ViewModel.AllRates == null || this.ViewModel.AllRates.Count == 0)
            {
                this._noRatesTextView.Visibility = ViewStates.Visible;
                this._ratesRecyclerView.Visibility = ViewStates.Gone;
            }
            else
            {
                this._noRatesTextView.Visibility = ViewStates.Gone;
                this._ratesRecyclerView.Visibility = ViewStates.Visible;

                if (this._ratesRecyclerAdapter == null)
                {
                    this._ratesRecyclerAdapter = new RatesRecyclerAdapter(this, this.ViewModel.AllRates, i =>
                    {
                        Intent detailsIntent = new Intent(this, typeof (ExchangeRateDetailsActivity));
                        detailsIntent.PutExtra(Helpers.AndroidConstants.SelectedRateCode, this.ViewModel.AllRates[i].CurrencyCode);
                        this.StartActivity(detailsIntent);
                    });

                    this._ratesRecyclerView.SetAdapter(this._ratesRecyclerAdapter);
                    this._ratesRecyclerView.SetLayoutManager(this._ratesRecylerViewLayoutManager);
                }
                else
                {
                    this._ratesRecyclerAdapter.UpdateDataSet(this.ViewModel.AllRates);
                }
            }
        }

        private void SwipeRefreshLayoutOnRefresh(object sender, EventArgs eventArgs)
        {
            this._swipeRefreshLayout.Refreshing = true;
            this.ViewModel.RefreshRates();
        }

        private void ViewModelDataChanged(object sender, EventArgs eventArgs)
        {
            this.RunOnUiThread(() =>
            {
                this.UpdateViews(); 
            });
        }

        private void ViewModelRefreshError(object sender, string s)
        {
            this.RunOnUiThread(() =>
            {
                this._swipeRefreshLayout.Refreshing = this.ViewModel.IsDataRefreshing;
                Snackbar.Make(this._swipeRefreshLayout, s, Snackbar.LengthLong).Show();
            });
        }

        private void ViewModelRefreshSuccess(object sender, string s)
        {
            this.RunOnUiThread(() =>
            {
                this._swipeRefreshLayout.Refreshing = this.ViewModel.IsDataRefreshing;
                Snackbar.Make(this._swipeRefreshLayout, s, Snackbar.LengthLong).Show();
            });
        }
    }
}