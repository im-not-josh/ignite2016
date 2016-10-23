using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Xtrade.Droid
{
	[Activity (Label = "Xtrade", Icon = "@mipmap/ic_launcher", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

		    ToolbarResource = Resource.Layout.toolbar;

			global::Xamarin.Forms.Forms.Init (this, bundle);
            
            
			LoadApplication (new Xtrade.App ());
		}
	}
}

