namespace common.emailsender;

public interface IEmailSender
{
	void SendEmail(EmailMessage message);
	void SendCodeToVerifyEmail(EmailMessage message);
}

