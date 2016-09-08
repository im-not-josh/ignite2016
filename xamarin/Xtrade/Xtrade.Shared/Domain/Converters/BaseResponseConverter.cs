namespace Xtrade.Shared.Domain.Converters
{
    using System;
    using Interfaces.Domain.ResponseModels;
    using Newtonsoft.Json.Converters;
    using ResponseModels;

    public class BaseResponseConverter<T> : CustomCreationConverter<IBaseResponse<T>>
    {
        public override IBaseResponse<T> Create(Type objectType)
        {
            return new BaseResponse<T>();
        }
    }
}
