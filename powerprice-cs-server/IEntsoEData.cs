using System;

namespace powerprice_cs_server
{
	public interface IEntsoEData
	{
		public IEntsoETimeSeries TimeSeries { set; get; }
		public string Type { set; get; }
		public DateTime CreatedDateTime { set; get; }		
		public Tuple<DateTime, DateTime>? TimeInterval { set; get; }
	}

	public interface IEntsoETimeSeries
	{
		public string MRID { set; get; }
		public string BusinessType { set; get; }
	}
}