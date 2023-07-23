using Grpc.Net.Client;
using powerprice_cs_rpc;

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

        public PriceDataReply GetPowerPriceData(DateOnly date)
        {
            var client = new PriceData.PriceDataClient(_grpcChannel);
            var priceDataReply = client.GetPriceData(new PriceDataRequest
            {
                Date = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTimeOffset(date.ToDateTime(TimeOnly.Parse("00:00:00")))
            });

            return priceDataReply;
        }

        public IComResponse Query(IComCommand command)
        {
            return _comMethod.Query(command);
        }
    }
}

