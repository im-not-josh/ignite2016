namespace Xtrade.Shared.Domain.Converters
{
    using System;
    using Interfaces.Domain.ResponseModels;
    using Newtonsoft.Json.Converters;
    using ResponseModels;

    public class RatesWrapperConverter : CustomCreationConverter<IRatesWrapper>
    {
        public override IRatesWrapper Create(Type objectType)
        {
            return new RatesWrapper();
        }
    }
}
