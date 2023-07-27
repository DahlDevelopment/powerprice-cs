using System;
namespace powerprice_cs_server
{
	public interface IEntsoEBroker<T>
	{
        IEntsoEData<T> GetPriceData(DateOnly date);
		IEntsoEData<T> GetPriceData(DateOnly date, Options options);
	}

	
}

