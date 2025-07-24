using Haelya.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Application.Interfaces.Auth
{
    public interface IRefreshTokenService
    {
        Task<RefreshToken> GenerateTokenAsync(long userId);
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task<bool> ValidateAsync(string token);
        Task RevokeAsync(string token);
        Task DeleteAllByUserIdAsync(long userId);
    }
}
