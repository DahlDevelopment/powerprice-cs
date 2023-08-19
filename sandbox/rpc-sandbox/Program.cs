using powerprice_cs_client;

// The port number must match the poert of the gRPC server
var com = new TCPIPCom();
var client = new Client(com);
var reply = client.GetPowerPriceData(DateOnly.FromDateTime(DateTime.UtcNow));
//var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });
//Console.WriteLine("Greeting: " + reply.Message);

//var reply = await client.GetPriceDataAsync(new PriceDataRequest { Date = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow) });
var marketDocumentMeta = reply.MarketDocumentMeta;
var timeSeries = reply.PriceDataTimeSeries;
var period = timeSeries.Periods.First();

if (marketDocumentMeta is not null &&  timeSeries is not null && period is not null)
{
    Console.WriteLine("MarketDocument mRID: " +  marketDocumentMeta.MRID);
    Console.WriteLine("MarketDocument revisionNumber: " + marketDocumentMeta.RevisionNumber);
    Console.WriteLine("MarketDocument type: " + marketDocumentMeta.Type);
    Console.WriteLine("MarketDocument time interval start: " + marketDocumentMeta.MarketDocumentTimeInterval.Start.ToDateTime().ToString());
    Console.WriteLine("MarketDocument time interval end: " + marketDocumentMeta.MarketDocumentTimeInterval.End.ToDateTime().ToString());

    Console.WriteLine("TimeSeries mRID: " + timeSeries.MRID);
    Console.WriteLine("TimeSeries businesstype: " + timeSeries.BusinessType);
    Console.WriteLine("TimeSeries measure unit: " + timeSeries.MeasureUnit);
    Console.WriteLine("TimeSeries currency: " + timeSeries.Currency);
    Console.WriteLine("TimeSeries curve type: " + timeSeries.CurveType);
    Console.WriteLine("TimeSeries Domain: " + timeSeries.Domain);
    Console.WriteLine("TimeSeries # of periods: " + timeSeries.Periods.Count().ToString());

    Console.WriteLine("");
    Console.WriteLine("------------------------------------------");
    Console.WriteLine("First Period");
    Console.WriteLine("------------------------------------------");
    Console.WriteLine("Period time interval start: " + period.PeriodTimeInterval.Start.ToDateTime().ToString());
    Console.WriteLine("Period time interval end: " + period.PeriodTimeInterval.End.ToDateTime().ToString());
    Console.WriteLine("Period time resolution: " + period.TimeResolution);
    Console.WriteLine("Period price data: " + period.PriceData);

}


