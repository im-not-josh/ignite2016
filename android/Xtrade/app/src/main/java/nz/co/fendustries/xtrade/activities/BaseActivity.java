package nz.co.fendustries.xtrade.activities;

import android.os.Build;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.WindowManager;

/**
 * Created by joshuafenemore on 21/10/16.
 */
public class BaseActivity extends AppCompatActivity
{
    @Override
    protected  void onCreate(Bundle bundle)
    {
        super.onCreate(bundle);

        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.LOLLIPOP)
        {
            this.getWindow().addFlags(WindowManager.LayoutParams.FLAG_DRAWS_SYSTEM_BAR_BACKGROUNDS);
        }
    }
}