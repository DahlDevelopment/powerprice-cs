using Grpc.Core;
using powerprice_cs_rpc;
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

        var broker = new EntsoEBroker(line.First());
        var server = new PowerPriceServer(broker);

        Options opts = new()
        {
            Zone = Zones.NO4,
            Date = DateOnly.FromDateTime(DateTime.Today),
            DocumentType = DocumentTypes.A44
        };

        var data = server.GetPriceData(DateOnly.FromDateTime(DateTime.Today), opts);

        return Task.FromResult(new PriceDataReply
        {
            PriceData = { data.Data }
        });
    }
}
