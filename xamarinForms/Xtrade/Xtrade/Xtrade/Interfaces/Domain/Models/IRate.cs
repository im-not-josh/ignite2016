namespace Xtrade.Shared.Interfaces.Domain.Models
{
    using System;
    using System.Collections.Generic;

    public interface IRate
    {
        DateTime DeviceModified { get; set; }

        string CurrencyCode { get; set; }

        string Description { get; set; }

        bool IsFeatured { get; set; }

        string SmallestNote { get; set; }

        decimal BuysNotes { get; set; }

        decimal BuysCheques { get; set; }

        decimal BuysPayments { get; set; }

        decimal SellsNotes { get; set; }

        IList<string> AsbBuys { get; set; }

        IList<string> AsbSells { get; set; }

        string ImageSource { get; }
    }
}