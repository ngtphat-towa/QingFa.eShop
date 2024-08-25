using Microsoft.AspNetCore.Identity.Data;

using QingFa.EShop.Application.Core.Models;
using QingFa.EShop.Application.Identity.Models.Responses;

namespace QingFa.EShop.Application.Identity.Services
{
    public interface IAuthenticationService
    {
        Task<Result<RegisterResponse>> RegisterAsync(RegisterRequest request);
        Task<Result<LoginResponse>> LoginAsync(LoginRequest request);
        Task<Result<ManageInfoResponse>> GetManageInfoAsync(); 
        Task<Result> ConfirmEmailAsync(string userId, string code);
        Task<Result> ResendConfirmationEmailAsync(string email);
        Task<Result> ForgotPasswordAsync(string email);
        Task<Result<ResetPasswordResponse>> ResetPasswordAsync(ResetPasswordRequest request);
        Task<Result<RefreshResponse>> RefreshTokenAsync(string refreshToken);
    }
}
