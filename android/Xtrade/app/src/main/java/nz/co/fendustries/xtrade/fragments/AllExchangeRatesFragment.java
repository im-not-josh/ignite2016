package nz.co.fendustries.xtrade.fragments;

import android.content.Intent;
import android.os.Bundle;
import android.support.design.widget.Snackbar;
import android.support.v4.app.Fragment;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import javax.inject.Inject;

import dagger.Lazy;
import nz.co.fendustries.xtrade.R;
import nz.co.fendustries.xtrade.XtradeApplication;
import nz.co.fendustries.xtrade.activities.ExchangeRateDetailsActivity;
import nz.co.fendustries.xtrade.activities.HomeActivity;
import nz.co.fendustries.xtrade.adapters.RatesRecyclerAdapter;
import nz.co.fendustries.xtrade.helpers.AndroidConstants;
import nz.co.fendustries.xtrade.interfaces.AllExchangeRatesViewContract;
import nz.co.fendustries.xtrade.interfaces.RecyclerViewItemTapCallback;
import nz.co.fendustries.xtrade.presenters.AllExchangeRatesPresenter;

/**
 * Created by joshuafenemore on 21/10/16.
 */
public class AllExchangeRatesFragment extends Fragment implements AllExchangeRatesViewContract
{
    private TextView noRatesTextView;
    private SwipeRefreshLayout swipeRefreshLayout;
    private RecyclerView ratesRecyclerView;
    private RecyclerView.LayoutManager ratesRecylerViewLayoutManager;
    private RatesRecyclerAdapter ratesRecyclerAdapter;

    @Inject
    Lazy<AllExchangeRatesPresenter> allExchangeRatesPresenterLazy;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
        super.onCreateView(inflater, container, savedInstanceState);
        ((XtradeApplication)this.getActivity().getApplication()).getXtradeComponent().inject(this);

        View view = inflater.inflate(R.layout.fragment_all_exchange_rates, container, false);

        this.noRatesTextView = (TextView) view.findViewById(R.id.noRatesTextView);
        this.swipeRefreshLayout = (SwipeRefreshLayout) view.findViewById(R.id.swipeRefreshLayout);
        this.ratesRecyclerView = (RecyclerView) view.findViewById(R.id.ratesRecyclerView);
        this.ratesRecylerViewLayoutManager = new LinearLayoutManager(this.getActivity());
        this.ratesRecyclerView.setLayoutManager(this.ratesRecylerViewLayoutManager);

        return view;
    }

    @Override
    public void onResume()
{
    super.onResume();

    ((HomeActivity)this.getActivity()).setActionBarTitle(this.getString(R.string.allRatesLabel));

    this.swipeRefreshLayout.setOnRefreshListener(new SwipeRefreshLayout.OnRefreshListener()
    {
        @Override
        public void onRefresh()
        {
            allExchangeRatesPresenterLazy.get().refreshRate();
        }
    });

    this.allExchangeRatesPresenterLazy.get().setAllExchangeRatesViewContract(this);
}

    @Override
    public void onPause()
    {
        super.onPause();

        this.allExchangeRatesPresenterLazy.get().setAllExchangeRatesViewContract(null);

        this.swipeRefreshLayout.setRefreshing(false);
        this.swipeRefreshLayout.setOnRefreshListener(null);
        this.swipeRefreshLayout.destroyDrawingCache();
        this.swipeRefreshLayout.clearAnimation();
    }

    @Override
    public void showRatesView()
    {
        this.getActivity().runOnUiThread(new Runnable()
        {
            @Override
            public void run()
            {
                noRatesTextView.setVisibility(View.GONE);
                ratesRecyclerView.setVisibility(View.VISIBLE);

                ratesRecyclerAdapter = new RatesRecyclerAdapter((AppCompatActivity) getActivity(), allExchangeRatesPresenterLazy.get().getAllRates(), new RecyclerViewItemTapCallback()
                {
                    @Override
                    public void onItemTap(int position)
                    {
                        Intent detailsIntent = new Intent(getActivity(), ExchangeRateDetailsActivity.class);
                        detailsIntent.putExtra(AndroidConstants.SELECTED_RATE_CODE, allExchangeRatesPresenterLazy.get().getAllRates().get(position).getCurrencyCode());
                        startActivity(detailsIntent);
                    }
                });

                ratesRecyclerView.setAdapter(ratesRecyclerAdapter);
            }
        });
    }

    @Override
    public void showRefreshingView(boolean isRefreshing)
    {
        this.swipeRefreshLayout.setRefreshing(isRefreshing);
    }

    @Override
    public String getRefreshSuccessMessage()
    {
        return this.isVisible() ? this.getString(R.string.rates_refreshed) : "";
    }

    @Override
    public String getRefreshFailedMessage()
    {
        return this.isVisible() ? this.getString(R.string.rates_refresh_fail) : "";
    }

    @Override
    public void showRefreshMessage(String message)
    {
        if (this.isVisible())
        {
            Snackbar.make(this.ratesRecyclerView, message, Snackbar.LENGTH_LONG).show();
        }
    }

    @Override
    public void showNoRatesView()
    {
        this.getActivity().runOnUiThread(new Runnable()
        {
            @Override
            public void run()
            {
                ratesRecyclerView.setVisibility(View.GONE);
                noRatesTextView.setVisibility(View.VISIBLE);
            }
        });
    }
}