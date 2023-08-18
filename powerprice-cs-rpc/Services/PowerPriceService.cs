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

        PriceDataOptions opts = new()
        {
            Zone = Zones.NO4,
            Date = DateOnly.FromDateTime(DateTime.Today),
            DocumentType = DocumentTypes.A44
        };

        var data = PowerPriceServer.GetPriceData(broker, DateOnly.FromDateTime(DateTime.Today), opts) as EntsoEPriceData;

        // for-loop to construct the correct timestamp objects
        //List<Google.Protobuf.WellKnownTypes.Timestamp> timestamps = new();
        //foreach(var timestamp in data.TimeStamps)
        //{
        //    timestamps.Add(Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(timestamp));
        //}

        // testing lambda/linq
        //var timestamps = data.Timestamps
        //    .Select(x => Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(x.ToUniversalTime()))
        //    .ToList();


        if (data is not null)
        {
            var timeSeries = data.TimeSeries;
            var period = data.Periods.First();
            
            if (timeSeries is not null && period is not null)
            {

                var priceperiod = new PriceDataPeriod
                {
                    PriceData = { period.PriceData },
                    TimeInterval = new TimeInterval
                    {
                        Start = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(period.Tinterval.Value.Start),
                        End = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(period.Tinterval.Value.End)
                    },
                    TimeResolution = period.Resolution
                };

                var priceDataTimeseries = new PriceDataTimeSeries
                {
                    MRID = data.TimeSeries.MRID,
                    BusinessType = data.TimeSeries.BusinessType,
                    Currency = data.TimeSeries.Currency,
                    CurveType = timeSeries.CurveType,
                    MeasureUnit = timeSeries.MeasureUnit,
                    Periods = { priceperiod }
                };

                var marketMeta = new MarketDocumentMeta
                {
                    MRID = data.MRID,
                    RevisionNumber = data.RevisonNumber,
                    TimeInterval = new TimeInterval
                    {
                        Start = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(data.EntsoETimeInteval!.Value.Start),
                        End = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(data.EntsoETimeInteval!.Value.End)
                    },
                    Type = data.Type
                };

                return Task.FromResult(new PriceDataReply
                {
                    MarketDocumentMeta = marketMeta,
                    PriceDataTimeSeries = priceDataTimeseries
                
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
