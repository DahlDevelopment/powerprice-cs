using powerprice_cs_server;
using System.IO;
using System.Net.Http.Headers;
using System.Web;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Console.WriteLine(DateTime.Now.ToString(""));

string enstoeKeyFile = @".entsoe_key_secret";
var line = File.ReadLines(enstoeKeyFile);
Console.WriteLine("File Contents: " + line.First());

var broker = new EntsoEBroker(line.First());
var server = new PowerPriceServer(broker);

Options opts = new()
{
    Zone = Zones.NO4,
    Date = DateOnly.FromDateTime(DateTime.Today),
    DocumentType = DocumentTypes.A44
};

server.GetPriceData(DateOnly.FromDateTime(DateTime.Today), opts);


