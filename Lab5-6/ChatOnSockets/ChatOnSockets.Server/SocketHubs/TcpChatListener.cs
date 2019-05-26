using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatOnSockets.Server.SocketHubs
{
	public class TcpChatListener
	{
		private static readonly TcpChatListener _instance = new TcpChatListener();
		public static TcpChatListener Instance => _instance;

		private TcpListener _tcpListener;

		public TcpChatListener()
		{
			IPAddress tcpIp;

			if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["TCP_IP"]))
				tcpIp = IPAddress.Any;
			else
				tcpIp = new IPAddress(Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["TCP_IP"]));

			var tcpPort = int.Parse(ConfigurationManager.AppSettings["TCP_PORT"]);

			_tcpListener = new TcpListener(tcpIp, tcpPort);
		}

		public void Listen()
		{
			try
			{
				_tcpListener = new TcpListener(IPAddress.Any, 15123);
				_tcpListener.Start();
				Console.WriteLine("Server started. Waiting for connections...");

				while (true)
				{
					var tcpClient = _tcpListener.AcceptTcpClient();
					var tmpStream = tcpClient.GetStream();

					string message = GetMessage(tmpStream);

					var tmp = message.Substring(0, 32);
					var clientId = tmp;

					message = clientId + " joined";

					var client = ChatServer.Instance.Clients[clientId];
					client.SetTcp(tcpClient);

					BroadcastMessage(message, clientId);

					Task.Run(() => client.Process());
					//Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
					//clientThread.Start();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				//Disconnect();
			}
		}

		public void BroadcastMessage(string message, string id)
		{
			var data = Encoding.Unicode.GetBytes(message);

			foreach (var client in ChatServer.Instance.Clients)
			{
				if (client.Value.ClientId != id)
				{
					client.Value.NStream.Write(data, 0, data.Length);
				}
			}
		}

		public static string GetMessage(NetworkStream nStream)
		{
			var data = new byte[64];
			var builder = new StringBuilder();
			var bytes = 0;

			do
			{
				bytes = nStream.Read(data, 0, data.Length);
				builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
			}
			while (nStream.DataAvailable);

			return builder.ToString();
		}
	}
}
