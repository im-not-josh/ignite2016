namespace Xtrade.Shared.ViewModels
{
    using Interfaces.ViewModels;
    using MvvmHelpers;

    public class ConvertedRateViewModel : BaseViewModel, IConvertedRateViewModel
    {
        public string Code { get; set; }

        public string ImageSource { get { return "flag_" + this.Code.ToLower() + ".png"; } }

        private string _convertedRate;
        public string ConvertedRate {
            get { return this._convertedRate; }
            set { SetProperty(ref this._convertedRate, value); } }

        public decimal SellRate { get; set; }
    }
}
