package nz.co.fendustries.xtrade.services;

import com.google.gson.Gson;

import java.io.IOException;
import java.lang.annotation.Annotation;

import javax.inject.Inject;

import dagger.Lazy;
import nz.co.fendustries.xtrade.ApiInterface;
import nz.co.fendustries.xtrade.XtradeApplication;
import nz.co.fendustries.xtrade.domain.responseModels.RestRequestFailure;
import nz.co.fendustries.xtrade.helpers.AndroidConstants;
import okhttp3.ResponseBody;
import retrofit2.Converter;
import retrofit2.Response;
import retrofit2.Retrofit;

/**
 * Created by joshuafenemore on 12/10/16.
 */
public class BaseService
{
    @Inject
    protected Lazy<Gson> gsonLazy;

    @Inject
    protected Lazy<ApiInterface> apiInterfaceLazy;

    @Inject
    protected Lazy<Retrofit> retrofitLazy;

    public BaseService(XtradeApplication xtradeApplication)
    {
        xtradeApplication.getXtradeComponent().inject(this);
    }

    protected RestRequestFailure parseRestRequestFailure(Response<?> response)
    {
        Converter<ResponseBody, RestRequestFailure> converter = retrofitLazy.get().responseBodyConverter(RestRequestFailure.class, new Annotation[0]);

        RestRequestFailure restRequestFailure;

        try
        {
            restRequestFailure = converter.convert(response.errorBody());
            restRequestFailure.setStatus(response.code());
        }
        catch (IOException e)
        {
            return new RestRequestFailure("Conversion Error: " + e.getMessage(), AndroidConstants.API_CONVERSION_EXCEPTION);
        }

        return restRequestFailure;
    }
}