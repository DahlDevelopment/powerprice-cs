namespace powerprice_cs_client
{
    public class Client
    {
        private readonly IComMethod _comMethod;

        public Client(IComMethod comMethod)
        {
            _comMethod = comMethod;
        }

        public IComResponse Query(IComCommand command)
        {
            return _comMethod.Query(command);
        }
    }
}

