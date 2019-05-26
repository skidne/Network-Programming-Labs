using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatOnSockets
{
	public class TcpChatClient
	{
		private static readonly TcpChatClient _instance = new TcpChatClient();
		public static TcpChatClient Instance => _instance;

		private const string host = "127.0.0.1";
		private const int port = 15123;
		private static TcpClient client;
		private static NetworkStream stream;

		private TcpChatClient()
		{
			
		}

		public void Start()
		{
			client = new TcpClient();

			client.Connect(host, port);
			stream = client.GetStream();

			var message = UdpChatClient.ClientId;
			//var data = Encoding.Unicode.GetBytes(message);
			//stream.Write(data, 0, data.Length);

			SendMessage(message);

			Task.Run(() => ReceiveMessage());

			//var receiveThread = new Thread(new ThreadStart(ReceiveMessage));
			//receiveThread.Start();
			Console.WriteLine("Hi, {0}", UdpChatClient.ClientId);
			//SendMessage();
		}

		public void SendMessage(string message)
		{
			while (true)
			{
				var data = Encoding.Unicode.GetBytes(message);

				stream.Write(data, 0, data.Length);
			}
		}

		private void ReceiveMessage()
		{
			while (true)
			{
				try
				{
					var data = new byte[64];
					var builder = new StringBuilder();
					var bytes = 0;

					do
					{
						bytes = stream.Read(data, 0, data.Length);
						builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
					}
					while (stream.DataAvailable);

					string message = builder.ToString();
					(Application.OpenForms["Chat"].Controls["richTextBox2"] as RichTextBox).AppendText(message);
				}
				catch
				{
					(Application.OpenForms["Chat"].Controls["richTextBox2"] as RichTextBox).AppendText("Disconnected");
					//Console.ReadLine();
					Disconnect();
				}
			}
		}
		private void Disconnect()
		{
			if (stream != null)
				stream.Close();
			if (client != null)
				client.Close();

			Environment.Exit(0);
		}
	}
}
