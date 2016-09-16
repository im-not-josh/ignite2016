namespace Xtrade.Shared.Domain.Models
{
    using System;
    using System.Collections.Generic;
    using Interfaces.Domain.Models;
    using Newtonsoft.Json;
    using SQLite;

    public class Rate : IRate
    {
        [JsonIgnore]
        public DateTime DeviceModified { get; set; }

        [PrimaryKey, Indexed]
        [JsonProperty(PropertyName = "currencyCode")]
        public string CurrencyCode { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "isFeatured")]
        public bool IsFeatured { get; set; }

        [JsonProperty(PropertyName = "smallestNote")]
        public string SmallestNote { get; set; }

        [JsonProperty(PropertyName = "buysNotes")]
        public double BuysNotes { get; set; }

        [JsonProperty(PropertyName = "buysCheques")]
        public double BuysCheques { get; set; }

        [JsonProperty(PropertyName = "buysPayments")]
        public double BuysPayments { get; set; }

        [JsonProperty(PropertyName = "sellsNotes")]
        public double SellsNotes { get; set; }

        [JsonProperty(PropertyName = "asbBuys")]
        [Ignore]
        public IList<string> AsbBuys { get; set; }

        [JsonProperty(PropertyName = "asbSells")]
        [Ignore]
        public IList<string> AsbSells { get; set; }
    }
}