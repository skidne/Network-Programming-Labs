using MailKit;
using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;

namespace Lab4
{
	public class EmailService : IEmailService
	{
		private readonly string _to;
		private readonly string _from;
		private readonly string _password;

		public EmailService(string to, string from, string pass)
		{
			_to = to;
			_from = from;
			_password = pass;
		}
		
		public List<MimeMessage> Receive(int maxCount = 10)
		{
			using (var emailClient = new Pop3Client())
			{
				emailClient.Connect("pop.gmail.com", 995, true);

				emailClient.Authenticate(_from, _password);

				var emails = new List<MimeMessage>();
				for (var i = 0; i < emailClient.Count && i < maxCount; i++)
					emails.Add(emailClient.GetMessage(i));

				emailClient.Disconnect(true);

				return emails;
			}
		}

		public void Send(MimeMessage emailMessage)
		{
			using (var emailClient = new SmtpClient(new ProtocolLogger(Console.OpenStandardOutput())))
			{
				emailClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
				emailClient.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
				emailClient.Authenticate(_from, _password);

				emailClient.Send(emailMessage);

				emailClient.Disconnect(true);
			}
		}
	}
}