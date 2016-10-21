package nz.co.fendustries.xtrade.adapters;

import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.RecyclerView;
import android.view.View;
import android.view.ViewGroup;

import java.util.List;
import java.util.Locale;

import nz.co.fendustries.xtrade.R;
import nz.co.fendustries.xtrade.adapters.viewHolders.RateViewHolder;
import nz.co.fendustries.xtrade.domain.models.Rate;
import nz.co.fendustries.xtrade.interfaces.RecyclerViewItemTapCallback;

/**
 * Created by joshuafenemore on 21/10/16.
 */
public class RatesRecyclerAdapter extends RecyclerView.Adapter<RateViewHolder>
{
    private java.text.NumberFormat currencyInstance = java.text.NumberFormat.getCurrencyInstance(Locale.US);

    private List<Rate> allRates;
    private AppCompatActivity activity;
    private RecyclerViewItemTapCallback recyclerViewItemTapCallback;

    public RatesRecyclerAdapter(AppCompatActivity activity, List<Rate> allRates, RecyclerViewItemTapCallback recyclerViewItemTapCallback)
    {
        this.allRates = allRates;
        this.activity = activity;
        this.recyclerViewItemTapCallback = recyclerViewItemTapCallback;
    }

    @Override
    public int getItemCount()
    {
        return this.allRates != null ? this.allRates.size() : 0;
    }


    @Override
    public RateViewHolder onCreateViewHolder(ViewGroup parent, int viewType)
    {
        View itemView = this.activity.getLayoutInflater().inflate(R.layout.recycler_item_rate, parent, false);
        return new RateViewHolder(itemView, this.recyclerViewItemTapCallback);
    }

    @Override
    public void onBindViewHolder(final RateViewHolder viewHolder, int position)
    {
        Rate rate = this.allRates != null ? this.allRates.get(position) : null;

        if (rate != null && viewHolder != null)
        {
            int flagID = this.activity.getResources().getIdentifier("flag_" + rate.getCurrencyCode().toLowerCase(), "drawable", this.activity.getPackageName());

            if (flagID == 0)
            {
                viewHolder.getForexFlagImageView().setVisibility(View.GONE);
            }
            else
            {
                viewHolder.getForexFlagImageView().setVisibility(View.VISIBLE);
                viewHolder.getForexFlagImageView().setImageDrawable(this.activity.getResources().getDrawable(flagID));
            }

            viewHolder.getForexCodeTextView().setText(rate.getCurrencyCode());
            viewHolder.getBuyRateTextView().setText(currencyInstance.format(rate.getBuysNotes()));
            viewHolder.getSellRateTextView().setText(currencyInstance.format(rate.getSellsNotes()));
        }
    }
}