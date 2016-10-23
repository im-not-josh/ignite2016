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
        private MainPage mainPage;

        public NavigationDrawer()
        {
            
        }

		public NavigationDrawer (MainPage mainPage)
		{
		    this.mainPage = mainPage;
			InitializeComponent ();
        }
	}
}
