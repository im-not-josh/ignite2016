namespace Xtrade.Shared.Domain.ResponseModels
{
    using Interfaces.Domain.ResponseModels;

    public class BaseResponse<T> : IBaseResponse<T>
    {
        public BaseResponse()
        {
            this.ErrorCode = 0;
            this.ErrorMessage = string.Empty;
        }

        public T Result { get; set; }

        public string ErrorMessage { get; set; }

        public int ErrorCode { get; set; }

        public bool IsValid()
        {
            return string.IsNullOrWhiteSpace(this.ErrorMessage) && this.ErrorCode == 0;
        }
    }
}