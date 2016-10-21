package nz.co.fendustries.xtrade.activities;

import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.design.widget.NavigationView;
import android.support.v4.app.FragmentTransaction;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.widget.Toolbar;
import android.view.MenuItem;

import nz.co.fendustries.xtrade.R;
import nz.co.fendustries.xtrade.fragments.AllExchangeRatesFragment;
import nz.co.fendustries.xtrade.fragments.CalculateFragment;

/**
 * Created by joshuafenemore on 21/10/16.
 */
public class HomeActivity extends BaseActivity
{
    private Toolbar applicationToolbar;
    private DrawerLayout drawerLayout;
    private NavigationView navigationView;

    @Override
    protected void onCreate(Bundle bundle)
    {
        super.onCreate(bundle);

        this.setContentView(R.layout.activity_home);

        this.applicationToolbar = (Toolbar) this.findViewById(R.id.applicationToolbar);
        this.drawerLayout = (DrawerLayout) this.findViewById(R.id.drawerLayout);
        this.navigationView = (NavigationView) this.findViewById(R.id.navigationView);

        this.setSupportActionBar(this.applicationToolbar);
        this.getSupportActionBar().setHomeAsUpIndicator(R.drawable.ic_navigation_menu);
        this.getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        this.navigationView.setNavigationItemSelectedListener(new NavigationView.OnNavigationItemSelectedListener()
        {
            @Override
            public boolean onNavigationItemSelected(@NonNull MenuItem item)
            {
                item.setChecked(true);
                switch (item.getItemId())
                {
                    case R.id.navigation_all_exchange_rates:
                        showAllExchangeRatesFragment();
                        break;
                    case R.id.navigation_calculate:
                        showCalculateFragment();
                        break;
                }

                drawerLayout.closeDrawer(GravityCompat.START);
                return true;
            }
        });

        if (bundle == null)
        {
            this.showAllExchangeRatesFragment();
        }
    }

    public void setActionBarTitle(String title)
    {
        this.getSupportActionBar().setTitle(title);
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item)
    {
        switch (item.getItemId())
        {
            case android.R.id.home:
                this.drawerLayout.openDrawer(GravityCompat.START);
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }
    }

    @Override
    public void onBackPressed()
    {
        if (this.drawerLayout.isDrawerOpen(GravityCompat.START))
        {
            this.drawerLayout.closeDrawer(GravityCompat.START);
        }
        else
        {
            if (this.getSupportFragmentManager().getBackStackEntryCount() > 0)
            {
                this.getSupportFragmentManager().popBackStackImmediate();
                this.navigationView.setCheckedItem(R.id.navigation_all_exchange_rates);
            }
            else
            {
                super.onBackPressed();
            }
        }
    }

    private void showAllExchangeRatesFragment()
    {
        this.clearFragmentBackStack();

        this.navigationView.setCheckedItem(R.id.navigation_all_exchange_rates);
        AllExchangeRatesFragment allExchangeRatesFragment = new AllExchangeRatesFragment();
        FragmentTransaction fragmentTransaction = this.getSupportFragmentManager().beginTransaction();
        fragmentTransaction.replace(R.id.containerFrameLayout, allExchangeRatesFragment, allExchangeRatesFragment.getClass().getSimpleName());
        fragmentTransaction.commit();
    }

    private void showCalculateFragment()
    {
        this.clearFragmentBackStack();

        this.navigationView.setCheckedItem(R.id.navigation_calculate);
        CalculateFragment calculateFragment = new CalculateFragment();
        FragmentTransaction fragmentTransaction = this.getSupportFragmentManager().beginTransaction();
        fragmentTransaction.replace(R.id.containerFrameLayout, calculateFragment, calculateFragment.getClass().getSimpleName());
        fragmentTransaction.addToBackStack(calculateFragment.getClass().getSimpleName());
        fragmentTransaction.commit();
    }

    private void clearFragmentBackStack()
    {
        for (int i = 0;i < this.getSupportFragmentManager().getBackStackEntryCount();i++)
        {
            this.getSupportFragmentManager().popBackStackImmediate();
        }
    }
}