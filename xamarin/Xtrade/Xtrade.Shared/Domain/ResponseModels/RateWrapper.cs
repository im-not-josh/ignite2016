namespace Xtrade.Shared.Domain.ResponseModels
{
    using System.Collections.Generic;
    using Interfaces.Domain.Models;
    using Interfaces.Domain.ResponseModels;
    using Newtonsoft.Json;

    public class RateWrapper : IRateWrapper
    {
        [JsonProperty(PropertyName = "value")]
        public IRate Value { get; set; }
    }
}
