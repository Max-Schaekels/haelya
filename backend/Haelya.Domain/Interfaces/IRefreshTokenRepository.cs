using Haelya.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Domain.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task AddAsync(RefreshToken token);
        Task SaveChangesAsync();
        Task<RefreshToken[]> GetAllByUserIdAsync(long userId);
        void RemoveRange(IEnumerable<RefreshToken> tokens);
       
    }
}
