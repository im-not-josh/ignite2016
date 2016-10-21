package nz.co.fendustries.xtrade;

import android.app.Application;

/**
 * Created by joshuafenemore on 21/10/16.
 */
public class XtradeApplication extends Application
{
    private XtradeComponent xtradeComponent;

    @Override
    public void onCreate()
    {
        super.onCreate();
        this.createComponent();
    }

    private void createComponent()
    {
        this.xtradeComponent = null;

        this.xtradeComponent = DaggerXtradeComponent.builder()
                .xtradeModule(new XtradeModule(this))
                .build();
    }

    public XtradeComponent getXtradeComponent()
    {
        return xtradeComponent;
    }
}