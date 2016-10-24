package nz.co.fendustries.xtrade;

import android.util.Log;

import com.google.gson.FieldNamingPolicy;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.io.IOException;
import java.util.concurrent.TimeUnit;

import javax.inject.Singleton;

import dagger.Module;
import dagger.Provides;
import nz.co.fendustries.xtrade.presenters.AllExchangeRatesPresenter;
import nz.co.fendustries.xtrade.presenters.CalculatePresenter;
import nz.co.fendustries.xtrade.presenters.ExchangeRateDetailsPresenter;
import nz.co.fendustries.xtrade.repository.XtradeRepository;
import nz.co.fendustries.xtrade.services.RateService;
import okhttp3.Interceptor;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;
import okhttp3.ResponseBody;
import okhttp3.logging.HttpLoggingInterceptor;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

/**
 * Created by joshuafenemore on 21/10/16.
 */
@Module
public class XtradeModule
{
    private static String BASE_URL = "https://api.asb.co.nz/";
    private static final String AUTHENTICATION_TOKEN = "l7xx8573e82a108846e2a5d4bf7631d89e41";
    private static final int CONNECT_TIMEOUT_SECONDS = 5;
    private static final int READ_TIMEOUT_SECONDS = 120;
    private static final int WRITE_TIMEOUT_SECONDS = 120;
    private XtradeApplication xtradeApplication;

    public XtradeModule(XtradeApplication xtradeApplication)
    {
        this.xtradeApplication = xtradeApplication;
    }

    @Provides
    @Singleton
    public AllExchangeRatesPresenter provideAllExchangeRatesPresenter()
    {
        return new AllExchangeRatesPresenter(this.xtradeApplication);
    }

    @Provides
    @Singleton
    public ExchangeRateDetailsPresenter provideExchangeRateDetailsPresenter()
    {
        return new ExchangeRateDetailsPresenter(this.xtradeApplication);
    }

    @Provides
    @Singleton
    public CalculatePresenter provideCalculatePresenter()
    {
        return new CalculatePresenter(this.xtradeApplication);
    }

    @Provides
    @Singleton
    public XtradeRepository provideXtradeRepository()
    {
        return new XtradeRepository(this.xtradeApplication);
    }

    @Provides
    @Singleton
    public RateService provideRateService()
    {
        return new RateService(this.xtradeApplication);
    }

    @Provides
    @Singleton
    public ApiInterface provideApiInterface(Retrofit retrofit)
    {
        return retrofit.create(ApiInterface.class);
    }

    @Provides
    @Singleton
    public OkHttpClient provideOkHttpClient()
    {
        if (BuildConfig.DEBUG)
        {
            HttpLoggingInterceptor httpLoggingInterceptor = new HttpLoggingInterceptor();
            httpLoggingInterceptor.setLevel(HttpLoggingInterceptor.Level.BASIC);

            Interceptor interceptor = new Interceptor()
            {
                @Override
                public Response intercept(Chain chain) throws IOException
                {
                    Request.Builder requestBuilder = chain.request().newBuilder().header("apikey", AUTHENTICATION_TOKEN);

                    Request request = requestBuilder.build();

                    Log.d("Api Service Request", String.format("Calling: %s , with headers: %s", request.url(), request.headers()));

                    Response response = chain.proceed(request);

                    String responseBody = response.body().string();

                    Log.d("Api Service Response", String.format("Response: %s", responseBody));

                    return response.newBuilder()
                            .body(ResponseBody.create(response.body().contentType(), responseBody))
                            .build();
                }
            };

            return new OkHttpClient.Builder().connectTimeout(CONNECT_TIMEOUT_SECONDS, TimeUnit.SECONDS).readTimeout(READ_TIMEOUT_SECONDS, TimeUnit.SECONDS).writeTimeout(WRITE_TIMEOUT_SECONDS, TimeUnit.SECONDS).addInterceptor(interceptor).addNetworkInterceptor(httpLoggingInterceptor).build();
        }
        else
        {
            Interceptor interceptor = new Interceptor()
            {
                @Override
                public Response intercept(Chain chain) throws IOException
                {
                    Request.Builder requestBuilder = chain.request().newBuilder().header("apikey", AUTHENTICATION_TOKEN);

                    Request request = requestBuilder.build();

                    Response response = chain.proceed(request);

                    String responseBody = response.body().string();

                    return response.newBuilder()
                            .body(ResponseBody.create(response.body().contentType(), responseBody))
                            .build();
                }
            };

            return new OkHttpClient.Builder().connectTimeout(CONNECT_TIMEOUT_SECONDS, TimeUnit.SECONDS).readTimeout(READ_TIMEOUT_SECONDS, TimeUnit.SECONDS).writeTimeout(WRITE_TIMEOUT_SECONDS, TimeUnit.SECONDS).addInterceptor(interceptor).build();
        }
    }

    @Provides
    @Singleton
    public Retrofit provideRetrofit(Gson gson, OkHttpClient okHttpClient)
    {
        Retrofit.Builder retrofitBuilder = new Retrofit.Builder();
        retrofitBuilder.client(okHttpClient);
        retrofitBuilder.baseUrl(BASE_URL);
        retrofitBuilder.addConverterFactory(GsonConverterFactory.create(gson));

        return retrofitBuilder.build();
    }

    @Provides
    @Singleton
    public Gson provideGson()
    {
        return new GsonBuilder().create();
    }
}