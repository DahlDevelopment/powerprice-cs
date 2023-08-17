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

            XElement root = XElement.Load(xmlStream);
            var xmlNamespace = root.Name.Namespace;

            var xmlTimeSeriesRoot = root.Descendants(xmlNamespace + "TimeSeries").First();
            if (xmlTimeSeriesRoot is not null)
            {
                priceData.TimeSeries = ParsePriceDataTimeSeries(xmlTimeSeriesRoot);
            }

            return priceData;
		}

        static internal EntsoEPriceDataTimeSeries ParsePriceDataTimeSeries(XElement timeSeriesRoot)
        {
            EntsoEPriceDataTimeSeries timeSeries = new();

            var xmlNamespace = timeSeriesRoot.Name.Namespace;
                

            foreach(var xmlPeriod in timeSeriesRoot.Descendants(xmlNamespace + "Period"))
            {
                timeSeries.Periods.Add(ParsePriceDataPeriod(xmlPeriod));            
            }

            ParsePriceDataTimeSeriesMeta(timeSeriesRoot, ref timeSeries);

            return timeSeries;
        }

        static internal void ParsePriceDataTimeSeriesMeta(in XElement timeSeriesRoot, ref EntsoEPriceDataTimeSeries timeSeries)
        {
            var xmlNamespace = timeSeriesRoot.Name.Namespace;

            static string? ParseXmlStringElement(XNamespace xnamespace, XElement root, string ID)
            {
                string? ret = null;
                var tmp = root.Element(xnamespace + ID);
                if (tmp is not null)
                {
                    ret = tmp.Value.ToString();
                }

                return ret;
            }

            timeSeries.MRID = ParseXmlStringElement(xmlNamespace, timeSeriesRoot, "mRID");
            timeSeries.BusinessType = ParseXmlStringElement(xmlNamespace, timeSeriesRoot, "businessType");
            timeSeries.Currency = ParseXmlStringElement(xmlNamespace, timeSeriesRoot, "currency_Unit.name");
            timeSeries.MeasureUnit = ParseXmlStringElement(xmlNamespace, timeSeriesRoot, "price_Measure_Unit.name");
            timeSeries.CurveType = ParseXmlStringElement(xmlNamespace, timeSeriesRoot, "curveType");

        }

        static internal EntsoEPriceDataPeriod ParsePriceDataPeriod(XElement periodRoot)
        {
            return new()
            {
                TimeInterval = ParsePriceDataPeriodTimeInterval(periodRoot),
                Resolution = ParsePriceDataPeriodResolution(periodRoot),
                PriceData = ParsePriceDataValues(periodRoot)
            };
        }

		static internal List<double> ParsePriceDataValues(XElement root)
		{
            var xmlNamespace = root.Name.Namespace;

            var points = root.Descendants(xmlNamespace + "Point");

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

        static internal TimeInterval ParsePriceDataPeriodTimeInterval(in XElement periodRoot)
        {
            var xmlNamespace = periodRoot.Name.Namespace;
            var timeInterval = periodRoot.Descendants(xmlNamespace + "timeInterval");

            DateTime GetTimeIntervalDT(string period)
            {
                var timeIntervalPeriod = timeInterval.Elements(xmlNamespace + period).First().Value;
                return DateTime.Parse(timeIntervalPeriod).ToUniversalTime();
            }

            var timeIntervalStart = GetTimeIntervalDT("start");
            var timeIntervalEnd = GetTimeIntervalDT("end");

            return new TimeInterval(timeIntervalStart, timeIntervalEnd);
        }

        static internal string ParsePriceDataPeriodResolution(in XElement periodRoot)
        {
            var xmlNamespace = periodRoot.Name.Namespace;
            return periodRoot.Descendants(xmlNamespace + "resolution").First().Value;
        }

    }
}

