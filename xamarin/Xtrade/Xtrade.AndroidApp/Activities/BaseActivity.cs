namespace Xtrade.AndroidApp.Activities
{
    using Android.OS;
    using Android.Support.V7.App;
    using Android.Views;

    public class BaseActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                this.Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            }
        }
    }
}