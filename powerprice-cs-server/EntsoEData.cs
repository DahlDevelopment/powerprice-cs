using System;

namespace powerprice_cs_server
{
	public class EntsoEData
	{
		public EntsoETimeSeries TimeSeries { set; get; } = new();
		public string? Type { set; get; }
		public DateTime? CreatedDateTime { set; get; }		
		public Tuple<DateTime, DateTime>? TimeInterval { set; get; }
	}

	public class EntsoETimeSeries
	{
		public string? MRID { set; get; }
		public string? BusinessType { set; get; }
	}
}