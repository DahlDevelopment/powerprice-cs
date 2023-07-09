using System;
using System.Net.Http;


namespace powerprice_cs_server
{

    public class EntsoEBroker : IEntsoEBroker
	{
        private static readonly HttpClient _client = new();

        public EntsoEBroker()
		{

		}

        public IEntsoEData GetPriceData()
        {
            throw new NotImplementedException();
        }
    }
}

