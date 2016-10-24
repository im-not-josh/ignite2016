using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Xtrade
{
    public partial class NavigationDrawer : ContentPage
    {
        private AndroidMainPage mainPage;

		public NavigationDrawer (AndroidMainPage mainPage)
		{
		    this.mainPage = mainPage;
			InitializeComponent ();

            NavigationItem.NavigationItemSelected += NavigationItemOnNavigationItemSelected;
        }

        private async void NavigationItemOnNavigationItemSelected(object sender, NavigationItem.NavigationItemSelectedEventArgs args)
        {
            await this.mainPage.NavigateAsync(args.Index);
        }
    }
}
