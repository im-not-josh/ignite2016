namespace Xtrade.Shared.Interfaces.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Models;
    using Shared.Domain.Models;

    public interface IXtradeRepository
    {
        Task InsertRatesAsync(IList<IRate> newRates);

        Task<IList<Rate>> GetAllRates();

        Task InsertRateAsync(IRate newRate);

        Task<Rate> GetRateByCode(string code);
    }
}