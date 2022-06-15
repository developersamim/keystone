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
        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = "<div style=\"padding-left:100px;\">";
        bodyBuilder.HtmlBody += "<div style=\"border-style:ridge;padding:15px 30px;\">";
        bodyBuilder.HtmlBody += "<h1 style=\"margin-bottom:30px;\">Your email verificaiton code for KeyStone</h1>";
        bodyBuilder.HtmlBody += "<p style=\"margin-bottom:15px;\">Use this code to verify your email<p>\n";
        bodyBuilder.HtmlBody += "<span style=\"background-color:powderblue;padding:5px 10px;\">12345</span>";
        bodyBuilder.HtmlBody += "<hr style=\"margin-bottom:15px;margin-top:40px;\">";
        bodyBuilder.HtmlBody += "<p style=\"color:gray;\">KeyStone</p>";
        bodyBuilder.HtmlBody += "</div>";
        bodyBuilder.HtmlBody += "</div>";

        var emailMessage = new MimeMessage();
        //emailMessage.From.Add(new MailboxAddress(emailSetting.From));
        emailMessage.From.Add(new MailboxAddress("email", emailSetting.From));
        emailMessage.To.AddRange(message.To);
        emailMessage.Subject = message.Subject;
        emailMessage.Body = bodyBuilder.ToMessageBody();
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

