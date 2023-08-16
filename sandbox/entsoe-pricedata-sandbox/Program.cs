using System.Text;
using System.Xml;
using System.Xml.Linq;
using powerprice_cs_server;


// ---------- GET DATA ----------
// ------------------------------
// Get the entsoe api key
//string enstoeKeyFile = @".entsoe_key_secret";
//var line = File.ReadLines(enstoeKeyFile);

//// Initiate the server and get the data
//var broker = new EntsoEBroker(line.First());
//var server = new PowerPriceServer(broker);

//Options opts = new()
//{
//    Zone = Zones.NO4,
//    Date = DateOnly.FromDateTime(DateTime.Today),
//    DocumentType = DocumentTypes.A44
//};

//EntsoEPriceData data = (EntsoEPriceData)server.GetPriceData(DateOnly.FromDateTime(DateTime.Today), opts);

//// Save to xml file for later use if necessary
//var filePath = "rawdata.xml";
//File.WriteAllText(filePath, data.RawData);

var a = 1;
// ---------- GET DATA END ----------
// ----------------------------------

XmlDocument xmlDoc = new();
xmlDoc.Load("rawdata.xml");

// Need to reference the xml document namespace
// when searching it. Without that namespace reference
// nothing is found using LINQ XML.
// https://stackoverflow.com/a/46397001
var xmldata = new MemoryStream(Encoding.UTF8.GetBytes(File.ReadAllText("rawdata.xml")));
XElement root = XElement.Load(xmldata);
var timeSeries = root.Descendants(root.Name.Namespace + "TimeSeries");

var values = from point in timeSeries.Descendants(root.Name.Namespace + "Point")
             select (double)point.Element(root.Name.Namespace + "price.amount");

//IEnumerable<double> values = from val in root.Descendants(root.Name.Namespace + "Point")
//                             select (double)val.Element("price.amount");

EntsoEPriceData priceData = new();
priceData.TimeInterval = ParsePriceDataTimeInterval(root);

var b = 1;



TimeInterval ParsePriceDataTimeInterval(in XElement root)
{
    var xmlNamespace = root.Name.Namespace;
    var timeInterval = root.Descendants(xmlNamespace + "period.timeInterval");

    DateTime GetTimeIntervalDT(string period)
    {
        var timeIntervalPeriod = timeInterval.Elements(xmlNamespace + period).First().Value;
        return DateTime.Parse(timeIntervalPeriod).ToUniversalTime();
    }

    var timeIntervalStart = GetTimeIntervalDT("start");
    var timeIntervalEnd = GetTimeIntervalDT("end");

    return new TimeInterval(timeIntervalStart, timeIntervalEnd);
}

//static void ParsePriceDataTimeSeriesMeta(in XElement root, EntsoEPriceData priceData)
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