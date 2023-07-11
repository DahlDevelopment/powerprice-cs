using System;

namespace powerprice_cs_server
{
	public interface IEntsoEData
	{
        public List<double> Data { set; get; }
		public List<DateTime> TimeStamps { set; get; }
	}
}

