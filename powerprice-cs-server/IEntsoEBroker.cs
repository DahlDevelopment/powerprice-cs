using System;
namespace powerprice_cs_server
{
	public interface IEntsoEBroker
	{
        IEntsoEData GetPriceData(/*date or other form of get-identifier*/);
	}
}

