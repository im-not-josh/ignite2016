package nz.co.fendustries.xtrade;

import javax.inject.Singleton;

import dagger.Component;
import nz.co.fendustries.xtrade.activities.ExchangeRateDetailsActivity;
import nz.co.fendustries.xtrade.fragments.AllExchangeRatesFragment;
import nz.co.fendustries.xtrade.presenters.AllExchangeRatesPresenter;
import nz.co.fendustries.xtrade.presenters.ExchangeRateDetailsPresenter;
import nz.co.fendustries.xtrade.services.BaseService;
import nz.co.fendustries.xtrade.services.RateService;

/**
 * Created by joshuafenemore on 21/10/16.
 */
@Singleton
@Component(modules = {XtradeModule.class})
public interface XtradeComponent
{
    void inject(BaseService baseService);
    void inject(RateService rateService);
    void inject(ExchangeRateDetailsPresenter exchangeRateDetailsPresenter);
    void inject(AllExchangeRatesPresenter allExchangeRatesPresenter);
    void inject(ExchangeRateDetailsActivity exchangeRateDetailsActivity);
    void inject(AllExchangeRatesFragment allExchangeRatesFragment);
}