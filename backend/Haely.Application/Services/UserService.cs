using AutoMapper;
using BCrypt.Net;
using Haelya.Application.DTOs.User;
using Haelya.Application.Exceptions;
using Haelya.Application.Interfaces;
using Haelya.Domain.Entities;
using Haelya.Domain.Interfaces;
using System;
using System.Collections.Generic;
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

        public async Task ChangePasswordAsync(long id, ChangePasswordDTO dto)
        {
            try
            {
                string hashPassword = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);

                await _userRepository.UpdatePasswordAsync(id, hashPassword);
                await _logger.LogAsync(id, "Mot de passe modifié avec succès");
            }
            catch (Exception ex)
            {
                await _logger.LogAsync(id, $"Erreur lors du changement de mot de passe : {ex.Message}");
                throw; 
            }
        }

        public async Task DeleteAsync(long id)
        {
            try
            {
                await _userRepository.DeleteAsync(id);
                await _logger.LogAsync(id, "Utilisateur supprimé avec succès");
            }
            catch (Exception ex)
            {
                await _logger.LogAsync(id, $"Erreur lors de la suppression de l'utilisateur : {ex.Message}");
                throw;
            }
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
                throw new UserNotFoundException();
            }
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetByIdAsync(long id)
        {
            User? user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new UserNotFoundException();
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
                    throw new InvalidCredentialsException();
                }

                if (!BCrypt.Net.BCrypt.Verify(dto.Password, hashpassword))
                {
                    User? user = await _userRepository.GetByEmailAsync(dto.Email);
                    await _logger.LogAsync(user?.Id, "Mot de passe incorrect lors du login.");
                    throw new IncorrectPasswordException();
                }

                User? userSuccess = await _userRepository.GetByEmailAsync(dto.Email);
                if (userSuccess == null)
                {
                    await _logger.LogAsync(null, "Utilisateur introuvable après vérification du mot de passe.");
                    throw new UserNotFoundException();
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

        public async Task<UserDTO> RegisterAsync(RegisterDTO dto)
        {
            if (await _userRepository.EmailExistsAsync(dto.Email))
            {
                throw new EmailAlreadyUsedException();
            }
            User user = _mapper.Map<User>(dto);
            
            user.HashPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            user.RegisterDate = DateTime.UtcNow;

            await _userRepository.AddAsync(user);
            await _logger.LogAsync(user.Id, "Utilisateur enregistré avec succès");

            return _mapper.Map<UserDTO>(user);
            
        }

        public async Task UpdateAsync(long id, UpdateUserDTO dto)
        {
            
            User? existingUser = await _userRepository.GetByIdAsync(id);

            if (existingUser == null)
            {
                await _logger.LogAsync(null, $"Tentative de mise à jour sur un utilisateur inexistant (id: {id})");
                throw new UserNotFoundException();
            }

            existingUser.FirstName = dto.FirstName;
            existingUser.LastName = dto.LastName;
            existingUser.PhoneNumber = dto.PhoneNumber;
            existingUser.BirthDate = dto.BirthDate;

            await _userRepository.UpdateAsync(existingUser);
            await _logger.LogAsync(existingUser.Id, $"Utilisateur mis à jour avec succès");

        }
    }
}
