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

// ---------- GET DATA END ----------
// ----------------------------------

XmlDocument xmlDoc = new();
xmlDoc.Load("rawdata.xml");

// Need to reference the xml document namespace
// when searching it. Without that namespace reference
// nothing is found using LINQ XML.
// https://stackoverflow.com/a/46397001
XElement root = XElement.Load("rawdata.xml");
var timeSeries = root.Descendants(root.Name.Namespace + "TimeSeries");

var values = from point in timeSeries.Descendants(root.Name.Namespace + "Point")
             select (double)point.Element(root.Name.Namespace + "price.amount");

//IEnumerable<double> values = from val in root.Descendants(root.Name.Namespace + "Point")
//                             select (double)val.Element("price.amount");


//var vals = values.ToList();
var a = 1;
//from item in purchaseOrder.Descendants("Item")
//select (string)item.Attribute("PartNumber");