using System;
using System.Collections.Generic;
using System.Text;

namespace Xtrade
{
    using Xamarin.Forms;

    public class MainPageiOS : TabbedPage
    {
        public MainPageiOS()
        {
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            Children.Add(new NavigationPage(new ExchangeRates()));
            Children.Add(new NavigationPage(new Calculate()));
        }

        
    }
}
