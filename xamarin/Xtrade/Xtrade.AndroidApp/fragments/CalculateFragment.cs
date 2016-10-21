namespace Xtrade.AndroidApp.Fragments
{
    using System;
    using Activities;
    using Adapters;
    using Android.Content;
    using Android.OS;
    using Android.Support.V7.App;
    using Android.Support.V7.Widget;
    using Android.Text;
    using Android.Views;
    using Android.Views.InputMethods;
    using Android.Widget;
    using Shared.Interfaces.ViewModels;

    public class CalculateFragment : BaseFragment<ICalculateViewModel>
    {
        private EditText _valueEditText;
        private RecyclerView _ratesRecyclerView;
        private RecyclerView.LayoutManager _ratesRecylerViewLayoutManager;
        private ConvertedRatesRecyclerAdapter _ratesRecyclerAdapter;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            View view = inflater.Inflate(Resource.Layout.fragment_calculate, container, false);
            this._valueEditText = view.FindViewById<EditText>(Resource.Id.valueEditText);
            this._ratesRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.calculatedRatesRecyclerView);
            this._ratesRecylerViewLayoutManager = new LinearLayoutManager(this.Activity);
            this._ratesRecyclerView.SetLayoutManager(this._ratesRecylerViewLayoutManager);

            return view;
        }

        public override void OnResume()
        {
            base.OnResume();

            ((HomeActivity)this.Activity).SetActionBarTitle(this.GetString(Resource.String.calculateLabel));

            this.ViewModel.OnViewModelDataChanged += this.ViewModelDataChanged;
            this._valueEditText.TextChanged += this.ValueEditTextOnTextChanged;

            this.ViewModel.UpdateData("");
        }

        private void ValueEditTextOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            this.ViewModel.UpdateData(textChangedEventArgs.Text.ToString().Trim('$'));
        }

        public override void OnPause()
        {
            base.OnPause();

            InputMethodManager imm = (InputMethodManager)this.Activity.GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(this.Activity.Window.DecorView.WindowToken, 0);

            this._ratesRecyclerAdapter = null;
            this._valueEditText.TextChanged -= this.ValueEditTextOnTextChanged;
            this.ViewModel.OnViewModelDataChanged -= this.ViewModelDataChanged;
        }

        private void UpdateViews()
        {
            if (this._ratesRecyclerAdapter == null)
            {
                this._ratesRecyclerAdapter = new ConvertedRatesRecyclerAdapter((AppCompatActivity) this.Activity, this.ViewModel.ConvertedRateViewModels);
                this._ratesRecyclerView.SetAdapter(this._ratesRecyclerAdapter);
            }
            else
            {
                this._ratesRecyclerAdapter.NotifyDataSetChanged();
            }

            this._valueEditText.TextChanged -= this.ValueEditTextOnTextChanged;
            this._valueEditText.Text = this.ViewModel.DollarValue;
            this._valueEditText.SetSelection(this.ViewModel.DollarValue.Length);
            this._valueEditText.TextChanged += this.ValueEditTextOnTextChanged;
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