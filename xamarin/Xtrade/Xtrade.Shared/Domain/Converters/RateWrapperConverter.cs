namespace Xtrade.Shared.Domain.Converters
{
    using System;
    using Interfaces.Domain.ResponseModels;
    using Newtonsoft.Json.Converters;
    using ResponseModels;

    public class RateWrapperConverter : CustomCreationConverter<IRateWrapper>
    {
        public override IRateWrapper Create(Type objectType)
        {
            return new RateWrapper();
        }
    }
}
