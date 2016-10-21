package nz.co.fendustries.xtrade.interfaces;

/**
 * Created by joshuafenemore on 21/10/16.
 */
public interface ExchangeRateDetailsViewContract
{
    void showRefreshingView(boolean isRefreshing);

    void updateView();

    void showRefreshMessage(String message);

    String getRefreshSuccessMessage();

    String getRefreshFailedMessage();
}