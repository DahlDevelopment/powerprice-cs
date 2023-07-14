using powerprice_cs_server;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var broker = new EntsoEBroker();
var server = new PowerPriceServer(broker);
