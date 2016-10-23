using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Xtrade
{
	public class NavigationItem : ContentView
	{
	    public void OnNavigationItemSelected(NavigationItemSelectedEventArgs e)
	    {
	        NavigationItemSelected?.Invoke(this, e);
	    }

	    public event NavigationItemSelectedHandler NavigationItemSelected;

		public class NavigationItemSelectedEventArgs : EventArgs
		{
			public int Index { get; set; }
		}

	    public delegate void NavigationItemSelectedHandler(object sender, NavigationItemSelectedEventArgs args);

	}
}
