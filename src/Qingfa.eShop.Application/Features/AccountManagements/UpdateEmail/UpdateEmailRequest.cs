namespace QingFa.EShop.Application.Features.AccountManagements.UpdateEmail
{
    public record UpdateEmailRequest 
    {
        public string NewEmail { get; set; } = default!;
        public string ConfirmEmail { get; set; } = default!;
    }
}
