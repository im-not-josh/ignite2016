namespace Xtrade.AndroidApp.Activities
{
    using Android.App;
    using Android.OS;
    using Android.Views;
    using Android.Widget;
    using Shared.Interfaces.ViewModels;
    using Toolbar = Android.Support.V7.Widget.Toolbar;

    [Activity(Label = "@string/allRatesLabel", Theme = "@style/Xtrade", MainLauncher = true, Icon = "@drawable/icon")]
    public class AllExchangeRatesActivity : BaseActivity<IAllRatesViewModel>
    {
        private Toolbar applicationToolbar;
        private TextView noRatesTextView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.activityAllExchangeRates);

            this.applicationToolbar = this.FindViewById<Toolbar>(Resource.Id.applicationToolbar);
            this.noRatesTextView = this.FindViewById<TextView>(Resource.Id.noRatesTextView);

            this.SetSupportActionBar(this.applicationToolbar);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                this.Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            }
        }
    }
}