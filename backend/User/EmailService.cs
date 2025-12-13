using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace backend.User;

public class EmailService
{
    private readonly EmailSettings _settings;

    public EmailService(IOptions<EmailSettings> options)
    {
        _settings = options.Value;
    }

    public async Task SendVerificationEmailAsync(string toEmail, string code)
    {
        var message = new MailMessage
        {
            From = new MailAddress(_settings.From, "Wander Buddy"),
            Subject = "Kod weryfikacyjny",
            Body = $"""
                        <h2>Weryfikacja adresu e-mail</h2>
                        <p>Twój kod weryfikacyjny:</p>
                        <h1>{code}</h1>
                        <p>Kod jest ważny przez 15 minut.</p>
                    """,
            IsBodyHtml = true
        };

        message.To.Add(toEmail);

        using var smtp = new SmtpClient(_settings.SmtpServer, _settings.Port)
        {
            Credentials = new NetworkCredential(
                _settings.Username,
                _settings.Password
            ),
            EnableSsl = true
        };

        await smtp.SendMailAsync(message);
    }
}