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

    [Activity(Label = "@string/exchangeRateDetails", Theme = "@style/Xtrade")]
    public class ExchangeRateDetailsActivity : BaseActivity<ISelectedRateViewModel>
    {
        private Toolbar _applicationToolbar;
        private SwipeRefreshLayout _swipeRefreshLayout;
        private TextView _forexCodeTextView;
        private TextView _forexCountryTextView;
        private ImageView _forexFlagImageView;
        private TextView _buyNotesTextView;
        private TextView _buyChequesTextView;
        private TextView _buyPaymentsTextView;
        private TextView _sellsNotesTextView;
        private TextView _smallestNotesTextViewextView;

        private string _selectedRateCode;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.SetContentView(Resource.Layout.activity_exchange_rate_details);

            this._applicationToolbar = this.FindViewById<Toolbar>(Resource.Id.applicationToolbar);
            this._swipeRefreshLayout = this.FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout);
            this._forexCodeTextView = this.FindViewById<TextView>(Resource.Id.forexCodeTextView);
            this._forexCountryTextView = this.FindViewById<TextView>(Resource.Id.forexCountryTextView);
            this._forexFlagImageView = this.FindViewById<ImageView>(Resource.Id.forexFlagImageView);
            this._buyNotesTextView = this.FindViewById<TextView>(Resource.Id.buyNotesTextView);
            this._buyChequesTextView = this.FindViewById<TextView>(Resource.Id.buyChequesTextView);
            this._buyPaymentsTextView = this.FindViewById<TextView>(Resource.Id.buyPaymentsTextView);
            this._sellsNotesTextView = this.FindViewById<TextView>(Resource.Id.sellsNotesTextView);
            this._smallestNotesTextViewextView = this.FindViewById<TextView>(Resource.Id.smallestNoteTextView);

            this.SetSupportActionBar(this._applicationToolbar);
            this.SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            Bundle bundle = this.Intent.Extras ?? savedInstanceState;

            if (bundle != null && bundle.ContainsKey(Helpers.AndroidConstants.SelectedRateCode))
            {
                this._selectedRateCode = bundle.GetString(Helpers.AndroidConstants.SelectedRateCode, "");

                if (string.IsNullOrWhiteSpace(this._selectedRateCode))
                {
                    this.Finish();
                }
            }
            else
            {
                this.Finish();
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    this.Finish();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            this.ViewModel.OnViewModelDataChanged += this.ViewModelDataChanged;
            this.ViewModel.OnRefreshError += this.ViewModelRefreshError;
            this.ViewModel.OnRefreshSuccess += this.ViewModelRefreshSuccess;

            this._swipeRefreshLayout.Refresh += this.SwipeRefreshLayoutOnRefresh;

            this._swipeRefreshLayout.Refreshing = true;
            this.ViewModel.LoadData(this._selectedRateCode);
        }
        
        protected override void OnPause()
        {
            base.OnPause();

            this.ViewModel.OnViewModelDataChanged -= this.ViewModelDataChanged;
            this.ViewModel.OnRefreshError -= this.ViewModelRefreshError;
            this.ViewModel.OnRefreshSuccess -= this.ViewModelRefreshSuccess;

            this._swipeRefreshLayout.Refresh -= this.SwipeRefreshLayoutOnRefresh;
        }

        protected override void OnSaveInstanceState(Bundle outgoingState)
        {
            outgoingState.PutString(Helpers.AndroidConstants.SelectedRateCode, this._selectedRateCode);
            base.OnSaveInstanceState(outgoingState);
        }

        private void UpdateViews()
        {
            this._swipeRefreshLayout.Refreshing = this.ViewModel.IsDataRefreshing;

            this._forexCodeTextView.Text = this.ViewModel.SelectedRate.CurrencyCode;
            this._forexCountryTextView.Text = this.ViewModel.SelectedRate.Description;

            int flagID = this.Resources.GetIdentifier("flag_" + this.ViewModel.SelectedRate.CurrencyCode.ToLower(), "drawable", this.PackageName);

            if (flagID == 0)
            {
                this._forexFlagImageView.Visibility = ViewStates.Gone;
            }
            else
            {
                this._forexFlagImageView.Visibility = ViewStates.Visible;
                this._forexFlagImageView.SetImageDrawable(this.Resources.GetDrawable(flagID));
            }

            this._buyNotesTextView.Text = this.ViewModel.SelectedRate.BuysNotes.ToString("C");
            this._buyChequesTextView.Text = this.ViewModel.SelectedRate.BuysCheques.ToString("C");
            this._buyPaymentsTextView.Text = this.ViewModel.SelectedRate.BuysPayments.ToString("C");
            this._sellsNotesTextView.Text = this.ViewModel.SelectedRate.SellsNotes.ToString("C");
            this._smallestNotesTextViewextView.Text = this.ViewModel.SelectedRate.SmallestNote;
        }

        private void SwipeRefreshLayoutOnRefresh(object sender, EventArgs eventArgs)
        {
            this._swipeRefreshLayout.Refreshing = true;
            this.ViewModel.RefreshSelectedRate();
        }

        private void ViewModelDataChanged(object sender, EventArgs eventArgs)
        {
            this.UpdateViews();
        }

        private void ViewModelRefreshError(object sender, string s)
        {
            this._swipeRefreshLayout.Refreshing = this.ViewModel.IsDataRefreshing;
            Snackbar.Make(this._swipeRefreshLayout, s, Snackbar.LengthLong).Show();
        }

        private void ViewModelRefreshSuccess(object sender, string s)
        {
            this._swipeRefreshLayout.Refreshing = this.ViewModel.IsDataRefreshing;
            Snackbar.Make(this._swipeRefreshLayout, s, Snackbar.LengthLong).Show();
        }
    }
}