using Haelya.Application.DTOs.User;
using Haelya.Application.Interfaces;
using Haelya.Application.Interfaces.Auth;
using Haelya.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Haelya.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthController(IUserService userService, ITokenService tokenService, IRefreshTokenService refreshTokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
            _refreshTokenService = refreshTokenService;
        }

        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userService.RegisterAsync(dto);
            return NoContent();
        }

        [HttpPost(nameof(Login))]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LoginResponseDTO response = await _userService.LoginAsync(dto);

            //  Ajouter le refresh token dans un cookie sécurisé
            Response.Cookies.Append("refreshToken", response.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, 
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(30),
                Path = "/api/auth/refresh"
            });


            return Ok(new
            {
                user = response.User,
                accessToken = response.AccessToken
            });
        }
        
        [HttpPost(nameof(Refresh))]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponseDTO>> Refresh()
        {
            string? refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrWhiteSpace(refreshToken))
                return Unauthorized(new { error = "Refresh token manquant" });

            RefreshTokenRequestDTO dto = new RefreshTokenRequestDTO { RefreshToken = refreshToken };
            LoginResponseDTO response = await _userService.RefreshAsync(dto);

            // On remplace le cookie avec le nouveau refresh token (rotation)
            Response.Cookies.Append("refreshToken", response.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(30),
                Path = "/api/auth/refresh"
            });

            return Ok(new
            {
                user = response.User,
                accessToken = response.AccessToken
            });
        }

        [HttpPost(nameof(Logout))]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            string? refreshToken = Request.Cookies["refreshToken"];

            if (!string.IsNullOrWhiteSpace(refreshToken))
            {
                await _refreshTokenService.RevokeAsync(refreshToken);
            }

            Response.Cookies.Delete("refreshToken");

            return NoContent();
        }
    }
}
