using AutoMapper;
using BCrypt.Net;
using Haelya.Application.DTOs.User;
using Haelya.Application.Interfaces;
using Haelya.Domain.Entities;
using Haelya.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ISecurityLogger _logger;

        public UserService(IUserRepository userRepository, IMapper maper, ISecurityLogger logger) 
        {
            _userRepository = userRepository;
            _mapper = maper;
            _logger = logger;
        }

        public Task ChangePasswordAsync(long id, ChangePasswordDTO dto)
        {
           return _userRepository.UpdatePasswordAsync(id, dto.NewPassword);
        }

        public Task DeleteAsync(long id)
        {
            return _userRepository.DeleteAsync(id);
        }

        public Task<bool> EmailExistsAsync(string email)
        {
            return _userRepository.EmailExistsAsync(email);
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            List <User> users =  await _userRepository.GetAllAsync();
            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task<UserDTO> GetByEmailAsync(string email)
        {
            User? user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                throw new ArgumentException("User invalide");
            }
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetByIdAsync(long id)
        {
            User? user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> LoginAsync(LoginDTO dto)
        {
            try
            {
                string? hashpassword = await _userRepository.GetPasswordHashByEmailAsync(dto.Email);

                if (string.IsNullOrEmpty(hashpassword))
                {
                    await _logger.LogAsync(null, "Tentative de connexion avec un email invalide.");
                    throw new ArgumentException("Email invalide.");
                }

                if (!BCrypt.Net.BCrypt.Verify(dto.Password, hashpassword))
                {
                    User? user = await _userRepository.GetByEmailAsync(dto.Email);
                    await _logger.LogAsync(user?.Id, "Mot de passe incorrect lors du login.");
                    throw new ArgumentException("Mot de passe invalide.");
                }

                User? userSuccess = await _userRepository.GetByEmailAsync(dto.Email);
                if (userSuccess == null)
                {
                    await _logger.LogAsync(null, "Utilisateur introuvable après vérification du mot de passe.");
                    throw new KeyNotFoundException("Utilisateur introuvable.");
                }

                await _logger.LogAsync(userSuccess.Id, "Connexion réussie.");
                return _mapper.Map<UserDTO>(userSuccess);
            }
            catch (Exception ex)
            {
                await _logger.LogAsync(null, $"Erreur inattendue lors du login : {ex.Message}");
                throw; 
            }
        }

        public Task<UserDTO> RegisterAsync(RegisterDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(long id, UpdateUserDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
