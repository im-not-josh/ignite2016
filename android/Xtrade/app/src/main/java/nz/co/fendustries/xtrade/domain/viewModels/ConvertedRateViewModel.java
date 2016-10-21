package nz.co.fendustries.xtrade.domain.viewModels;

/**
 * Created by joshuafenemore on 21/10/16.
 */
public class ConvertedRateViewModel
{
    private String code;
    private String convertedRate;
    private double sellRate;

    public String getCode()
    {
        return code;
    }

    public void setCode(String code)
    {
        this.code = code;
    }

    public String getConvertedRate()
    {
        return convertedRate;
    }

    public void setConvertedRate(String convertedRate)
    {
        this.convertedRate = convertedRate;
    }

    public double getSellRate()
    {
        return sellRate;
    }

    public void setSellRate(double sellRate)
    {
        this.sellRate = sellRate;
    }
}