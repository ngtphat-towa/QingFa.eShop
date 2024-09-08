namespace QingFa.EShop.Application.Features.AccountManagements.ConfirmEmail
{
    public class ConfirmEmailRequest
    {
        public string UserId { get; set; } = default!;
        public string Token { get; set; } = default!;
        public string? ChangeEmail { get; set; }
    }

}
