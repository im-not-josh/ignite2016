using Android.App;

namespace Xtrade.AndroidApp
{
    using System;

#if DEBUG
    [Application(Debuggable = true, Theme = "@style/Xtrade")]
#else
    [Application(Debuggable = false, Theme = "@style/Xtrade")]
#endif
    public class XtradeApplication : Application
    {
        public XtradeApplication(IntPtr handle, Android.Runtime.JniHandleOwnership transfer) : base(handle, transfer)
        {
            Current = this;
        }

        public static XtradeApplication Current { get; private set; }
    }
}