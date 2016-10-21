package nz.co.fendustries.xtrade.presenters;

import javax.inject.Inject;

import dagger.Lazy;
import nz.co.fendustries.xtrade.XtradeApplication;
import nz.co.fendustries.xtrade.domain.models.Rate;
import nz.co.fendustries.xtrade.domain.responseModels.RatesResponse;
import nz.co.fendustries.xtrade.domain.responseModels.RestRequestFailure;
import nz.co.fendustries.xtrade.interfaces.ExchangeRateDetailsViewContract;
import nz.co.fendustries.xtrade.interfaces.RestRequestWithResultCallback;
import nz.co.fendustries.xtrade.repository.XtradeRepository;
import nz.co.fendustries.xtrade.services.RateService;

/**
 * Created by joshuafenemore on 21/10/16.
 */
public class ExchangeRateDetailsPresenter
{
    private ExchangeRateDetailsViewContract exchangeRateDetailsViewContract;
    private Rate selectedRate;

    @Inject
    Lazy<XtradeRepository> xtradeRepositoryLazy;

    @Inject
    Lazy<RateService> rateServiceLazy;

    public ExchangeRateDetailsPresenter(XtradeApplication xtradeApplication)
    {
        xtradeApplication.getXtradeComponent().inject(this);
    }

    public Rate getSelectedRate()
    {
        return selectedRate;
    }

    public void setExchangeRateDetailsViewContract(ExchangeRateDetailsViewContract exchangeRateDetailsViewContract, String selectedCode)
    {
        if (exchangeRateDetailsViewContract != null)
        {
            this.exchangeRateDetailsViewContract = exchangeRateDetailsViewContract;
            this.selectedRate = this.xtradeRepositoryLazy.get().getRateByCode(selectedCode);
            this.exchangeRateDetailsViewContract.updateView();
            this.exchangeRateDetailsViewContract.showRefreshingView(false);
        }
        else
        {
            this.selectedRate = null;
            this.exchangeRateDetailsViewContract = null;
        }
    }

    public void refreshRate()
    {
        if (this.selectedRate != null)
        {
            this.exchangeRateDetailsViewContract.showRefreshingView(true);
            this.rateServiceLazy.get().getAllRateByCode(this.selectedRate.getCurrencyCode(), new RestRequestWithResultCallback<RatesResponse>()
            {
                @Override
                public void onCompletedWithResult(RatesResponse response)
                {
                    selectedRate = response.getValue().get(0);
                    xtradeRepositoryLazy.get().updateRateViewModel(selectedRate);

                    if (exchangeRateDetailsViewContract != null)
                    {
                        exchangeRateDetailsViewContract.showRefreshingView(false);
                        exchangeRateDetailsViewContract.showRefreshMessage(exchangeRateDetailsViewContract.getRefreshSuccessMessage());
                        exchangeRateDetailsViewContract.updateView();
                    }
                }

                @Override
                public void onError(RestRequestFailure restRequestFailure)
                {
                    if (exchangeRateDetailsViewContract != null)
                    {
                        exchangeRateDetailsViewContract.showRefreshingView(false);
                        exchangeRateDetailsViewContract.showRefreshMessage(exchangeRateDetailsViewContract.getRefreshFailedMessage());
                    }
                }
            });
        }
    }
}