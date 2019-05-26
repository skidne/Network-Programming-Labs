using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatOnSockets.Server.SocketHubs
{
	public class UdpListener
	{
		private const int _listeningPort = 14567;
		private readonly UdpClient udpClient;

		public UdpListener()
		{
			udpClient = new UdpClient(_listeningPort);
		}

		//Listening for broadcastings. Necessary for discovery
		public void StartListening()
		{
			var clientEP = new IPEndPoint(IPAddress.Any, _listeningPort);

			try
			{
				udpClient.BeginReceive(Receive, new object());
			}
			catch (SocketException e)
			{
				Debug.WriteLine(e);
			}
		}

		private void Receive(IAsyncResult ar)
		{
			IPEndPoint clientEP = new IPEndPoint(IPAddress.Any, _listeningPort);
			var bytes = udpClient.EndReceive(ar, ref clientEP);
			var message = Encoding.ASCII.GetString(bytes);

			ChatServer.Instance.AddUpdateClient(message.Substring(0, 32), new Client { ClientId = message.Substring(0, 32), LastUpdate = DateTime.UtcNow });
			Console.WriteLine("From {0} received: {1} ", clientEP.Address.ToString(), message);
			StartListening();
		}
	}
}
