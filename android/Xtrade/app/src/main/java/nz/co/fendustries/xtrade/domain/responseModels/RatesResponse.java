package nz.co.fendustries.xtrade.domain.responseModels;

import java.util.List;

import nz.co.fendustries.xtrade.domain.models.Rate;

/**
 * Created by joshuafenemore on 21/10/16.
 */
public class RatesResponse
{
    private List<Rate> value;

    public List<Rate> getValue()
    {
        return value;
    }

    public void setValue(List<Rate> value)
    {
        this.value = value;
    }
}