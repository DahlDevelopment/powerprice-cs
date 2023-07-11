using System;
namespace powerprice_cs_client
{
    public class TCPIPCommand : IComCommand
    {
        private string _command;
        public string Command {
            get => _command;
            set => _command = value;
        }

        public TCPIPCommand(string command)
        {
            _command = command;
        }

    }

    public class TCPIPResponse : IComResponse
    {

    }

    
	public class TCPIPCom : IComMethod
	{
        private static readonly HttpClient _client = new();

        public TCPIPCom()
		{
		}

        public IComResponse Query(IComCommand command)
        {
            try
            {
                if (command is not TCPIPCommand)
                {
                    throw new ArgumentException("Command needs to be an TCPIPCommand");
                }               
            }
            catch (ArgumentException ex)
            {            
                Console.WriteLine("Error: " + ex.Message);
            }
            

            return new TCPIPResponse();
        }

        public void QueryAsync(IComCommand command, IComResponse response, Action<IComResponse> responseHandler)
        {

            try
            {
                if (command is not TCPIPCommand)
                {
                    throw new ArgumentException("Command needs to be a TCPIPCommand Object");
                }

                if (response is not TCPIPResponse)
                {
                    throw new ArgumentException("Reponse needs to be a TCPIPResponse Object");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            _client.GetAsync(command.Command);
            responseHandler(response);
        }
    }
}

