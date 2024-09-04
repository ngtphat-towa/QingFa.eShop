using FluentEmail.Core;



namespace QingFa.EShop.Infrastructure.Identity.Services.Emails
{
    internal class FluentEmailSender : IEmailSender
    {
        private readonly IFluentEmail _fluentEmail;

        public FluentEmailSender(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            await _fluentEmail
                .To(to)
                .Subject(subject)
                .Body(body)
                .SendAsync();
        }
    }
}
