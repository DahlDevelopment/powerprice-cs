using System.Web;

namespace powerprice_cs_server
{

    public static class DocumentTypes
    {
        public static readonly string A44 = "A44";  // Price Document
    }

    public static class Zones
    {
        public static readonly string NO1 = "10YNO-1--------2";  // NO1 Eastern Norway
        public static readonly string NO2 = "10YNO-2--------T";  // NO2 Southern Norway
        public static readonly string NO3 = "10YNO-3--------J";  // NO3 Central Norway
        public static readonly string NO4 = "10YNO-4--------9";  // NO4 Northern Norway
        public static readonly string NO5 = "10Y1001A1001A48H";  // NO5 Western Norway
    }

    public struct Options
    {
        public string   DocumentType { set; get; } = DocumentTypes.A44;
        public string   Zone { set; get; } = Zones.NO4;
        public DateOnly Date { set; get; } = DateOnly.FromDateTime(DateTime.Now);

        public Options(DateOnly date, string documentType, string zone)
        {
            DocumentType = documentType;
            Zone = zone;
            Date = date;
        }

        public Options(DateOnly date, string documentType)   : this(date, documentType,     Zones.NO4) { }
        public Options(DateOnly date)                        : this(date, DocumentTypes.A44, Zones.NO4) { }
    }

    internal static class URLBuilder
    {
        public static class Headers
        {
            public static readonly string header = "https://web-api.tp.entsoe.eu/api";
            public static readonly string inDomainID = "In_Domain";
            public static readonly string outDomainID = "out_Domain";
            public static readonly string documentTypeID = "documentType";
            public static readonly string periodStartID = "periodStart";
            public static readonly string periodEndID = "periodEnd";
            public static readonly string timeInterval = "timeInterval";
            public static readonly string securityTokenID = "securityToken";
        }
    }

    internal static class RESTClient
    {
        private static readonly HttpClient _client = new();

        public static async Task<string> GetHttpRequest(string urlName, string securityToken, IDictionary<string, string> parameters)
        {
            var builder = new UriBuilder(urlName);
            var query = HttpUtility.ParseQueryString(builder.Query);

            foreach(var entry in parameters)
            {
                query[entry.Key] = entry.Value;
            }
            query[URLBuilder.Headers.securityTokenID] = securityToken;
            builder.Query = query.ToString();

            var url = builder.ToString();
            Console.WriteLine("Aggregated GET url: ");
            Console.WriteLine(url);

            HttpResponseMessage response = _client.GetAsync(url).Result;
            HttpContent content = response.Content;
            var xml = content.ReadAsStringAsync().Result;
            return xml;
        }
    }



    /// <summary>
    /// EntsoEPriceDataBroker: 
    ///     Implements IEntsoEBroker using the Entso-E REST API.
    /// </summary>
    /// <remarks>
    ///     Helper classes and structs defined above.
    /// </remarks>
    public class EntsoEPriceDataBroker : IEntsoEBroker
	{
        private static readonly HttpClient _httpClient = new();
        private readonly string _entsoeapi_key;
        public Options _options { get; set; } = new Options();

        public EntsoEPriceDataBroker(string entsoeapi_key)
		{
            _entsoeapi_key = entsoeapi_key;
            Console.WriteLine("Key: " + _entsoeapi_key);
        }

        ~EntsoEPriceDataBroker()
        {
            _httpClient.Dispose();
        }

        /// <summary>
        /// Pulls data for the given DateOnly date
        /// </summary>
        /// <returns>IEntsoEData Containing the data pulled from Entso-E</returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEntsoEData GetPriceData(DateOnly date)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>
            {
                { URLBuilder.Headers.documentTypeID, _options.DocumentType },
                { URLBuilder.Headers.inDomainID, _options.Zone },
                { URLBuilder.Headers.outDomainID, _options.Zone },
                { URLBuilder.Headers.periodStartID, date.ToString("yyyyMMdd") + "0000" },
                { URLBuilder.Headers.periodEndID, date.ToString("yyyyMMdd") + "2200" }
            };

            var res = RESTClient.GetHttpRequest(URLBuilder.Headers.header, _entsoeapi_key, parameters);
            if(res.IsCompletedSuccessfully)
            {
                Console.Write(res.Result.ToString());
            }

            var dummyData = new EntsoEPriceData();
            dummyData.PriceData = new List<double>{ 9,8,7,6,5,4,3,2,1};
            dummyData.Timestamps = new List<DateTime> { DateTime.Now, DateTime.Now.AddDays(1) };
            dummyData.Currency = "EUR";
            //dummyData.MeasureUnit = "MWH"; // Don't include to see how null is handled
            dummyData.TimeResolution = "PT60M";
            dummyData.RawData = res.Result.ToString();

            return dummyData;
        }

        public IEntsoEData GetPriceData(DateOnly date, Options options)
        {
            _options = options;
            return GetPriceData(date);
        }
    }
}

