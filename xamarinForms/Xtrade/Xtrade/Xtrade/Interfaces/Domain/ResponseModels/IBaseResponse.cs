namespace Xtrade.Shared.Interfaces.Domain.ResponseModels
{
    public interface IBaseResponse<T>
    {
        T Result { get; set; }

        string ErrorMessage { get; set; }

        int ErrorCode { get; set; }

        bool IsValid();
    }
}