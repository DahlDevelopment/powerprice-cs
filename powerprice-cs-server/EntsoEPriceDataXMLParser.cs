using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;

[assembly: InternalsVisibleTo("entsoe-pricedata-sandbox")]
namespace powerprice_cs_server
{
    internal static class EntsoEPriceDataXMLParser
	{
		static public EntsoEPriceData ParsePriceData(string? rawData)
		{
            if(rawData is null)
            {
                throw new ArgumentNullException(rawData);
            }

			var xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(rawData));
			EntsoEPriceData priceData = new();
            EntsoEPriceDataPeriod period = new();
            priceData.AddPeriod(period);            

            XElement root = XElement.Load(xmlStream);
            //var xmlNamespace = root.Name.Namespace;

            period.PriceData = ParsePriceDataValues(root);

            return priceData;
		}

		static internal List<double> ParsePriceDataValues(XElement root)
		{
            var xmlNamespace = root.Name.Namespace;

            var timeSeries = root.Descendants(xmlNamespace + "TimeSeries");
            var points = timeSeries.Descendants(xmlNamespace + "Point");

            List<double> priceDataValues = new();
            foreach (var point in points)
            {
                var elem = point.Element(xmlNamespace + "price.amount");

                if (elem is not null)
                {
                    priceDataValues.Add((double)elem);
                }
            }

			return priceDataValues;
        }

        //static internal void ParsePriceDataTimeSeriesMeta(in XElement root, out EntsoEPriceData priceData)
        //{
        //    var xmlNamespace = root.Name.Namespace;
        //    var timeSeries = root.Descendants(xmlNamespace + "TimeSeries");

        //    var period = timeSeries.Descendants(xmlNamespace + "Period");
        //    var timeInterval = period.Descendants(xmlNamespace + "timeInterval");

        //    var timeIntervalStart = timeInterval.Elements(xmlNamespace + "start");
        //    var timeIntervalEnd = timeInterval.Elements(xmlNamespace + "end");

        //    priceData.TimeInterval.Start = DateTime.FromFileTimeUtc(timeIntervalStart.First().Value.ToString());
        //    priceData.TimeInterval.End = timeIntervalEnd;



        //}

    }
}

