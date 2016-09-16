namespace Xtrade.AndroidApp.Adapters.ViewHolders
{
    using System;
    using Android.Support.V7.Widget;
    using Android.Views;
    using Android.Widget;

    public class RateViewHolder : RecyclerView.ViewHolder
    {
        public ImageView ForexFlagImageView { get; set; }

        public TextView Rate1TextView { get; set; }

        public TextView Rate1HeaderTextView { get; set; }

        public TextView Rate2TextView { get; set; }

        public TextView Rate2HeaderTextView { get; set; }

        public TextView Rate3TextView { get; set; }

        public TextView Rate3HeaderTextView { get; set; }

        public TextView CountryNameTextView { get; set; }

        public TextView ForexCodeTextView { get; set; }

        public RateViewHolder(View itemView, Action<int> itemClickAction) : base(itemView)
        {
            this.ForexFlagImageView = itemView.FindViewById<ImageView>(Resource.Id.forexFlagImageView);
            this.Rate1TextView = itemView.FindViewById<TextView>(Resource.Id.rate1TextView);
            this.Rate1HeaderTextView = itemView.FindViewById<TextView>(Resource.Id.rate1HeaderTextView);
            this.Rate2TextView = itemView.FindViewById<TextView>(Resource.Id.rate2TextView);
            this.Rate2HeaderTextView = itemView.FindViewById<TextView>(Resource.Id.rate2HeaderTextView);
            this.Rate3TextView = itemView.FindViewById<TextView>(Resource.Id.rate3TextView);
            this.Rate3HeaderTextView = itemView.FindViewById<TextView>(Resource.Id.rate3HeaderTextView);
            this.CountryNameTextView = itemView.FindViewById<TextView>(Resource.Id.countryNameTextView);
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