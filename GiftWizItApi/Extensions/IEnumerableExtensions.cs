using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> list, Func<T, TKey> propertySelector)
        {
            return list.GroupBy(propertySelector).Select(x => x.First());
        }
    }
}
