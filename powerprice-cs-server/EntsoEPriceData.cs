namespace powerprice_cs_server
{
    using PriceDataPeriods = List<EntsoEPriceDataPeriod?>;
    
    public class EntsoEPriceData : EntsoEData
    {
        public new EntsoEPriceDataTimeSeries TimeSeries { set; get; } = new();
        public string? RawData { set; get; }


        // Convenience Methods
        // ---------------------------------------------------------------------

        /// <summary>
        /// Generates a List of DateTimes, from timeInterval.Start to
        /// timeInterval.End (always included), spaced timeStepInMinutes apart.
        /// </summary>
        /// <param name="timeInterval"></param>
        /// <param name="timeStepInMinutes"></param>
        /// <returns></returns>
        public static List<DateTime> CalculateTimePoints(TimeInterval timeInterval, int timeStepInMinutes)
        {
            List<DateTime> dateTimes = new();

            for(var dt = timeInterval.Start; dt < timeInterval.End; dt = dateTimes.Last().AddMinutes(timeStepInMinutes))
            {
                dateTimes.Add(dt);
            }

            dateTimes.Add(timeInterval.End); // Inclusive last item

            return dateTimes;
        }

        /// <summary>
        /// Returns the EntsoEPriceData TimeSeries MeasureUnit (e.g. "MWH")
        /// </summary>
        public string? MeasureUnit
        {
            set => TimeSeries.MeasureUnit = value;
            get => TimeSeries.MeasureUnit ?? "No Measure Unit Set";
        }

        /// <summary>
        /// Returns the EntsoEPriceData TimeSeries Currency (e.g. "EUR")
        /// </summary>
        public string? Currency
        {
            set => TimeSeries.Currency = value;
            get => TimeSeries.Currency ?? "No Currency Set";
        }

        /// <summary>
        /// Returns the EntsoEPriceData TimeSeries CurveType (e.g. "A01")
        /// </summary>
        public string? CurveType
        {
            set => TimeSeries.CurveType = value;
            get => TimeSeries.CurveType ?? "No Curve Type Set";
        }

        /// <summary>
        /// Add a EntsoEPriceDataPeriod to the EntsoEPriceData TimeSeries
        /// </summary>
        /// <param name="period"></param>
        public void AddPeriod(EntsoEPriceDataPeriod period)
        {
            TimeSeries.Periods.Add(period);
        }

        /// <summary>
        /// Get all EntsoEPriceDataPeriods from the EntsoEPriceData TimeSeries
        /// </summary>
        /// <returns></returns>
        public PriceDataPeriods Periods { get => TimeSeries.Periods; }
    }


    public class EntsoEPriceDataTimeSeries : EntsoETimeSeries
    {
		public string? MeasureUnit	{ set; get; }	// e.g. MWH
		public string? Currency		{ set; get; }   // e.g. EUR
		public string? CurveType	{ set; get; }
        public PriceDataPeriods Periods { set; get; } = new();
    }

    public class EntsoEPriceDataPeriod  
    {       
        public TimeInterval? Tinterval { set; get; }
        public string? Resolution { set; get; } // e.g. PT60M
        public List<double>? PriceData { set; get; }
    }
}

