using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatOnSockets.Server.SocketHubs
{
	public class ChatServer
	{
		private static readonly ChatServer _instance = new ChatServer();
		public static ChatServer Instance => _instance;

		public ConcurrentDictionary<string, Client> Clients;
		public int Count => Clients.Count;

		public void Start()
		{
		}

		static ChatServer() { }
		private ChatServer()
		{
			Clients = new ConcurrentDictionary<string, Client>();
			var timer = new Timer(CheckConnections, null, 0, 5000);
		}

		public void AddUpdateClient(string clientId, Client udpClient)
		{
			Clients.AddOrUpdate(clientId, udpClient, (k, v) => 
			{
				v.LastUpdate = DateTime.UtcNow;

				return v;
			});
		}

		public void CheckConnections(object obj)
		{
			foreach (var client in Clients)
			{
				if (client.Value.LastUpdate.AddSeconds(5) < DateTime.UtcNow)
				{
					Console.WriteLine("{0} : {1}", client.Value.LastUpdate.AddSeconds(5).ToString(), DateTime.UtcNow.ToString());
					Clients.TryRemove(client.Key, out var tmp);
				}
			}
			Console.WriteLine("{0} online clients!", Clients.Count);
		}
	}
}
