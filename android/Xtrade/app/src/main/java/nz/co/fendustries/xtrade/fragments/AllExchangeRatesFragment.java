package nz.co.fendustries.xtrade.fragments;

import android.content.Intent;
import android.os.Bundle;
import android.support.design.widget.Snackbar;
import android.support.v4.app.Fragment;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.LinearLayoutCompat;
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
    private TextView _noRatesTextView;
    private SwipeRefreshLayout _swipeRefreshLayout;
    private RecyclerView _ratesRecyclerView;
    private RecyclerView.LayoutManager _ratesRecylerViewLayoutManager;
    private RatesRecyclerAdapter _ratesRecyclerAdapter;

    @Inject
    Lazy<AllExchangeRatesPresenter> allExchangeRatesPresenterLazy;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
        super.onCreateView(inflater, container, savedInstanceState);
        ((XtradeApplication)this.getActivity().getApplication()).getXtradeComponent().inject(this);

        View view = inflater.inflate(R.layout.fragment_all_exchange_rates, container, false);

        this._noRatesTextView = (TextView) view.findViewById(R.id.noRatesTextView);
        this._swipeRefreshLayout = (SwipeRefreshLayout) view.findViewById(R.id.swipeRefreshLayout);
        this._ratesRecyclerView = (RecyclerView) view.findViewById(R.id.ratesRecyclerView);
        this._ratesRecylerViewLayoutManager = new LinearLayoutManager(this.getActivity());
        this._ratesRecyclerView.setLayoutManager(this._ratesRecylerViewLayoutManager);

        return view;
    }

    @Override
    public void onResume()
{
    super.onResume();

    ((HomeActivity)this.getActivity()).setActionBarTitle(this.getString(R.string.allRatesLabel));

    this._swipeRefreshLayout.setOnRefreshListener(new SwipeRefreshLayout.OnRefreshListener()
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

        this._swipeRefreshLayout.setRefreshing(false);
        this._swipeRefreshLayout.setOnRefreshListener(null);
        this._swipeRefreshLayout.destroyDrawingCache();
        this._swipeRefreshLayout.clearAnimation();
    }

    @Override
    public void showRatesView()
    {
        this.getActivity().runOnUiThread(new Runnable()
        {
            @Override
            public void run()
            {
                _noRatesTextView.setVisibility(View.GONE);
                _ratesRecyclerView.setVisibility(View.VISIBLE);

                _ratesRecyclerAdapter = new RatesRecyclerAdapter((AppCompatActivity) getActivity(), allExchangeRatesPresenterLazy.get().getAllRates(), new RecyclerViewItemTapCallback()
                {
                    @Override
                    public void onItemTap(int position)
                    {
                        Intent detailsIntent = new Intent(getActivity(), ExchangeRateDetailsActivity.class);
                        detailsIntent.putExtra(AndroidConstants.SELECTED_RATE_CODE, allExchangeRatesPresenterLazy.get().getAllRates().get(position).getCurrencyCode());
                        startActivity(detailsIntent);
                    }
                });

                _ratesRecyclerView.setAdapter(_ratesRecyclerAdapter);
            }
        });
    }

    @Override
    public void showRefreshingView(boolean isRefreshing)
    {
        this._swipeRefreshLayout.setRefreshing(isRefreshing);
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
            Snackbar.make(this._ratesRecyclerView, message, Snackbar.LENGTH_LONG).show();
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
                _ratesRecyclerView.setVisibility(View.GONE);
                _noRatesTextView.setVisibility(View.VISIBLE);
            }
        });
    }
}