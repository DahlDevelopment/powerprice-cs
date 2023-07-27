namespace powerprice_cs_server
{
    public class PowerPriceServer
	{

		public PowerPriceServer()
		{
			Console.WriteLine("Server Initiated");
		}

		public static IEntsoEData<double> GetPriceData(EntsoEPriceDataBroker broker, DateOnly date, Options opts)
		{
			return broker.GetPriceData(date, opts);
		}
	}
}

