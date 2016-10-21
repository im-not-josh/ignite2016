package nz.co.fendustries.xtrade;

import nz.co.fendustries.xtrade.domain.responseModels.RatesResponse;
import retrofit2.Call;
import retrofit2.http.GET;
import retrofit2.http.Query;

/**
 * Created by joshuafenemore on 21/10/16.
 */
public interface ApiInterface
{
    @GET("public/v1/exchange-rates")
    Call<RatesResponse> getRates();

    @GET("public/v1/exchange-rates")
    Call<RatesResponse> getRates(@Query("currencyCode") String code);
}