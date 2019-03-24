using MimeKit;
using System.Collections.Generic;

namespace Lab4
{
	public interface IEmailService
	{
		List<MimeMessage> Receive(int maxCount);
		void Send(MimeMessage emailMessage);
	}
}
