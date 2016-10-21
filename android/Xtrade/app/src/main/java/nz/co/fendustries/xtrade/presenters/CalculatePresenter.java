package nz.co.fendustries.xtrade.presenters;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Locale;

import javax.inject.Inject;

import dagger.Lazy;
import nz.co.fendustries.xtrade.XtradeApplication;
import nz.co.fendustries.xtrade.domain.models.Rate;
import nz.co.fendustries.xtrade.domain.responseModels.RatesResponse;
import nz.co.fendustries.xtrade.domain.responseModels.RestRequestFailure;
import nz.co.fendustries.xtrade.domain.viewModels.ConvertedRateViewModel;
import nz.co.fendustries.xtrade.interfaces.AllExchangeRatesViewContract;
import nz.co.fendustries.xtrade.interfaces.CalculateViewContract;
import nz.co.fendustries.xtrade.interfaces.RestRequestWithResultCallback;
import nz.co.fendustries.xtrade.repository.XtradeRepository;
import nz.co.fendustries.xtrade.services.RateService;

/**
 * Created by joshuafenemore on 21/10/16.
 */
public class CalculatePresenter
{
    private CalculateViewContract calculateViewContract;
    private List<ConvertedRateViewModel> convertedRateViewModels;
    private double doubleDollarValue;
    private java.text.NumberFormat currencyInstance = java.text.NumberFormat.getCurrencyInstance(Locale.US);

    @Inject
    Lazy<XtradeRepository> xtradeRepositoryLazy;

    @Inject
    Lazy<RateService> rateServiceLazy;

    public CalculatePresenter(XtradeApplication xtradeApplication)
    {
        xtradeApplication.getXtradeComponent().inject(this);
        this.convertedRateViewModels = new ArrayList<>();
    }

    public String getDollarValue()
    {
        return String.format("$%.0f", this.doubleDollarValue);
    }

    public List<ConvertedRateViewModel> getConvertedRateViewModels()
    {
        return convertedRateViewModels;
    }

    public void setCalculateViewContract(CalculateViewContract calculateViewContract, String value)
    {
        if (calculateViewContract != null)
        {
            this.calculateViewContract = calculateViewContract;
            this.updateData(value);
        }
        else
        {
            this.calculateViewContract = null;
        }
    }

    public void updateData(String newValue)
    {
        if (this.convertedRateViewModels.size() == 0)
        {
            List<Rate> rates = this.xtradeRepositoryLazy.get().getAllRateModels();

            for (Rate rate : rates)
            {
                this.convertedRateViewModels.add(new ConvertedRateViewModel(rate.getCurrencyCode(), rate.getSellsNotes()));
            }

            Collections.sort(this.convertedRateViewModels);
        }

        try
        {
            this.doubleDollarValue = Double.parseDouble(newValue.replace("$",""));
        }
        catch (Exception ex)
        {
            this.doubleDollarValue = 0;
        }

        for (ConvertedRateViewModel convertedRateViewModel : this.convertedRateViewModels)
        {
            convertedRateViewModel.setConvertedRate(this.currencyInstance.format(convertedRateViewModel.getSellRate() * this.doubleDollarValue));
        }

        if (this.calculateViewContract != null)
        {
            this.calculateViewContract.updateView();
        }
    }
}