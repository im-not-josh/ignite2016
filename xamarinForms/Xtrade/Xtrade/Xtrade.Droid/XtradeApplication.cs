using Android.App;

namespace Xtrade.AndroidApp
{
    using System;
    using Android.OS;

#if DEBUG
    [Application(Debuggable = true, Theme = "@style/Xtrade")]
#else
    [Application(Debuggable = false, Theme = "@style/Xtrade")]
#endif
    public class XtradeApplication : Application, Application.IActivityLifecycleCallbacks
    {
        public XtradeApplication(IntPtr handle, Android.Runtime.JniHandleOwnership transfer) : base(handle, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            RegisterActivityLifecycleCallbacks(this);
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
        }

        public void OnActivityDestroyed(Activity activity)
        {
            
        }

        public void OnActivityPaused(Activity activity)
        {
            
        }

        public void OnActivityResumed(Activity activity)
        {
            
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
            
        }

        public void OnActivityStarted(Activity activity)
        {
            
        }
        public void OnActivityStopped(Activity activity)
        { }
    }
}