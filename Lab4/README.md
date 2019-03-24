# Laboratory Work No.4

## SMTP/POP3 protocols

### Task
- Use the SMTP/POP3 protocols to send/receive emails (bonus for attachments and
    html formatted text):

---

### Solution

__Language__: C#

- I used the _MailKit_ package, since it's more flexible than other clients (works with _MimeKit_)

- To send emails we have to use the __SMTP__ protocol, which is implemented in _MailKit_, and is used like this:

```cs
using (var emailClient = new SmtpClient(new ProtocolLogger(Console.OpenStandardOutput())))
{
	// accepts all SSL certificates (should not be used in development, tho)
	emailClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
	// using gmail here, port 587 (uses TLS)
	emailClient.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
	// authentification required
	emailClient.Authenticate(_from, _password);

	emailClient.Send(emailMessage);

	emailClient.Disconnect(true);
}
```

- In the __Main__ I sent an email with an html body (having a centered image) and an attachment.

- To receive emails, we have to use the __POP3__ protocol, like this:

```cs
using (var emailClient = new Pop3Client())
{
	// again gmail, port 995 is SSL, so I specify to useSSL
	emailClient.Connect("pop.gmail.com", 995, true);

	emailClient.Authenticate(_from, _password);

	var emails = new List<MimeMessage>();
	for (var i = 0; i < emailClient.Count && i < maxCount; i++)
		emails.Add(emailClient.GetMessage(i));

	emailClient.Disconnect(true);

	return emails;
}
```

- Also, for security purposes, I take the email addresses and the user's credentials as input in the Console.

---

The sent email looks like this (in browser):
![general kenobi](https://user-images.githubusercontent.com/22482507/54880555-5c08b180-4e4e-11e9-810c-ee58ba7be7a4.JPG)
