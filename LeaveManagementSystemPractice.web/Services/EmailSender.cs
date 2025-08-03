using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace LeaveManagementSystemPractice.web.Services;

public class EmailSender(IConfiguration configuration) : IEmailSender
{
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var fromAddress = configuration["EmailSettings:DefaultEmailAddress"] ??
                          throw new InvalidOperationException("Default email address not configured.");
        var smtpServer = configuration["EmailSettings:Server"] ??
                          throw new InvalidOperationException("SMTP server not configured.");
        var smtpPort = Convert.ToInt32(configuration["EmailSettings:Port"] ??
                       throw new InvalidOperationException("SMTP port not configured."));
        
        var message = new MailMessage
        {
            From = new MailAddress(fromAddress),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };
        
        message.To.Add(new MailAddress(email));

        using var client = new SmtpClient(smtpServer, smtpPort);
        await client.SendMailAsync(message);
    }
}