namespace Xtrade.Shared.Interfaces.Managers
{
    using System.Threading.Tasks;
    using Domain.ResponseModels;
    using Newtonsoft.Json;

    public interface IWebServiceManager
    {
        Task<IBaseResponse<T>> GetAndParse<T>(string url, JsonConverter[] customConverters);
    }
}