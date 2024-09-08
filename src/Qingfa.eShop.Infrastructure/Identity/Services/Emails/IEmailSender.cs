namespace QingFa.EShop.Infrastructure.Identity.Services.Emails
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
