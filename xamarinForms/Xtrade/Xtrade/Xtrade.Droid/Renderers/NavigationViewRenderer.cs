using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xtrade;
using Xtrade.Droid.Renderers;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Xamarin.Forms.Platform.Android;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(NavigationItem), typeof(NavigationViewRenderer))]
namespace Xtrade.Droid.Renderers
{
    public class NavigationViewRenderer : Xamarin.Forms.Platform.Android.ViewRenderer<NavigationItem, NavigationView>
    {
        private NavigationView navigationView;
        protected IMenuItem selectedItem;

        protected override void OnElementChanged(ElementChangedEventArgs<NavigationItem> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }

            var view = Inflate(Forms.Context, Resource.Layout.navigation_view, null);
            navigationView = view.JavaCast<NavigationView>();

            navigationView.NavigationItemSelected += NavigationViewOnNavigationItemSelected;
            SetNativeControl(navigationView);

            navigationView.SetCheckedItem(Resource.Id.navigation_all_exchange_rates);
        }

        public override void OnViewRemoved(View child)
        {
            base.OnViewRemoved(child);
            navigationView.NavigationItemSelected -= this.NavigationViewOnNavigationItemSelected;
        }

        private void NavigationViewOnNavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs navigationItemSelectedEventArgs)
        {
            if (selectedItem != null)
            {
                selectedItem.SetChecked(false);
            }

            navigationView.SetCheckedItem(navigationItemSelectedEventArgs.MenuItem.ItemId);
            selectedItem = navigationItemSelectedEventArgs.MenuItem;

            int id = 0;
            switch (navigationItemSelectedEventArgs.MenuItem.ItemId)
            {
                case Resource.Id.navigation_all_exchange_rates:
                    id = 0;
                    break;
                case Resource.Id.navigation_calculate:
                    id = 0;
                    break;
            }

            this.Element.OnNavigationItemSelected(new NavigationItem.NavigationItemSelectedEventArgs { Index = id });
        }
    }
}