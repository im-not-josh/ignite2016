namespace Xtrade.Shared.Interfaces.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Models;

    public interface IXtradeRepository
    {
        Task InsertRatesAsync(IList<IRate> newRates);
    }
}