using Microsoft.Extensions.Configuration;

using SendGrid;
using SendGrid.Helpers.Mail;

namespace QingFa.EShop.Infrastructure.Services
{
    internal class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string htmlContent)
    {
        var apiKey = _configuration["SendGrid:ApiKey"];
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("noreply@example.com", "MyApp");
        var to = new EmailAddress(toEmail);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent: null, htmlContent);
        await client.SendEmailAsync(msg);
    }
}
}
