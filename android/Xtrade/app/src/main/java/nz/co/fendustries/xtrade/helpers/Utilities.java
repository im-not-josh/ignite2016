package nz.co.fendustries.xtrade.helpers;

/**
 * Created by joshuafenemore on 21/10/16.
 */
public class Utilities
{
    public static boolean isNullOrWhiteSpace(String string)
    {
        return (string == null || string.length() == 0 || string.trim().length() == 0);
    }
}