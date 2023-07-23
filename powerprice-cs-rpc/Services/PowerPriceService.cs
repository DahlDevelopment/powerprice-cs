using Grpc.Core;
using powerprice_cs_rpc;

namespace powerprice_cs_rpc.Services;

public class PowerPriceService : PriceData.PriceDataBase
{
    private readonly ILogger<PowerPriceService> _logger;
    public PowerPriceService(ILogger<PowerPriceService> logger)
    {
        _logger = logger;
    }


    public override Task<PriceDataReply> GetPriceData(PriceDataRequest request, ServerCallContext context)
    {
        return Task.FromResult(new PriceDataReply
        {
            PriceData = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }
        });
    }
}

