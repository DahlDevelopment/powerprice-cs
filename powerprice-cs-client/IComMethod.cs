using System;
namespace powerprice_cs_client
{
	public interface IComMethod
	{
		public void QueryAsync(IComCommand command, IComResponse response, Action<IComResponse> responseHandler);
		public IComResponse Query(IComCommand command);
	}
}

