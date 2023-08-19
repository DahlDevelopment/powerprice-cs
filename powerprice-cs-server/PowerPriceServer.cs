namespace powerprice_cs_server
{
    public class PowerPriceServer
	{

		public PowerPriceServer()
		{
			Console.WriteLine("Server Initiated");
		}

		public static EntsoEData? GetPriceData(EntsoEPriceDataBroker broker, DateOnly date, PriceDataOptions opts)
		{
			return broker.GetPriceData(date, opts);
		}
	}
}

