using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatOnSockets
{
	public class UdpChatClient
	{
		private static readonly UdpChatClient _instance = new UdpChatClient();
		public static UdpChatClient Instance => _instance;

		public static string ClientId;

		private readonly UdpClient udpClient;

		static UdpChatClient() { }

		private UdpChatClient()
		{
			ClientId = Guid.NewGuid().ToString();
			udpClient = new UdpClient();
		}

		public void BroadcastId()
		{
			while (true)
			{
				UdpClient udpClient = new UdpClient();
				udpClient.EnableBroadcast = true;
				IPEndPoint ip = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 14567);
				byte[] bytes = Encoding.ASCII.GetBytes(ClientId);
				udpClient.Send(bytes, bytes.Length, ip);
				udpClient.Close();
				Console.WriteLine("Sent: {0} ", ClientId);
				Thread.Sleep(500);
			}
		}
	}
}
