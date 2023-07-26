using System.Xml;
using powerprice_cs_server;

// Get the entsoe api key
string enstoeKeyFile = @".entsoe_key_secret";
var line = File.ReadLines(enstoeKeyFile);

// Initiate the server and get the data
var broker = new EntsoEBroker(line.First());
var server = new PowerPriceServer(broker);

Options opts = new()
{
    Zone = Zones.NO4,
    Date = DateOnly.FromDateTime(DateTime.Today),
    DocumentType = DocumentTypes.A44
};

EntsoEPriceData data = (EntsoEPriceData)server.GetPriceData(DateOnly.FromDateTime(DateTime.Today), opts);

// Save to xml file for later use if necessary
var filePath = "rawdata.xml";
File.WriteAllText(filePath, data.RawData);

XmlDocument xmlDoc = new XmlDocument();
xmlDoc.Load("rawdata.xml");

XmlNodeList points = xmlDoc.GetElementsByTagName("Point");


var a = 1;

//var parser = new EntsoEPriceDataXMLParser();