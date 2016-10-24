using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace Xtrade.iOS
{
    using Refractored.XamForms.PullToRefresh.iOS;

    // The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
		    var tint = UIColor.FromRGB(21,101,192);

		    UINavigationBar.Appearance.BarTintColor = tint;
            UINavigationBar.Appearance.TintColor = UIColor.White;
		    UIBarButtonItem.Appearance.TintColor = tint;
		    UITabBar.Appearance.TintColor = tint;
            
            PullToRefreshLayoutRenderer.Init();

			global::Xamarin.Forms.Forms.Init ();
			LoadApplication (new Xtrade.App ());

			return base.FinishedLaunching (app, options);
		}
	}
}
