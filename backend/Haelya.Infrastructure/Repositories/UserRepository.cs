using Haelya.Domain.Entities;
using Haelya.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HaelyaDbContext _context;

        public UserRepository(HaelyaDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(long id)
        {
            User? user = await GetByIdAsync(id);
            if (user is null)
            {
                throw new KeyNotFoundException("User not found");
            }


            user.FirstName = "Supprimé";
            user.LastName = "Utilisateur";
            user.Email = $"deleted_user_{id}@anonyme.local";
            user.PhoneNumber = null;
            user.BirthDate = null;
            await _context.SaveChangesAsync();
            

        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdAsync(long id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task UpdateAsync(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            
            if (!await _context.Users.AnyAsync(u => u.Id == user.Id))
            {
                throw new KeyNotFoundException("User not found");
            }
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

        }

        public async Task UpdatePasswordAsync(long id, string hashPassword)
        {
            User? user = await GetByIdAsync(id);
            if (user is null)
            {
                throw new KeyNotFoundException("User not found");
            }
            user.HashPassword = hashPassword;
            await _context.SaveChangesAsync();

        }

        public async Task<string?> GetPasswordHashByEmailAsync(string email)
        {
            return await _context.Users
                .Where(u => u.Email == email)
                .Select(u => u.HashPassword)
                .FirstOrDefaultAsync();
        }
    }
}
