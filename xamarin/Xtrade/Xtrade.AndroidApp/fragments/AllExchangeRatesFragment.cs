namespace Xtrade.AndroidApp.Fragments
{
    using System;
    using Activities;
    using Adapters;
    using Android.Content;
    using Android.OS;
    using Android.Support.Design.Widget;
    using Android.Support.V4.Widget;
    using Android.Support.V7.App;
    using Android.Support.V7.Widget;
    using Android.Views;
    using Android.Widget;
    using Shared.Interfaces.ViewModels;

    public class AllExchangeRatesFragment : BaseFragment<IAllRatesViewModel>
    {
        private TextView _noRatesTextView;
        private SwipeRefreshLayout _swipeRefreshLayout;
        private RecyclerView _ratesRecyclerView;
        private RecyclerView.LayoutManager _ratesRecylerViewLayoutManager;
        private RatesRecyclerAdapter _ratesRecyclerAdapter;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            View view = inflater.Inflate(Resource.Layout.fragmnety_all_exchange_rates, container, false);

            this._noRatesTextView = view.FindViewById<TextView>(Resource.Id.noRatesTextView);
            this._swipeRefreshLayout = view.FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout);
            this._ratesRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.ratesRecyclerView);
            this._ratesRecylerViewLayoutManager = new LinearLayoutManager(this.Activity);

            this.Activity.ActionBar.SetTitle(Resource.String.allRatesLabel);

            return view;
        }

        public override void OnResume()
        {
            base.OnResume();

            this.ViewModel.OnViewModelDataChanged += this.ViewModelDataChanged;
            this.ViewModel.OnRefreshError += this.ViewModelRefreshError;
            this.ViewModel.OnRefreshSuccess += this.ViewModelRefreshSuccess;

            this._swipeRefreshLayout.Refresh += this.SwipeRefreshLayoutOnRefresh;

            this._swipeRefreshLayout.Refreshing = true;
            this.ViewModel.LoadData();
        }

        public override void OnPause()
        {
            base.OnPause();

            this.ViewModel.OnViewModelDataChanged -= this.ViewModelDataChanged;
            this.ViewModel.OnRefreshError -= this.ViewModelRefreshError;
            this.ViewModel.OnRefreshSuccess -= this.ViewModelRefreshSuccess;

            this._swipeRefreshLayout.Refreshing = false;
            this._swipeRefreshLayout.Refresh -= this.SwipeRefreshLayoutOnRefresh;
            this._swipeRefreshLayout.DestroyDrawingCache();
            this._swipeRefreshLayout.ClearAnimation();
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
                    this._ratesRecyclerAdapter = new RatesRecyclerAdapter((AppCompatActivity)this.Activity, this.ViewModel.AllRates, i =>
                    {
                        Intent detailsIntent = new Intent(this.Activity, typeof (ExchangeRateDetailsActivity));
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
            this.Activity.RunOnUiThread(() =>
            {
                if (this.IsVisible)
                {
                    this.UpdateViews();
                }
            });
        }

        private void ViewModelRefreshError(object sender, string s)
        {
            this.Activity.RunOnUiThread(() =>
            {
                if (this.IsVisible)
                {
                    this._swipeRefreshLayout.Refreshing = this.ViewModel.IsDataRefreshing;
                    Snackbar.Make(this._swipeRefreshLayout, s, Snackbar.LengthLong).Show();
                }
            });
        }

        private void ViewModelRefreshSuccess(object sender, string s)
        {
            this.Activity.RunOnUiThread(() =>
            {
                if (this.IsVisible)
                {
                    this._swipeRefreshLayout.Refreshing = this.ViewModel.IsDataRefreshing;
                    Snackbar.Make(this._swipeRefreshLayout, s, Snackbar.LengthLong).Show();
                }
            });
        }
    }
}