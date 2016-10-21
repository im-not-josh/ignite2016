package nz.co.fendustries.xtrade.interfaces;

import nz.co.fendustries.xtrade.domain.responseModels.RestRequestFailure;

/**
 * Created by joshuafenemore on 12/10/16 2:51 PM.
 * <p>
 * The Rest Request With Result Callbacks Interface for the NZDF project
 */
public interface RestRequestWithResultCallback<T>
{
    void onCompletedWithResult(T response);

    void onError(RestRequestFailure restRequestFailure);
}