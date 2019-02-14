using System;
using System.Collections.Generic;
using System.Threading;

namespace Lab2
{
	class Program
	{
		static void Main(string[] args)
		{
			AutoResetEvent[] autoResetEvents = {
				new AutoResetEvent(false),
				new AutoResetEvent(false),
				new AutoResetEvent(false) 
			};
			var lastEvent = new ManualResetEvent(false);

			var threads = new List<Thread>
			{
				new Thread(() => { Console.WriteLine("7"); autoResetEvents[0].Set(); }),
				new Thread(() => { Console.WriteLine("6"); autoResetEvents[1].Set(); }),
				new Thread(() => { Console.WriteLine("5"); autoResetEvents[2].Set(); }),
				new Thread(() => { WaitHandle.WaitAll(autoResetEvents);
											Console.WriteLine("4"); lastEvent.Set(); }),
				new Thread(() => { lastEvent.WaitOne(); Console.WriteLine("3"); }),
				new Thread(() => { lastEvent.WaitOne(); Console.WriteLine("2"); }),
				new Thread(() => { lastEvent.WaitOne(); Console.WriteLine("1"); })
			};

			threads.ForEach(x => x.Start());
			Console.ReadKey();
		}
	}
}
