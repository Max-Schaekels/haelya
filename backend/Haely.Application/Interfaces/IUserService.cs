using Haelya.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> RegisterAsync(RegisterDTO dto);
        Task<UserDTO> LoginAsync(LoginDTO dto);
        Task<List<UserDTO>> GetAllAsync();
        Task<UserDTO> GetByIdAsync(long id);
        Task<UserDTO> GetByEmailAsync(string email);
        Task<bool> EmailExistsAsync(string email);
        Task UpdateAsync(long id, UpdateUserDTO dto);
        Task ChangePasswordAsync(long id, ChangePasswordDTO dto);
        Task DeleteAsync(long id);

    }
}
