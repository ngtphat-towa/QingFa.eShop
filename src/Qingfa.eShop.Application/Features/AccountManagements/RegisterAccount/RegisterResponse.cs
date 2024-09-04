namespace QingFa.EShop.Application.Features.AccountManagements.RegisterAccount
{
    public record RegisterResponse(
     Guid UserId,
     string UserName,
     string Email,
     string Message
 );
}
