using System;
namespace powerprice_cs_server
{
	public class PowerPriceServer
	{
		private IEntsoEBroker _broker;

		public PowerPriceServer(IEntsoEBroker broker)
		{
			_broker = broker;
			Console.WriteLine("Server Initiated");
		}

		public IEntsoEData GetPriceData(DateOnly date, Options opts)
		{

			return _broker.GetPriceData(date, opts);
		}
	}
}

