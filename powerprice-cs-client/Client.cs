using Grpc.Net.Client;
using powerprice_cs_rpc;
using powerprice_cs_common;

namespace powerprice_cs_client
{
    public class Client
    {
        private readonly IComMethod _comMethod;
        private readonly GrpcChannel _grpcChannel;        

        public Client(IComMethod comMethod)
        {
            _comMethod = comMethod;
            _grpcChannel = GrpcChannel.ForAddress("http://localhost:5288"); //TODO: Need to figure out how to set the correct address for remote hosts
        }

        public PriceDataReply GetPowerPriceData(PriceDataOptions options)
        {
            var client = new PriceDataService.PriceDataServiceClient(_grpcChannel);

            var requestOtions = new PriceDataRequestOptions
            {
                Date = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTimeOffset(options.Date.ToDateTime(TimeOnly.Parse("00:00:00"))),
                Zone = options.Zone
            };

            requestOtions.DocumentType = requestOtions.HasDocumentType ? requestOtions.DocumentType : PriceDataOptions.defaultDocumentTypePriceData;

            var priceDataReply = client.GetPriceData(new PriceDataRequest
            {
                RequestOptions = requestOtions
            });
            
            return priceDataReply;
        }

        public IComResponse Query(IComCommand command)
        {
            return _comMethod.Query(command);
        }
    }
}

