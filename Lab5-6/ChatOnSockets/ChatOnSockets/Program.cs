using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatOnSockets
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Task.Run(() => UdpChatClient.Instance.BroadcastId());
			//Task.Run
			Task.Run(() => TcpChatClient.Instance.Start());
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Chat());
		}
	}
}
