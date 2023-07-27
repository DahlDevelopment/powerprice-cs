namespace powerprice_cs_server
{
    public class EntsoEPriceData : IEntsoEData<double>
	{
		private List<double> _priceData;
		private List<DateTime> _timeStamps;
		private string _rawData;

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

		public string RawData {
			get => _rawData;
			set => _rawData = value;
		}
		
    }
}

