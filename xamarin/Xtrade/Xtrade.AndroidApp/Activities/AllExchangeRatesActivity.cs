namespace Xtrade.AndroidApp.Activities
{
    using Android.App;
    using Android.OS;
    using Android.Widget;
    using Shared.Interfaces.ViewModels;

    [Activity(Label = "@string/allRatesLabel", MainLauncher = true, Icon = "@drawable/icon")]
    public class AllExchangeRatesActivity : BaseActivity<IAllRatesViewModel>
    {
        private TextView noRatesTextView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.activityAllExchangeRates);

            this.noRatesTextView = this.FindViewById<TextView>(Resource.Id.noRatesTextView);
        }
    }
}