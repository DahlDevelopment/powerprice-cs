using System;
using powerprice_cs_common;

namespace powerprice_cs_server
{
	public interface IEntsoEBroker
	{
		EntsoEData? GetPriceData(PriceDataOptions options);
	}

	public interface IEntsoECommunicator
	{
		public string? GetEntsoEPriceData(PriceDataOptions options, string api_key);
	}
}

