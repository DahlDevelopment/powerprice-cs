using System;

namespace powerprice_cs_server
{
	public interface IEntsoEData<T>
	{
        public List<T> Data { set; get; }
		public List<DateTime> TimeStamps { set; get; }
		public string RawData { set; get; }
	}
}

