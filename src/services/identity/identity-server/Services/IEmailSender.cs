using System;
using identity_server.Models;

namespace identity_server.Services;

public interface IEmailSender
{
	void SendEmail(EmailMessage message);
}

