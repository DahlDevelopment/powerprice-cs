using Grpc.Core;
using powerprice_cs_server;

namespace powerprice_cs_rpc.Services;
using powerprice_cs_common;

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

        var broker = new EntsoEPriceDataBroker(line.First(), new RESTClient());

        PriceDataOptions opts = new()
        {
            Zone = request.RequestOptions.Zone,
            Date = DateOnly.FromDateTime(request.RequestOptions.Date.ToDateTime()),
            DocumentType = request.RequestOptions.DocumentType
        };

        var data = PowerPriceServer.GetPriceData(broker, opts) as EntsoEPriceData;

        if (data is not null)
        {
            var timeSeries = data.TimeSeries;
            var period = data.Periods.First();
            
            if (timeSeries is not null && period is not null)
            {

                var priceperiod = new PriceDataPeriod
                {
                    PriceData = { period.PriceData },
                    PeriodTimeInterval = new RpcTimeInterval
                    {
                        Start = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(period.PeriodTimeInterval.Value.Start),
                        End = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(period.PeriodTimeInterval.Value.End)
                    },
                    TimeResolution = period.Resolution
                };

                return Task.FromResult(new PriceDataReply
                {
                    MarketDocumentMeta = new MarketDocumentMeta
                    {
                        MRID = data.MRID,
                        RevisionNumber = data.RevisonNumber,
                        MarketDocumentTimeInterval = new RpcTimeInterval
                        {
                            Start = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(data.MarketDocumentTimeInterval!.Value.Start),
                            End = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(data.MarketDocumentTimeInterval!.Value.End)
                        },
                        Type = data.Type
                    },

                    PriceDataTimeSeries = new PriceDataTimeSeries
                    {
                        MRID = data.TimeSeries.MRID,
                        BusinessType = data.TimeSeries.BusinessType,
                        Currency = data.TimeSeries.Currency,
                        CurveType = timeSeries.CurveType,
                        MeasureUnit = timeSeries.MeasureUnit,
                        Domain = timeSeries.Domain,
                        Periods = { priceperiod }
                    }

                });
            }
            else
            {
                return Task.FromResult(new PriceDataReply
                {
                });
            }
        }
        else
        {
            return Task.FromResult(new PriceDataReply
            {
            });
        }

    }
    
}
