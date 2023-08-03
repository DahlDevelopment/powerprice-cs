using System;
namespace powerprice_cs_server
{
	public interface IEntsoEBroker
	{
        IEntsoEData GetPriceData(DateOnly date);
		IEntsoEData GetPriceData(DateOnly date, Options options);
	}

	
}

