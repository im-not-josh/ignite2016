namespace Xtrade.Shared.Interfaces.Domain.ResponseModels
{
    using System.Collections.Generic;
    using Models;

    public interface IRatesWrapper
    {
        List<IRate> Value { get; set; }
    }
}