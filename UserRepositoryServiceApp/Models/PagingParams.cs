namespace UserRepositoryServiceApp.Models
{
    public class PagingParams
    {
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
        public OrderBy? OrderBy { get; set; }
        public Filter Filter { get; set; }
    }

    public enum OrderBy
    {
        DateModified,
        Locale
    }

    public class Filter
    {
        public FilterBy FilterBy { get; set; }  
        public string FilterValue { get; set; }  
    }

    public enum FilterBy
    {
        AdvertisingOptIn,
        CountryIsoCode
    }
}
