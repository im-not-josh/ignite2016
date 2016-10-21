package nz.co.fendustries.xtrade.services;

import nz.co.fendustries.xtrade.XtradeApplication;
import nz.co.fendustries.xtrade.domain.responseModels.RatesResponse;
import nz.co.fendustries.xtrade.domain.responseModels.RestRequestFailure;
import nz.co.fendustries.xtrade.helpers.AndroidConstants;
import nz.co.fendustries.xtrade.interfaces.RestRequestWithResultCallback;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

/**
 * Created by joshuafenemore on 21/10/16.
 */
public class RateService extends BaseService
{
    public RateService(XtradeApplication xtradeApplication)
    {
        super(xtradeApplication);
        xtradeApplication.getXtradeComponent().inject(this);
    }

    public void getAllRates(final RestRequestWithResultCallback restRequestWithResultCallback)
    {
        if (restRequestWithResultCallback != null)
        {
            try
            {
                Call<RatesResponse> call = this.apiInterfaceLazy.get().getRates();
                call.enqueue(new Callback<RatesResponse>()
                {
                    @Override
                    public void onResponse(Call<RatesResponse> call, Response<RatesResponse> response)
                    {
                        if (response.isSuccessful())
                        {
                            restRequestWithResultCallback.onCompletedWithResult(response.body());
                        }
                        else
                        {
                            restRequestWithResultCallback.onError(parseRestRequestFailure(response));
                        }
                    }

                    @Override
                    public void onFailure(Call<RatesResponse> call, Throwable t)
                    {
                        restRequestWithResultCallback.onError(new RestRequestFailure(t.getMessage(), AndroidConstants.API_CONNECTIVITY_EXCEPTION));
                    }
                });
            }
            catch (Exception e)
            {
                restRequestWithResultCallback.onError(new RestRequestFailure(AndroidConstants.UNEXPECTED_ERROR_MESSAGE + e.getMessage(), AndroidConstants.UNKNOWN_API_EXCEPTION));
            }
        }
    }

    public void getAllRateByCode(String code, final RestRequestWithResultCallback restRequestWithResultCallback)
    {
        if (restRequestWithResultCallback != null)
        {
            try
            {
                Call<RatesResponse> call = this.apiInterfaceLazy.get().getRates(code);
                call.enqueue(new Callback<RatesResponse>()
                {
                    @Override
                    public void onResponse(Call<RatesResponse> call, Response<RatesResponse> response)
                    {
                        if (response.isSuccessful())
                        {
                            restRequestWithResultCallback.onCompletedWithResult(response.body());
                        }
                        else
                        {
                            restRequestWithResultCallback.onError(parseRestRequestFailure(response));
                        }
                    }

                    @Override
                    public void onFailure(Call<RatesResponse> call, Throwable t)
                    {
                        restRequestWithResultCallback.onError(new RestRequestFailure(t.getMessage(), AndroidConstants.API_CONNECTIVITY_EXCEPTION));
                    }
                });
            }
            catch (Exception e)
            {
                restRequestWithResultCallback.onError(new RestRequestFailure(AndroidConstants.UNEXPECTED_ERROR_MESSAGE + e.getMessage(), AndroidConstants.UNKNOWN_API_EXCEPTION));
            }
        }
    }
}