package nz.co.fendustries.xtrade.presenters;

import java.util.Collections;
import java.util.List;

import javax.inject.Inject;

import dagger.Lazy;
import nz.co.fendustries.xtrade.XtradeApplication;
import nz.co.fendustries.xtrade.domain.models.Rate;
import nz.co.fendustries.xtrade.domain.responseModels.RatesResponse;
import nz.co.fendustries.xtrade.domain.responseModels.RestRequestFailure;
import nz.co.fendustries.xtrade.interfaces.AllExchangeRatesViewContract;
import nz.co.fendustries.xtrade.interfaces.RestRequestWithResultCallback;
import nz.co.fendustries.xtrade.repository.XtradeRepository;
import nz.co.fendustries.xtrade.services.RateService;

/**
 * Created by joshuafenemore on 21/10/16.
 */
public class AllExchangeRatesPresenter
{
    private AllExchangeRatesViewContract allExchangeRatesViewContract;
    private List<Rate> allRates;

    @Inject
    Lazy<XtradeRepository> xtradeRepositoryLazy;

    @Inject
    Lazy<RateService> rateServiceLazy;

    public AllExchangeRatesPresenter(XtradeApplication xtradeApplication)
    {
        xtradeApplication.getXtradeComponent().inject(this);
    }

    public List<Rate> getAllRates()
    {
        return allRates;
    }

    public void setAllExchangeRatesViewContract(AllExchangeRatesViewContract allExchangeRatesViewContract)
    {
        if (allExchangeRatesViewContract != null)
        {
            this.allExchangeRatesViewContract = allExchangeRatesViewContract;
            this.allRates = this.xtradeRepositoryLazy.get().getAllRateModels();
            Collections.sort(this.allRates);
            this.updateView();
            this.refreshRate();
        }
        else
        {
            this.allExchangeRatesViewContract = null;
        }
    }

    public void refreshRate()
    {
        this.allExchangeRatesViewContract.showRefreshingView(true);
        this.rateServiceLazy.get().getAllRates(new RestRequestWithResultCallback<RatesResponse>()
        {
            @Override
            public void onCompletedWithResult(RatesResponse response)
            {
                allRates = response.getValue();
                Collections.sort(allRates);
                xtradeRepositoryLazy.get().insertAllRates(response.getValue());

                if (allExchangeRatesViewContract != null)
                {
                    allExchangeRatesViewContract.showRefreshingView(false);
                    allExchangeRatesViewContract.showRefreshMessage(allExchangeRatesViewContract.getRefreshSuccessMessage());
                    updateView();
                }
            }

            @Override
            public void onError(RestRequestFailure restRequestFailure)
            {
                if (allExchangeRatesViewContract != null)
                {
                    allExchangeRatesViewContract.showRefreshingView(false);
                    allExchangeRatesViewContract.showRefreshMessage(allExchangeRatesViewContract.getRefreshFailedMessage());
                    updateView();
                }
            }
        });
    }

    private void updateView()
    {
        if (this.allRates == null || this.allRates.size() == 0)
        {
            this.allExchangeRatesViewContract.showNoRatesView();
        }
        else
        {
            this.allExchangeRatesViewContract.showRatesView();
        }
    }
}