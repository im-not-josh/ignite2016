namespace Xtrade.AndroidApp.Adapters.ViewHolders
{
    using System;
    using Android.Support.V7.Widget;
    using Android.Views;
    using Android.Widget;

    public class RateViewHolder : RecyclerView.ViewHolder
    {
        public ImageView ForexFlagImageView { get; set; }

        public TextView ForexCodeTextView { get; set; }

        public TextView BuyRateTextView { get; set; }

        public TextView SellRateTextView { get; set; }

        public RateViewHolder(View itemView, Action<int> itemClickAction) : base(itemView)
        {
            this.ForexFlagImageView = itemView.FindViewById<ImageView>(Resource.Id.forexFlagImageView);
            this.ForexCodeTextView = itemView.FindViewById<TextView>(Resource.Id.forexCodeTextView);
            this.BuyRateTextView = itemView.FindViewById<TextView>(Resource.Id.buyRateTextView);
            this.SellRateTextView = itemView.FindViewById<TextView>(Resource.Id.sellRateTextView);
            this.ForexCodeTextView = itemView.FindViewById<TextView>(Resource.Id.forexCodeTextView);

            if (itemClickAction != null)
            {
                itemView.Click += (sender, args) =>
                {
                    itemClickAction(this.AdapterPosition);
                };
            }
        }
    }
}