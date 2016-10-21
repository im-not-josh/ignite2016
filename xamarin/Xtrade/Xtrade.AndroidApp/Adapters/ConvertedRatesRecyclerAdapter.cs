namespace Xtrade.AndroidApp.Adapters
{
    using System;
    using System.Collections.Generic;
    using Android.Support.V7.App;
    using Android.Support.V7.Widget;
    using Android.Views;
    using Shared.ViewModels;
    using ViewHolders;

    public class ConvertedRatesRecyclerAdapter : RecyclerView.Adapter
    {
        private IList<ConvertedRateViewModel> _convertedRates;
        private readonly AppCompatActivity _activity;

        public ConvertedRatesRecyclerAdapter(AppCompatActivity activity, IList<ConvertedRateViewModel> convertedRates)
        {
            this._convertedRates = convertedRates;
            this._activity = activity;
        }

        public override int ItemCount
        {
            get { return this._convertedRates != null ? this._convertedRates.Count : 0; }
        }

        public ConvertedRateViewModel this[int position]
        {
            get { return this._convertedRates[position]; }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.recycler_item_converted_rate, parent, false);
            return new ConvertedRateViewHolder(itemView);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ConvertedRateViewHolder viewHolder = holder as ConvertedRateViewHolder;
            ConvertedRateViewModel rate = this._convertedRates != null ? this._convertedRates[position] : null;

            if (rate != null && viewHolder != null)
            {
                int flagID = this._activity.Resources.GetIdentifier("flag_" + rate.Code.ToLower(), "drawable", this._activity.PackageName);

                if (flagID == 0)
                {
                    viewHolder.ForexFlagImageView.Visibility = ViewStates.Gone;
                }
                else
                {
                    viewHolder.ForexFlagImageView.Visibility = ViewStates.Visible;
                    viewHolder.ForexFlagImageView.SetImageDrawable(this._activity.Resources.GetDrawable(flagID));
                }

                viewHolder.ForexCodeTextView.Text = rate.Code;
                viewHolder.SellRateTextView.Text = rate.ConvertedRate;
            }
        }
    }
}