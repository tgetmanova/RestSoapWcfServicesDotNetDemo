using System;
using System.Collections.Generic;
using System.Linq;
using static UserRepositoryServiceApp.Models.FilterRule;
using static UserRepositoryServiceApp.Models.OrderRule;

namespace UserRepositoryServiceApp.Models
{
    // TODO Design and implement Paging
    public class PagingParams
    {
        public int? PageSize { get; set; }   
        public int? PageNumber { get; set; }
        public OrderBy? OrderBy { get; set; }
        public FilterBy? FilterBy { get; set; }
    }

    public class OrderRule
    {
        internal IEnumerable<T> Order<T, Tkey>(Func<T, Tkey> orderCriteria, IList<T> collection)
        {
            return collection.OrderBy(orderCriteria);
        }

        public enum OrderBy {
            DateModified,
            Locale
        }
    }

    public class FilterRule
    {
        public IEnumerable<T> Filter<T, Tkey>(Func<T, bool> filterCriteria, IList<T> collection)
        {
            return collection.Where(filterCriteria);
        }

        public enum FilterBy {
            AdvertisingOptIn,
            CountryIsoCode
        }
    }
}
