using ChatOnSockets.Server.SocketHubs;
using System;
using System.Net.Sockets;
using System.Text;

namespace ChatOnSockets.Server
{
	public class Client
	{
		private TcpClient tcpClient;

		public string ClientId { get; set; }
		public NetworkStream NStream { get; set; }
		public DateTime LastUpdate { get; set; }

		public Client()
		{
			tcpClient = null;
		}

		public void SetTcp(TcpClient tcpClient)
		{
			this.tcpClient = tcpClient;
			NStream = tcpClient.GetStream();
		}

		public void Process()
		{
			try
			{
				string message;

				NStream = tcpClient.GetStream();


				//ClientId = new Guid(message);

				//message = ClientId + " joined";

				//TcpChatListener.Instance.BroadcastMessage(message, ClientId);
				//Console.WriteLine(message);

				while (true)
				{
					try
					{
						message = TcpChatListener.GetMessage(NStream);
						message = string.Format("{0}: {1}", ClientId, message);
						Console.WriteLine(message);
						TcpChatListener.Instance.BroadcastMessage(message, ClientId);
					}
					catch
					{
						message = String.Format("{0}: left", ClientId);
						Console.WriteLine(message);
						TcpChatListener.Instance.BroadcastMessage(message, ClientId);
						break;
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				//TcpChatListener.Instance.RemoveConnection(ClientId);
				Close();
			}
		}

		//private string GetMessage()
		//{
		//	var data = new byte[64];
		//	var builder = new StringBuilder();
		//	var bytes = 0;

		//	do
		//	{
		//		bytes = NStream.Read(data, 0, data.Length);
		//		builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
		//	}
		//	while (NStream.DataAvailable);

		//	return builder.ToString();
		//}

		public void Close()
		{
			if (NStream != null)
				NStream.Close();
			if (tcpClient != null)
				tcpClient.Close();
		}
	}
}
