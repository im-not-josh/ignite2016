namespace Xtrade.Shared.Domain.Converters
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json.Converters;

    public class ListConverter<T> : CustomCreationConverter<IList<T>>
    {
        public override IList<T> Create(Type objectType)
        {
            return new List<T>();
        }
    }
}
