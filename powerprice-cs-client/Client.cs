using Grpc.Net.Client;

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

        public IComResponse Query(IComCommand command)
        {
            return _comMethod.Query(command);
        }
    }
}

