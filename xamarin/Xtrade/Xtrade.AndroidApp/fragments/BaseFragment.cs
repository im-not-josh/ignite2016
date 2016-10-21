namespace Xtrade.AndroidApp.Fragments
{
    using Android.OS;
    using Android.Support.V4.App;
    using Shared;

    public class BaseFragment<T> : Fragment
    {
        protected T ViewModel { get; private set; }

        public override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.ViewModel = BootStrapper.Resolve<T>();
        }
    }
}