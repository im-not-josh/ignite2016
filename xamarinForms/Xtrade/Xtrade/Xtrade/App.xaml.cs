using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Xtrade
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

		    switch (Device.OS)
		    {
		        case TargetPlatform.Android:
                    MainPage = new Xtrade.MainPage();
                    break;
                default:
                    throw new NotImplementedException();
            }
			
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
