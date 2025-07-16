using Haelya.Application.DTOs.User;
using Haelya.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using System.Security.Claims;

namespace Haelya.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        private long? GetUserIdFromClaims()
        {
            string? userIdClaim = User.FindFirst(ClaimTypes.Sid)?.Value;
            if (long.TryParse(userIdClaim, out long userId))
            {
                return userId;
            }
            return null;
        }

        [Authorize]
        [HttpGet("profil")]
        public async Task<IActionResult> GetProfile()
        {
            long? userId = GetUserIdFromClaims();
            if (userId == null)
            {
                return Unauthorized();
            }

            UserDTO user = await _userService.GetByIdAsync(userId.Value);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);

        }

        [Authorize(Roles ="admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            List<UserDTO> users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO dto)
        {
            long? userId = GetUserIdFromClaims();
            if (userId == null)
            {
                return Unauthorized();
            }
            await _userService.UpdateAsync(userId.Value, dto);
            return NoContent();
        }

        [Authorize]
        [HttpPut("password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO dto)
        {
            long? userId = GetUserIdFromClaims();
            if (userId == null)
            {
                return Unauthorized();
            }
            await _userService.ChangePasswordAsync(userId.Value, dto);
            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetUserById(long id)
        {
            UserDTO user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteOwnAccount()
        {
            long? userId = GetUserIdFromClaims();
            if (userId == null)
            {
                return Unauthorized();
            }
            await _userService.DeleteAsync(userId.Value);
            return NoContent();
        }
    }
}
