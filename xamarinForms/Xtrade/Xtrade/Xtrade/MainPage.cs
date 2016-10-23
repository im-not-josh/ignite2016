using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Xtrade
{
	public class MainPage : MasterDetailPage
	{
	    private Dictionary<int, NavigationPage> pages;

		public MainPage()
		{
			pages = new Dictionary<int, NavigationPage>();
            Master = new NavigationDrawer(this);

            pages.Add(0, new NavigationPage(new ExchangeRates()));

		    Detail = pages[0];
		}
	}
}
