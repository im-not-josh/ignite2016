namespace Xtrade.Shared.Interfaces.Domain.Models
{
    using System;

    public interface IRate
    {
        DateTime DeviceModified { get; set; }

        string CurrencyCode { get; set; }

        string Description { get; set; }

        bool IsFeatured { get; set; }

        string SmallestNote { get; set; }

        double BuysNotes { get; set; }

        double BuysCheques { get; set; }

        double BuysPayments { get; set; }

        double SellsNotes { get; set; }
    }
}