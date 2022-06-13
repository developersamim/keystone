using System;
using identity_server.Models;
using identity_server.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace identity_server.Services;

public class EmailSender : IEmailSender
{
    private readonly EmailSetting emailSetting;
    public EmailSender(EmailSetting emailSetting)
    {
        this.emailSetting = emailSetting;
    }
    public void SendEmail(EmailMessage message)
    {
        var emailMessage = CreateEmailMessage(message);
        Send(emailMessage);
    }

    private MimeMessage CreateEmailMessage(EmailMessage message)
    {
        var emailMessage = new MimeMessage();
        //emailMessage.From.Add(new MailboxAddress(emailSetting.From));
        emailMessage.From.Add(new MailboxAddress("email", emailSetting.From));
        emailMessage.To.AddRange(message.To);
        emailMessage.Subject = message.Subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
        return emailMessage;
    }
    private void Send(MimeMessage mailMessage)
    {
        using (var client = new SmtpClient())
        {
            try
            {
                client.Connect(emailSetting.SmtpServer, emailSetting.Port, true);
                //client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(emailSetting.UserName, emailSetting.Password);
                client.Send(mailMessage);
            }
            catch
            {
                //log an error message or throw an exception or both.
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}

