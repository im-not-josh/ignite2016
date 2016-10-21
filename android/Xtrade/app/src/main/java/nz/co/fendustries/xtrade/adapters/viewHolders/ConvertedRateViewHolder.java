package nz.co.fendustries.xtrade.adapters.viewHolders;

import android.support.v7.widget.RecyclerView;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

import nz.co.fendustries.xtrade.R;

/**
 * Created by joshuafenemore on 21/10/16.
 */
public class ConvertedRateViewHolder extends RecyclerView.ViewHolder
{
    private ImageView forexFlagImageView;
    private TextView forexCodeTextView;
    private TextView sellRateTextView;

    public ConvertedRateViewHolder(View itemView)
    {
        super(itemView);
        this.forexFlagImageView = (ImageView) itemView.findViewById(R.id.forexFlagImageView);
        this.forexCodeTextView = (TextView) itemView.findViewById(R.id.forexCodeTextView);
        this.sellRateTextView = (TextView) itemView.findViewById(R.id.sellRateTextView);
    }

    public ImageView getForexFlagImageView()
    {
        return forexFlagImageView;
    }

    public TextView getForexCodeTextView()
    {
        return forexCodeTextView;
    }

    public TextView getSellRateTextView()
    {
        return sellRateTextView;
    }
}