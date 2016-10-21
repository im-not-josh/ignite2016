package nz.co.fendustries.xtrade.adapters.viewHolders;

import android.support.v7.widget.RecyclerView;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

import nz.co.fendustries.xtrade.R;
import nz.co.fendustries.xtrade.interfaces.RecyclerViewItemTapCallback;

/**
 * Created by joshuafenemore on 21/10/16.
 */
public class RateViewHolder extends RecyclerView.ViewHolder
{
    private ImageView forexFlagImageView;
    private TextView forexCodeTextView;
    private TextView buyRateTextView;
    private TextView sellRateTextView;

    public RateViewHolder(View itemView, final RecyclerViewItemTapCallback recyclerViewItemTapCallback)
    {
        super(itemView);
        this.forexFlagImageView = (ImageView) itemView.findViewById(R.id.forexFlagImageView);
        this.forexCodeTextView = (TextView) itemView.findViewById(R.id.forexCodeTextView);
        this.buyRateTextView = (TextView) itemView.findViewById(R.id.buyRateTextView);
        this.sellRateTextView = (TextView) itemView.findViewById(R.id.sellRateTextView);

        itemView.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
                if (recyclerViewItemTapCallback != null)
                {
                    recyclerViewItemTapCallback.onItemTap(getAdapterPosition());
                }
            }
        });
    }

    public ImageView getForexFlagImageView()
    {
        return forexFlagImageView;
    }

    public TextView getForexCodeTextView()
    {
        return forexCodeTextView;
    }

    public TextView getBuyRateTextView()
    {
        return buyRateTextView;
    }

    public TextView getSellRateTextView()
    {
        return sellRateTextView;
    }
}