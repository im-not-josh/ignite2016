package nz.co.fendustries.xtrade.repository;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

import nz.co.fendustries.xtrade.domain.models.Rate;

/**
 * Created by joshuafenemore on 21/10/16.
 */
public class XtradeSQLiteDatabaseHelper extends SQLiteOpenHelper
{
    private static final int DATABASE_VERSION = 1;
    private static final String DATABASE_NAME = "XtradeDatabase.db";

    private static final String DATABASE_CREATE_TABLE_RATE_MODEL = "CREATE TABLE " + Rate.RateModelContract.TABLE_NAME
            + "(" + Rate.RateModelContract._ID + " INTEGER PRIMARY KEY AUTOINCREMENT, "
            + Rate.RateModelContract.COLUMN_NAME_RATE_BUYS_CHEQUES + " DOUBLE, "
            + Rate.RateModelContract.COLUMN_NAME_RATE_BUYS_NOTES+ " DOUBLE, "
            + Rate.RateModelContract.COLUMN_NAME_RATE_BUYS_PAYMENTS + " DOUBLE, "
            + Rate.RateModelContract.COLUMN_NAME_RATE_CURRENCY_CODE + " TEXT, "
            + Rate.RateModelContract.COLUMN_NAME_RATE_DESCRIPTION + " TEXT, "
            + Rate.RateModelContract.COLUMN_NAME_RATE_IS_FEATURED + " INTEGER, "
            + Rate.RateModelContract.COLUMN_NAME_RATE_SELLS_NOTES + " DOUBLE, "
            + Rate.RateModelContract.COLUMN_NAME_RATE_SMALLEST_NOTE + " TEXT);";

    public XtradeSQLiteDatabaseHelper(Context context)
    {
        super(context, DATABASE_NAME, null, DATABASE_VERSION);
    }

    @Override
    public void onCreate(SQLiteDatabase database)
    {
        database.execSQL(DATABASE_CREATE_TABLE_RATE_MODEL);
    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
    {
        db.execSQL("DROP TABLE IF EXISTS " + Rate.RateModelContract.TABLE_NAME);
        onCreate(db);
    }

    @Override
    public void onDowngrade(SQLiteDatabase db, int oldVersion, int newVersion)
    {
        onUpgrade(db, oldVersion, newVersion);
    }
}
