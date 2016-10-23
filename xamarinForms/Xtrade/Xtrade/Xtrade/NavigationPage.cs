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
		}

	    public NavigationPage()
	    {
	        
	    }
	}
}
