namespace Xtrade.Shared.Interfaces.Domain.ResponseModels
{
    using System.Collections.Generic;
    using Models;
    using Shared.Domain.Models;

    public interface IRatesWrapper
    {
        List<Rate> Value { get; set; }
    }
}