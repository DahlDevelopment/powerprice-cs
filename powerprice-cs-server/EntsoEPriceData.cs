namespace powerprice_cs_server
{
    public class EntsoEPriceData : IEntsoEData<double>
	{
		private List<double> _priceData;
		private List<DateTime> _timestamps;
		private string? _currency;		// e.g. EUR
		private string? _measureUnit;	// e.g. MWH
		private string? _timeResolution;// e.g. PT60M
		private string? _rawData;		// the whole xml document

		public EntsoEPriceData()
		{
			_priceData = new();
			_timestamps = new();
        }

		public List<double> Data {
			get => _priceData;
			set => _priceData = value;
		}

		public List<DateTime> Timestamps {
			get => _timestamps;
			set => _timestamps = value;
		}

		public string Currency
		{
			get => _currency ?? "No Currency Available";
			set => _currency = value;
		}

		public string MeasureUnit
		{
			get => _measureUnit ?? "No Measurement Unit Available";
			set => _measureUnit = value;
		}

		public string TimeResolution
		{
			get => _timeResolution ?? "No Time Resolution Available";
			set => _timeResolution = value;
		}

		public string RawData {
			get => _rawData ?? "No Raw Data Avaiable";
			set => _rawData = value;
		}
		
    }
}

