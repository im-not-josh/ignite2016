package nz.co.fendustries.xtrade.interfaces;

/**
 * Created by joshuafenemore on 21/10/16.
 */
public interface AllExchangeRatesViewContract
{
    void showRefreshingView(boolean isRefreshing);

    void showNoRatesView();

    void showRatesView();

    void showRefreshMessage(String message);

    String getRefreshSuccessMessage();

    String getRefreshFailedMessage();
}