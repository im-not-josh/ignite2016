namespace Xtrade.AndroidApp.Fragments
{
    using System;
    using Activities;
    using Adapters;
    using Android.Content;
    using Android.OS;
    using Android.Support.V7.App;
    using Android.Support.V7.Widget;
    using Android.Views;
    using Android.Widget;
    using Shared.Interfaces.ViewModels;

    public class CalculateFragment : BaseFragment<IAllRatesViewModel>
    {
        private ImageView _forexFlagImageView;
        private RecyclerView _ratesRecyclerView;
        private RecyclerView.LayoutManager _ratesRecylerViewLayoutManager;
        private RatesRecyclerAdapter _ratesRecyclerAdapter;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            View view = inflater.Inflate(Resource.Layout.fragment_calculate, container, false);
            this._forexFlagImageView = view.FindViewById<ImageView>(Resource.Id.forexFlagImageView);
            this._ratesRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.ratesRecyclerView);
            this._ratesRecylerViewLayoutManager = new LinearLayoutManager(this.Activity);

            return view;
        }

        public override void OnResume()
        {
            base.OnResume();

            ((HomeActivity)this.Activity).SetActionBarTitle(this.GetString(Resource.String.allRatesLabel));

            this.ViewModel.OnViewModelDataChanged += this.ViewModelDataChanged;

            this.ViewModel.LoadData();
        }

        public override void OnPause()
        {
            base.OnPause();

            this.ViewModel.OnViewModelDataChanged -= this.ViewModelDataChanged;
        }

        private void UpdateViews()
        {
            if (this._ratesRecyclerAdapter == null)
            {
                this._ratesRecyclerAdapter = new RatesRecyclerAdapter((AppCompatActivity) this.Activity, this.ViewModel.AllRates, i =>
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
    }
}