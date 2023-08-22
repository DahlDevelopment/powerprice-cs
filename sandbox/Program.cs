using Grpc.Net.Client;
using powerprice_cs_client;
using System.IO;
using System.Net.Http.Headers;
using System.Web;

// The port number must match the poert of the gRPC server
var com = new TCPIPCom();
var client = new Client(com);
var reply = client.GetPowerPriceData(DateOnly.FromDateTime(DateTime.UtcNow));
//var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });
//Console.WriteLine("Greeting: " + reply.Message);

//var reply = await client.GetPriceDataAsync(new PriceDataRequest { Date = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow) });
Console.WriteLine("Price Data: " + reply.PriceData);
Console.WriteLine("Press any key to exit...");


