using MailKit.Net.Smtp;
using MimeKit;

namespace EmailApp.Services;

public class EmailSender : IEmailSender
{
    private readonly EmailConfiguration emailConfig;

    public EmailSender(EmailConfiguration emailConfig)
    {
        this.emailConfig = emailConfig;
    }

    public void SendEmail(Message message)
    {
        var emailMessage = CreateEmailMessage(message);

        Send(emailMessage);
    }

    public async Task SendEmailAsync(Message message)
    {
        var emailMessage = CreateEmailMessage(message);

        await SendAsync(emailMessage);
    }

    private MimeMessage CreateEmailMessage(Message message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("Serviço de email", emailConfig.From));
        emailMessage.To.AddRange(message.To);
        emailMessage.Subject = message.Subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        { Text = string.Format("<h1 style='color:red'>{0}</h1><p>{1}</p>", message.Title, message.Content) };

        return emailMessage;
    }

    private void Send(MimeMessage message)
    {
        using var client = new SmtpClient();
        try
        {
            client.Connect(emailConfig.SmtpServer, emailConfig.Port, true);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(emailConfig.Username, emailConfig.Password);

            client.Send(message);
        }
        catch
        {
            throw;
        }
        finally
        {
            client.Disconnect(true);
            client.Dispose();
        }
    }

    private async Task SendAsync(MimeMessage message)
    {
        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(emailConfig.SmtpServer, emailConfig.Port, true);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            await client.AuthenticateAsync(emailConfig.Username, emailConfig.Password);

            await client.SendAsync(message);
        }
        catch
        {
            throw;
        }
        finally
        {
            await client.DisconnectAsync(true);
            client.Dispose();
        }
    }
}
