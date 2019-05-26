using ChatOnSockets.Server.SocketHubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatOnSockets.Server
{
	class Program
	{
		static void Main(string[] args)
		{
			var udpListener = new UdpListener();
			Task.Run(() => udpListener.StartListening());
			Task.Run(() => TcpChatListener.Instance.Listen());
			Console.WriteLine("Any key to quit...");
			Console.ReadKey();
		}
	}
}
