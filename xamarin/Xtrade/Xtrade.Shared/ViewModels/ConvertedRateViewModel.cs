namespace Xtrade.Shared.ViewModels
{
    using Interfaces.ViewModels;

    public class ConvertedRateViewModel : IConvertedRateViewModel
    {
        public string Code { get; set; }

        public string ConvertedRate { get; set; }

        public decimal SellRate { get; set; }
    }
}
