using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Xtrade
{
	public class NavigationPage : Xamarin.Forms.NavigationPage
	{
		public NavigationPage (Page page) : base(page)
		{
		    Title = page.Title;
		    Icon = page.Icon;
            this.init();
        }

	    public NavigationPage()
	    {
	        this.init();
	    }

	    private void init()
	    {
	        if (Device.OS == TargetPlatform.iOS)
	        {
	            BarTextColor = Color.White;
	        }
	    }
	}
}
