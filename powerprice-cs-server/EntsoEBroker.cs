using System.Web;
using powerprice_cs_common;

namespace powerprice_cs_server
{
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

    public class RESTClient : IEntsoECommunicator
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

        public string? GetEntsoEPriceData(PriceDataOptions options, string api_key)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>
            {
                { URLBuilder.Headers.documentTypeID, options.DocumentType },
                { URLBuilder.Headers.inDomainID, options.Zone },
                { URLBuilder.Headers.outDomainID, options.Zone },
                { URLBuilder.Headers.periodStartID, options.Date.ToString("yyyyMMdd") + "0000" },
                { URLBuilder.Headers.periodEndID, options.Date.ToString("yyyyMMdd") + "2200" }
            };

            var res = GetHttpRequest(URLBuilder.Headers.header, api_key, parameters);
            if(res.IsCompletedSuccessfully)
            {
                return res.Result.ToString();
            }
            else
            {
                return null;
            }
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
        private readonly string _entsoeapi_key;
        private readonly IEntsoECommunicator _communicator;

        public EntsoEPriceDataBroker(string entsoeapi_key, IEntsoECommunicator communicator)
		{
            _entsoeapi_key = entsoeapi_key;
            _communicator = communicator;
        }

        /// <summary>
        /// Pulls data for the given DateOnly date
        /// </summary>
        /// <returns>IEntsoEData Containing the data pulled from Entso-E</returns>
        public EntsoEData? GetPriceData(PriceDataOptions options)
        {
            var result = _communicator.GetEntsoEPriceData(options, _entsoeapi_key);

            EntsoEData? priceData = null;
            if (result is not null)
            {
                priceData = EntsoEPriceDataXMLParser.ParsePriceData(result);
            }

            return priceData;
        }
    }
}

