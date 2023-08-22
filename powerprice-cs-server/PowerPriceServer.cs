using powerprice_cs_common;

namespace powerprice_cs_server
{
    public class PowerPriceServer
	{

		public PowerPriceServer()
		{
			Console.WriteLine("Server Initiated");
		}

		public static EntsoEData? GetPriceData(EntsoEPriceDataBroker broker, PriceDataOptions opts)
		{
			return broker.GetPriceData(opts);
		}
	}
}

