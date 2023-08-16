using System;

namespace powerprice_cs_server
{
    public struct TimeInterval
    {
        public DateTime Start { set; get; }
        public DateTime End { set; get; }

        public TimeInterval(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
    }

    public class EntsoEData
	{
		public EntsoETimeSeries TimeSeries { set; get; } = new();
		public string? Type { set; get; }
		public DateTime? CreatedDateTime { set; get; }		
		public TimeInterval? TimeInterval { set; get; }
	}

	public class EntsoETimeSeries
	{
		public string? MRID { set; get; }
		public string? BusinessType { set; get; }
	}
}