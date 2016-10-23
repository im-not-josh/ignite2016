namespace Xtrade.Shared.Interfaces.ViewModels
{
    public interface IConvertedRateViewModel
    {
        string Code { get; set; }

        string ConvertedRate { get; set; }

        decimal SellRate { get; set; }

        string ImageSource { get; }
    }
}
