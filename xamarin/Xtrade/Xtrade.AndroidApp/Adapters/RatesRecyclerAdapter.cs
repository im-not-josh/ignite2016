namespace Xtrade.AndroidApp.Adapters
{
    using System;
    using System.Collections.Generic;
    using Android.Support.V7.App;
    using Android.Support.V7.Widget;
    using Android.Views;
    using Shared.Interfaces.Domain.Models;
    using ViewHolders;

    public class RatesRecyclerAdapter : RecyclerView.Adapter
    {
        private IList<IRate> _allRates;
        private readonly AppCompatActivity _activity;
        private readonly Action<int> _itemClickAction;

        public RatesRecyclerAdapter(AppCompatActivity activity, IList<IRate> allRates, Action<int> itemClickAction)
        {
            this._allRates = allRates;
            this._activity = activity;
            this._itemClickAction = itemClickAction;
        }

        public void UpdateDataSet(IList<IRate> allRates)
        {
            this._allRates = allRates;
            this.NotifyDataSetChanged();
        }

        public override int ItemCount
        {
            get { return this._allRates != null ? this._allRates.Count : 0; }
        }

        public IRate this[int position]
        {
            get { return this._allRates[position]; }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.recycler_item_rate, parent, false);
            return new RateViewHolder(itemView, this._itemClickAction);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RateViewHolder viewHolder = holder as RateViewHolder;
            IRate rate = this._allRates != null ? this._allRates[position] : null;

            if (rate != null && viewHolder != null)
            {
                int flagID = this._activity.Resources.GetIdentifier("flag_" + rate.CurrencyCode.ToLower(), "drawable", this._activity.PackageName);

                if (flagID == 0)
                {
                    viewHolder.ForexFlagImageView.Visibility = ViewStates.Gone;
                }
                else
                {
                    viewHolder.ForexFlagImageView.Visibility = ViewStates.Visible;
                    viewHolder.ForexFlagImageView.SetImageDrawable(this._activity.Resources.GetDrawable(flagID));
                }

                viewHolder.CountryNameTextView.Text = rate.Description;
                viewHolder.ForexCodeTextView.Text = rate.CurrencyCode;
                viewHolder.Rate1TextView.Text = rate.BuysNotes.ToString("C0");
                viewHolder.Rate2TextView.Text = rate.BuysCheques.ToString("C0");
                viewHolder.Rate3TextView.Text = rate.SellsNotes.ToString("C0");
            }
        }
    }
}