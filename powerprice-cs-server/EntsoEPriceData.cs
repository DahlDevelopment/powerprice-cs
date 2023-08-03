namespace powerprice_cs_server
{
    public class EntsoEPriceData : IEntsoEData
	{
		private EntsoEPriceDataTimeSeries _timeSeries;
		private List<DateTime> _timestamps;
		private string? _rawData;		// the whole xml document

		public EntsoEPriceData()
		{
			_timeSeries = new(); 
			_timestamps = new();
        }

        public string Type
        {
            set => throw new NotImplementedException();
            get => throw new NotImplementedException();
        }
        public DateTime CreatedDateTime
        {
            set => throw new NotImplementedException();
            get => throw new NotImplementedException();
        }

        public List<double> PriceData
		{
			get => _timeSeries.PriceData;
			set => _timeSeries.PriceData = value;
		}

		public List<DateTime> Timestamps
		{
			get => _timestamps;
			set => _timestamps = value;
		}

		public string Currency
		{
			get => _timeSeries.Currency ?? "No Currency Available";
			set => _timeSeries.Currency = value;
		}

		public string MeasureUnit
		{
			get => _timeSeries.MeasureUnit ?? "No Measurement Unit Available";
			set => _timeSeries.MeasureUnit = value;
		}

		public string TimeResolution
		{
			get => _timeSeries.Resolution ?? "No Time Resolution Available";
			set => _timeSeries.Resolution = value;
		}

		public string RawData
		{
			get => _rawData ?? "No Raw Data Avaiable";
			set => _rawData = value;
		}

        public IEntsoETimeSeries TimeSeries
		{
			get => _timeSeries;
			set => _timeSeries = (EntsoEPriceDataTimeSeries)value;
		}


    }

    internal class EntsoEPriceDataTimeSeries : IEntsoETimeSeries
    {
		private string? _mRID;
		private string? _businessType;

        public string MRID
		{
			set => _mRID = value;
			get => _mRID ?? "No mRID Available";
		}

        public string BusinessType
		{
			set => _businessType = value;
			get => _businessType ?? "No Business Type Available";
		}

        public string? Resolution	{ set; get; }	// e.g. PT60M
		public string? MeasureUnit	{ set; get; }	// e.g. MWH
		public string? Currency		{ set; get; }   // e.g. EUR
		public List<double> PriceData { set; get; } = new();

    }
}

