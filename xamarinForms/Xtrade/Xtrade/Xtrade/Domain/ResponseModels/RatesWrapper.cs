namespace Xtrade.Shared.Domain.ResponseModels
{
    using System.Collections.Generic;
    using Interfaces.Domain.Models;
    using Interfaces.Domain.ResponseModels;
    using Newtonsoft.Json;

    public class RatesWrapper : IRatesWrapper
    {
        [JsonProperty(PropertyName = "value")]
        public List<IRate> Value { get; set; }
    }
}
