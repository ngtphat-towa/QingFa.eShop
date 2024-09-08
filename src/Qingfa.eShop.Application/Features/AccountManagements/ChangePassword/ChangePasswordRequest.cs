namespace QingFa.EShop.Application.Features.AccountManagements.ChangePassword
{
    public record ChangePasswordRequest
    {
        public string CurrentPassword { get; set; } = default!;
        public string NewPassword { get; set; } = default!;
    }
}
