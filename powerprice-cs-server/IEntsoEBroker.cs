using System;
namespace powerprice_cs_server
{
	public interface IEntsoEBroker
	{
        IPriceData GetPriceData(/*date or other form of get-identifier*/);
	}
}

