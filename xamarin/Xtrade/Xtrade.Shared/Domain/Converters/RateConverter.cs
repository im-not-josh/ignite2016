namespace Xtrade.Shared.Domain.Converters
{
    using System;
    using Interfaces.Domain.Models;
    using Newtonsoft.Json.Converters;

    public class RateConverter : CustomCreationConverter<IRate>
    {
        public override IRate Create(Type objectType)
        {
            return new Models.Rate();
        }
    }
}
