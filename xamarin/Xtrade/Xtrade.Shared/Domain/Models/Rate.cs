﻿namespace Xtrade.Shared.Domain.Models
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
        public decimal BuysNotes { get; set; }

        [JsonProperty(PropertyName = "buysCheques")]
        public decimal BuysCheques { get; set; }

        [JsonProperty(PropertyName = "buysPayments")]
        public decimal BuysPayments { get; set; }

        [JsonProperty(PropertyName = "sellsNotes")]
        public decimal SellsNotes { get; set; }

        [JsonProperty(PropertyName = "asbBuys")]
        [Ignore]
        public IList<string> AsbBuys { get; set; }

        [JsonProperty(PropertyName = "asbSells")]
        [Ignore]
        public IList<string> AsbSells { get; set; }
    }
}