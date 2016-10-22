package nz.co.fendustries.xtrade.repository;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;

import java.util.ArrayList;
import java.util.List;

import nz.co.fendustries.xtrade.domain.models.Rate;

/**
 * Created by joshuafenemore on 21/10/16.
 */
public class XtradeRepository
{
    private XtradeSQLiteDatabaseHelper xtradeSQLiteDatabaseHelper;

    public XtradeRepository(Context context)
    {
        this.xtradeSQLiteDatabaseHelper = new XtradeSQLiteDatabaseHelper(context);
    }

    public void insertAllRates(List<Rate> rates)
    {
        if (rates != null && rates.size() > 0)
        {
            SQLiteDatabase database = this.xtradeSQLiteDatabaseHelper.getWritableDatabase();

            database.beginTransaction();
            database.delete(Rate.RateModelContract.TABLE_NAME, null, null);

            for (Rate rate : rates)
            {
                ContentValues rateModelDetails = new ContentValues();
                rateModelDetails.put(Rate.RateModelContract.COLUMN_NAME_RATE_BUYS_CHEQUES, rate.getBuysCheques());
                rateModelDetails.put(Rate.RateModelContract.COLUMN_NAME_RATE_BUYS_NOTES, rate.getBuysNotes());
                rateModelDetails.put(Rate.RateModelContract.COLUMN_NAME_RATE_BUYS_PAYMENTS, rate.getBuysPayments());
                rateModelDetails.put(Rate.RateModelContract.COLUMN_NAME_RATE_CURRENCY_CODE, rate.getCurrencyCode());
                rateModelDetails.put(Rate.RateModelContract.COLUMN_NAME_RATE_DESCRIPTION, rate.getDescription());
                rateModelDetails.put(Rate.RateModelContract.COLUMN_NAME_RATE_IS_FEATURED, rate.isFeatured() ? 1 : 0);
                rateModelDetails.put(Rate.RateModelContract.COLUMN_NAME_RATE_SMALLEST_NOTE, rate.getSmallestNote());
                rateModelDetails.put(Rate.RateModelContract.COLUMN_NAME_RATE_SELLS_NOTES, rate.getSellsNotes());

                database.insert(Rate.RateModelContract.TABLE_NAME, null, rateModelDetails);
            }

            database.setTransactionSuccessful();
            database.endTransaction();
            database.close();
        }
    }

    public void updateRateViewModel(Rate rate)
    {
        if (rate != null)
        {
            SQLiteDatabase database = this.xtradeSQLiteDatabaseHelper.getWritableDatabase();

            ContentValues rateModelDetails = new ContentValues();
            rateModelDetails.put(Rate.RateModelContract._ID, rate.getRateId());
            rateModelDetails.put(Rate.RateModelContract.COLUMN_NAME_RATE_BUYS_CHEQUES, rate.getBuysCheques());
            rateModelDetails.put(Rate.RateModelContract.COLUMN_NAME_RATE_BUYS_NOTES, rate.getBuysNotes());
            rateModelDetails.put(Rate.RateModelContract.COLUMN_NAME_RATE_BUYS_PAYMENTS, rate.getBuysPayments());
            rateModelDetails.put(Rate.RateModelContract.COLUMN_NAME_RATE_CURRENCY_CODE, rate.getCurrencyCode());
            rateModelDetails.put(Rate.RateModelContract.COLUMN_NAME_RATE_DESCRIPTION, rate.getDescription());
            rateModelDetails.put(Rate.RateModelContract.COLUMN_NAME_RATE_IS_FEATURED, rate.isFeatured() ? 1 : 0);
            rateModelDetails.put(Rate.RateModelContract.COLUMN_NAME_RATE_SMALLEST_NOTE, rate.getSmallestNote());
            rateModelDetails.put(Rate.RateModelContract.COLUMN_NAME_RATE_SELLS_NOTES, rate.getSellsNotes());

            database.update(Rate.RateModelContract.TABLE_NAME, rateModelDetails, Rate.RateModelContract.COLUMN_NAME_RATE_CURRENCY_CODE + " = ?", new String[] { rate.getCurrencyCode() });
        }
    }

    public List<Rate> getAllRateModels()
    {
        List<Rate> rateModels = new ArrayList<>();
        SQLiteDatabase database = this.xtradeSQLiteDatabaseHelper.getReadableDatabase();

        String[] detailProjection = {Rate.RateModelContract.COLUMN_NAME_RATE_SMALLEST_NOTE,
                Rate.RateModelContract.COLUMN_NAME_RATE_SELLS_NOTES,
                Rate.RateModelContract.COLUMN_NAME_RATE_IS_FEATURED,
                Rate.RateModelContract.COLUMN_NAME_RATE_DESCRIPTION,
                Rate.RateModelContract.COLUMN_NAME_RATE_BUYS_CHEQUES,
                Rate.RateModelContract.COLUMN_NAME_RATE_BUYS_NOTES,
                Rate.RateModelContract.COLUMN_NAME_RATE_BUYS_PAYMENTS,
                Rate.RateModelContract.COLUMN_NAME_RATE_CURRENCY_CODE,
                Rate.RateModelContract._ID};

        Cursor detailCursor = database.query(Rate.RateModelContract.TABLE_NAME, detailProjection, null, null, null, null, null);

        if (detailCursor != null)
        {
            if (detailCursor.moveToFirst())
            {
                while (!detailCursor.isAfterLast())
                {
                    Rate rate = new Rate(detailCursor);
                    rateModels.add(rate);
                    detailCursor.moveToNext();
                }
            }

            detailCursor.close();
            database.close();
        }

        return rateModels;
    }

    public Rate getRateByCode(String code)
    {
        Rate rate = null;
        SQLiteDatabase database = this.xtradeSQLiteDatabaseHelper.getReadableDatabase();

        String[] detailProjection = {Rate.RateModelContract.COLUMN_NAME_RATE_SMALLEST_NOTE,
                Rate.RateModelContract.COLUMN_NAME_RATE_SELLS_NOTES,
                Rate.RateModelContract.COLUMN_NAME_RATE_IS_FEATURED,
                Rate.RateModelContract.COLUMN_NAME_RATE_DESCRIPTION,
                Rate.RateModelContract.COLUMN_NAME_RATE_BUYS_CHEQUES,
                Rate.RateModelContract.COLUMN_NAME_RATE_BUYS_NOTES,
                Rate.RateModelContract.COLUMN_NAME_RATE_BUYS_PAYMENTS,
                Rate.RateModelContract.COLUMN_NAME_RATE_CURRENCY_CODE,
                Rate.RateModelContract._ID};

        Cursor detailCursor = database.query(Rate.RateModelContract.TABLE_NAME, detailProjection, Rate.RateModelContract.COLUMN_NAME_RATE_CURRENCY_CODE+ " = ?", new String[] { code }, null, null, null);

        if (detailCursor != null)
        {
            if (detailCursor.moveToFirst())
            {
                rate = new Rate(detailCursor);
            }

            detailCursor.close();
            database.close();
        }

        return rate;
    }
}