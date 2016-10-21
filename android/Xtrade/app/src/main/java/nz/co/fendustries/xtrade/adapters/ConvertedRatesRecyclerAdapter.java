package nz.co.fendustries.xtrade.adapters;

import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.RecyclerView;
import android.view.View;
import android.view.ViewGroup;

import java.util.List;

import nz.co.fendustries.xtrade.R;
import nz.co.fendustries.xtrade.adapters.viewHolders.ConvertedRateViewHolder;
import nz.co.fendustries.xtrade.domain.viewModels.ConvertedRateViewModel;

/**
 * Created by joshuafenemore on 21/10/16.
 */
public class ConvertedRatesRecyclerAdapter extends RecyclerView.Adapter<ConvertedRateViewHolder>
{
    private List<ConvertedRateViewModel> allRates;
    private AppCompatActivity activity;

    public ConvertedRatesRecyclerAdapter(AppCompatActivity activity, List<ConvertedRateViewModel> allRates)
    {
        this.allRates = allRates;
        this.activity = activity;
    }

    @Override
    public int getItemCount()
    {
        return this.allRates != null ? this.allRates.size() : 0;
    }


    @Override
    public ConvertedRateViewHolder onCreateViewHolder(ViewGroup parent, int viewType)
    {
        View itemView = this.activity.getLayoutInflater().inflate(R.layout.recycler_item_converted_rate, parent, false);
        return new ConvertedRateViewHolder(itemView);
    }

    @Override
    public void onBindViewHolder(final ConvertedRateViewHolder viewHolder, int position)
    {
        ConvertedRateViewModel rate = this.allRates != null ? this.allRates.get(position) : null;

        if (rate != null && viewHolder != null)
        {
            int flagID = this.activity.getResources().getIdentifier("flag_" + rate.getCode().toLowerCase(), "drawable", this.activity.getPackageName());

            if (flagID == 0)
            {
                viewHolder.getForexFlagImageView().setVisibility(View.GONE);
            }
            else
            {
                viewHolder.getForexFlagImageView().setVisibility(View.VISIBLE);
                viewHolder.getForexFlagImageView().setImageDrawable(this.activity.getResources().getDrawable(flagID));
            }

            viewHolder.getForexCodeTextView().setText(rate.getCode());
            viewHolder.getSellRateTextView().setText(rate.getConvertedRate());
        }
    }
}