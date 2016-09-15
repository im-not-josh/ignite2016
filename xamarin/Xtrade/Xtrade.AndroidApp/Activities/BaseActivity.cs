namespace Xtrade.AndroidApp.Activities
{
    using Android.OS;
    using Android.Support.V7.App;
    using Android.Views;
    using Shared;

    public class BaseActivity<T> : AppCompatActivity
    {
        protected T ViewModel { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.ViewModel = BootStrapper.Resolve<T>();

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                this.Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            }
        }
    }
}