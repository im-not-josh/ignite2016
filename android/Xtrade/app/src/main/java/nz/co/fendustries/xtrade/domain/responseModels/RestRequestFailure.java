package nz.co.fendustries.xtrade.domain.responseModels;

/**
 * Created by joshuafenemore on 12/10/16.
 */

public class RestRequestFailure
{
    private String message;
    private int status;

    public RestRequestFailure()
    {
    }

    public RestRequestFailure(String message, int status)
    {
        this.message = message;
        this.status = status;
    }

    public String getMessage()
    {
        return message;
    }

    public void setMessage(String message)
    {
        this.message = message;
    }

    public int getStatus()
    {
        return this.status;
    }

    public void setStatus(int status)
    {
        this.status = status;
    }
}