package nz.co.fendustries.xtrade.activities;

import android.os.Bundle;
import android.support.design.widget.Snackbar;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.widget.Toolbar;
import android.view.MenuItem;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

import java.util.Locale;

import javax.inject.Inject;

import dagger.Lazy;
import nz.co.fendustries.xtrade.R;
import nz.co.fendustries.xtrade.XtradeApplication;
import nz.co.fendustries.xtrade.helpers.AndroidConstants;
import nz.co.fendustries.xtrade.helpers.Utilities;
import nz.co.fendustries.xtrade.interfaces.ExchangeRateDetailsViewContract;
import nz.co.fendustries.xtrade.presenters.ExchangeRateDetailsPresenter;

/**
 * Created by joshuafenemore on 21/10/16.
 */
public class ExchangeRateDetailsActivity extends BaseActivity implements ExchangeRateDetailsViewContract
{
    private Toolbar applicationToolbar;
    private SwipeRefreshLayout swipeRefreshLayout;
    private TextView forexCodeTextView;
    private TextView forexCountryTextView;
    private ImageView forexFlagImageView;
    private TextView buyNotesTextView;
    private TextView buyChequesTextView;
    private TextView buyPaymentsTextView;
    private TextView sellsNotesTextView;
    private TextView smallestNotesText;

    private java.text.NumberFormat currencyInstance = java.text.NumberFormat.getCurrencyInstance(Locale.US);
    private String selectedRateCode;

    @Inject
    Lazy<ExchangeRateDetailsPresenter> exchangeRateDetailsPresenterLazy;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);

        ((XtradeApplication)this.getApplication()).getXtradeComponent().inject(this);
        this.setContentView(R.layout.activity_exchange_rate_details);

        this.applicationToolbar = (Toolbar) this.findViewById(R.id.applicationToolbar);
        this.swipeRefreshLayout = (SwipeRefreshLayout) this.findViewById(R.id.swipeRefreshLayout);
        this.forexCodeTextView = (TextView) this.findViewById(R.id.forexCodeTextView);
        this.forexCountryTextView = (TextView) this.findViewById(R.id.forexCountryTextView);
        this.forexFlagImageView = (ImageView) this.findViewById(R.id.forexFlagImageView);
        this.buyNotesTextView = (TextView) this.findViewById(R.id.buyNotesTextView);
        this.buyChequesTextView = (TextView) this.findViewById(R.id.buyChequesTextView);
        this.buyPaymentsTextView = (TextView) this.findViewById(R.id.buyPaymentsTextView);
        this.sellsNotesTextView = (TextView) this.findViewById(R.id.sellsNotesTextView);
        this.smallestNotesText = (TextView) this.findViewById(R.id.smallestNoteTextView);

        this.setSupportActionBar(this.applicationToolbar);
        this.getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        Bundle bundle = this.getIntent().getExtras() != null ? this.getIntent().getExtras() : savedInstanceState;

        if (bundle != null && bundle.containsKey(AndroidConstants.SELECTED_RATE_CODE))
        {
            this.selectedRateCode = bundle.getString(AndroidConstants.SELECTED_RATE_CODE, "");

            if (Utilities.isNullOrWhiteSpace(this.selectedRateCode))
            {
                this.finish();
            }
        }
        else
        {
            this.finish();
        }
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item)
    {
        switch (item.getItemId())
        {
            case android.R.id.home:
                this.finish();
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }
    }

    @Override
    protected void onResume()
    {
        super.onResume();

        this.swipeRefreshLayout.setOnRefreshListener(new SwipeRefreshLayout.OnRefreshListener()
        {
            @Override
            public void onRefresh()
            {
                exchangeRateDetailsPresenterLazy.get().refreshRate();
            }
        });

        this.exchangeRateDetailsPresenterLazy.get().setExchangeRateDetailsViewContract(this, this.selectedRateCode);
        this.swipeRefreshLayout.setRefreshing(true);
    }

    @Override
    protected void onPause()
    {
        super.onPause();

        this.exchangeRateDetailsPresenterLazy.get().setExchangeRateDetailsViewContract(null, null);

        this.swipeRefreshLayout.setRefreshing(false);
        this.swipeRefreshLayout.setOnRefreshListener(null);
        this.swipeRefreshLayout.destroyDrawingCache();
        this.swipeRefreshLayout.clearAnimation();
    }

    @Override
    protected void onSaveInstanceState(Bundle outgoingState)
    {
        outgoingState.putString(AndroidConstants.SELECTED_RATE_CODE, this.selectedRateCode);
        super.onSaveInstanceState(outgoingState);
    }

    @Override
    public void updateView()
    {
        this.forexCodeTextView.setText(this.exchangeRateDetailsPresenterLazy.get().getSelectedRate().getCurrencyCode());
        this.forexCountryTextView.setText(this.exchangeRateDetailsPresenterLazy.get().getSelectedRate().getDescription());

        int flagID = this.getResources().getIdentifier("flag_" + this.exchangeRateDetailsPresenterLazy.get().getSelectedRate().getCurrencyCode().toLowerCase(), "drawable", this.getPackageName());

        if (flagID == 0)
        {
            this.forexFlagImageView.setVisibility(View.GONE);;
        }
        else
        {
            this.forexFlagImageView.setVisibility(View.VISIBLE);
            this.forexFlagImageView.setImageDrawable(this.getResources().getDrawable(flagID));
        }

        this.buyNotesTextView.setText(currencyInstance.format(this.exchangeRateDetailsPresenterLazy.get().getSelectedRate().getBuysNotes()));
        this.buyChequesTextView.setText(currencyInstance.format(this.exchangeRateDetailsPresenterLazy.get().getSelectedRate().getBuysCheques()));
        this.buyPaymentsTextView.setText(currencyInstance.format(this.exchangeRateDetailsPresenterLazy.get().getSelectedRate().getBuysPayments()));
        this.sellsNotesTextView.setText(currencyInstance.format(this.exchangeRateDetailsPresenterLazy.get().getSelectedRate().getSellsNotes()));
        this.smallestNotesText.setText(this.exchangeRateDetailsPresenterLazy.get().getSelectedRate().getSmallestNote());
    }

    @Override
    public void showRefreshingView(boolean isRefreshing)
    {
        this.swipeRefreshLayout.setRefreshing(isRefreshing);
    }

    @Override
    public String getRefreshSuccessMessage()
    {
        return this.getString(R.string.rate_refreshed);
    }

    @Override
    public String getRefreshFailedMessage()
    {
        return this.getString(R.string.rate_refresh_fail);
    }

    @Override
    public void showRefreshMessage(String message)
    {
        Snackbar.make(this.swipeRefreshLayout, message, Snackbar.LENGTH_LONG).show();
    }
}