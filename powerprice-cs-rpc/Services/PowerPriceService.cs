using Grpc.Core;
using powerprice_cs_server;

namespace powerprice_cs_rpc.Services;

public class PowerPriceService : PriceDataService.PriceDataServiceBase
{
    private readonly ILogger<PowerPriceService> _logger;
    public PowerPriceService(ILogger<PowerPriceService> logger)
    {
        _logger = logger;
    }


    public override Task<PriceDataReply> GetPriceData(PriceDataRequest request, ServerCallContext context)
    {
        string enstoeKeyFile = @".entsoe_key_secret";
        var line = File.ReadLines(enstoeKeyFile);
        //Console.WriteLine("File Contents: " + line.First());

        var broker = new EntsoEPriceDataBroker(line.First());

        Options opts = new()
        {
            Zone = Zones.NO4,
            Date = DateOnly.FromDateTime(DateTime.Today),
            DocumentType = DocumentTypes.A44
        };

        var data = (EntsoEPriceData)PowerPriceServer.GetPriceData(broker, DateOnly.FromDateTime(DateTime.Today), opts);

        // for-loop to construct the correct timestamp objects
        //List<Google.Protobuf.WellKnownTypes.Timestamp> timestamps = new();
        //foreach(var timestamp in data.TimeStamps)
        //{
        //    timestamps.Add(Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(timestamp));
        //}

        // testing lambda/linq
        var timestamps = data.Timestamps
            .Select(x => Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(x.ToUniversalTime()))
            .ToList();

        return Task.FromResult(new PriceDataReply
        {
            PriceData       = { data.Data },
            Timestamps      = { timestamps },
            Currency        = data.Currency,
            MeasureUnit     = data.MeasureUnit,
            TimeResolution  = data.TimeResolution
        });
    }
}
