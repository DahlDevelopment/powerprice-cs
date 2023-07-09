using System;

namespace powerprice_cs_server
{
	public class EntsoEPriceData : IEntsoEData
	{
		private List<double> _priceData;
		private List<DateTime> _timeStamps;

		public EntsoEPriceData()
		{
			_priceData = new();
			_timeStamps = new();
        }

		public List<double> Data {
			get => _priceData;
			set => _priceData = value;
		}

		public List<DateTime> TimeStamps {
			get => _timeStamps;
			set => _timeStamps = value;
		}
    }
}

