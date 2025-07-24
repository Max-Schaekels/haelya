using Haelya.Application.DTOs.User;
using Haelya.Application.Exceptions;
using Haelya.Application.Interfaces;
using Haelya.Application.Interfaces.Auth;
using Haelya.Domain.Entities.Auth;
using Haelya.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Application.Services.Auth
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _repository;

        public RefreshTokenService(IRefreshTokenRepository repository)
        {
            _repository = repository;
        }
        public async Task<RefreshToken> GenerateTokenAsync(long userId)
        {
            var token = new RefreshToken
            {
                UserId = userId,
                Token = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(30)
            };

            await _repository.AddAsync(token);
            await _repository.SaveChangesAsync();

            return token;
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _repository.GetByTokenAsync(token);
        }

        public async Task<bool> ValidateAsync(string token)
        {
            RefreshToken? tokenEntity = await _repository.GetByTokenAsync(token);

            if (tokenEntity == null)
                return false;

            if (tokenEntity.IsRevoked)
                return false;

            if (tokenEntity.ExpiresAt <= DateTime.UtcNow)
                return false;

            return true;
        }

        public async Task RevokeAsync(string token)
        {
            RefreshToken? tokenEntity = await _repository.GetByTokenAsync(token);

            if (tokenEntity == null)
                throw new RefreshTokenNotFoundException(token);

            if (!tokenEntity.IsRevoked)
            {
                tokenEntity.IsRevoked = true;
                tokenEntity.RevokedAt = DateTime.UtcNow;
                await _repository.SaveChangesAsync();
            }
        }

        public async Task DeleteAllByUserIdAsync(long userId)
        {
            RefreshToken[] tokens = await _repository.GetAllByUserIdAsync(userId);
            _repository.RemoveRange(tokens);
            await _repository.SaveChangesAsync();
        }
    }
}
