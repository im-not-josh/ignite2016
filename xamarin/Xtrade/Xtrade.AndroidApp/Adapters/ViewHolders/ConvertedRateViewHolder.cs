namespace Xtrade.AndroidApp.Adapters.ViewHolders
{
    using Android.Support.V7.Widget;
    using Android.Views;
    using Android.Widget;

    public class ConvertedRateViewHolder : RecyclerView.ViewHolder
    {
        public ImageView ForexFlagImageView { get; set; }

        public TextView ForexCodeTextView { get; set; }

        public TextView SellRateTextView { get; set; }

        public ConvertedRateViewHolder(View itemView) : base(itemView)
        {
            this.ForexFlagImageView = itemView.FindViewById<ImageView>(Resource.Id.forexFlagImageView);
            this.ForexCodeTextView = itemView.FindViewById<TextView>(Resource.Id.forexCodeTextView);
            this.SellRateTextView = itemView.FindViewById<TextView>(Resource.Id.sellRateTextView);
        }
    }
}