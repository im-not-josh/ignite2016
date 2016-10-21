package nz.co.fendustries.xtrade.domain.models;

import android.database.Cursor;
import android.provider.BaseColumns;

/**
 * Created by joshuafenemore on 21/10/16.
 */
public class Rate implements Comparable<Rate>
{
    private String currencyCode;
    private String description;
    private boolean isFeatured;
    private String smallestNote;
    private double buysNotes;
    private double buysCheques;
    private double buysPayments;
    private double sellsNotes;
    private String[] asbBuys;
    private String[] asbSells;
    private int rateId;

    public Rate(Cursor cursor)
    {
        if (cursor != null)
        {
            this.rateId =  cursor.getInt(cursor.getColumnIndex(RateModelContract.COLUMN_NAME_RATE_ID));
            this.currencyCode = cursor.getString(cursor.getColumnIndex(RateModelContract.COLUMN_NAME_RATE_CURRENCY_CODE));
            this.description = cursor.getString(cursor.getColumnIndex(RateModelContract.COLUMN_NAME_RATE_DESCRIPTION));
            this.isFeatured = cursor.getInt(cursor.getColumnIndex(RateModelContract.COLUMN_NAME_RATE_IS_FEATURED)) == 1;
            this.smallestNote = cursor.getString(cursor.getColumnIndex(RateModelContract.COLUMN_NAME_RATE_SMALLEST_NOTE));
            this.buysNotes = cursor.getDouble(cursor.getColumnIndex(RateModelContract.COLUMN_NAME_RATE_BUYS_NOTES));
            this.buysCheques = cursor.getDouble(cursor.getColumnIndex(RateModelContract.COLUMN_NAME_RATE_BUYS_CHEQUES));
            this.buysPayments = cursor.getDouble(cursor.getColumnIndex(RateModelContract.COLUMN_NAME_RATE_BUYS_PAYMENTS));
            this.sellsNotes = cursor.getDouble(cursor.getColumnIndex(RateModelContract.COLUMN_NAME_RATE_SELLS_NOTES));
        }
    }

    public int getRateId()
    {
        return rateId;
    }

    public void setRateId(int rateId)
    {
        this.rateId = rateId;
    }

    public String getCurrencyCode()
    {
        return currencyCode;
    }

    public void setCurrencyCode(String currencyCode)
    {
        this.currencyCode = currencyCode;
    }

    public String getDescription()
    {
        return description;
    }

    public void setDescription(String description)
    {
        this.description = description;
    }

    public boolean isFeatured()
    {
        return isFeatured;
    }

    public void setFeatured(boolean featured)
    {
        isFeatured = featured;
    }

    public String getSmallestNote()
    {
        return smallestNote;
    }

    public void setSmallestNote(String smallestNote)
    {
        this.smallestNote = smallestNote;
    }

    public double getBuysNotes()
    {
        return buysNotes;
    }

    public void setBuysNotes(double buysNotes)
    {
        this.buysNotes = buysNotes;
    }

    public double getBuysCheques()
    {
        return buysCheques;
    }

    public void setBuysCheques(double buysCheques)
    {
        this.buysCheques = buysCheques;
    }

    public double getBuysPayments()
    {
        return buysPayments;
    }

    public void setBuysPayments(double buysPayments)
    {
        this.buysPayments = buysPayments;
    }

    public double getSellsNotes()
    {
        return sellsNotes;
    }

    public void setSellsNotes(double sellsNotes)
    {
        this.sellsNotes = sellsNotes;
    }

    public String[] getAsbBuys()
    {
        return asbBuys;
    }

    public void setAsbBuys(String[] asbBuys)
    {
        this.asbBuys = asbBuys;
    }

    public String[] getAsbSells()
    {
        return asbSells;
    }

    public void setAsbSells(String[] asbSells)
    {
        this.asbSells = asbSells;
    }

    public int compareTo(Rate otherRate)
    {
        return otherRate.getCurrencyCode().compareToIgnoreCase(this.getCurrencyCode());
    }

    public static abstract class RateModelContract implements BaseColumns
    {
        public static final String TABLE_NAME = "RateModelContract";

        public static final String COLUMN_NAME_RATE_ID = "roomId";
        public static final String COLUMN_NAME_RATE_DESCRIPTION = "description";
        public static final String COLUMN_NAME_RATE_CURRENCY_CODE = "currencyCode";
        public static final String COLUMN_NAME_RATE_IS_FEATURED = "isFeatured";
        public static final String COLUMN_NAME_RATE_SMALLEST_NOTE = "smallestNote";
        public static final String COLUMN_NAME_RATE_BUYS_NOTES = "buysNotes";
        public static final String COLUMN_NAME_RATE_BUYS_CHEQUES = "buysCheques";
        public static final String COLUMN_NAME_RATE_BUYS_PAYMENTS = "buysPayments";
        public static final String COLUMN_NAME_RATE_SELLS_NOTES = "sellsNotes";
    }
}