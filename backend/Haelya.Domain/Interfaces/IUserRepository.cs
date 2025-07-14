using Haelya.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(long id);
        Task AddAsync(User user);
        Task<bool> EmailExistsAsync(string email);
        Task<List<User>> GetAllAsync();
        Task UpdateAsync (User user);
        Task UpdatePasswordAsync(long id, string hashPassword);
        Task DeleteAsync(long id);
        Task<string?> GetPasswordHashByEmailAsync(string email);
    }
}
