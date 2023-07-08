using System;
namespace powerprice_cs
{
	public interface IEntsoEBroker
	{
        IPriceData GetPriceData(/*date or other form of get-identifier*/);
	}
}

