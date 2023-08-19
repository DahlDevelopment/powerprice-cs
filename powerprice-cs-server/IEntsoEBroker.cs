using System;
namespace powerprice_cs_server
{
	public interface IEntsoEBroker
	{
        EntsoEData? GetPriceData(DateOnly date);
		EntsoEData? GetPriceData(DateOnly date, PriceDataOptions options);
	}

	
}

