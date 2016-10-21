namespace Xtrade.AndroidApp.Activities
{
    using System;
    using Android.App;
    using Android.OS;
    using Android.Support.Design.Widget;
    using Android.Support.V4.View;
    using Android.Support.V4.Widget;
    using Android.Views;
    using Android.Widget;
    using Fragments;
    using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;
    using Toolbar = Android.Support.V7.Widget.Toolbar;

    [Activity(Label = "@string/applicationName", Theme = "@style/XtradeHome", MainLauncher = true, Icon = "@mipmap/ic_launcher")]
    public class HomeActivity : BaseActivity
    {
        private Toolbar _applicationToolbar;
        private DrawerLayout _drawerLayout;
        private NavigationView _navigationView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.activity_home);

            this._applicationToolbar = this.FindViewById<Toolbar>(Resource.Id.applicationToolbar);
            this._drawerLayout = this.FindViewById<DrawerLayout>(Resource.Id.drawerLayout);
            this._navigationView = this.FindViewById<NavigationView>(Resource.Id.navigationView);

            this.SetSupportActionBar(this._applicationToolbar);
            this.SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_navigation_menu);
            this.SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            this._navigationView.NavigationItemSelected += this.NavigationViewOnNavigationItemSelected;

            this.ShowAllExchangeRatesFragment();
        }

        public void SetActionBarTitle(string title)
        {
            this.SupportActionBar.Title = title;
        }

        private void NavigationViewOnNavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs navigationItemSelectedEventArgs)
        {
            navigationItemSelectedEventArgs.MenuItem.SetChecked(true);
            switch (navigationItemSelectedEventArgs.MenuItem.ItemId)
            {
                case Resource.Id.navigation_all_exchange_rates:
                    this._navigationView.SetCheckedItem(Resource.Id.navigation_all_exchange_rates);
                    this.ShowAllExchangeRatesFragment();
                    break;
                case Resource.Id.navigation_calculate:
                    this._navigationView.SetCheckedItem(Resource.Id.navigation_calculate);
                    this.ShowCalculateFragment();
                    break;
            }

            this._drawerLayout.CloseDrawer(GravityCompat.Start);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    this._drawerLayout.OpenDrawer(GravityCompat.Start);
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public override void OnBackPressed()
        {
            if (this._drawerLayout.IsDrawerOpen(GravityCompat.Start))
            {
                this._drawerLayout.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }
        
        private void ShowAllExchangeRatesFragment()
        {
            this.ClearFragmentBackStack();

            AllExchangeRatesFragment allExchangeRatesFragment = new AllExchangeRatesFragment();
            FragmentTransaction fragmentTransaction = this.SupportFragmentManager.BeginTransaction();
            fragmentTransaction.Replace(Resource.Id.containerFrameLayout, allExchangeRatesFragment, allExchangeRatesFragment.GetType().Name);
            fragmentTransaction.Commit();
        }

        private void ShowCalculateFragment()
        {
            this.ClearFragmentBackStack();

            CalculateFragment calculateFragment = new CalculateFragment();
            FragmentTransaction fragmentTransaction = this.SupportFragmentManager.BeginTransaction();
            fragmentTransaction.Replace(Resource.Id.containerFrameLayout, calculateFragment, calculateFragment.GetType().Name);
            fragmentTransaction.AddToBackStack(calculateFragment.GetType().Name);
            fragmentTransaction.Commit();
        }

        private void ClearFragmentBackStack()
        {
            for (int i = 0;i < this.SupportFragmentManager.BackStackEntryCount;i++)
            {
                this.SupportFragmentManager.PopBackStackImmediate();
            }
        }
    }
}