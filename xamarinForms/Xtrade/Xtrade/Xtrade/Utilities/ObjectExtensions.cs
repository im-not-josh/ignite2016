namespace Xtrade.Shared.Utilities
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Models;
    using Interfaces.Domain.Models;
    using Interfaces.ViewModels;

    public static class ObjectExtensions
    {
        public static IList<IRate> OrderedRatesList(this IEnumerable<Rate> listToOrder)
        {
            return listToOrder.OrderBy(i => i.CurrencyCode).OfType<IRate>().ToList();
        }

        public static IList<IRate> OrderedRatesList(this IEnumerable<IRate> listToOrder)
        {
            return listToOrder.OrderBy(i => i.CurrencyCode).ToList();
        }

        public static IList<IConvertedRateViewModel> OrderedRatesList(this IEnumerable<IConvertedRateViewModel> listToOrder)
        {
            return listToOrder.OrderBy(i => i.Code).ToList();
        }
    }
}