using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Xtrade
{
    using System.Threading.Tasks;

    public class AndroidMainPage : MasterDetailPage
	{
	    private Dictionary<int, NavigationPage> pages;

		public AndroidMainPage()
		{
			pages = new Dictionary<int, NavigationPage>();
            Master = new NavigationDrawer(this);

            pages.Add(0, new NavigationPage(new ExchangeRates()));
            pages.Add(1, new NavigationPage(new Calculate()));

		    Detail = pages[0];
		}

	    public async Task NavigateAsync(int index)
	    {
	        IsPresented = false;
	        NavigationPage newPage = pages[index];

	        if (Detail == newPage)
	        {
	            await newPage.Navigation.PopToRootAsync();
	        }

	        Detail = newPage;
	    }
	}
}
