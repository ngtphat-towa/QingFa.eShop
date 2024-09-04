namespace QingFa.EShop.Application.Features.AccountManagements.VerifyUser
{
    public record VerifyUserRequest 
    {
        public string VerificationCode { get; set; } = default!;
    }
}
