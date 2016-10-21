namespace Xtrade.AndroidApp.Activities
{
    using Android.OS;
    using Shared;

    public class BaseViewModelActivity<T> : BaseActivity
    {
        protected T ViewModel { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.ViewModel = BootStrapper.Resolve<T>();
        }
    }
}