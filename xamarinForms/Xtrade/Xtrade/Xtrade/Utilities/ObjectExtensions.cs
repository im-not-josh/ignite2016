namespace Xtrade.Shared.Utilities
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Domain.Models;
    using Interfaces.Domain.Models;
    using Interfaces.ViewModels;
    using MvvmHelpers;

    public static class ObjectExtensions
    {
        public static IEnumerable<IRate> OrderedRatesList(this IEnumerable<Rate> listToOrder)
        {
            return listToOrder.OrderBy(i => i.CurrencyCode);
        }

        public static IEnumerable<IRate> OrderedRatesList(this IEnumerable<IRate> listToOrder)
        {
            return listToOrder.OrderBy(i => i.CurrencyCode).ToList();
        }

        public static IList<IConvertedRateViewModel> OrderedRatesList(this IEnumerable<IConvertedRateViewModel> listToOrder)
        {
            return listToOrder.OrderBy(i => i.Code).ToList();
        }
    }
}