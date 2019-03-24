using MimeKit;
using MimeKit.Utils;
using System;
using System.Linq;

namespace Lab4
{
	class Program
	{
		private static string GetPassword()
		{
			string pass = "";
			do
			{
				ConsoleKeyInfo key = Console.ReadKey(true);
				if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
				{
					pass += key.KeyChar;
					Console.Write("*");
				}
				else
				{
					if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
					{
						pass = pass.Substring(0, (pass.Length - 1));
						Console.Write("\b \b");
					}
					else if (key.Key == ConsoleKey.Enter)
						break;
				}
			} while (true);

			return pass;
		}

		static void Main(string[] args)
		{
			Console.WriteLine("Enter an email address:");
			var to = Console.ReadLine();
			Console.WriteLine("Enter your email address:");
			var from = Console.ReadLine();
			Console.WriteLine("Enter your password:");
			var password = GetPassword();

			var message = new MimeMessage();
			message.From.Add(new MailboxAddress(from));
			message.To.Add(new MailboxAddress(to));
			message.Subject = "Hello there";

			var builder = new BodyBuilder();
			var image = builder.LinkedResources.Add("C:\\Users\\skdn\\Desktop\\maxresdefault.jpg");
			image.ContentId = MimeUtils.GenerateMessageId();

			builder.TextBody = "General Kenobi";
			builder.HtmlBody = "<p>General Kenobi</p><br>" +
				$"<center><img src=\"cid:{image.ContentId}\"></center>";
			builder.Attachments.Add("C:\\Users\\skdn\\Desktop\\Anul_III_2018_Semestrul_VI1.xls");
			message.Body = builder.ToMessageBody();

			var emailService = new EmailService(to, from, password);
			emailService.Send(message);
			emailService.Receive(1).ForEach(x => Console.WriteLine($"From: {x.From.Mailboxes.First()}\n" +
				$"Subject: {x.Subject}\nContent: {x.TextBody}"));

			Console.ReadKey();
		}
	}
}
