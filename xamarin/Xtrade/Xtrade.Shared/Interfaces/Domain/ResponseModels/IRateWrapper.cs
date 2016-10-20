namespace Xtrade.Shared.Interfaces.Domain.ResponseModels
{
    using Models;

    public interface IRateWrapper
    {
        IRate Value { get; set; }
    }
}