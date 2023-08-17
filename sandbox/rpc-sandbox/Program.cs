using powerprice_cs_client;

// The port number must match the poert of the gRPC server
var com = new TCPIPCom();
var client = new Client(com);
var reply = client.GetPowerPriceData(DateOnly.FromDateTime(DateTime.UtcNow));
//var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });
//Console.WriteLine("Greeting: " + reply.Message);

//var reply = await client.GetPriceDataAsync(new PriceDataRequest { Date = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow) });
Console.WriteLine("Price Data: " + reply.PriceData);
//Console.WriteLine("Timestamps: " + reply.Timestamps);
Console.WriteLine("Currency: " + reply.Currency);
Console.WriteLine("MeasureUnit: " + reply.MeasureUnit);
Console.WriteLine("TimeResolution: " + reply.TimeResolution);
Console.WriteLine("TimeSeries BusinessType: " + reply.BusinessType);
Console.WriteLine("TimeSeries mRID: " + reply.MRID);
Console.WriteLine("TimeSeries CurveType: " + reply.CurveType);
Console.WriteLine("Press any key to exit...");


